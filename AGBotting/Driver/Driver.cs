using System;
using System.Collections.Generic;
using System.IO;

using AGB;
using AGB.D2;

using D2Packets;

namespace AGB.D2.Modules
{
    public class Driver : Module
    {
        private DriverConfig Config;

        // Set the last game to 60 seconds ago
        private DateTime LastGame = DateTime.Now.Subtract(TimeSpan.FromMilliseconds(25000));

        /// <summary>
        /// Resets each time you call Load()
        /// </summary>
        public int FailedGameCount = 0;
        public int GameCount = 0;

        public Driver()
        {
            Name = "Driver";
            Author = "ApacheChief";
            Version = "0.1.0";
        }

        public override void Load()
        {
            if (!Directory.Exists(BaseDirectory))
                Directory.CreateDirectory(BaseDirectory);

            Config = new DriverConfig(BaseDirectory + "config.xml");

            if (Config.GameName == null || Config.GamePassword == null)
                ThrowModuleException(new ModuleException(this, "You need to add the Driver configuration stuff"));
        }

        public override void Start(Game game)
        {
            Connect();
        }

        public void Connect()
        {
            // Reset failed game counter, becuase we're going to change cd keys
            FailedGameCount = 0;

            Game.Disconnect();

            // Normally modules probably wouldn't do stuff here, but since this is
            // the driver, we're going to start here -- added as a task 
            // so other modules can start doing their thing
            Game.TaskManager.AddTask((int)TaskPriority.Base, "Connecting and logging in to Battle.Net",
                delegate()
                {
                    RaiseUpdate("Waiting for a new cdkey/proxy combo...");

                    CdKeySetProxyCombo combo = WaitForACombo(Game);

                    if (combo == null)
                        throw new TaskException("Out of CdKey/Proxy combos");

                    RaiseUpdate("Got the CdKeySet: " + combo.CdKeySet.Classic.Substring(10).PadLeft(15, '*') + " and " + combo.CdKeySet.Expansion.Substring(10).PadLeft(15, '*'));

                    if (combo.Proxy.Port != 0)
                        RaiseUpdate("Got the Proxy: " + combo.Proxy.IP + ":" + combo.Proxy.Port);

                    RaiseUpdate("Connecting to Battle.net...");

                    ConnectResult cRes = Game.Connect(combo);

                    if (!cRes.HasCompletedSuccessfully)
                    {
                        // This combo may not be working, let's try again in an hour
                        BotManager.Instance.ReleaseCombo(Game.Profile.Character.Realm, combo, TimeSpan.FromHours(1));

                        if (cRes.AuthResponse != null && cRes.AuthResponse.Result == D2Packets.BnetServer.BnetAuthResult.CDKeyInUse)
                        {
                            // we can try again immediately
                            Reconnect(0);
                        }
                        else
                        {
                            // Try again in half an hour
                            Reconnect(60 * 1000 * 30);
                        }


                        if (cRes != null && cRes.AuthResponse != null)
                            throw new TaskException("Failed connecting to Battle.Net: AuthResponse -> " + cRes.AuthResponse.Result);
                        else
                            throw new TaskException("Failed connecting to Battle.Net, no response. (Unable to Connect error?)");
                    }

                    D2Packets.BnetServer.BnetLogonResponse bRes = Game.Login();

                    if (bRes == null || bRes.Result != D2Packets.BnetServer.BnetLogonResult.Success)
                    {
                        if (bRes != null)
                            throw new TaskException("Failed logging in to Battle.Net: BnetLogonResponse -> " + bRes.Result);
                        else
                            throw new TaskException("Failed logging in to Battle.Net, no response.");
                    }

                    RealmConnectResult rRes = Game.RealmConnect();

                    if (rRes.HasFailed)
                    {
                        // Try again in half an hour
                        Reconnect(60 * 1000 * 30);

                        throw new TaskException("Failed reconnecting to the Realm, probably Ip Banned or something, we'll try again in half an hour" + Environment.NewLine + rRes);
                    }
                });
        }

