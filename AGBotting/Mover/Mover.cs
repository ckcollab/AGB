using System;
using System.Collections.Generic;
using System.Threading;

using AGB.D2;

using D2Data;

namespace AGB.D2.Modules
{
    public class Mover : Module
    {
        #region Constructor
        public Mover()
        {
            Name = "Mover";
            Author = "ShadowDancer and ApacheChief";
            Version = "0.1.0";
        }
        #endregion

        #region Load override
        public override void Load()
        {

        }
        #endregion

        #region GoToTown
        /// <summary>
        /// Goes to town, with High priority
        /// </summary>
        public void GoToTown()
        {
            GoToTown((int)TaskPriority.High);
        }
        public void GoToTown(int priority)
        {
            // High priority, could be chickening?
            Game.TaskManager.AddTask(priority, "Going to town",
                delegate()
                {
                    if (Game.Hero.IsInTown)
                        return;

                    Item item = Game.Hero.Items.Find((Item i) => (i.Action.BaseItem.Class == D2Data.ItemClass.ScrollOfTownPortal) && (i.Action.Container == D2Data.ItemLocation.Inventory));
                    if (item == null)
                        item = Game.Hero.Items.Find((Item i) => (i.Action.BaseItem.Class == D2Data.ItemClass.TomeOfTownPortal) && (i.Action.Container == D2Data.ItemLocation.Inventory));

                    if (item == null)
                        ThrowModuleException(new ModuleException(this, "Couldn't find a Tome of Town Portal or a Town Portal, couldn't go to town!"));

                    item.Interact();

                    Object townPortal = townPortal = Game.Objects.Find(D2Data.GameObjectClass.TownPortal, 5000);

                    if (townPortal == null)
                        ThrowModuleException(new ModuleException(this, "Mover::GoTown Portal didnt appear."));

                    if (!townPortal.PortalInteractWait(5000))
                        ThrowModuleException(new ModuleException(this, "Mover::GoTown Didn't make it through the portal"));
                });
        }
        #endregion

        #region GoToAct
        /// <summary>
        /// Goes to the specified act town
        /// </summary>
        /// <param name="act">Starts at 1, ends at 5 -- not zero based!</param>
        public void GoToAct(ActLevel act)
        {
            if ((int)act < 1 || (int)act > 5)
                throw new ArgumentException("Act number invalid.  Should be greater than zero and less than 5, but is: " + act);

            GoToAct((int)TaskPriority.Base, act);
        }
        /// <summary>
        /// Goes to the specified act town
        /// </summary>
        /// <param name="priority">The base priority to use for adding tasks</param>
        /// <param name="act">Starts at 1, ends at 5 -- not zero based!</param>
        public void GoToAct(int priority, ActLevel act)
        {
            if ((int)act < 1 || (int)act > 5)
                throw new ArgumentException("Act number invalid.  Should be greater than zero and less than 5, but is: " + act);

            Game.TaskManager.AddTask(priority, "Going to act " + act,
                delegate()
                {
                    WaypointDestination town = WaypointDestination.RogueEncampment;

                    switch (act)
                    {
                        case ActLevel.Act1: town = WaypointDestination.RogueEncampment; break;
                        case ActLevel.Act2: town = WaypointDestination.LutGholein; break;
                        case ActLevel.Act3: town = WaypointDestination.KurastDocks; break;
                        case ActLevel.Act4: town = WaypointDestination.ThePandemoniumFortress; break;
                        case ActLevel.Act5: town = WaypointDestination.Harrogath; break;
                    }

                    if (Game.Hero.AreaLevel == (AreaLevel)town)
                        return;

                    GoToWaypointArea(priority + 1, town);

                    Game.TaskManager.AddTask(priority + 1, "Checking if we're in act " + act,
                        delegate()
                        {
                            if (Game.Hero.Act != act)
                                ThrowModuleException(new ModuleException(this, "We didn't end up in the act we tried to go to, try again next time??"));
                        });
                });
        }
        #endregion

