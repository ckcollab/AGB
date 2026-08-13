using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;

using D2Data;
using D2Packets.D2Packets;

namespace AGB.D2.Modules
{
    public class PickIt : Module
    {
        private PickItConfig Config;
        private Mover Mover;

        public PickItEvaluator Evaluator;

        public PickIt()
        {
            Name = "PickIt";
            Author = "ApacheChief and ShadowDancer";
            Version = "0.1.0";
        }

        public override void Load()
        {
            Config = new PickItConfig("PickIt/config.xml");

            Evaluator = new PickItEvaluator();

            foreach (PickItCategory category in Config.Categories)
                foreach (PickItRequirement requirement in category.Items)
                {
                    // Since we unserialized these, we have to re-build the Opcodes
                    // to evaluate them
                    requirement.BuildOps();

                    Evaluator.AddRequirements(requirement);
                }


            /* Testing pickit

            byte[] data = ETUtils.ByteConverter.ParseHex("9c 02 2e 01 03 af cf 3a 10 00 80 00 65 6c f3 e1 6a 02 af 2c 0c 04 cb 43 1a 12 81 81 01 02 9e 11 ca 89 a3 14 47 2b 8e 5a 1c a5 91 fd c9 7f");
            D2Packets.GameServer.WorldItemAction packet = new D2Packets.GameServer.WorldItemAction(data);

            Item item = new Item(Game);
            item.Initialize(packet);

            DateTime start = DateTime.Now;
            for (int i = 0; i < 1000; i++)
            {
                PickItResult result = Evaluator.Evaluate(item);
            }
            TimeSpan length = DateTime.Now.Subtract(start);

            Console.WriteLine(length.TotalMilliseconds);
            int done3498 = 1;
                        */



            if (!Bot.HasModule("Mover"))
                ThrowModuleException(new ModuleException(this, "Module 'Mover' couldn't be found, is it installed?"));

            Mover = Bot.GetModule("Mover") as Mover;

            Game.ItemDropped += ItemDropped;
        }

        void ItemDropped(Game game, Item item)
        {
            // Just pick up every item right now
            //PickUp(item);
        }

        /// <summary>
        /// Picks an item up from the ground
        /// </summary>
        /// <param name="item"></param>
        /// <param name="timeOut"></param>
        public void PickUp(Item item)
        {
            Game.TaskManager.AddTask(new Task((int)TaskPriority.AboveNormal, "Getting ready to pick up an item",
                delegate()
                {
                    // Since this function can execute in town before everything is loaded, let's make SURE
                    // we're ready to go
                    while (Game.Hero.X == 0 || Game.Hero.Y == 0 || Game.Hero.AreaLevel == AreaLevel.None || Game.Seed == 0)
                        System.Threading.Thread.Sleep(1);

                    if (item.Action.Destination != ItemDestination.Ground && item.Action.Container != ItemLocation.Ground)
                        throw new ArgumentException("Item has to be on the ground for PickIt, silly!");

                    if (!Game.Hero.Inventory.HasSpaceFor(item))
                    {
                        RaiseWarning("Hero inventory is full, cannot pickup this item!");
                        return;
                    }

                    Mover.MoveTo((int)TaskPriority.AboveNormal, item.X, item.Y);

                    Game.TaskManager.AddTask(new Task((int)TaskPriority.AboveNormal, "Picking up " + item.Action.Quality + " " + item.Action.BaseItem.Class,
                        delegate()
                        {
                            // Pickit off for now so bot dont freeeze yoooo
                            //item.PickUp(timeOut);

                            if (!item.ToCursor(10000))
                            {
                                RaiseWarning("Couldn't pick up the item!");
                                return;
                            }

                            if (!item.ToContainer(ItemLocation.Inventory, 10000))
                            {
                                RaiseWarning("Couldn't move the item to inventory!");
                                return;
                            }

                            Console.WriteLine("Do we have an item on the cursor?  " + (Game.Hero.ItemOnCursor != null));
                        }));
                }));
        }
    }
}
