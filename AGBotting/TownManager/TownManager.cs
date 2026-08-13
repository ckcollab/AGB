using System;
using System.Collections.Generic;
using System.Threading;

using AGB.D2;

using D2Data;

using D2Packets;
using D2Packets.D2Packets;

namespace AGB.D2.Modules
{
    public class TownManager : Module
    {
        private Mover Mover;
        private PickIt PickIt;

        public static Town[] Towns;

        public ManualResetEvent UpKeepFinished;

        static TownManager()
        {
            Towns = new Town[5];

            #region Act 1
            Towns[0] = new Town();
            Towns[0].AreaLevel = AreaLevel.RogueEncampment;

            Towns[0].Healer = NPCClass.Akara;
            Towns[0].Repairer = NPCClass.Charsi;
            Towns[0].PortalSeller = NPCClass.Akara;
            Towns[0].MercenaryReviver = NPCClass.Kashya;
            Towns[0].TownPortalArea = GameObjectClass.RogueBonfire;
            #endregion

            #region Act 2
            Towns[1] = new Town();
            Towns[1].AreaLevel = AreaLevel.LutGholein;

            Towns[1].Healer = NPCClass.Fara;
            Towns[1].Repairer = NPCClass.Fara;
            Towns[1].PortalSeller = NPCClass.Drognan;
            Towns[1].MercenaryReviver = NPCClass.Greiz;
            Towns[1].TownPortalArea = GameObjectClass.Gesturer;
            #endregion

            #region Act 3
            Towns[2] = new Town();
            Towns[2].AreaLevel = AreaLevel.KurastDocks;

            Towns[2].Healer = NPCClass.Ormus;
            Towns[2].Repairer = NPCClass.Hratli;
            Towns[2].PortalSeller = NPCClass.Ormus;
            Towns[2].MercenaryReviver = NPCClass.Asheara;
            Towns[2].TownPortalArea = GameObjectClass.Act3TownWaypoint;
            #endregion

            #region Act 4
            Towns[3] = new Town();
            Towns[3].AreaLevel = AreaLevel.ThePandemoniumFortress;

            Towns[3].Healer = NPCClass.Jamella;
            Towns[3].Repairer = NPCClass.Halbu;
            Towns[3].PortalSeller = NPCClass.Jamella;
            Towns[3].MercenaryReviver = NPCClass.Tyrael2;
            Towns[3].TownPortalArea = GameObjectClass.PandamoniumFortressWaypoint;
            #endregion

            #region Act 5
            Towns[4] = new Town();
            Towns[4].AreaLevel = AreaLevel.Harrogath;

            Towns[4].Healer = NPCClass.Malah;
            Towns[4].Repairer = NPCClass.Larzuk;
            Towns[4].PortalSeller = NPCClass.Malah;
            Towns[4].MercenaryReviver = NPCClass.QualKehk;
            Towns[4].TownPortalArea = GameObjectClass.ExpansionChandelier;
            #endregion
        }

        public TownManager()
        {
            Name = "TownManager";
            Author = "ApacheChief and ShadowDancer";
            Version = "0.1.0";

            UpKeepFinished = new ManualResetEvent(false);
        }

        public override void Load()
        {
            // Checking for required modules
            if (!Bot.HasModule("Mover"))
                ThrowModuleException(new ModuleException(this, "Module 'Mover' couldn't be found, is it installed?"));
            if (!Bot.HasModule("PickIt"))
                ThrowModuleException(new ModuleException(this, "Module 'PickIt' couldn't be found, is it installed?"));

            Mover = Bot.GetModule("Mover") as Mover;
            PickIt = Bot.GetModule("PickIt") as PickIt;
        }

        public override void GameEntered(Game game)
        {
            UpKeep();
        }