        #region Waypoint stuff
        public void GoToWaypointArea(WaypointDestination destination)
        {
            GoToWaypointArea((int)TaskPriority.Base, destination);
        }
        public void GoToWaypointArea(int priority, WaypointDestination destination)
        {
            // This has to be a task because it paths based on the Hero AreaLevel
            // and we need to wait to get the correct hero arealevel
            Game.TaskManager.AddTask(priority, "Going to WaypointArea - " + destination,
                delegate()
                {
                    if (!Game.Hero.IsInTown && !Game.Hero.AvailableWaypoints.Contains((WaypointDestination)Game.Hero.AreaLevel))
                        GoToTown();

                    GoToWaypoint(priority + 1);

                    // AboveNormal to correspond with the WalkTo tasks -- to go after them
                    Game.TaskManager.AddTask(priority + 1, "Taking waypoint",
                        delegate()
                        {
                            Map map = Game.MapManager.GetMap(Game.Hero.AreaLevel);

                            if (map == null)
                                ThrowModuleException(new ModuleException(this, "Mover::GoToWaypointArea - " + Game.Hero.AreaLevel + " Map is null"));

                            PresetUnit presetunit = map.FindWayPoint();
                            if (presetunit == null)
                                ThrowModuleException(new ModuleException(this, "Mover::GoToWaypointArea - PresetUnit is null"));

                            Object wayPoint = Game.Objects.Find((D2Data.GameObjectClass)presetunit.Id, 5000);

                            if (wayPoint == null)
                                ThrowModuleException(new ModuleException(this, "Mover::GoToWaypointArea - Couldn't find the Waypoint"));

                            wayPoint.WaypointInteractWait(destination, 10000);
                        });
                });
        }

        public void GoToWaypoint()
        {
            GoToWaypoint((int)TaskPriority.Base);
        }
        public void GoToWaypoint(int priority)
        {
            // Based on the CURRENT area level, better add it as a task!
            Game.TaskManager.AddTask(priority, "Going to Waypoint in current Hero AreaLevel",
                delegate()
                {
                    Map map = Game.MapManager.GetMap(Game.Hero.AreaLevel);

                    if (map == null)
                        ThrowModuleException(new ModuleException(this, "Mover::GoToWaypointArea - " + Game.Hero.AreaLevel + " Map is null"));

                    PresetUnit presetunit = map.FindWayPoint();
                    if (presetunit == null)
                        ThrowModuleException(new ModuleException(this, "Mover::GoToWaypointArea - Couldn't find the waypoint preset unit"));

                    MoveTo(priority + 1, presetunit.X, presetunit.Y);
                });
        }
        #endregion

        #region GoToArea
        public void GoToArea(D2Data.AreaLevel dest)
        {
            GoToArea((int)TaskPriority.Base, dest);
        }

        public void GoToArea(int priority, D2Data.AreaLevel dest)
        {
            Game.TaskManager.AddTask(new Task(priority, "GoToArea " + dest,
                delegate()
                {
                    if (Game.Hero.AreaLevel == dest)
                        return; //we have done

                    List<AreaLink> links = AreaLinker.GetLinks(Game.Hero.AvailableWaypoints, Game.Hero.Quests, Game.Hero.AreaLevel, dest);

                    if (links == null || links.Count == 0)
                        ThrowModuleException(new ModuleException(this, "Mover::GoToArea - Couldn't find a link path to the destination.  Start = " + Game.Hero.AreaLevel + "; Destination = " + dest));

                    // First link is a WP, so let's take it!
                    if (Game.Hero.AvailableWaypoints.Contains((WaypointDestination)links[0].AreaLevel))
                        GoToWaypointArea(priority + 1, (WaypointDestination)links[0].AreaLevel);

                    for (int i = 0; i < links.Count; i++)
                        if(links[i].AreaLevel != dest)
                            TakeLinkPath(priority + 1, links[i]);
                }));
        }
        #endregion

