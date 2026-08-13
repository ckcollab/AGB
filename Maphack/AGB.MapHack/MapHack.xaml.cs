/*
    This file is part of AGB.MapHack
 
    AGB.MapHack - Reveals the maps in Diablo II, clientlessly
    Copyright (C) 2008 Eric Carmichael

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows.Threading;

using System.Diagnostics;

using System.Runtime.InteropServices;

using D2Data;

using AGB;
using AGB.D2;
using AGB.D2.Net;
using AGB.D2.Net.Packets;

using DiabloReader;

namespace AGB.MapHack
{
    /// <summary>
    /// Interaction logic for MapHack.xaml
    /// </summary>
    public partial class MapHack : Window
    {
        #region Dll Imports
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, long dwNewLong);

        [DllImport("user32.dll")]
        static extern long GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

        [DllImport("user32.dll")]
        static extern bool GetWindowInfo(IntPtr hwnd, ref WINDOWINFO pwi);

        [DllImport("user32.dll")]
        static extern bool GetCursorPos(ref System.Drawing.Point lpPoint);

        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);
        #endregion

        #region Fields
        private TaskManager TaskManager = new TaskManager();

        private Config Config;

        private List<PathHighlight> PathHighlights;

        private Process Diablo;
        private Reader Reader;
        private DiabloReader.Hero Hero;

        private bool IsConnectedToAgb = false;
        private bool IsMapCacheLoaded = false;
        private bool IsAttached = false;
        private bool IsTeleporting = false;

        private AreaLevel MapArea;
        private int MapX;
        private int MapY;
        private uint MapSeed;

        private object MapLock = new object();

        private MapForm MapForm;
        private AGB.MapHack.Drawing.Unit Player;
        private AGB.MapHack.Drawing.Map Map;

        private int LayerMap;
        private int LayerObjects;
        private int LayerUnits;
        private int LayerPlayer;

        private Dictionary<AreaLevel, Map> PreviousMaps = new Dictionary<AreaLevel, Map>();
        private List<AGB.MapHack.Drawing.Unit> MonsterDrawings = new List<AGB.MapHack.Drawing.Unit>();

        private Character Character = new Character("AGBMapHack-NotARealCharacter", Realm.USWest, CharacterClass.Any);
        private SetNewGameInfoResultValue LastResult;

        private Utilities.GlobalKeyboardHook GlobalKeyboardHook = new Utilities.GlobalKeyboardHook();
        private System.Windows.Forms.Keys HookedKey = System.Windows.Forms.Keys.None;

        private List<PathNode> LastPath;
        #endregion

        #region Constructor
        public MapHack()
        {
            InitializeComponent();

            TaskManager.AddTask((int)TaskPriority.Base, "Starting MapHack", 
                delegate()
                {
                    Write("Loading MapCache... ", Brushes.LightGreen);

                    TaskManager.AddTask((int)TaskPriority.Base, "Loading MapHack",
                        delegate()
                        {
                            MapCache.Instance.Init();
                            Write("done!", Brushes.LightGreen);

                            IsMapCacheLoaded = true;
                        });
                });
            

            //Output.Document.Blocks.Clear();

            System.Windows.Forms.Timer processWatchTimer = new System.Windows.Forms.Timer();
            processWatchTimer.Interval = 200;
            processWatchTimer.Tick += new EventHandler(ProcessWatchTimer_Tick);
            processWatchTimer.Start();

            System.Windows.Forms.Timer drawTimer = new System.Windows.Forms.Timer();
            drawTimer.Interval = 33;
            drawTimer.Tick += new EventHandler(DrawTimer_Tick);
            drawTimer.Start();

            System.Windows.Forms.Timer formMovetimer = new System.Windows.Forms.Timer();
            formMovetimer.Interval = 100;
            formMovetimer.Tick += new EventHandler(FormMoveTimer_Tick);
            formMovetimer.Start();

            foreach (System.Windows.Forms.Keys key in Enum.GetValues(typeof(System.Windows.Forms.Keys)))
                AutoTeleHotkey.Items.Add(key.ToString());

            // Set it to A
            AutoTeleHotkey.SelectedIndex = 61;

            GlobalKeyboardHook.KeyUp += GlobalKeyboardHook_KeyUp;

            try
            {
                Config = new Config(Environment.CurrentDirectory + System.IO.Path.DirectorySeparatorChar + "config.xml");
            }
            catch (InvalidOperationException e)
            {
                MessageBox.Show("Something is wrong with the config:" + Environment.NewLine + e.Message, "AGB.MapHack Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                Process.GetCurrentProcess().Kill();
            }

            Username.Text = Config.AgbUsername;
            Password.Password = Config.AgbPassword;
            PathHighlights = Config.PathHighlights;

            WindowSizeX.Text = Config.WindowSizeX.ToString();
            WindowSizeY.Text = Config.WindowSizeY.ToString();

            RefreshPause.Text = Config.RefreshPause.ToString();

            if (Config.Key != System.Windows.Forms.Keys.None)
            {
                AutoTeleHotkey.SelectedItem = Config.Key.ToString();
                AutoTeleEnabled.IsChecked = true;
            }

            if (PathHighlights == null)
                PathHighlights = new List<PathHighlight>();

            AgbSocket.Instance.Disconnected += AGBSocket_Disconnected;

            MessageBox.Show("AGB.Maphack\nBy ApacheChief\nThis maphack requires you to have an account at agbotting.net\nYou must have runs on your account in order for this to work.\n\nThis is an alpha release.");

            // Test example
            //WarpType.Act1CaveDown

            /*
            PathHighlights.Add(new PathHighlight("Mephisto", AreaLevel.DuranceOfHateLevel2, 
                new PresetUnit[]
                {
                    new PresetUnit(){Id=67, Type=UnitType.Warp}, 
                    new PresetUnit(){Id=67, Type=UnitType.Warp}
                }, 0x00FF00FF, true));
            */
        }
        #endregion

        #region Timer events
        private void ProcessWatchTimer_Tick(object sender, EventArgs e)
        {
            List<Process> processList = new List<Process>();

            foreach (Process proc in Process.GetProcesses())
            {
                if (proc.ProcessName.Contains("Diablo") || proc.ProcessName.Contains("diablo") || proc.ProcessName.Contains("d2loader") || proc.ProcessName == "Game" || proc.ProcessName.Contains("D2Loader"))
                    processList.Add(proc);
            }

            if (Processes.Items.Count != processList.Count)
            {
                Processes.ItemsSource = processList;
                Processes.DisplayMemberPath = "MainWindowTitle";
                Processes.SelectedValuePath = "Id";
            }

            if ((Processes.SelectedItem == null || Processes.SelectedIndex == -1) && Processes.Items.Count > 0)
                Processes.SelectedIndex = 0;

            //Processes = processList.ToArray();
        }

        private void DrawTimer_Tick(object sender, EventArgs e)
        {
            if (Reader != null)
            {
                if (Hero != null && Hero.Seed != 0 && IsConnectedToAgb)
                {
                    int playerX = Hero.X - MapX;
                    int playerY = Hero.Y - MapY;

                    Player.X = playerX;
                    Player.Y = playerY;

                    // We joined a new game
                    if (MapSeed != Hero.Seed)
                        SetSeed(Hero);

                    // We changed areas
                    if (MapArea != Hero.AreaLevel)
                        SetMap(Hero.AreaLevel);

                    if (Map != null)
                    {
                        Map.PlayerX = playerX;
                        Map.PlayerY = playerY;

                        MapForm.Panel.OffsetX = -(int)((playerX - (MapForm.Panel.Width / 2)) * MapForm.Panel.Scale);
                        MapForm.Panel.OffsetY = -(int)((playerY - (MapForm.Panel.Height / 2)) * MapForm.Panel.Scale);
                    }


                    /*
                    List<Monster> monsters = Monster.GetMonstersInArea(Reader);

                    if (monsters.Count > 0)
                    {
                        foreach (Monster monster in monsters)
                        {
                            var drawing = MonsterDrawings.Find((AGB.MapHack.Drawing.Unit u) => (u.ID == (uint)monster.Pointer));

                            int monsterX = monster.X - MapX;
                            int monsterY = monster.Y - MapY;

                            if (drawing == null)
                            {
                                var monsterDrawing = new AGB.MapHack.Drawing.Unit(System.Drawing.Color.Red, monsterX, monsterY);
                                monsterDrawing.ID = (uint)monster.Pointer;
                                MapForm.Panel.AddDrawing(LayerUnits, monsterDrawing);

                                MonsterDrawings.Add(monsterDrawing);
                            }
                            else
                            {
                                drawing.X = monsterX;
                                drawing.Y = monsterY;
                            }
                        }
                    }*/

                    //foreach (KeyValuePair<IntPtr, Monster> pair in Monsters)
                    //{
                    //    if(!MonsterDrawings.ContainsKey(pair.Key))
                   //         MonsterDrawings.Add(
                   // }
                }
            }
        }

        /// <summary>
        /// Also tracks form settings like window size and stuff
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMoveTimer_Tick(object sender, EventArgs e)
        {
            if (Diablo != null)
            {
                WINDOWINFO info = new WINDOWINFO();
                info.cbSize = (uint)Marshal.SizeOf(info);
                GetWindowInfo(Diablo.MainWindowHandle, ref info);

                if (info.rcWindow.Left != 0 && info.rcWindow.Bottom != 0)
                    MapForm.Location = new System.Drawing.Point(info.rcWindow.Left - MapForm.Width + (int)info.cxWindowBorders, info.rcWindow.Bottom - MapForm.Height - (int)info.cyWindowBorders);

                // Settings
                int windowSizeX;
                Int32.TryParse(WindowSizeX.Text, out windowSizeX);

                int windowSizeY;
                Int32.TryParse(WindowSizeY.Text, out windowSizeY);

                if (windowSizeX != 0 && windowSizeY != 0 && windowSizeX > 100 && windowSizeX <= 800 && windowSizeY > 100 && windowSizeY <= 600)
                {
                    if (MapForm.Width != windowSizeX || MapForm.Height != windowSizeY)
                    {
                        MapForm.SetSize(windowSizeX, windowSizeY);
                    }
                }

                int refreshPause;
                Int32.TryParse(RefreshPause.Text, out refreshPause);

                if (refreshPause > 25)
                {
                    MapForm.Panel.RefreshPause = refreshPause;
                }
            }

            if (AutoTeleEnabled.IsChecked == false)
                AutoTeleHotkey.Visibility = Visibility.Hidden;
            else
                AutoTeleHotkey.Visibility = Visibility.Visible;

            HookedKey = (System.Windows.Forms.Keys)Enum.Parse(typeof(System.Windows.Forms.Keys), AutoTeleHotkey.Text);

            if (GlobalKeyboardHook.HookedKeys.Count == 0)
                GlobalKeyboardHook.HookedKeys.Add(HookedKey);

            if (HookedKey != GlobalKeyboardHook.HookedKeys[0])
                GlobalKeyboardHook.HookedKeys[0] = HookedKey;
        }
        #endregion

        #region Form events
        private void Attach_Click(object sender, RoutedEventArgs e)
        {
            if (!IsMapCacheLoaded)
            {
                MessageBox.Show("Error: MapCache hasn't finished loading yet, please wait a few moments", "AGB.MapHack Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Process process = Processes.SelectedItem as Process;

            if (process == null)
            {
                MessageBox.Show("Error: You must start Diablo II, and select it from the drop-down menu", "AGB.MapHack Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // This is so ugly, but it doesn't freeze the UI
            TaskManager.AddTask((int)TaskPriority.Base, "Connecting to AGB",
                delegate()
                {
                    if (!IsConnectedToAgb)
                    {
                        WriteLine("Connecting to AGB Server... ", Brushes.LightGreen);

                        bool connectionResult = AgbSocket.Instance.Connect("209.20.81.74", 12302);
                        //bool connectionResult = AgbSocket.Instance.Connect("76.178.133.236", 12301);
                        //bool connectionResult = AgbSocket.Instance.Connect("127.0.0.1", 12302);

                        if (!connectionResult)
                        {
                            Write("couldn't connect to server!", Brushes.Pink);
                            return;
                        }

                        Write("done!", Brushes.LightGreen);

                        AGBPacket welcomePacket = AgbSocket.Instance.Welcome(15000);

                        if (welcomePacket == null)
                        {
                            WriteLine("Never received welcome message!", Brushes.Pink);
                            return;
                        }

                        WriteLine((welcomePacket as WelcomeResult).Message + Environment.NewLine, Brushes.LightCoral);

                        WriteLine("Logging in... ", Brushes.LightGreen);

                        LoginResult result = AgbSocket.Instance.Login(GetAgbUsername(), GetAgbPassword(), 10000);

                        if (result == null)
                        {
                            Write("contacting the server timed out.", Brushes.Pink);
                            return;
                        }

                        if (result.Result == LoginResultValue.Success)
                        {
                            Write("success!", Brushes.LightGreen);
                            IsConnectedToAgb = true;
                        }
                        else
                        {
                            Write(result.Result.ToString().ToLower() + ", unable to login! (Make sure you're not using your FORUMS account)", Brushes.Pink);
                            return;
                        }
                    }

                    WriteLine("Attaching to process #" + process.Id);

                    Diablo = process;

                    Reader = new Reader(process);

                    Hero = new DiabloReader.Hero(Reader);

                    if (Hero == null || Hero.Name == "")
                    {
                        WriteLine("Unable to read memory for Hero, you're not logged into BNet, are you?!", Brushes.Pink);
                        return;
                    }

                    WriteLine("Found " + Hero.Name + "@" + Hero.Realm.ToString());

                    IsAttached = true;

                    //SetSeed(Hero);
                    //SetMap(Hero.AreaLevel);
                });
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Config.AgbUsername = Username.Text;
            Config.AgbPassword = Password.Password;

            Config.PathHighlights = PathHighlights;

            // Settings
            int windowSizeX;
            Int32.TryParse(WindowSizeX.Text, out windowSizeX);

            int windowSizeY;
            Int32.TryParse(WindowSizeY.Text, out windowSizeY);

            if (windowSizeX > 100 && windowSizeX <= 800 && windowSizeY > 100 && windowSizeY <= 600)
            {
                if (MapForm.Width != windowSizeX || MapForm.Height != windowSizeY)
                {
                    MapForm.SetSize(windowSizeX, windowSizeY);
                }
            }
            else
            {
                MessageBox.Show("WindowSize x and y must be between 100 and 300", "AGB.MapHack Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int refreshPause;
            Int32.TryParse(RefreshPause.Text, out refreshPause);

            if (refreshPause > 25)
            {
                MapForm.Panel.RefreshPause = refreshPause;
            }
            else
            {
                MessageBox.Show("Refresh pause must be greater than 25", "AGB.MapHack Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Config.WindowSizeX = windowSizeX;
            Config.WindowSizeY = windowSizeY;

            Config.RefreshPause = refreshPause;

            if (AutoTeleEnabled.IsChecked == true)
                Config.Key = (System.Windows.Forms.Keys)Enum.Parse(typeof(System.Windows.Forms.Keys), AutoTeleHotkey.SelectedItem.ToString());
            else
                Config.Key = System.Windows.Forms.Keys.None;

            Config.Save(Environment.CurrentDirectory + System.IO.Path.DirectorySeparatorChar + "config.xml");

            WriteLine("Config saved!", Brushes.LightGreen);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //IntPtr handle = Process.GetCurrentProcess().MainWindowHandle;
            //long initialStyle = GetWindowLong(handle, -20);

            //SetWindowLong(handle, -20, initialStyle | 0x80000 | 0x20);
            //SetLayeredWindowAttributes(handle, 0, (byte)(255 * .7), 0x2);
            MapForm = new MapForm();
            MapForm.Show();

            MapForm.Location = new System.Drawing.Point(5000, 5000);

            MapForm.SetSize(150, 150);

            long initialStyle = GetWindowLong(MapForm.Handle, -20);

            SetWindowLong(MapForm.Handle, -20, initialStyle | (long)WindowExStyles.LAYERED);
            //SetWindowLong(form.Handle, -20, initialStyle | (long)WindowExStyles.LAYERED | (long)WindowExStyles.TRANSPARENT);
            //SetLayeredWindowAttributes(form.Handle, 0, (byte)(255 * .7), 0x2);

            LayerMap = MapForm.Panel.AddLayer();
            LayerObjects = MapForm.Panel.AddLayer();
            LayerUnits = MapForm.Panel.AddLayer();
            LayerPlayer = MapForm.Panel.AddLayer();

            Player = new AGB.MapHack.Drawing.Unit(System.Drawing.Color.Green, 50, 50);
            //Player.FillRectangle = true;

            MapForm.Panel.AddDrawing(LayerPlayer, Player);

            MapForm.Panel.Background = System.Drawing.Color.Black;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MapForm.Close();

            AgbSocket.Instance.Quit();

            Process.GetCurrentProcess().Kill();
        }

        private void GlobalKeyboardHook_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (AutoTeleEnabled.IsChecked == true)
            {
                if (LastPath != null && IsTeleporting == false)
                {
                    IsTeleporting = true;

                    TaskManager.AddTask((int)TaskPriority.Base, "Teleporting, Dave!",
                        delegate()
                        {
                            Sender.SendPacket(Reader, new D2Packets.GameClient.SelectSkill(SkillType.Teleport, SkillHand.Right).Data);

                            System.Threading.Thread.Sleep(250);

                            foreach (PathNode node in LastPath)
                            {
                                int oldHeroX = Hero.X;
                                int oldHeroY = Hero.Y;

                                Sender.SendPacket(Reader, new D2Packets.GameClient.CastRightSkill(node.X, node.Y).Data);

                                //WriteLine("Waiting until I tele to: " + node.X + ", " + node.Y);

                                // Just wait until we've moved
                                while (Hero.X == oldHeroX && Hero.Y == oldHeroY)
                                {
                                    System.Threading.Thread.Sleep(10);
                                }
                            }

                            IsTeleporting = false;
                        });
                }
            }
        }
        #endregion

        #region AGBSocket events
        void AGBSocket_Disconnected()
        {
            WriteLine("Connection to AGBotting.net lost!", Brushes.Pink);

            IsConnectedToAgb = false;
            MapSeed = 0;
            MapArea = AreaLevel.None;
        }
        #endregion

        #region Updating map
        private void SetSeed(DiabloReader.Hero hero)
        {
            if (!IsConnectedToAgb)
            {
                WriteLine("Cannot set new game info, you're not connected to AGB any more!", Brushes.Pink);
                return;
            }

            if (!IsAttached)
                return;

            if (LastResult != SetNewGameInfoResultValue.Success)
            {
                IsAttached = false;
                MessageBox.Show("Error: Unable to set new game info -- do you have any runs left?", "AGB.MapHack Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            lock (MapLock)
            {
                PreviousMaps.Clear();

                // If map seed is already set
                if (MapSeed == hero.Seed)
                    return;

                // Or we're not in game yet
                if (Hero.X == 0 || Hero.Y == 0)
                    return;

                Character.Name = hero.Name;
                Character.Realm = hero.Realm;

                SetNewGameInfoResult result = AgbSocket.Instance.SetNewGameInfo(Character, (int)hero.Seed, 0, hero.Difficulty, 10000);

                if(result == null)
                {
                    Write("Contacting the server timed out.", Brushes.Pink);
                    return;
                }

                LastResult = result.Result;

                if (LastResult != SetNewGameInfoResultValue.Success)
                {
                    WriteLine("SetNewGameInfo failed: " + LastResult, Brushes.Pink);
                    return;
                }

                MapSeed = hero.Seed;

                // Joined a new game, in case we were in town or something let's
                // reset the area
                MapArea = AreaLevel.None;

                WriteLine("New game, seed = " + hero.Seed + "; difficulty = " + hero.Difficulty);
            }
        }

        private void SetMap(AreaLevel level)
        {
            if (!IsConnectedToAgb)
            {
                WriteLine("Cannot get a new map, you're not connected to AGB any more!", Brushes.Pink);
                return;
            }

            lock (MapLock)
            {
                // If we're going between acts or have already loaded the level
                if (level == AreaLevel.None || MapArea == level)
                    return;

                if (Hero.X == 0 || Hero.Y == 0 || Hero.AreaLevel != level)
                    return;

                if (LastResult != SetNewGameInfoResultValue.Success)
                    return;


                DateTime start = DateTime.Now;

                AGB.D2.Map map;

                if (PreviousMaps.ContainsKey(level))
                {
                    map = PreviousMaps[level];
                }
                else
                {
                    GetMapResult mapResult = AgbSocket.Instance.GetMap(Character, level, 10000);

                    if (mapResult == null)
                    {
                        Write("Contacting the server timed out.", Brushes.Pink);
                        return;
                    }

                    if (mapResult.Result != GetMapResultValue.Success)
                    {
                        WriteLine("Unabled to get map: " + mapResult.Result, Brushes.Pink);
                        return;
                    }

                    if (mapResult.Map == null)
                    {
                        WriteLine("Loading map failed.", Brushes.Pink);
                        return;
                    }

                    map = mapResult.Map;

                    PreviousMaps.Add(level, map);

                    //map.ThickenWalls();
                }

                MapArea = map.AreaLevel;

                MapX = map.X;
                MapY = map.Y;

                // Clear map
                MapForm.Panel.ClearLayer(LayerMap);

                // Clear monsters
                MonsterDrawings.Clear();
                MapForm.Panel.ClearLayer(LayerUnits);

                Map = new AGB.MapHack.Drawing.Map(System.Drawing.Color.White, map.Collisions);

                MapForm.Panel.AddDrawing(LayerMap, Map);
                //Map.Collisions = map.Collisions;

                // New map loaded, we don't want to save an old path and accidentally
                // activate it in town
                LastPath = null;

                DateTime isInNextArea = DateTime.Now;

                while (DateTime.Now.Subtract(isInNextArea).TotalMilliseconds < 10000) 
                {
                    if (map.IsInBounds(Hero.X, Hero.Y))
                        break;

                    System.Threading.Thread.Sleep(10);
                }

                if (!map.IsInBounds(Hero.X, Hero.Y))
                {
                    WriteLine("Hero was NEVER in bounds of the map...?", Brushes.Pink);
                }

                foreach (PathHighlight pathHighlight in PathHighlights.FindAll((PathHighlight path) => (path.AreaLevel == map.AreaLevel)))
                {
                    List<PathNode> path = new List<PathNode>();

                    if (pathHighlight.UseTeleport)
                        path = map.GetTeleportPath(Hero.X, Hero.Y, pathHighlight.Exits);
                    else
                        path = map.GetWalkPath(Hero.X, Hero.Y, pathHighlight.Exits);

                    if (path == null || path.Count == 0)
                    {
                        WriteLine("Unable to find path: " + pathHighlight.Name, Brushes.Pink);

                        PresetUnit presetUnit = map.FindPresetUnit(pathHighlight.Exits);

                        if (presetUnit != null)
                        {
                            WriteLine("But, I found the PresetUnit!", Brushes.LightGreen);

                            MapForm.Panel.AddDrawing(LayerUnits, new AGB.MapHack.Drawing.Line(System.Drawing.Color.Orange, Hero.X - MapX, Hero.Y - MapY, presetUnit.X - MapX, presetUnit.Y - MapY));
                        }
                        break;
                    }

                    LastPath = path;

                    PathNode trailingNode = new PathNode(Hero.X, Hero.Y);

                    byte r = (byte)(pathHighlight.Color >> 16);
                    byte g = (byte)(pathHighlight.Color >> 8);
                    byte b = (byte)(pathHighlight.Color);

                    Color brushColor = Color.FromRgb(r, g, b);

                    WriteLine("Path: " + pathHighlight.Name, new SolidColorBrush(brushColor));

                    System.Drawing.Color color = System.Drawing.Color.FromArgb(r, g, b);

                    MapForm.Panel.AddDrawing(LayerMap, new AGB.MapHack.Drawing.Path(path, color, MapX, MapY));
                }

                PresetUnit highestWarp = new PresetUnit();
                if (map.PresetUnits != null)
                {
                    foreach (PresetUnit unit in map.PresetUnits)
                    {
                        if (unit.Type == UnitType.Warp)
                        {
                            if (unit.Id > highestWarp.Id)
                                highestWarp = unit;

                            MapForm.Panel.AddDrawing(LayerUnits, new AGB.MapHack.Drawing.Square(System.Drawing.Color.Red, unit.X - MapX, unit.Y - MapY, 10));
                        }

                        if (unit.Type == UnitType.GameObject)
                            if ((unit.Id == 119) || (unit.Id == 157) || (unit.Id == 156) || (unit.Id == 237) || (unit.Id == 398) || (unit.Id == 429) ||
                                (unit.Id == 402) || (unit.Id == 323) || (unit.Id == 288) || (unit.Id == 324) || (unit.Id == 238) || (unit.Id == 496) ||
                                (unit.Id == 511) || (unit.Id == 494))
                                MapForm.Panel.AddDrawing(LayerUnits, new AGB.MapHack.Drawing.Square(System.Drawing.Color.Blue, unit.X - MapX, unit.Y - MapY, 10));
                    }
                }

                // draw line to highest warp, if it exists, EXCEPT in canyon of magi!
                if (level != AreaLevel.CanyonOfTheMagi)
                    if (highestWarp.Id != 0)
                        MapForm.Panel.AddDrawing(LayerUnits, new AGB.MapHack.Drawing.Line(System.Drawing.Color.Purple, Hero.X - MapX, Hero.Y - MapY, highestWarp.X - MapX, highestWarp.Y - MapY));

                WriteLine(level + " loaded in " + DateTime.Now.Subtract(start).TotalMilliseconds + "ms");
            }
        }
        #endregion

        #region Form multi-threading stuff
        public string GetAgbUsername()
        {
            string username = "";
            this.Dispatcher.Invoke(DispatcherPriority.Normal,
                (System.Windows.Forms.MethodInvoker)(() =>
                {
                    username = Username.Text;
                }));
            return username;
        }

        public string GetAgbPassword()
        {
            string password = "";
            this.Dispatcher.Invoke(DispatcherPriority.Normal,
                (System.Windows.Forms.MethodInvoker)(() =>
                {
                    password = Password.Password;
                }));
            return password;
        }

        public void WriteLine(string text)
        {
            WriteLine(text, Brushes.White);
        }
        public void WriteLine(string text, Brush color)
        {
            this.Dispatcher.Invoke(DispatcherPriority.Normal,
                (System.Windows.Forms.MethodInvoker)(() =>
                {
                    Paragraph p = new Paragraph();
                    p.Margin = new Thickness(0);
                    p.Inlines.Add(text);
                    p.SetValue(TextElement.ForegroundProperty, color);

                    Output.Document.Blocks.Add(p);
                    Output.ScrollToEnd();
                }));
        }

        public void Write(string text)
        {
            Write(text, Brushes.White);
        }
        public void Write(string text, Brush color)
        {
            this.Dispatcher.Invoke(DispatcherPriority.Normal,
                (System.Windows.Forms.MethodInvoker)(() =>
                {
                    Paragraph p = Output.Document.Blocks.LastBlock as Paragraph;
                    p.Margin = new Thickness(0);
                    p.Inlines.Add(text);

                    Inline i = p.Inlines.LastInline as Inline;
                    i.SetValue(TextElement.ForegroundProperty, color);

                    Output.ScrollToEnd();
                }));
        }
        #endregion
    }
}