        public void UpKeep()
        {
            Game.TaskManager.AddTask((int)TaskPriority.Base, "Delaying the start",
                delegate()
                {
                    while (Game.Hero.X == 0 || Game.Hero.Y == 0 || Game.Hero.AreaLevel == AreaLevel.None || Game.Seed == 0)
                        System.Threading.Thread.Sleep(1);
                    System.Threading.Thread.Sleep(500);
                });

            // PickUp body
            Game.TaskManager.AddTask((int)TaskPriority.Base, "Checking for corpse",
                delegate()
                {
                    List<Player> players = Game.Players.Find(Game.Hero.Player.Name);
                    if (players != null)
                    {
                        foreach (Player player in players)
                        {
                            if (player.Uid != Game.Hero.Uid)
                            {
                                Game.TaskManager.AddTask((int)TaskPriority.Base + 1, "Pick a body",
                                    delegate()
                                    {
                                        Game.Socket.Game.Send(new D2Packets.GameClient.UnitInteract(D2Data.UnitType.Player, player.Uid).Data);

                                        D2Packets.D2Packets.D2Packet packet = Game.Socket.PacketHandler.WaitForPacket(D2Packets.D2Packets.GameServerPacket.AssignPlayerCorpse, 15000);
                                    });
                            }
                        }
                    }
                });

            // Check life/mana with Chicken -- see if we should talk to Town Healer

            // Check durability -- see if we should talk to Town Repairer
            /*
            // Buying tomes/portals if we dont have enougth:
            Game.TaskManager.AddTask((int)TaskPriority.Base, "Checking for tomes",
                delegate()
                {
                    List<WishListItem> itemWishList = new List<WishListItem>();

                    // do we have a tome of town portal?
                    Item tomeOfTownPortal = Game.Hero.Items.Find((Item i) => (i.Action.BaseItem.Class == D2Data.ItemClass.TomeOfTownPortal));
                    if (tomeOfTownPortal != null)
                    {
                        if (tomeOfTownPortal.SimpleStats.ContainsKey("Quantity"))
                        {
                            int quantity = tomeOfTownPortal.SimpleStats["Quantity"];
                            Console.WriteLine("Avaliable Portals: " + quantity);
                            if (quantity < 10)
                            {
                                //buy some portals
                                itemWishList.Add(new WishListItem(D2Data.ItemClass.ScrollOfTownPortal, 20 - quantity));
                            }
                        }
                    }
                    else
                    {
                        //buy a tome... if money
                        if (Game.Hero.GoldInStash + Game.Hero.GoldInInventory > 500)
                            itemWishList.Add(new WishListItem(D2Data.ItemClass.TomeOfTownPortal, 1));

                        //we cannt leave the city with at leats 1
                        itemWishList.Add(new WishListItem(D2Data.ItemClass.ScrollOfTownPortal, 20));
                    }

                    // do we have a tome of identify?
                    Item tomeOfIdentify = Game.Hero.Items.Find((Item i) => (i.Action.BaseItem.Class == D2Data.ItemClass.TomeOfIdentify));
                    if (tomeOfIdentify == null)
                    {
                        //buy a tome... if money
                        if (Game.Hero.GoldInStash + Game.Hero.GoldInInventory > 500)
                            itemWishList.Add(new WishListItem(D2Data.ItemClass.TomeOfIdentify, 1));
                    }

                    if (itemWishList.Count > 0)
                    {
                        Game.TaskManager.AddTask((int)TaskPriority.Base + 1, "Trying to buy tomes",
                        delegate()
                        {
                            Mover.MoveTo((int)TaskPriority.Base + 2, UnitType.NPC, (int)Towns[(int)Game.Hero.Act].PortalSeller, AreaLevel.Harrogath);

                            Game.TaskManager.AddTask((int)TaskPriority.Base + 2, "Buying tomes",
                            delegate()
                            {
                                NPC npc = Game.NPCs.Find(Towns[(int)Game.Hero.Act].PortalSeller);
                                if (npc == null)
                                    ThrowModuleException(new ModuleException(this, "We are here, but the npc isnt!"));

                                Mover.MoveTo((int)TaskPriority.Base + 3, npc.X, npc.Y);

                                Game.TaskManager.AddTask((int)TaskPriority.Base + 3, "Talking with NPC and buying",
                                    delegate()
                                    {
                                        if (!npc.OpenTrade(15000))
                                        {
                                            Console.WriteLine("*** Cannt open a trade with the npc");
                                        }
                                        System.Threading.Thread.Sleep(1000); //TODO: reduce this number
                                        List<Item> items = Game.Items.GetFromShop();

                                        foreach (WishListItem wlitem in itemWishList)
                                        {
                                            foreach (Item witem in items)
                                            {
                                                Console.WriteLine("There is a " + witem.Action.BaseItem.Class + " here...");
                                                if (witem.Action.BaseItem.Class == wlitem.ItemClass)
                                                {
                                                    Console.WriteLine("i want it!");
                                                    for (int i = 0; i < wlitem.Count; i++)
                                                        npc.BuyItem(witem);
                                                }
                                            }
                                        }

                                        npc.CloseTrade();
                                    });
                            });
                        });
                    }

                });

            // Identify items
            Game.TaskManager.AddTask((int)TaskPriority.Base, "Checking for items to identify",
                 delegate()
                 {
                     Identify((int)TaskPriority.Base);
                 });

            // Stash
            Game.TaskManager.AddTask((int)TaskPriority.Base, "Checking for items to stash",
                 delegate()
                 {
                     Stash((int)TaskPriority.Base);
                 });

            // Get a merc
            Game.TaskManager.AddTask((int)TaskPriority.Base, "Check if we have a merc",
                 delegate()
                 {
                     if (Game.Mercenary.Uid == 0)
                     {
                         //buy a new one
                         ResurectMerc((int)TaskPriority.Base + 1);
                     }
                 });
            */
            // Check if Hero.AvailableWaypoints.Count is == 0, if so, go talk to the waypoint in this town to grab them
            Game.TaskManager.AddTask((int)TaskPriority.Base, "Checking for available waypoints",
                delegate()
                {
                    if (Game.Hero.AvailableWaypoints.Count == 0)
                    {
                        Mover.GoToWaypoint((int)TaskPriority.Base + 1);

                        Game.TaskManager.AddTask(new Task((int)TaskPriority.Base + 1, "Talking to waypoint, to get available waypoints",
                            delegate()
                            {
                                Map map = Game.MapManager.GetMap(Game.Hero.AreaLevel);

                                if (map == null)
                                    ThrowModuleException(new ModuleException(this, "TownManager::GameEntered - Map is null"));

                                PresetUnit presetunit = map.FindWayPoint();
                                if (presetunit == null)
                                    ThrowModuleException(new ModuleException(this, "TownManager::GameEntered - PresetUnit is null"));

                                Object wayPoint = Game.Objects.Find((D2Data.GameObjectClass)presetunit.Id, 5000);

                                if (wayPoint == null)
                                    ThrowModuleException(new ModuleException(this, "TownManager::GameEntered - Couldn't find the Waypoint"));

                                // Open it, for shits
                                if (wayPoint.OpenWaypoint(10000) == false)
                                    ThrowModuleException(new ModuleException(this, "TownManager::GameEntered - Couldn't open the Waypoint"));

                                wayPoint.CloseWaypoint();
                            }));
                    }
                });
            
            UpKeepFinished.Set();
        }