        /*
        public void WalkTo(UnitType type, int id)
        {
            WalkTo((int)TaskPriority.Base, type, id);
        }
        public void WalkTo(int priority, UnitType type, int id)
        {
            // Based on the CURRENT area level, better add it as a task!
            Game.TaskManager.AddTask(priority, "Walk to " + type + " with Id = " + id,
                delegate()
                {
                    // just a wrapper, no need to increment priority
                    WalkTo(priority, type, id, Game.Hero.AreaLevel);
                });
        }
        public void WalkTo(UnitType type, int id, AreaLevel areaLevel)
        {
            WalkTo((int)TaskPriority.Base, type, id, areaLevel);
        }
        public void WalkTo(int priority, UnitType type, int id, AreaLevel areaLevel)
        {
            // Don't need to add this function as a task becuase it doesn't
            // matter when it executes
            Map map = Game.MapManager.GetMap(areaLevel);

            if (map == null)
                ThrowModuleException(new ModuleException(this, "Mover::WalkTo - Map is null"));

            PresetUnit unit = map.FindPresetUnit(type, id);
            if (unit == null)
                ThrowModuleException(new ModuleException(this, "Mover::WalkTo - PresetUnit is null"));

            WalkTo(priority, unit.X, unit.Y, areaLevel);
        }
        public void WalkTo(int x, int y)
        {
            WalkTo((int)TaskPriority.Base, x, y);
        }
        public void WalkTo(int priority, int x, int y)
        {
            // Based on the CURRENT area level, better add it as a task!
            Game.TaskManager.AddTask(priority, "Starting walk to in current Hero AreaLevel",
                delegate()
                {
                    WalkTo(priority + 1, x, y, Game.Hero.AreaLevel);
                });
        }
        public void WalkTo(int x, int y, AreaLevel areaLevel)
        {
            WalkTo((int)TaskPriority.Base, x, y, areaLevel);
        }
        public void WalkTo(int priority, int x, int y, AreaLevel areaLevel)
        {
            // This has to be a task because it paths based on the Hero x/y 
            // and we need to wait to get the correct hero.x/y
            Game.TaskManager.AddTask(priority, "Walk to " + x + ", " + y,
                delegate()
                {
                    Map map = Game.MapManager.GetMap(areaLevel);

                    if (map == null)
                        ThrowModuleException(new ModuleException(this, "Mover::WalkTo Map = null"));

                    Console.WriteLine(" - - - - - - WalkTo(" + x + ", " + y + ") from (" + Game.Hero.X + ", " + Game.Hero.Y + ") in " + areaLevel);
                    List<PathNode> walkPath = map.GetWalkPath(Game.Hero.X, Game.Hero.Y, x, y);

                    if (walkPath == null)
                        ThrowModuleException(new ModuleException(this, "Mover::WalkTo - Unable to find walk path!"));

                    walkPath.Add(new PathNode(x, y));

                    TakeWalkPath(priority + 1, walkPath);
                });
        }*/

        #region MoveTo
        public void MoveTo(int priority, UnitType type, int id, AreaLevel areaLevel)
        {
            Game.TaskManager.AddTask(priority, "Finding preset unit to move to",
                delegate()
                {
                    Map map = Game.MapManager.GetMap(areaLevel);

                    if (map == null)
                        ThrowModuleException(new ModuleException(this, "Mover::MoveTo - " + Game.Hero.AreaLevel + " Map is null"));

                    PresetUnit unit = map.FindPresetUnit(type, id);
                    if (unit == null)
                        ThrowModuleException(new ModuleException(this, "Mover::MoveTo - PresetUnit is null"));

                    MoveTo(priority + 1, unit.X, unit.Y, areaLevel);
                });
        }
        public void MoveTo(int x, int y)
        {
            MoveTo((int)TaskPriority.Base, x, y);
        }
        public void MoveTo(int priority, int x, int y)
        {
            // Based on the CURRENT area level, better add it as a task!
            Game.TaskManager.AddTask(priority, "Starting move to " + x + ", " + y + " in current Hero AreaLevel",
                delegate()
                {
                    MoveTo(priority + 1, x, y, Game.Hero.AreaLevel);
                });
        }
        public void MoveTo(int x, int y, AreaLevel areaLevel)
        {
            MoveTo((int)TaskPriority.Base, x, y, areaLevel);
        }
        public void MoveTo(int priority, int x, int y, AreaLevel areaLevel)
        {
            // Check if we're in the target area
            // If we're not, get area links to it
            // 







            // This has to be a task because it paths based on the Hero x/y 
            // and we need to wait to get the correct hero.x/y
            Game.TaskManager.AddTask(priority, "Move to " + x + ", " + y,
                delegate()
                {
                    Map map = Game.MapManager.GetMap(areaLevel);

                    if (map == null)
                        ThrowModuleException(new ModuleException(this, "Mover::MoveTo - " + Game.Hero.AreaLevel + " Map = null"));

                    List<PathNode> path = new List<PathNode>();

                    if (IsTown(areaLevel) || !Game.Hero.HasTeleport)
                        path = map.GetWalkPath(Game.Hero.X, Game.Hero.Y, x, y);
                    else
                        path = map.GetTeleportPath(Game.Hero.X, Game.Hero.Y, x, y);

                    if (path == null || path.Count == 0)
                        ThrowModuleException(new ModuleException(this, "Mover::MoveTo - Unable to find path!"));

                    if (IsTown(areaLevel) || !Game.Hero.HasTeleport)
                    {
                        TakeWalkNodeRecursive(priority + 1, x, y, path, 0);

                        Console.WriteLine("Seed = " + Game.Seed + "; AreaLevel = " + areaLevel + "; Hero = " + Game.Hero.X + ", " + Game.Hero.Y + "; Destination = " + x + ", " + y);
                    }
                    else
                        TakeTelePath(priority + 1, path);
                });
        }
        #endregion

