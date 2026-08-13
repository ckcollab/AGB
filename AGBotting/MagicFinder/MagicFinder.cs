using System;
using System.Collections.Generic;

using AGB;
using AGB.D2;

using D2Data;

namespace AGB.D2.Modules
{
    public class MagicFinder : Module
    {
        private Killer Killer;
        private Mover Mover;
        private TownManager TownManager;

        public MagicFinder()
        {
            Name = "MagicFinder";
            Author = "ApacheChief";
            Version = "0.1.0";
        }

        public override void Load()
        {
            // Checking for required modules
            if (!Bot.HasModule("Killer"))
                ThrowModuleException(new ModuleException(this, "Module 'Killer' couldn't be found, is it installed?"));
            if (!Bot.HasModule("Mover"))
                ThrowModuleException(new ModuleException(this, "Module 'Mover' couldn't be found, is it installed?"));
            if (!Bot.HasModule("TownManager"))
                ThrowModuleException(new ModuleException(this, "Module 'TownManager' couldn't be found, is it installed?"));

            Killer = Bot.GetModule("Killer") as Killer;
            Mover = Bot.GetModule("Mover") as Mover;
            TownManager = Bot.GetModule("TownManager") as TownManager;
        }

        public override void GameEntered(Game game)
        {
            TownManager.UpKeepFinished.WaitOne();

            TownManager.Heal((int)TaskPriority.Base, ActLevel.Act1);

            // Andy
            Mover.GoToArea(AreaLevel.CatacombsLevel4);
            Mover.MoveTo(22561, 9561);
            Killer.Kill(NPCClass.Andariel);
            TaskSleep(2000);
            /*
            Mover.GoToTown();

            // Mephisto
            Mover.GoToArea(AreaLevel.DuranceOfHateLevel3);
            Mover.MoveTo((int)TaskPriority.Base, UnitType.NPC, (int)NPCClass.Mephisto, AreaLevel.DuranceOfHateLevel3);
            Killer.Kill(NPCClass.Mephisto);
            TaskSleep(2000);

            Game.TaskManager.AddTask((int)TaskPriority.Base, "Go Town",
                delegate()
                {
                    Mover.GoToTown();
                });

            Game.TaskManager.AddTask((int)TaskPriority.Base, "Starting a new run.",
                delegate()
                {
                    System.Threading.Thread.Sleep(10000);
                });
            */
            // Done!
            Game.TaskManager.AddTask((int)TaskPriority.Base, "Done, leaving game",
                delegate()
                {
                    Game.LeaveGame();
                });

            //return;

            // Pindle

            /*
            int walkDelay = 60;

            // Move from: start of act 5, to: next to WP
            MoveStaticTask(5103, 5043, walkDelay);
            MoveStaticTask(5110, 5064, walkDelay);

            // Move down to portal
            MoveStaticTask(5119, 5090, walkDelay);
            MoveStaticTask(5131, 5108, walkDelay);
            MoveStaticTask(5119, 5120, walkDelay);

            // Go through portal
            Game.TaskManager.AddTask((int)TaskPriority.Base, "Went through portal",
                delegate()
                {
                    Object portal = Game.Objects.Find(GameObjectClass.PermanentTownPortal);

                    if (portal == null)
                        ThrowModuleException(new ModuleException(this, "Portal not found!"));

                    if(!portal.PortalInteractWait(5000))
                        ThrowModuleException(new ModuleException(this, "Couldn't go through the portal"));
                });

            // Go up to Pindle
            TeleportTask(10064, 13293, 2000);
            TeleportTask(10063, 13268, 2000);
            TeleportTask(10058, 13238, 2000);

            // Kill Pindle
            Game.TaskManager.AddTask((int)TaskPriority.Base, "Found Pindle, killing him",
                delegate()
                {
                    DateTime start = DateTime.Now;

                    List<NPC> pindlesGroup = null;

                    while(pindlesGroup == null && DateTime.Now.Subtract(start).TotalMilliseconds < 5000)
                        // Grab the whole group of his units
                        pindlesGroup = Game.NPCs.FindAll(NPCClass.DefiledWarrior);

                    if (pindlesGroup == null)
                        ThrowModuleException(new ModuleException(this, "Couldn't find Pindle and his goons..."));

                    // Put out the hit
                    Killer.Kill(pindlesGroup);  
                });

            // Done!
            Game.TaskManager.AddTask((int)TaskPriority.Base, "Done, left game",
                delegate()
                {
                    Console.WriteLine("leave game");
                    Game.LeaveGame();
                });
            */
        }

        private void TaskSleep(int ms)
        {
            Game.TaskManager.AddTask((int)TaskPriority.Base, "Waiting for items to drop",
                delegate()
                {
                    System.Threading.Thread.Sleep(ms);
                });
        }

        private void TeleportTask(int x, int y, int timeout)
        {
            Game.TaskManager.AddTask((int)TaskPriority.Base, "Teleported to " + x + ", " + y,
                delegate()
                {
                    Game.Hero.TeleportWait(x, y, timeout);
                });
        }
    }
}