        /// <summary>
        /// Goes to town in your current act and heals you
        /// </summary>
        public void Heal()
        {
            Heal((int)TaskPriority.Base);
        }
        /// <summary>
        /// Goes to town in your current act and heals you
        /// </summary>
        public void Heal(int priority)
        {
            // Since this is based on the hero, add it as a task
            Game.TaskManager.AddTask(priority, "Healing in current hero act",
                delegate()
                {
                    Heal(priority + 1, Game.Hero.Act);
                });
        }
        /// <summary>
        /// Goes to town in the specified act and heals you
        /// </summary>
        /// <param name="priority"></param>
        /// <param name="act">1 for act 1, 5 for act 5 -- not zero based!</param>
        public void Heal(int priority, ActLevel act)
        {
            Game.TaskManager.AddTask(priority, "Going to heal in act " + act,
                delegate()
                {
                    Mover.GoToAct(priority + 1, act);

                    // Specify the AreaLevel for this, because the Hero might not
                    // be in the same area
                    Mover.MoveTo(priority + 1, UnitType.NPC, (int)Towns[(int)act].Healer, Towns[(int)act].AreaLevel);

                    Game.TaskManager.AddTask(priority + 1, "Talking to healer",
                        delegate()
                        {
                            NPC healer = Game.NPCs.Find(Towns[(int)act].Healer, 5000);

                            if (healer == null)
                                ThrowModuleException(new ModuleException(this, "TownManager::Heal - Couldn't find the NPC to heal at"));

                            //if (!healer.Stop(5000))
                            //    ThrowModuleException(new ModuleException(this, "TownManager::Heal - Couldn't stop the NPC"));

                            Mover.MoveTo(priority + 2, healer.X, healer.Y);

                            if (!healer.OpenTrade(5000))
                                ThrowModuleException(new ModuleException(this, "TownManager::Heal - Couldn't open trade with the NPC"));

                            healer.CloseTrade();
                        });
                });
        }