        #region Path taking helpers
        private void TakeLinkPath(int priority, AreaLink areaLink)
        {
            Game.TaskManager.AddTask(new Task(priority, "Taking link path in " + areaLink.AreaLevel,
                delegate()
                {
                    //TODO: there must be a case depending of the link type...
                    //maybe it isnt a warp but a object, or etc

                    Map map = Game.MapManager.GetMap(areaLink.AreaLevel);

                    if (map == null)
                        ThrowModuleException(new ModuleException(this, "Mover::TakeLinkPath - Map = null, Lag!?"));

                    if (areaLink.Exits != null)
                    {
                        PresetUnit warpPreset = map.FindWarps(areaLink.Exits);

                        if (warpPreset == null)
                            ThrowModuleException(new ModuleException(this, "Mover::TakeLinkPath - WarpPreset == null"));

                        MoveTo(priority + 1, warpPreset.Type, warpPreset.Id, areaLink.AreaLevel);

                        Game.TaskManager.AddTask(new Task(priority + 1, "Warping to next area",
                            delegate()
                            {
                                Warp warp = Game.Warps.Find((D2Data.WarpType)warpPreset.Id, 5000);

                                if (warp == null)
                                    ThrowModuleException(new ModuleException(this, "Mover::TakeLinkPath - Warp = null, Lag!?"));

                                if (!warp.InteractWait(10000))
                                    ThrowModuleException(new ModuleException(this, "Mover::TakeLinkPath - Couldn't make it through the warp"));
                            }));
                    }
                }));
        }

        /*
        private void TakeWalkPath(int priority, List<PathNode> path)
        {
            for (int i = 0; i < path.Count; i++)
            {
                int x = i;

                Task lastTask = new Task(priority, "Walking to " + path[x].X + ", " + path[x].Y,
                    delegate()
                    {
                        if(!Game.Hero.MoveWaitStatic(path[x].X, path[x].Y, 200))
                    });

                Game.TaskManager.AddTask(lastTask);
            }
        }*/

        /// <summary>
        /// 
        /// </summary>
        /// <param name="priority"></param>
        /// <param name="destX">the destination X in case we need to re-path</param>
        /// <param name="destY">the destination Y in case we need to re-path</param>
        /// <param name="nodes"></param>
        /// <param name="currentNode"></param>
        private void TakeWalkNodeRecursive(int priority, int destX, int destY, List<PathNode> nodes, int currentNode)
        {
            Task lastTask = new Task(priority, "Walking to node " + (currentNode + 1) + "/" + nodes.Count + " at " + nodes[currentNode].X + ", " + nodes[currentNode].Y,
                delegate()
                {
                    // If the walk was successful
                    if (Game.Hero.MoveWaitStatic(nodes[currentNode].X, nodes[currentNode].Y, 200))
                    {
                        // we still have some walking to do
                        if (currentNode < nodes.Count - 1)
                            TakeWalkNodeRecursive(priority, destX, destY, nodes, ++currentNode);
                    }
                    else
                    {
                        // don't add any more walks, re-path to the last node
                        MoveTo(destX, destY, priority);
                    }
                });

            Game.TaskManager.AddTask(lastTask);
        }

        private void TakeTelePath(int priority, List<PathNode> path)
        {
            for (int i = 0; i < path.Count; i++)
            {
                int x = i;

                Game.TaskManager.AddTask(new Task(priority, "Teleporting to " + path[x].X + ", " + path[x].Y,
                    delegate()
                    {
                        Game.Hero.TeleportWait(path[x].X, path[x].Y, 5000);
                        System.Threading.Thread.Sleep(240); //skill fps = 6
                    }));
            }
        }
        #endregion

        public static bool IsTown(AreaLevel level)
        {
            return  level == AreaLevel.RogueEncampment ||
                    level == AreaLevel.LutGholein ||
                    level == AreaLevel.KurastDocks ||
                    level == AreaLevel.ThePandemoniumFortress ||
                    level == AreaLevel.Harrogath;
        }
    }
}
