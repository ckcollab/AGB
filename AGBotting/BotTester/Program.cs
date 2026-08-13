using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using AGB;
using AGB.D2;
using AGB.D2.Net;
using AGB.D2.Net.Packets;

using AGB.D2.Modules;

using D2Data;

namespace BotTester
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Config
            BotTesterConfig config = new BotTesterConfig("config.xml");

            WriteLine("AGB.BotTester v0.1.0", ConsoleColor.Green);
            Console.WriteLine("-------------------------------------------------------------------------------");

            foreach (CdKeySet cdKeySet in config.CdKeys)
                BotManager.Instance.AddCdKeySet(cdKeySet);

            foreach (AGB.Net.Proxy proxy in config.Proxies)
                BotManager.Instance.AddProxy(proxy);

            BotManager.Instance.LoadGameLog();
            #endregion

            #region Connecting and logging in to AGB
            // Real deal
            string agbIp = "76.178.133.236";
            int agbPort = 12302;

            if (File.Exists("DEBUG"))
            {
                // Test server
                agbIp = "209.20.81.74";
                //agbIp = "127.0.0.1";
                agbPort = 12302;
            }

            // If we disconnect, reload all bots
            AgbSocket.Instance.Disconnected += AgbSocket_Disconnected;

            // Connect to agb
            Console.Write("Connecting to AGB (" + agbIp + ":" + agbPort + ")...");
            AgbSocket.Instance.Connect(agbIp, agbPort);
            WriteLine("done!" + Environment.NewLine, ConsoleColor.Green);

            // Print welcome message when received
            Console.WriteLine("Server welcome message:");
            AGBPacket welcomePacket = AgbSocket.Instance.Welcome(15000);

            WriteLine((welcomePacket as WelcomeResult).Message + Environment.NewLine, ConsoleColor.Green);

            Console.Write("Logging in...");
            LoginResult result = AgbSocket.Instance.Login(config.AgbUsername, config.AgbPassword, 5000);
            WriteLine("done!", ConsoleColor.Green);

            if (result == null)
            {
                Console.WriteLine("\t--> Didn't get a response!" + Environment.NewLine);
                Console.WriteLine("Press any key to exit...");
                Console.Read();
                return;
            }

            Console.WriteLine("\t--> Result: " + result.Result.ToString() + Environment.NewLine);

            if (result.Result != LoginResultValue.Success)
            {
                Console.WriteLine("Press any key to exit...");
                Console.Read();
                return;
            }
            #endregion

            #region TESTING!
            /* Ping

            DateTime now = DateTime.Now;
            AgbSocket.Instance.Ping(10000);
            TimeSpan length = DateTime.Now.Subtract(now);
            int done123 = 1;

                         */

            /* Load testing the server
            for (int i = 0; i < 50000; i++)
            {
                AGBSocket.Instance.SetNewGameInfo(config.Profiles[0].Character, 597800917, 0, D2Data.GameDifficulty.Hell, 10000);
                System.Threading.Thread.Sleep(250);
            }
             */

            /* Ping
            AgbSocket.Instance.Ping();
            AgbSocket.Instance.Message("ApacheChief", config.Profiles[0].Character, config.Profiles[0].Character, "fdfsdsfd");

            AGBPacket packet = AgbSocket.Instance.PacketHandler.WaitForPacket((int)PacketType.Message, 5000);

            Message msg = packet as Message;

            int done23123 = 1;
             */

            /* Testing botmanager
            CdKeySetProxyCombo combo = BotManager.Instance.GetCombo(Realm.USWest);

            int count0 = BotManager.Instance.GetGameCountInLastHour(Realm.USWest, combo);

            BotManager.Instance.EnterGame(Realm.USWest, combo);
            BotManager.Instance.EnterGame(Realm.USWest, combo);
            BotManager.Instance.EnterGame(Realm.USWest, combo);

            int count1 = BotManager.Instance.GetGameCountInLastHour(Realm.USWest, combo);

            System.Threading.Thread.Sleep(5000);

            BotManager.Instance.EnterGame(Realm.USWest, combo);
            BotManager.Instance.EnterGame(Realm.USWest, combo);

            int count = BotManager.Instance.GetGameCountInLastHour(Realm.USWest, combo);

            int don3423243 = 1;
             */




            /* Testing map drawing

            AgbSocket.Instance.SetNewGameInfo(config.Profiles[0].Character, 597800917, 0, D2Data.GameDifficulty.Hell, 10000);

            Map map = AgbSocket.Instance.GetMap(config.Profiles[0].Character, AreaLevel.DuranceOfHateLevel2, 10000).Map;

            map.DumpMap("test");

            int done31 = 1;
             */



            /* Testing loading maps statically
            DateTime start = DateTime.Now;

            List<Room> nihlathaksTempleRooms = new List<Room>();
            nihlathaksTempleRooms.Add(new Room() { Id1 = 1088, Id2 = 0 });
            Map nihlathaksTemple = new Map(AreaLevel.NihlathaksTemple, nihlathaksTempleRooms, 10000, 13180, 110, 135);
            //nihlathaksTemple.LoadCollisions();

            TimeSpan length = DateTime.Now.Subtract(start);
            Console.WriteLine(length.TotalMilliseconds);

            //nihlathaksTemple.DumpMap("fsdasdf");

            int afeweww = 32;
             */

            /* Testing arealinka

            List<WaypointDestination> availableWaypoints = new List<WaypointDestination>();
            availableWaypoints.Add(WaypointDestination.DuranceOfHateLevel2);
            //availableWaypoints.Add(WaypointDestination.HallsOfPain);

            Dictionary<QuestType, QuestStanding> quests = new Dictionary<QuestType, QuestStanding>();
            quests.Add(QuestType.PrisonOfIce, QuestStanding.Complete);
            quests.Add(QuestType.TheBlackenedTemple, QuestStanding.Complete);

            //List<AreaLink> links = AreaLinker.GetLinks(availableWaypoints, quests, AreaLevel.DuranceOfHateLevel3, AreaLevel.ThePandemoniumFortress);
            List<AreaLink> links = AreaLinker.GetLinks(availableWaypoints, quests, AreaLevel.ThePandemoniumFortress, AreaLevel.DuranceOfHateLevel3);

            //List<AreaLink> links = AreaLinker.GetLinks(availableWaypoints, quests, AreaLevel.KurastDocks, AreaLevel.DuranceOfHateLevel3);
            //links.Reverse();
            foreach (AreaLink link in links)
            {
                Console.Write(" - " + link.AreaLevel + ":: Portal Id = " + link.PortalId + "; Warp Id(s) = ");

                if(link.Exits != null)
                    foreach (int exit in link.Exits)
                        Console.Write(exit + " ");

                Console.WriteLine();
            }
            Console.ReadLine();
            */


            /* Testing OpenWaypoint
            //D2Packets.GameServer.OpenWaypoint packet = new D2Packets.GameServer.OpenWaypoint(ETUtils.ByteConverter.ParseHex(" 63 b9 2f b8 16 02 01 01 02 04 48 00 00 00 00 00 00 00 00 00 00"));

            WaypointsAvailiable waypoints = WaypointsAvailiable.None;
            List<WaypointDestination> availableWaypoints = new List<WaypointDestination>();

            foreach (WaypointsAvailiable waypoint in Enum.GetValues(typeof(WaypointsAvailiable)))
            {
                waypoints |= waypoint;
            }

            foreach (WaypointsAvailiable waypoint in Enum.GetValues(typeof(WaypointsAvailiable)))
            {
                if (waypoint != WaypointsAvailiable.None && waypoint != WaypointsAvailiable.HaveList && (waypoint & waypoints) == waypoint)
                {
                    WaypointDestination destination = (WaypointDestination)Enum.Parse(typeof(WaypointDestination), waypoint.ToString());

                    if (!availableWaypoints.Contains(destination))
                        availableWaypoints.Add(destination);
                }
            }

            int done1341 = 1;
            */

            /*
            TaskManager myManager = new TaskManager();
            myManager.AddTask(0, "asdf1", delegate() { Console.WriteLine("task 1, priority " + 0); });
            myManager.AddTask(0, "asdf2", delegate() { Console.WriteLine("task 2, priority " + 0); });
            myManager.AddTask(0, "asdf3", 
                delegate()
                {
                    Console.WriteLine("task 3, priority " + 0);

                    myManager.AddTask(1, "asdf7", 
                        delegate() 
                        { 
                            Console.WriteLine("task 7, priority " + 1);

                            myManager.AddTask(2, "asdf10", delegate() { Console.WriteLine("task 10, priority " + 2); });
                            myManager.AddTask(2, "asdf11", delegate() { Console.WriteLine("task 11, priority " + 2); });
                        });
                    myManager.AddTask(1, "asdf8", delegate() { Console.WriteLine("task 8, priority " + 1); });
                    myManager.AddTask(1, "asdf9", delegate() { Console.WriteLine("task 9, priority " + 1); });
                });
            myManager.AddTask(0, "asdf4", delegate() { Console.WriteLine("task 4, priority " + 0); });
            myManager.AddTask(0, "asdf5", delegate() { Console.WriteLine("task 5, priority " + 0); });
            myManager.AddTask(0, "asdf6", delegate() { Console.WriteLine("task 6, priority " + 0); });

            myManager.AddTask(1, "asdf4", delegate() { Console.WriteLine("task 12, priority " + 1); });
            myManager.AddTask(1, "asdf5", delegate() { Console.WriteLine("task 13, priority " + 1); });
            myManager.AddTask(1, "asdf6", delegate() { Console.WriteLine("task 14, priority " + 1); });
            System.Threading.Thread.Sleep(5000);

            int done4312 = 1;
            */

            /* Walk pathing

            AgbSocket.Instance.SetNewGameInfo(config.Profiles[0].Character, 1143447799, 0, D2Data.GameDifficulty.Hell, 10000);

            Map map = AgbSocket.Instance.GetMap(config.Profiles[0].Character, AreaLevel.RogueEncampment, 10000).Map;

            PresetUnit unit = map.FindPresetUnit(UnitType.GameObject, (int)GameObjectClass.RogueBonfire);

            int act = 1;

            int offsetX = 0;
            int offsetY = 0;

            switch (act)
            {
                case 1: offsetX = 5; offsetY = 5; break;
                case 2: offsetX = 5; offsetY = 5; break;
            }

            List<PathNode> path = map.GetWalkPath(5828, 5725, unit.X + offsetX, unit.Y + offsetY);

            map.DumpMap("test", path);

            int done31 = 1;
             */

            /* Pathing through area links fiiixored
            AgbSocket.Instance.SetNewGameInfo(config.Profiles[0].Character, 941581044, 0, D2Data.GameDifficulty.Hell, 10000);

            List<WaypointDestination> waypoints = new List<WaypointDestination>();
            waypoints.Add(WaypointDestination.RogueEncampment);
            //waypoints.Add(WaypointDestination.InnerCloister);

            Dictionary<QuestType, QuestStanding> quests = new Dictionary<QuestType, QuestStanding>();
            quests.Add(QuestType.PrisonOfIce, QuestStanding.Complete);

            AreaLevel start = AreaLevel.RogueEncampment;
            AreaLevel destination = AreaLevel.CatacombsLevel4;

            var links = AreaLinker.GetLinks(waypoints, quests, start, destination).ToArray();

            Console.WriteLine("Finding links from " + start + " to " + destination);

            for (int i = 0; i < links.Length; i++)
            {
                Console.WriteLine("\tMap #" + i + " = " + links[i].AreaLevel);
                Map map = AgbSocket.Instance.GetMap(config.Profiles[0].Character, links[i].AreaLevel, 10000).Map;

                // This area doesn't have an exit warp, we have to stitch it to the next area
                while (links[i].Exits == null && i + 1 < links.Length)
                {
                    i++;

                    Console.WriteLine("\t\tStitching with " + links[i].AreaLevel);

                    Map map2 = AgbSocket.Instance.GetMap(config.Profiles[0].Character, links[i].AreaLevel, 10000).Map;

                    map.StitchWith(map2);
                }

                PresetUnit startUnit = new PresetUnit();

                // try to start from a WP
                startUnit = map.FindWayPoint();

                // if no wp is here, find the lowest warp
                if (startUnit == null)
                {
                    startUnit = new PresetUnit();
                    startUnit.Id = ushort.MaxValue;

                    map.PresetUnits.ForEach(
                        delegate(PresetUnit unit) 
                        {
                            if (unit.Type == UnitType.Warp && unit.Id < startUnit.Id)
                                startUnit = unit;
                        });
                }

                List<PathNode> path = new List<PathNode>();

                if(links[i].Exits != null)
                    path = map.GetTeleportPath(new PresetUnit[] { startUnit }, links[i].AreaLevel, links[i].Exits);

                map.DumpMap(i.ToString(), path);
                System.Threading.Thread.Sleep(1000);
            }



            //AGB.Util.DumpCollision(AGB.D2.MapCache.Instance.Rooms[256, 0].Collisions, "asdf");
            int done = 1;
            */

            /* Pathing through area links
             
            AGBSocket.Instance.SetNewGameInfo(config.Profiles[0].Character, 941581044, 0, D2Data.GameDifficulty.Hell, 10000);

            List<WaypointDestination> waypoints = new List<WaypointDestination>();
            waypoints.Add(WaypointDestination.RogueEncampment);
            waypoints.Add(WaypointDestination.ColdPlains);
            //waypoints.Add(WaypointDestination.InnerCloister);

            AreaLevel start = AreaLevel.RogueEncampment;
            AreaLevel destination = AreaLevel.CatacombsLevel4;

            var links = AreaLinker.GetLinks(waypoints, start, destination).ToArray();

            Console.WriteLine("Finding links from " + start + " to " + destination);

            for (int i = 0; i < links.Length; i++)
            {
                Console.WriteLine("\tMap #" + i + " = " + links[i].AreaLevel);
                Map map = AGBSocket.Instance.GetMap(config.Profiles[0].Character, links[i].AreaLevel, 10000).Map;

                // This area doesn't have an exit warp, we have to stitch it to the next area
                while (links[i].Exits == null && i + 1 < links.Length)
                {
                    i++;

                    Console.WriteLine("\t\tStitching with " + links[i].AreaLevel);

                    Map map2 = AGBSocket.Instance.GetMap(config.Profiles[0].Character, links[i].AreaLevel, 10000).Map;

                    map.StitchWith(map2);
                }

                PresetUnit startUnit = new PresetUnit();

                // try to start from a WP
                startUnit = map.FindWayPoint();

                // if no wp is here, find the lowest warp
                if (startUnit == null)
                {
                    startUnit = new PresetUnit();
                    startUnit.Id = ushort.MaxValue;

                    map.PresetUnits.ForEach(
                        delegate(PresetUnit unit) 
                        {
                            if (unit.Type == UnitType.Warp && unit.Id < startUnit.Id)
                                startUnit = unit;
                        });
                }

                List<PathNode> path = new List<PathNode>();

                if(links[i].Exits != null)
                    path = map.GetTeleportPath(new PresetUnit[] { startUnit }, links[i].Exits);

                map.DumpMap(i.ToString(), path);
                System.Threading.Thread.Sleep(1000);
            }



            //AGB.Util.DumpCollision(AGB.D2.MapCache.Instance.Rooms[256, 0].Collisions, "asdf");
            int done = 1;
             */

            /* Finding area links
            List<WaypointDestination> waypoints = new List<WaypointDestination>();
            waypoints.Add(WaypointDestination.RogueEncampment);
            waypoints.Add(WaypointDestination.ColdPlains);
            //waypoints.Add(WaypointDestination.InnerCloister);

            AreaLevel start = AreaLevel.JailLevel1;
            AreaLevel destination = AreaLevel.CatacombsLevel4;

            var links = AreaLinker.GetLinks(waypoints, start, destination);

            Console.WriteLine("Finding links from " + start + " to " + destination);

            foreach (AreaLink link in links)
            {
                Console.Write("\t" + link.AreaLevel);

                if (link.Entrances != null)
                {
                    Console.Write(" through these warps: ");

                    foreach (int i in link.Entrances)
                        Console.Write(i);
                }
                
                Console.Write(Environment.NewLine);
            }

            int done = 1;
            */

            /* Finding closest Waypoint
             
            List<WaypointDestination> waypoints = new List<WaypointDestination>();
            waypoints.Add(WaypointDestination.RogueEncampment);
            waypoints.Add(WaypointDestination.ColdPlains);
            waypoints.Add(WaypointDestination.InnerCloister);

            var dest = AreaLinker.GetClosestWp(waypoints, AreaLevel.JailLevel1, AreaLevel.CatacombsLevel4);

            int done = 1;
             */

            /* Cross area pathing

            AgbSocket.Instance.SetNewGameInfo(config.Profiles[0].Character, 941581044, 0, D2Data.GameDifficulty.Hell, 10000);

            Map rogueEncampment = AgbSocket.Instance.GetMap(config.Profiles[0].Character, D2Data.AreaLevel.RogueEncampment, 10000).Map;
            Map bloodMoor = AgbSocket.Instance.GetMap(config.Profiles[0].Character, D2Data.AreaLevel.BloodMoor, 10000).Map;
            Map coldPlains = AgbSocket.Instance.GetMap(config.Profiles[0].Character, D2Data.AreaLevel.ColdPlains, 10000).Map;
            Map stoneyField = AgbSocket.Instance.GetMap(config.Profiles[0].Character, D2Data.AreaLevel.StonyField, 10000).Map;

            rogueEncampment.StitchWith(bloodMoor);
            rogueEncampment.StitchWith(coldPlains);
            rogueEncampment.StitchWith(stoneyField);

            DateTime start = DateTime.Now;

            PresetUnit unit = rogueEncampment.FindPresetUnit(D2Data.UnitType.GameObject, 17);

            List<PathNode> path = rogueEncampment.GetWalkPath(5153, 4233, unit.X, unit.Y, 50);
            TimeSpan length = DateTime.Now.Subtract(start);

            rogueEncampment.DumpMap("test", path);

            int done154 = 1;
             */
            #endregion

            foreach (Profile profile in config.Profiles)
            {
                Console.WriteLine("Loaded profile for " + profile.Character.Name + "@" + profile.Character.Realm + Environment.NewLine);

                Bot bot = new Bot(profile);

                BotManager.Instance.AddBot(bot);

                #region Task events
                //bot.Game.TaskManager.TaskAdded +=
                //    delegate(Task task)
                //    {
                //        Console.WriteLine("Added: " + task.Description);
                //    };
                bot.Game.TaskManager.TaskBeganExecuting +=
                    delegate(Task task)
                    {
                        WriteLine(profile.Character.Name + "@" + profile.Character.Realm + " > " + task.Description, ConsoleColor.White);
                    };
                //bot.Game.TaskManager.TaskExecuted +=
                //    delegate(Task task)
                //    {
                //        Console.WriteLine(profile.Character.Name + "@" + profile.Character.Realm + " > " + task.Description);
                //    };
                bot.Game.TaskManager.TaskException +=
                    delegate(Task task, TaskException e)
                    {
                        WriteLine(profile.Character.Name + "@" + profile.Character.Realm + " > " + e.Message, ConsoleColor.Red);
                    };
                #endregion

                #region Modules
                Console.WriteLine("Adding Modules...");

                bot.AddModule(new MagicFinder());
                bot.AddModule(new Killer());
                bot.AddModule(new Mover());
                bot.AddModule(new PickIt());
                bot.AddModule(new Chicken());
                bot.AddModule(new TownManager());
                bot.AddModule(new Driver());
                //bot.AddModule(new TestDriver());

                bot.AddModules(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "modules");

                foreach(Module module in bot.Modules)
                {
                    Console.WriteLine("\t" + module.Name + " " + module.Version +
                                      " by " + module.Author);

                    module.Warning +=
                        delegate(Module m, string message)
                        {
                            WriteLine(profile.Character.Name + "@" + profile.Character.Realm + " > " + m.Name + " warning: " + message, ConsoleColor.Yellow);
                        };
                    module.ExceptionThrown +=
                        delegate(ModuleException e)
                        {
                            WriteLine(profile.Character.Name + "@" + profile.Character.Realm + " > " + e.Module.Name + " exception: " + e.Message, ConsoleColor.Red);
                        };
                    module.StatusUpdated +=
                        delegate(Module m, string status)
                        {
                            WriteLine(profile.Character.Name + "@" + profile.Character.Realm + " > " + status, ConsoleColor.White);
                        };
                }

                Console.WriteLine(Environment.NewLine);

                if (!bot.LoadModules())
                {
                    WriteLine("Modules failed to load... stopping bot", ConsoleColor.Red);
                    bot.UnloadModules();
                }
                else
                {
                    bot.Start();
                }
            }
            #endregion
        }

        static void AgbSocket_Disconnected()
        {
            List<Bot> bots = BotManager.Instance.GetBots();

            // Before we reconnect, stop all the bots
            foreach (Bot bot in bots)
                bot.Game.TaskManager.Reset();

            WriteLine("Disconnected from AGB, reconnecting", ConsoleColor.Red);

            for (; ; )
            {
                string agbIp = "76.178.133.236";
                int agbPort = 12301;

                if (File.Exists("DEBUG"))
                {
                    // Test server
                    //agbIp = "192.168.153.128";
                    agbIp = "127.0.0.1";
                    agbPort = 12302;
                }

                // If successfully connected, break out
                if (AgbSocket.Instance.Connect(agbIp, agbPort))
                    break;

                WriteLine("Reconnect failed, trying again in 30 seconds", ConsoleColor.Red);

                // Sleep for 30 seconds and try again
                System.Threading.Thread.Sleep(30000);
            }

            // And restart
            foreach (Bot bot in bots)
                bot.LoadModules();
        }

        static void WriteLine(string text, ConsoleColor color)
        {
            Util.FileAppend("log.log", text + Environment.NewLine);

            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = oldColor;
        }
    }
}