        /// <summary>
        /// Goes to the current act's town TP Area with base priority
        /// </summary>
        public void GoToTownPortalArea()
        {
            GoToTownPortalArea((int)TaskPriority.Base);
        }
        /// <summary>
        /// Goes to the current act's town TP Area with specified priority
        /// </summary>
        /// <param name="priority"></param>
        public void GoToTownPortalArea(int priority)
        {
            // Since this is based on the hero, add it as a task
            Game.TaskManager.AddTask(priority, "Going to TP Area in current hero act",
            delegate()
            {
                GoToTownPortalArea(priority + 1, Game.Hero.Act);
            });
        }
        /// <summary>
        /// Goes to the specified act's town TP Area
        /// </summary>
        /// <param name="priority"></param>
        public void GoToTownPortalArea(int priority, ActLevel act)
        {
            Game.TaskManager.AddTask(priority, "Moving to TP Area in act " + act,
            delegate()
            {
                Mover.GoToAct(priority + 1, act);

                Game.TaskManager.AddTask(priority + 1, "Moving to TP Area",
                delegate()
                {
                    int offsetX = 0;
                    int offsetY = 0;
                    Map map = Game.MapManager.GetMap(Game.Hero.AreaLevel);

                    if (map == null)
                        ThrowModuleException(new ModuleException(this, "TownManager::GoToTpArea - Map is null"));

                    PresetUnit tpArea = map.FindPresetUnit(UnitType.GameObject, (int)Towns[(int)act].TownPortalArea);

                    if (tpArea == null)
                        ThrowModuleException(new ModuleException(this, "TownManager::GoToTpArea - Couldn't find the TP Area"));

                    switch (act)
                    {
                        case ActLevel.Act1: offsetX = 18; offsetY = 26; break;
                        case ActLevel.Act2: offsetX = 23; offsetY = 28; break;
                        case ActLevel.Act5: offsetX = 29; offsetY = 5; break;
                        default: break;
                    }

                    Mover.MoveTo(priority + 2, tpArea.X + offsetX, tpArea.Y + offsetY);
                });
            });
        }

