using System;
using System.Collections.Generic;

using AGB;
using AGB.D2;

namespace AGB.D2.Modules
{
    public class Chicken : Module
    {
        private Mover Mover;
        private TownManager TownManager;

        public int RegularPotionHP = 75;
        public int RegularPotionMana = 50;

        public int RejuvHP = 60;
        public int RejuvMana = 30;

        public int ChickenHP = 20;
        public int ChickenMana = -1;

        public int MercRegularPotion = 75;
        public int MercRejuv = 30;

        public int MercChicken = -1;

        public Chicken()
        {
            Name = "Chicken";
            Author = "ApacheChief and ShadowDancer";
            Version = "0.1.0";
        }

        public override void Load()
        {
            if (!Bot.HasModule("Mover"))
                ThrowModuleException(new ModuleException(this, "Module 'Mover' couldn't be found, is it installed?"));
            if (!Bot.HasModule("TownManager"))
                ThrowModuleException(new ModuleException(this, "Module 'TownManager' couldn't be found, is it installed?"));

            Mover = Bot.GetModule("Mover") as Mover;
            TownManager = Bot.GetModule("TownManager") as TownManager;

            Game.Hero.LifeChanged += LifeChanged;
            Game.Hero.ManaChanged += ManaChanged;
            Game.Mercenary.LifeChanged += LifeChanged;
        }

        void LifeChanged(NPC npc, int oldValue, int newValue)
        {
            if (oldValue <= 0 || newValue <= 0) 
                return;

            int hitPercent = 100 - ((100 * newValue) / oldValue);

            int totalpercent = 0;

            //if (npc.IsMercenary)
            //    totalpercent = npc.LifeAsPercent;
            //else
                if (npc.MaxLife > 0)
                    totalpercent = 100 * newValue / npc.MaxLife;

            Console.WriteLine("life changed: oldValue = " + oldValue + "; newValue = " + newValue + "; hitPercent = " + hitPercent + "%; totalPercent = " + totalpercent + "%; isMerc = " + npc.IsMercenary);

            //dont do anything if we are in town
            if (Game.Hero.IsInTown)
                return;

            //drop ratio = 20% per frame ?
            /*
            if (hitPercent <= 80)
            {
                Console.WriteLine("hitPercent <= 80");
                Game.LeaveGame();
                return; //we dont know anything else be done.
            }*/

            if (totalpercent <= ChickenHP)
            {
                Console.WriteLine("totalpercent <= " + ChickenHP);
                Game.LeaveGame();
                return; //we dont know anything else be done.
            }


            if (totalpercent <= RegularPotionHP)
            {
                if (DateTime.Now.Subtract(npc.LastDrink).TotalMilliseconds > 1000)
                {
                    Console.WriteLine("Drinking a health pot");
                    if (Game.Hero.DrinkPotion(npc.IsMercenary, PotionType.Health, 2500) == true)
                    {
                        npc.LastDrink = DateTime.Now;
                    }
                    else
                    {
                        Console.WriteLine("Failed to drink (no health pots?)");
                        Mover.GoToTown(65555); //lets buy some or heal.(?)
                        //town manager should implement a method we can call here.
                    }
                }
            }
        }

        void ManaChanged(NPC npc, int oldValue, int newValue)
        {
            
        }
    }
}