        public override void LobbyEntered(Game game)
        {
            if(BotManager.Instance.GetGameCountInLastHour(game.Profile.Character.Realm, game.Profile.CdKeySetProxyCombo) > 0)
                System.Threading.Thread.Sleep(10000);

            game.TaskManager.AddTask((int)TaskPriority.Base, "Attempting to enter game",
                delegate()
                {
                    int gamesInLastHour = BotManager.Instance.GetGameCountInLastHour(game.Profile.Character.Realm, game.Profile.CdKeySetProxyCombo);

                    RaiseUpdate("Games in the last hour on this combo: " + gamesInLastHour);

                    if (gamesInLastHour >= 19)
                    {
                        RaiseUpdate("Ran out of runs on the current combo, getting a new one");

                        // Try to get a new combo
                        Reconnect(5000);

                        return;
                    }

                    if (FailedGameCount > 3)
                    {
                        RaiseUpdate("This CdKey set has failed more than 3 times to create games, let's find a better set");

                        // Try to get a new combo, wait 15 minutes
                        Reconnect(60 * 1000 * 15);

                        return;
                    }

                    GameCount++;

                    string gamename = Config.GameName;
                    string gamepassword = Config.GamePassword;

                    if (gamename == "-1")
                        gamename = AGB.Util.RandomString(7, 10);
                    else
                        gamename += GameCount;

                    if (gamepassword == "-1") 
                        gamepassword = AGB.Util.RandomString(7, 10);

                    RaiseUpdate("Creating " + game.Profile.Difficulty + " game #" + ((int)(game.GameCounter / 2) + 1) + " " + gamename + "/" + gamepassword);

                    EnterGameResult jGameRes = game.CreateGame(game.Profile.Difficulty, gamename, gamepassword);

                    // join game to debug
                    /*
                    Console.Write("Game name: ");
                    string name = Console.ReadLine();
                    Console.Write("Game pass: ");
                    string pass = Console.ReadLine();

                    StatusUpdate("Joining " + name + "/" + pass + " to debug");

                    EnterGameResult jGameRes = Game.JoinGame(name, pass);

                    if (!jGameRes.HasCompletedSuccessfully)
                        throw new TaskException("Failed to enter game.  " + Environment.NewLine + jGameRes);
                     */
                    LastGame = DateTime.Now;

                    if (jGameRes == null || !jGameRes.HasCompletedSuccessfully)
                    {
                        // Leave the realm
                        Game.Socket.Realm.Close();

                        game.TaskManager.AddTask((int)TaskPriority.Base, "Trying to do another game",
                            delegate()
                            {
                                // Call game exited so it reconnects to realm
                                GameExited(Game);
                            });

                        FailedGameCount++;

                        throw new TaskException("Failed to create game.  " + Environment.NewLine + jGameRes);
                    }

                    RaiseUpdate("Joined the game");
                });
        }

        public override void GameExited(Game game)
        {
            // Wait at least 60 seconds between games
            while (DateTime.Now.Subtract(LastGame).TotalMilliseconds < 45000)
                System.Threading.Thread.Sleep(1000);

            RealmConnectResult rRes = Game.RealmConnect(true);

            if (rRes.HasFailed)
            {
                // Try again in half an hour
                Reconnect(60 * 1000 * 30);

                throw new TaskException("Failed reconnecting to the Realm, probably Ip Banned or something, we'll try again in half an hour" + Environment.NewLine + rRes);
            }
        }

        private CdKeySetProxyCombo WaitForACombo(Game game)
        {
            CdKeySetProxyCombo combo = null;

            while (combo == null)
            {
                System.Threading.Thread.Sleep(100);

                combo = BotManager.Instance.GetCombo(game.Profile.Character.Realm);
            }

            return combo;
        }

        private void Reconnect(int sleep)
        {
            Game.TaskManager.AddTask((int)TaskPriority.Base, "Restarting driver",
                delegate()
                {
                    // This cd key set is tagged or something, release it for an hour
                    BotManager.Instance.ReleaseCombo(Game.Profile.Character.Realm, Game.Profile.CdKeySetProxyCombo, TimeSpan.FromHours(1));

                    RaiseUpdate("Waiting " + sleep + "ms");
                    System.Threading.Thread.Sleep(sleep);

                    Game.Socket.Close();

                    // gooooo!
                    Connect();
                });
        }
    }
}