        /*
         * [ ] If $$$ less than $5000 then quit
         * [ ] Go to Portal dealer
         * [ ] Buy Tome of Identify if it doesn't exist
         * [ ] Buy Scroll of Identify if Tome quantity < 10
         * [ ] Identify item(s)
         * [ ] Sell back any items that aren't PickItResult.Keep
         */
        /*
        public void Identify(int priority)
        {
            List<Item> olist = Game.Hero.Items.FindAll(
                    (Item i) => i.Action.Container == D2Data.ItemLocation.Inventory && 
                    !i.IsIdentified && 
                    PickIt.Evaluator.Evaluate(i) == PickItResult.Keep);

            Item tomeOfIdentify = Game.Hero.Items.Find((Item i) => (i.Action.BaseItem.Class == D2Data.ItemClass.TomeOfIdentify));
            if (tomeOfIdentify == null)
                ThrowModuleException(new ModuleException(this, "this character doesnt have a tome of identify"));

            Game.TaskManager.AddTask(priority, "Trying to indentify",
            delegate()
            {
                Mover.MoveTo(priority + 1, UnitType.NPC, (int)Towns[4].PortalSeller, AreaLevel.Harrogath);

                Game.TaskManager.AddTask(priority + 1, "??",
                delegate()
                {
                    NPC npc = Game.NPCs.Find(Towns[(int)Game.Hero.Act].PortalSeller);
                    if (npc == null)
                        ThrowModuleException(new ModuleException(this, "We are here, but the npc isnt!"));

                    Mover.MoveTo(priority + 2, npc.X, npc.Y);

                    Game.TaskManager.AddTask(priority + 2, "Talking with NPC and buying",
                        delegate()
                        {
                            if (!npc.OpenTrade(15000))
                            {
                                Console.WriteLine("*** Cannt open a trade with the npc");
                            }
                            System.Threading.Thread.Sleep(1000); //TODO: reduce this number

                            foreach (Item i in olist)
                            {
                                Boolean identified = false;

                                tomeOfIdentify = Game.Hero.Items.Find((Item it) => (it.Action.BaseItem.Class == D2Data.ItemClass.TomeOfIdentify));
                                if (tomeOfIdentify != null)
                                {
                                    if (tomeOfIdentify.SimpleStats.ContainsKey("Quantity"))
                                    {
                                        int quantity = tomeOfIdentify.SimpleStats["Quantity"];

                                        if(quantity < 20)
                                        {
                                            List<Item> items = Game.Items.GetFromShop();

                                            Item identifyScroll = items.Find((Item it) => it.Action.BaseItem.Class == ItemClass.ScrollOfIdentify);
                                            npc.BuyItem(identifyScroll);
                                        }
                                    }
                                }

                                PacketEventHandler useStackableItem =
                                    delegate(D2Packets.D2Packets.D2Packet args)
                                    {
                                        D2Packets.GameServer.UseStackableItem packet = new D2Packets.GameServer.UseStackableItem(args.Data);

                                        Game.Socket.Game.Send(new D2Packets.GameClient.IdentifyItem(i.Uid, tomeOfIdentify.Uid).Data);

                                        identified = true;
                                    };
                                Game.Socket.PacketHandler.AddAsyncListener(GameServerPacket.UseStackableItem, useStackableItem);

                                Game.Socket.Game.Send(new D2Packets.GameClient.UseContainerItem(tomeOfIdentify.Uid, Game.Hero.X, Game.Hero.Y).Data);

                                DateTime watchstart = DateTime.Now;
                                while (DateTime.Now.Subtract(watchstart).TotalMilliseconds < 5000)
                                {
                                    if (identified)
                                        break;

                                    System.Threading.Thread.Sleep(1);
                                }

                                Game.Socket.PacketHandler.RemoveAsyncListener(GameServerPacket.UseStackableItem, useStackableItem);

                                // failed...
                                if (!identified)
                                    ThrowModuleException(new ModuleException(this, "TownManager::Identify -- Failed identifying an item"));
                            }

                            npc.CloseTrade();
                        });
                });

            });
        }


        public void Stash(int priority)
        {
            List<Item> ilist = Game.Hero.Items.FindAll(
                (Item i) => (i.Action.Container == D2Data.ItemLocation.Inventory));

            List<Item> olist = new List<Item>();

            foreach (Item i in ilist)
            {
                //skip these items:
                if (i.Action.BaseItem.Class == ItemClass.GrandCharm)
                    continue;
                if (i.Action.BaseItem.Class == ItemClass.LargeCharm)
                    continue;
                if (i.Action.BaseItem.Class == ItemClass.SmallCharm)
                    continue;
                if (i.Action.BaseItem.Class == ItemClass.ScrollOfIdentify)
                    continue;
                if (i.Action.BaseItem.Class == ItemClass.ScrollOfTownPortal)
                    continue;
                if (i.Action.BaseItem.Class == ItemClass.TomeOfIdentify)
                    continue;
                if (i.Action.BaseItem.Class == ItemClass.TomeOfTownPortal)
                    continue;

                olist.Add(i);
                Console.WriteLine("We have something to stash!");
            }

            if (olist.Count == 0)
                return;

            Mover.GoToTown();
            Mover.MoveTo(priority, UnitType.GameObject, (int)D2Data.GameObjectClass.Bank, Game.Hero.AreaLevel);

            Game.TaskManager.AddTask(priority, "opening the stash.",
                delegate()
                {
                    Object bank = Game.Objects.Find(GameObjectClass.Bank);

                    if (bank == null)
                        ThrowModuleException(new ModuleException(this, "Stasher :: Bank object not found"));

                    Boolean stashOpen = false;

                    PacketEventHandler updateItemUI =
                        delegate(D2Packets.D2Packets.D2Packet args)
                        {
                            D2Packets.GameServer.UpdateItemUI packet = new D2Packets.GameServer.UpdateItemUI(args.Data);

                            if (packet.Action == ItemUIAction.OpenStash)
                            {
                                stashOpen = true;
                            }
                        };

                    Game.Socket.PacketHandler.AddAsyncListener(GameServerPacket.UpdateItemUI, updateItemUI);

                    //lets open the stash.
                    bank.Interact();

                    DateTime watchstart = DateTime.Now;
                    while (DateTime.Now.Subtract(watchstart).TotalMilliseconds < 15000)
                    {
                        if (stashOpen)
                            break;

                        Thread.Sleep(1);
                    }

                    Game.Socket.PacketHandler.RemoveAsyncListener(D2Packets.D2Packets.GameServerPacket.UpdateItemUI, updateItemUI);

                    if (!stashOpen)
                        ThrowModuleException(new ModuleException(this, "TownManager::Stash -- Stash was never opened!"));

                    Console.WriteLine("Stash is now open");
                 });

            //start moving the items...

            //close the stash
            Game.TaskManager.AddTask(priority, "closing the stash.",
                delegate()
                {
                    Game.Socket.Game.Send(new D2Packets.GameClient.ClickButton(GameButton.CloseStash, 0).Data);
                });
        }

        public void ResurectMerc(int priority)
        {
            if (!Game.Hero.IsInTown)
                Mover.GoToTown(priority);

            //not enougth gold
            if (Game.Hero.GoldInStash + Game.Hero.GoldInInventory < 50000)
            {
                Console.WriteLine("*** We dont have enougth money to resurect the merc.");
                return;
            }

            Mover.MoveTo(priority, UnitType.NPC, (int)Towns[(int)Game.Hero.Act].MercenaryReviver, Game.Hero.AreaLevel);
            //interact with him.

            Game.TaskManager.AddTask(priority, "??",
            delegate()
            {
                NPC npc = Game.NPCs.Find(Towns[(int)Game.Hero.Act].MercenaryReviver);
                if (npc == null)
                    ThrowModuleException(new ModuleException(this, "TownManager::RessurectMerc -- We are here, but the NPC isnt!"));

                Mover.MoveTo(priority + 1, npc.X, npc.Y);

                Game.TaskManager.AddTask(priority + 1, "Talking with NPC and buying a merc (?)",
                    delegate()
                    {
                        uint EquipItemUid = 0;

                        PacketEventHandler ownedItemActionDelg =
                            delegate(D2Packets.D2Packets.D2Packet args)
                            {
                                D2Packets.GameServer.OwnedItemAction packet = new D2Packets.GameServer.OwnedItemAction(args.Data);

                                if (packet.OwnerType == UnitType.NPC)
                                {
                                    if (packet.OwnerUID == Game.Mercenary.Uid)
                                    {
                                        if (packet.Action == ItemActionType.Equip)
                                        {
                                            if (packet.Category == ItemCategory.Weapon ||
                                                packet.Category == ItemCategory.Weapon2)
                                            {
                                                EquipItemUid = packet.UID;
                                            }
                                        }
                                    }
                                }
                            };

                        Game.Socket.PacketHandler.AddAsyncListener(D2Packets.D2Packets.GameServerPacket.OwnedItemAction, ownedItemActionDelg);

                        if (!npc.ReviveMerc(15000))
                        {
                            Console.WriteLine("*** Cannt revive the merc");
                            ThrowModuleException(new ModuleException(this, "TownManager::RessurectMerc -- Couldn't revive the mercenary!"));
                        }

                        DateTime watchstart = DateTime.Now;
                        while (DateTime.Now.Subtract(watchstart).TotalMilliseconds < 5000)
                        {
                            if (EquipItemUid != 0)
                                break;

                            System.Threading.Thread.Sleep(1);
                        }

                        if (EquipItemUid == 0)
                            return;

                        //item to cursor.
                        //there is surelly a nice codelooking way to do this (?)
                        Game.Socket.Game.Send(new D2Packets.GameClient.ChangeMercEquipment(EquipmentLocation.RightHand).Data);
                        //we can safelly assume the item'll be picked, so lets wait until itemincursor change.

                        DateTime startWatchingItemOnCursor = DateTime.Now;
                        while (DateTime.Now.Subtract(startWatchingItemOnCursor).TotalMilliseconds < 15000)
                        {
                            if (Game.Hero.ItemOnCursor != null)
                                break;

                            Thread.Sleep(1);
                        }

                        //equip cursor item to merc
                        Game.Socket.Game.Send(new D2Packets.GameClient.ChangeMercEquipment(EquipmentLocation.RightHand).Data);

                        Game.Socket.PacketHandler.RemoveAsyncListener(D2Packets.D2Packets.GameServerPacket.OwnedItemAction, ownedItemActionDelg);
                    });
            });
        }*/

        public override void GameExited(Game game)
        {
            UpKeepFinished.Reset();
        }
    }
}
