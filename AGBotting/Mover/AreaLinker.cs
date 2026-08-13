using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;

using System.Runtime.InteropServices;

using D2Data;

using AGB;
using AGB.D2;
using AGB.D2.Net;
using AGB.D2.Net.Packets;

namespace AGB.D2.Modules
{
    public delegate bool AreaLinkRequirement(List<WaypointDestination> availableWaypoints, Dictionary<QuestType, QuestStanding> quests);

    public class AreaLink
    {
        private AreaLinkRequirement Requirement;

        public AreaLevel AreaLevel;
        public int[] Exits;

        /// <summary>
        /// If applicable, the Id of a portal instead of an exit
        /// </summary>
        public int PortalId;

        public AreaLink(AreaLevel areaLevel)
        {
            AreaLevel = areaLevel;
        }

        public AreaLink(AreaLevel areaLevel, int[] exits)
            : this(areaLevel)
        {
            Exits = exits;
        }

        public AreaLink(AreaLevel areaLevel, int portalId, AreaLinkRequirement requirement)
            : this(areaLevel)
        {
            PortalId = portalId;
            Requirement = requirement;
        }

        public bool IsActive(List<WaypointDestination> availableWaypoints, Dictionary<QuestType, QuestStanding> quests)
        {
            if (Requirement != null)
                return Requirement(availableWaypoints, quests);

            // No requirement, this link is always available
            return true;
        }
    }

    internal class AreaDefinition
    {
        public AreaLevel AreaLevel;
        public AreaLink[] Links;

        public bool HasWaypoint;

        public AreaDefinition(AreaLevel areaLevel, AreaLink[] links)
        {
            AreaLevel = areaLevel;
            Links = links;
        }

        public AreaDefinition(AreaLevel areaLevel, AreaLink[] links, bool hasWaypoint)
            : this(areaLevel, links)
        {
            HasWaypoint = hasWaypoint;
        }

        public AreaLink FindLinkTo(AreaLevel level, List<WaypointDestination> availableWaypoints, Dictionary<QuestType, QuestStanding> quests)
        {
            foreach (AreaLink link in Links)
                if (link.AreaLevel == level && link.IsActive(availableWaypoints, quests))
                    return link;

            return null;
        }
    }

    internal class AreaNode : IComparable<AreaNode>
    {
        public AreaLevel AreaLevel;

        public AreaNode Parent;

        public int Score;

        public bool IsOpen = false;

        public AreaNode(AreaLevel areaLevel, int score)
        {
            AreaLevel = areaLevel;
            Score = score;
        }

        public int CompareTo(AreaNode node)
        {
            if (Score < node.Score) 
                return -1;
            else if (Score == node.Score) 
                return 0;
            else 
                return 1;
        }
    }

    public class AreaLinker
    {
        private static AreaDefinition[] Areas = new AreaDefinition[140];

        static AreaLinker()
        {
            Areas[(int)AreaLevel.None] = null;

            #region Act 1
            Areas[(int)AreaLevel.RogueEncampment] = new AreaDefinition(AreaLevel.RogueEncampment, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.BloodMoor),
                    new AreaLink(AreaLevel.MooMooFarm) // Need to add portal stuff
                }, true); // Has WayPoint
            Areas[(int)AreaLevel.BloodMoor] = new AreaDefinition(AreaLevel.BloodMoor, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.RogueEncampment),
                    new AreaLink(AreaLevel.DenOfEvil, new int[] {0, 1, 2, 3}),
                    new AreaLink(AreaLevel.ColdPlains)
                });
            Areas[(int)AreaLevel.ColdPlains] = new AreaDefinition(AreaLevel.ColdPlains, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.BloodMoor),
                    new AreaLink(AreaLevel.BurialGrounds),
                    new AreaLink(AreaLevel.CaveLevel1, new int [] {0, 1, 2, 3}),
                    new AreaLink(AreaLevel.StonyField)
                }, true); // Has WayPoint
            Areas[(int)AreaLevel.StonyField] = new AreaDefinition(AreaLevel.StonyField, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.ColdPlains),
                    new AreaLink(AreaLevel.Tristram), // Need to add portal stuff
                    new AreaLink(AreaLevel.UndergroundPassageLevel1, new int [] {0, 1, 2, 3})
                }, true); // Has WayPoint
            Areas[(int)AreaLevel.DarkWood] = new AreaDefinition(AreaLevel.DarkWood, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.UndergroundPassageLevel1, new int [] {0, 1, 2, 3}),
                    new AreaLink(AreaLevel.BlackMarsh)
                }, true); // Has WayPoint
            Areas[(int)AreaLevel.BlackMarsh] = new AreaDefinition(AreaLevel.BlackMarsh, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.DarkWood),
                    new AreaLink(AreaLevel.TamoeHighland),
                    new AreaLink(AreaLevel.HoleLevel1, new int [] {0, 1, 2, 3}),
                    new AreaLink(AreaLevel.ForgottenTower, new int [] {10})
                }, true); // Has WayPoint
            Areas[(int)AreaLevel.TamoeHighland] = new AreaDefinition(AreaLevel.TamoeHighland, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.BlackMarsh),
                    new AreaLink(AreaLevel.MonasteryGate),
                    new AreaLink(AreaLevel.PitLevel1, new int [] {0, 1, 2, 3})
                });
            Areas[(int)AreaLevel.DenOfEvil] = new AreaDefinition(AreaLevel.DenOfEvil, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.BloodMoor, new int [] {4})
                });
            Areas[(int)AreaLevel.CaveLevel1] = new AreaDefinition(AreaLevel.CaveLevel1, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.ColdPlains, new int [] {4}),
                    new AreaLink(AreaLevel.CaveLevel2, new int [] {5})
                });
            Areas[(int)AreaLevel.UndergroundPassageLevel1] = new AreaDefinition(AreaLevel.UndergroundPassageLevel1, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.StonyField, new int [] {3}),
                    new AreaLink(AreaLevel.UndergroundPassageLevel2, new int [] {5}),
                    new AreaLink(AreaLevel.DarkWood, new int [] {3})
                });
            Areas[(int)AreaLevel.HoleLevel1] = new AreaDefinition(AreaLevel.HoleLevel1, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.BlackMarsh, new int [] {4}),
                    new AreaLink(AreaLevel.HoleLevel2, new int [] {5})
                });
            Areas[(int)AreaLevel.PitLevel1] = new AreaDefinition(AreaLevel.PitLevel1, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.TamoeHighland, new int [] {4}),
                    new AreaLink(AreaLevel.PitLevel2, new int [] {5})
                });
            Areas[(int)AreaLevel.CaveLevel2] = new AreaDefinition(AreaLevel.CaveLevel2, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.CaveLevel1, new int [] {4})
                });
            Areas[(int)AreaLevel.UndergroundPassageLevel2] = new AreaDefinition(AreaLevel.UndergroundPassageLevel2, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.UndergroundPassageLevel1, new int [] {4})
                });
            Areas[(int)AreaLevel.HoleLevel2] = new AreaDefinition(AreaLevel.HoleLevel2, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.HoleLevel1, new int [] {4})
                });
            Areas[(int)AreaLevel.PitLevel2] = new AreaDefinition(AreaLevel.PitLevel2, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.PitLevel1, new int [] {4})
                });
            Areas[(int)AreaLevel.BurialGrounds] = new AreaDefinition(AreaLevel.BurialGrounds, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.ColdPlains),
                    new AreaLink(AreaLevel.Crypt, new int [] {6}),
                    new AreaLink(AreaLevel.Mausoleum, new int [] {7})
                });
            Areas[(int)AreaLevel.Crypt] = new AreaDefinition(AreaLevel.Crypt, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.BurialGrounds, new int [] {8})
                });
            Areas[(int)AreaLevel.Mausoleum] = new AreaDefinition(AreaLevel.Mausoleum, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.BurialGrounds, new int [] {8})
                });
            Areas[(int)AreaLevel.ForgottenTower] = new AreaDefinition(AreaLevel.ForgottenTower, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.BlackMarsh, new int [] {11}),
                    new AreaLink(AreaLevel.TowerCellarLevel1, new int [] {12})
                });
            Areas[(int)AreaLevel.TowerCellarLevel1] = new AreaDefinition(AreaLevel.TowerCellarLevel1, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.ForgottenTower, new int [] {8}),
                    new AreaLink(AreaLevel.TowerCellarLevel2, new int [] {9})
                });
            Areas[(int)AreaLevel.TowerCellarLevel2] = new AreaDefinition(AreaLevel.TowerCellarLevel2, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.TowerCellarLevel1, new int [] {8}),
                    new AreaLink(AreaLevel.TowerCellarLevel3, new int [] {9})
                });
            Areas[(int)AreaLevel.TowerCellarLevel3] = new AreaDefinition(AreaLevel.TowerCellarLevel3, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.TowerCellarLevel2, new int [] {8}),
                    new AreaLink(AreaLevel.TowerCellarLevel4, new int [] {9})
                });
            Areas[(int)AreaLevel.TowerCellarLevel4] = new AreaDefinition(AreaLevel.TowerCellarLevel4, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.TowerCellarLevel3, new int [] {8}),
                    new AreaLink(AreaLevel.TowerCellarLevel5, new int [] {9})
                });
            Areas[(int)AreaLevel.TowerCellarLevel5] = new AreaDefinition(AreaLevel.TowerCellarLevel5, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.TowerCellarLevel4, new int [] {8})
                });
            Areas[(int)AreaLevel.MonasteryGate] = new AreaDefinition(AreaLevel.MonasteryGate, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.TamoeHighland),
                    new AreaLink(AreaLevel.OuterCloister)
                });
            Areas[(int)AreaLevel.OuterCloister] = new AreaDefinition(AreaLevel.OuterCloister, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.MonasteryGate),
                    new AreaLink(AreaLevel.Barracks)
                }, true); // Has waypoint
            Areas[(int)AreaLevel.Barracks] = new AreaDefinition(AreaLevel.Barracks, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.OuterCloister),
                    new AreaLink(AreaLevel.JailLevel1, new int[] {14})
                });
            Areas[(int)AreaLevel.JailLevel1] = new AreaDefinition(AreaLevel.JailLevel1, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.Barracks, new int[] {13}),
                    new AreaLink(AreaLevel.JailLevel2, new int[] {14})
                }, true); // Has waypoint
            Areas[(int)AreaLevel.JailLevel2] = new AreaDefinition(AreaLevel.JailLevel2, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.JailLevel1, new int[] {13}),
                    new AreaLink(AreaLevel.JailLevel3, new int[] {14})
                });
            Areas[(int)AreaLevel.JailLevel3] = new AreaDefinition(AreaLevel.JailLevel3, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.JailLevel2, new int[] {13}),
                    new AreaLink(AreaLevel.InnerCloister, new int[] {13}) // double check this bullshit, wtf?  13 and 13?  how do i tell them apart?
                });
            Areas[(int)AreaLevel.InnerCloister] = new AreaDefinition(AreaLevel.InnerCloister, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.JailLevel3, new int[] {14}),
                    new AreaLink(AreaLevel.Cathedral)
                }, true); // Has waypoint
            Areas[(int)AreaLevel.Cathedral] = new AreaDefinition(AreaLevel.Cathedral, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.InnerCloister),
                    new AreaLink(AreaLevel.CatacombsLevel1, new int[] {15})
                });
            Areas[(int)AreaLevel.CatacombsLevel1] = new AreaDefinition(AreaLevel.CatacombsLevel1, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.Cathedral, new int[] {16}),
                    new AreaLink(AreaLevel.CatacombsLevel2, new int[] {18})
                });
            Areas[(int)AreaLevel.CatacombsLevel2] = new AreaDefinition(AreaLevel.CatacombsLevel2, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.CatacombsLevel1, new int[] {17}),
                    new AreaLink(AreaLevel.CatacombsLevel3, new int[] {18})
                }, true); // Has waypoint
            Areas[(int)AreaLevel.CatacombsLevel3] = new AreaDefinition(AreaLevel.CatacombsLevel3, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.CatacombsLevel2, new int[] {17}),
                    new AreaLink(AreaLevel.CatacombsLevel4, new int[] {18})
                });
            Areas[(int)AreaLevel.CatacombsLevel4] = new AreaDefinition(AreaLevel.CatacombsLevel4, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.CatacombsLevel3, new int[] {17})
                });
            Areas[(int)AreaLevel.Tristram] = new AreaDefinition(AreaLevel.Tristram, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.StonyField) // portal shit
                });
            Areas[(int)AreaLevel.MooMooFarm] = new AreaDefinition(AreaLevel.MooMooFarm, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.RogueEncampment) // portal shit
                });
            #endregion
            #region Act 2
            Areas[(int)AreaLevel.LutGholein] = new AreaDefinition(AreaLevel.LutGholein, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.SewersLevel1Act2, new int[] {20}),
                    new AreaLink(AreaLevel.HaremLevel1, new int[] {24}),
                    new AreaLink(AreaLevel.RockyWaste)
                }, true); // Has waypoint
            Areas[(int)AreaLevel.RockyWaste] = new AreaDefinition(AreaLevel.RockyWaste, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.LutGholein),
                    new AreaLink(AreaLevel.StonyTombLevel1, new int [] {33, 34, 35, 36}),
                    new AreaLink(AreaLevel.DryHills)
                });
            Areas[(int)AreaLevel.DryHills] = new AreaDefinition(AreaLevel.DryHills, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.RockyWaste),
                    new AreaLink(AreaLevel.HallsOfTheDeadLevel1, new int [] {33, 34, 35, 36}),
                    new AreaLink(AreaLevel.FarOasis)
                }, true); // Has waypoint
            Areas[(int)AreaLevel.FarOasis] = new AreaDefinition(AreaLevel.FarOasis, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.DryHills),
                    new AreaLink(AreaLevel.MaggotLairLevel1, new int [] {47}),
                    new AreaLink(AreaLevel.LostCity)
                }, true); // Has waypoint
            Areas[(int)AreaLevel.LostCity] = new AreaDefinition(AreaLevel.LostCity, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.FarOasis),
                    new AreaLink(AreaLevel.AncientTunnels, new int [] {50}),
                    new AreaLink(AreaLevel.ValleyOfSnakes)
                }, true); // Has waypoint
            Areas[(int)AreaLevel.ValleyOfSnakes] = new AreaDefinition(AreaLevel.ValleyOfSnakes, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.LostCity),
                    new AreaLink(AreaLevel.Unknown60, new int [] {37}) // Unknown60 == claw viper 1
                });
            Areas[(int)AreaLevel.CanyonOfTheMagi] = new AreaDefinition(AreaLevel.CanyonOfTheMagi, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.TalRashasTomb1, new int [] {38}),
                    new AreaLink(AreaLevel.TalRashasTomb2, new int [] {39}),
                    new AreaLink(AreaLevel.TalRashasTomb3, new int [] {40}),
                    new AreaLink(AreaLevel.TalRashasTomb4, new int [] {41}),
                    new AreaLink(AreaLevel.TalRashasTomb5, new int [] {42}),
                    new AreaLink(AreaLevel.TalRashasTomb6, new int [] {43}),
                    new AreaLink(AreaLevel.TalRashasTomb7, new int [] {44})
                }, true); // Has waypoint
            Areas[(int)AreaLevel.SewersLevel1Act2] = new AreaDefinition(AreaLevel.SewersLevel1Act2, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.LutGholein, new int [] {21}),
                    new AreaLink(AreaLevel.SewersLevel2Act2, new int [] {23})
                });
            Areas[(int)AreaLevel.SewersLevel2Act2] = new AreaDefinition(AreaLevel.SewersLevel2Act2, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.SewersLevel1Act2, new int [] {22}),
                    new AreaLink(AreaLevel.SewersLevel3Act2, new int [] {23})
                }, true); // Has waypoint
            Areas[(int)AreaLevel.SewersLevel3Act2] = new AreaDefinition(AreaLevel.SewersLevel3Act2, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.SewersLevel2Act2, new int [] {22})
                });
            Areas[(int)AreaLevel.HaremLevel1] = new AreaDefinition(AreaLevel.HaremLevel1, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.LutGholein, new int [] {25}),
                    new AreaLink(AreaLevel.HaremLevel2, new int [] {28})
                });
            Areas[(int)AreaLevel.HaremLevel2] = new AreaDefinition(AreaLevel.HaremLevel2, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.HaremLevel1, new int [] {26}),
                    new AreaLink(AreaLevel.PalaceCellarLevel1, new int [] {28})
                });
            Areas[(int)AreaLevel.PalaceCellarLevel1] = new AreaDefinition(AreaLevel.PalaceCellarLevel1, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.HaremLevel2, new int [] {26}),
                    new AreaLink(AreaLevel.PalaceCellarLevel2, new int [] {32})
                }, true); // Has waypoint
            Areas[(int)AreaLevel.PalaceCellarLevel2] = new AreaDefinition(AreaLevel.PalaceCellarLevel2, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.PalaceCellarLevel1, new int [] {31}),
                    new AreaLink(AreaLevel.PalaceCellarLevel3, new int [] {32})
                });
            Areas[(int)AreaLevel.PalaceCellarLevel3] = new AreaDefinition(AreaLevel.PalaceCellarLevel3, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.PalaceCellarLevel2, new int [] {31}),
                    new AreaLink(AreaLevel.ArcaneSanctuary) // portal shit
                });
            Areas[(int)AreaLevel.StonyTombLevel1] = new AreaDefinition(AreaLevel.StonyTombLevel1, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.RockyWaste, new int [] {45}),
                    new AreaLink(AreaLevel.StonyTombLevel2, new int [] {46})
                });
            Areas[(int)AreaLevel.StonyTombLevel2] = new AreaDefinition(AreaLevel.StonyTombLevel2, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.StonyTombLevel1, new int [] {45})
                });
            Areas[(int)AreaLevel.HallsOfTheDeadLevel1] = new AreaDefinition(AreaLevel.HallsOfTheDeadLevel1, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.DryHills, new int [] {45}),
                    new AreaLink(AreaLevel.HallsOfTheDeadLevel2, new int [] {46})
                });
            Areas[(int)AreaLevel.HallsOfTheDeadLevel2] = new AreaDefinition(AreaLevel.HallsOfTheDeadLevel2, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.HallsOfTheDeadLevel1, new int [] {45}),
                    new AreaLink(AreaLevel.HallsOfTheDeadLevel3, new int [] {46})
                }, true); // Has waypoint
            Areas[(int)AreaLevel.HallsOfTheDeadLevel3] = new AreaDefinition(AreaLevel.HallsOfTheDeadLevel3, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.HallsOfTheDeadLevel2, new int [] {45})
                });
            Areas[(int)AreaLevel.ClawViperTempleLevel2] = new AreaDefinition(AreaLevel.ClawViperTempleLevel2, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.ValleyOfSnakes, new int [] {45}),
                    new AreaLink(AreaLevel.Unknown60, new int [] {46})
                });
            Areas[(int)AreaLevel.MaggotLairLevel1] = new AreaDefinition(AreaLevel.MaggotLairLevel1, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.FarOasis, new int [] {48}),
                    new AreaLink(AreaLevel.MaggotLairLevel2, new int [] {49})
                });
            Areas[(int)AreaLevel.MaggotLairLevel2] = new AreaDefinition(AreaLevel.MaggotLairLevel2, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.MaggotLairLevel1, new int [] {48}),
                    new AreaLink(AreaLevel.MaggotLairLevel3, new int [] {49})
                });
            Areas[(int)AreaLevel.MaggotLairLevel3] = new AreaDefinition(AreaLevel.MaggotLairLevel3, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.MaggotLairLevel2, new int [] {48})
                });
            Areas[(int)AreaLevel.AncientTunnels] = new AreaDefinition(AreaLevel.AncientTunnels, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.LostCity, new int [] {22})
                });
            Areas[(int)AreaLevel.TalRashasTomb1] = new AreaDefinition(AreaLevel.TalRashasTomb1, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.CanyonOfTheMagi, new int [] {45}),
                    new AreaLink(AreaLevel.DurielsLair, new int [] {100})
                });
            Areas[(int)AreaLevel.TalRashasTomb1] = new AreaDefinition(AreaLevel.TalRashasTomb1, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.CanyonOfTheMagi, new int [] {45}),
                    new AreaLink(AreaLevel.DurielsLair, new int [] {100})
                });
            Areas[(int)AreaLevel.TalRashasTomb2] = new AreaDefinition(AreaLevel.TalRashasTomb2, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.CanyonOfTheMagi, new int [] {45}),
                    new AreaLink(AreaLevel.DurielsLair, new int [] {100})
                });
            Areas[(int)AreaLevel.TalRashasTomb3] = new AreaDefinition(AreaLevel.TalRashasTomb3, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.CanyonOfTheMagi, new int [] {45}),
                    new AreaLink(AreaLevel.DurielsLair, new int [] {100})
                });
            Areas[(int)AreaLevel.TalRashasTomb4] = new AreaDefinition(AreaLevel.TalRashasTomb4, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.CanyonOfTheMagi, new int [] {45}),
                    new AreaLink(AreaLevel.DurielsLair, new int [] {100})
                });
            Areas[(int)AreaLevel.TalRashasTomb5] = new AreaDefinition(AreaLevel.TalRashasTomb5, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.CanyonOfTheMagi, new int [] {45}),
                    new AreaLink(AreaLevel.DurielsLair, new int [] {100})
                });
            Areas[(int)AreaLevel.TalRashasTomb6] = new AreaDefinition(AreaLevel.TalRashasTomb6, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.CanyonOfTheMagi, new int [] {45}),
                    new AreaLink(AreaLevel.DurielsLair, new int [] {100})
                });
            Areas[(int)AreaLevel.TalRashasTomb7] = new AreaDefinition(AreaLevel.TalRashasTomb7, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.CanyonOfTheMagi, new int [] {45}),
                    new AreaLink(AreaLevel.DurielsLair, new int [] {100})
                });
            Areas[(int)AreaLevel.DurielsLair] = new AreaDefinition(AreaLevel.DurielsLair, new AreaLink[] 
                {
                });
            Areas[(int)AreaLevel.ArcaneSanctuary] = new AreaDefinition(AreaLevel.ArcaneSanctuary, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.CanyonOfTheMagi, new int [] {45}), // portal shit
                    new AreaLink(AreaLevel.PalaceCellarLevel3, new int [] {100}) // portal shit
                }, true); // Has waypoint
            #endregion
            #region Act 3
            Areas[(int)AreaLevel.KurastDocks] = new AreaDefinition(AreaLevel.KurastDocks, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.SpiderForest)
                }, true); // Has waypoint
            Areas[(int)AreaLevel.SpiderForest] = new AreaDefinition(AreaLevel.SpiderForest, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.KurastDocks),
                    new AreaLink(AreaLevel.GreatMarsh),
                    new AreaLink(AreaLevel.SpiderCavern, new int [] {51}),
                    new AreaLink(AreaLevel.SpiderCave, new int [] {51})
                }, true); // Has waypoint
            Areas[(int)AreaLevel.GreatMarsh] = new AreaDefinition(AreaLevel.GreatMarsh, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.SpiderForest),
                    new AreaLink(AreaLevel.FlayerJungle)
                }, true); // Has waypoint
            Areas[(int)AreaLevel.FlayerJungle] = new AreaDefinition(AreaLevel.FlayerJungle, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.GreatMarsh),
                    new AreaLink(AreaLevel.SwampyPitLevel1, new int [] {53}),
                    new AreaLink(AreaLevel.FlayerDungeonLevel1, new int [] {54}),
                    new AreaLink(AreaLevel.LowerKurast)
                }, true); // Has waypoint
            Areas[(int)AreaLevel.LowerKurast] = new AreaDefinition(AreaLevel.LowerKurast, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.FlayerJungle),
                    new AreaLink(AreaLevel.KurastBazaar)
                }, true); // Has waypoint
            Areas[(int)AreaLevel.KurastBazaar] = new AreaDefinition(AreaLevel.KurastBazaar, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.LowerKurast),
                    new AreaLink(AreaLevel.SewersLevel1Act3, new int [] {57}),
                    new AreaLink(AreaLevel.RuinedTemple, new int [] {61}),
                    new AreaLink(AreaLevel.DisusedFane, new int [] {61}),
                    new AreaLink(AreaLevel.UpperKurast)
                }, true); // Has waypoint
            Areas[(int)AreaLevel.UpperKurast] = new AreaDefinition(AreaLevel.UpperKurast, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.KurastBazaar),
                    new AreaLink(AreaLevel.SewersLevel1Act3, new int [] {57}),
                    new AreaLink(AreaLevel.ForgottenReliquary, new int [] {61}),
                    new AreaLink(AreaLevel.ForgottenTemple, new int [] {61}),
                    new AreaLink(AreaLevel.KurastCauseway)
                }, true); // Has waypoint
            Areas[(int)AreaLevel.KurastCauseway] = new AreaDefinition(AreaLevel.KurastCauseway, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.UpperKurast),
                    new AreaLink(AreaLevel.RuinedFane, new int [] {61}),
                    new AreaLink(AreaLevel.DisusedReliquary, new int [] {61}),
                    new AreaLink(AreaLevel.Travincal)
                });
            Areas[(int)AreaLevel.Travincal] = new AreaDefinition(AreaLevel.Travincal, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.KurastCauseway),
                    new AreaLink(AreaLevel.DuranceOfHateLevel1, new int [] {64})
                }, true); // Has waypoint
            Areas[(int)AreaLevel.SpiderCave] = new AreaDefinition(AreaLevel.SpiderCave, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.SpiderForest, new int [] {52})
                });
            Areas[(int)AreaLevel.SpiderCavern] = new AreaDefinition(AreaLevel.SpiderCavern, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.SpiderForest, new int [] {52})
                });
            Areas[(int)AreaLevel.SwampyPitLevel1] = new AreaDefinition(AreaLevel.SwampyPitLevel1, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.FlayerJungle, new int [] {55}),
                    new AreaLink(AreaLevel.SwampyPitLevel2, new int [] {56})
                });
            Areas[(int)AreaLevel.SwampyPitLevel2] = new AreaDefinition(AreaLevel.SwampyPitLevel2, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.SwampyPitLevel1, new int [] {55}),
                    new AreaLink(AreaLevel.SwampyPitLevel3, new int [] {56})
                });
            Areas[(int)AreaLevel.SwampyPitLevel3] = new AreaDefinition(AreaLevel.SwampyPitLevel3, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.SwampyPitLevel2, new int [] {55})
                });
            Areas[(int)AreaLevel.FlayerDungeonLevel1] = new AreaDefinition(AreaLevel.FlayerDungeonLevel1, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.FlayerJungle, new int [] {55}),
                    new AreaLink(AreaLevel.FlayerDungeonLevel2, new int [] {56})
                });
            Areas[(int)AreaLevel.FlayerDungeonLevel2] = new AreaDefinition(AreaLevel.FlayerDungeonLevel2, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.FlayerDungeonLevel1, new int [] {55}),
                    new AreaLink(AreaLevel.FlayerDungeonLevel3, new int [] {56})
                });
            Areas[(int)AreaLevel.FlayerDungeonLevel3] = new AreaDefinition(AreaLevel.FlayerDungeonLevel3, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.FlayerDungeonLevel2, new int [] {55})
                });
            Areas[(int)AreaLevel.SewersLevel1Act3] = new AreaDefinition(AreaLevel.SewersLevel1Act3, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.KurastBazaar, new int [] {59}),
                    new AreaLink(AreaLevel.UpperKurast, new int [] {59}),
                    new AreaLink(AreaLevel.SewersLevel2Act3, new int [] {60}),
                });
            Areas[(int)AreaLevel.SewersLevel2Act3] = new AreaDefinition(AreaLevel.SewersLevel2Act3, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.SewersLevel1Act3, new int [] {58})
                });
            Areas[(int)AreaLevel.RuinedTemple] = new AreaDefinition(AreaLevel.RuinedTemple, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.KurastBazaar, new int [] {62, 63})
                });
            Areas[(int)AreaLevel.DisusedFane] = new AreaDefinition(AreaLevel.DisusedFane, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.KurastBazaar, new int [] {62, 63})
                });
            Areas[(int)AreaLevel.ForgottenReliquary] = new AreaDefinition(AreaLevel.ForgottenReliquary, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.UpperKurast, new int [] {62, 63})
                });
            Areas[(int)AreaLevel.ForgottenTemple] = new AreaDefinition(AreaLevel.ForgottenTemple, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.UpperKurast, new int [] {62, 63})
                });
            Areas[(int)AreaLevel.RuinedFane] = new AreaDefinition(AreaLevel.RuinedFane, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.KurastCauseway, new int [] {62, 63})
                });
            Areas[(int)AreaLevel.DisusedReliquary] = new AreaDefinition(AreaLevel.DisusedReliquary, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.KurastCauseway, new int [] {62, 63})
                });
            Areas[(int)AreaLevel.DuranceOfHateLevel1] = new AreaDefinition(AreaLevel.DuranceOfHateLevel1, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.Travincal, new int [] {65, 66}),
                    new AreaLink(AreaLevel.DuranceOfHateLevel2, new int [] {67, 68})
                });
            Areas[(int)AreaLevel.DuranceOfHateLevel2] = new AreaDefinition(AreaLevel.DuranceOfHateLevel2, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.DuranceOfHateLevel1, new int [] {65, 66}),
                    new AreaLink(AreaLevel.DuranceOfHateLevel3, new int [] {67, 68})
                }, true); // Has waypoint
            Areas[(int)AreaLevel.DuranceOfHateLevel3] = new AreaDefinition(AreaLevel.DuranceOfHateLevel3, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.ThePandemoniumFortress, (int)GameObjectClass.HellGate,
                        new AreaLinkRequirement(
                            delegate(List<WaypointDestination> waypoints, Dictionary<QuestType, QuestStanding> quests)
                            {
                                return (quests[QuestType.TheBlackenedTemple] & QuestStanding.Complete) == QuestStanding.Complete;
                            })),
                    new AreaLink(AreaLevel.DuranceOfHateLevel2, new int [] {65, 66})
                });
            #endregion
            #region Act 4
            Areas[(int)AreaLevel.ThePandemoniumFortress] = new AreaDefinition(AreaLevel.ThePandemoniumFortress, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.OuterSteppes)
                }, true); // Has WayPoint
            Areas[(int)AreaLevel.OuterSteppes] = new AreaDefinition(AreaLevel.OuterSteppes, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.ThePandemoniumFortress),
                    new AreaLink(AreaLevel.PlainsOfDespair)
                });
            Areas[(int)AreaLevel.PlainsOfDespair] = new AreaDefinition(AreaLevel.PlainsOfDespair, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.OuterSteppes),
                    new AreaLink(AreaLevel.CityOfTheDamned)
                });
            Areas[(int)AreaLevel.CityOfTheDamned] = new AreaDefinition(AreaLevel.CityOfTheDamned, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.PlainsOfDespair),
                    new AreaLink(AreaLevel.RiverOfFlame, new int [] {69})
                }, true); // Has waypoint
            Areas[(int)AreaLevel.RiverOfFlame] = new AreaDefinition(AreaLevel.RiverOfFlame, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.CityOfTheDamned, new int [] {70}),
                    new AreaLink(AreaLevel.ChaosSanctuary)
                }, true); // Has waypoint
            Areas[(int)AreaLevel.ChaosSanctuary] = new AreaDefinition(AreaLevel.ChaosSanctuary, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.RiverOfFlame)
                });
            #endregion
            #region Act 5
            Areas[(int)AreaLevel.Harrogath] = new AreaDefinition(AreaLevel.Harrogath, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.BloodyFoothills),
                    new AreaLink(AreaLevel.NihlathaksTemple, 60, 
                        delegate(List<WaypointDestination> availableWaypoints, Dictionary<QuestType, QuestStanding> quests)
                        {
                            // Someone grabbed the waypoint, so it's not available any more!
                            // Also make sure quest has been completed
                            return !availableWaypoints.Contains(WaypointDestination.HallsOfPain) && (quests[QuestType.PrisonOfIce] & QuestStanding.Complete) == QuestStanding.Complete;
                        }) // Portal stuff!!
                }, true); // Has WayPoint
            Areas[(int)AreaLevel.BloodyFoothills] = new AreaDefinition(AreaLevel.BloodyFoothills, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.Harrogath),
                    new AreaLink(AreaLevel.FrigidHighlands)
                });
            Areas[(int)AreaLevel.FrigidHighlands] = new AreaDefinition(AreaLevel.FrigidHighlands, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.BloodyFoothills),
                    new AreaLink(AreaLevel.Abaddon),
                    new AreaLink(AreaLevel.ArreatPlateau)
                }, true); // Has waypoint
            Areas[(int)AreaLevel.ArreatPlateau] = new AreaDefinition(AreaLevel.ArreatPlateau, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.FrigidHighlands),
                    new AreaLink(AreaLevel.PitOfAcheron),
                    new AreaLink(AreaLevel.CrystallinePassage, new int [] {71})
                }, true); // Has waypoint
            Areas[(int)AreaLevel.CrystallinePassage] = new AreaDefinition(AreaLevel.CrystallinePassage, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.ArreatPlateau, new int [] {73}),
                    new AreaLink(AreaLevel.FrozenRiver, new int [] {74}),
                    new AreaLink(AreaLevel.GlacialTrail, new int [] {75})
                }, true); // Has waypoint
            Areas[(int)AreaLevel.FrozenRiver] = new AreaDefinition(AreaLevel.FrozenRiver, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.CrystallinePassage, new int [] {73})
                });
            Areas[(int)AreaLevel.GlacialTrail] = new AreaDefinition(AreaLevel.GlacialTrail, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.CrystallinePassage, new int [] {73}),
                    new AreaLink(AreaLevel.FrozenTundra, new int [] {74}),
                    new AreaLink(AreaLevel.DrifterCavern, new int [] {75})
                }, true); // Has waypoint
            Areas[(int)AreaLevel.DrifterCavern] = new AreaDefinition(AreaLevel.DrifterCavern, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.GlacialTrail, new int [] {73})
                });
            Areas[(int)AreaLevel.FrozenTundra] = new AreaDefinition(AreaLevel.FrozenTundra, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.GlacialTrail, new int [] {72}),
                    new AreaLink(AreaLevel.TheAncientsWay, new int [] {71}),
                    new AreaLink(AreaLevel.InfernalPit)
                });

            // Missing link to FrozenTundra?!?!
            Areas[(int)AreaLevel.TheAncientsWay] = new AreaDefinition(AreaLevel.TheAncientsWay, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.ArreatPlateau, new int [] {73}),
                    new AreaLink(AreaLevel.ArreatSummit, new int [] {74}),
                    new AreaLink(AreaLevel.IcyCellar, new int [] {75})
                }, true); // Has waypoint
            Areas[(int)AreaLevel.IcyCellar] = new AreaDefinition(AreaLevel.IcyCellar, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.TheAncientsWay, new int [] {73})
                });
            Areas[(int)AreaLevel.ArreatSummit] = new AreaDefinition(AreaLevel.ArreatSummit, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.TheAncientsWay, new int [] {79}),
                    new AreaLink(AreaLevel.TheWorldStoneKeepLevel1, new int [] {80})
                });
            Areas[(int)AreaLevel.NihlathaksTemple] = new AreaDefinition(AreaLevel.NihlathaksTemple, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.Harrogath, 60, 
                        delegate(List<WaypointDestination> availableWaypoints, Dictionary<QuestType, QuestStanding> quests)
                        {
                            // Someone grabbed the waypoint, so it's not available any more!
                            return !availableWaypoints.Contains(WaypointDestination.HallsOfPain);
                        }), // Portal stuff!!
                    new AreaLink(AreaLevel.HallsOfAnguish, new int [] {76})
                });
            Areas[(int)AreaLevel.HallsOfAnguish] = new AreaDefinition(AreaLevel.HallsOfAnguish, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.NihlathaksTemple, new int [] {78}),
                    new AreaLink(AreaLevel.HallsOfPain, new int [] {77}),
                });
            Areas[(int)AreaLevel.HallsOfAnguish] = new AreaDefinition(AreaLevel.HallsOfAnguish, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.NihlathaksTemple, new int [] {78}),
                    new AreaLink(AreaLevel.HallsOfPain, new int [] {77}),
                });
            Areas[(int)AreaLevel.HallsOfPain] = new AreaDefinition(AreaLevel.HallsOfPain, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.HallsOfAnguish, new int [] {78}),
                    new AreaLink(AreaLevel.HallsOfVaught, new int [] {77}),
                }, true); // Has waypoint
            Areas[(int)AreaLevel.HallsOfVaught] = new AreaDefinition(AreaLevel.HallsOfVaught, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.HallsOfPain, new int [] {78})
                });
            Areas[(int)AreaLevel.Abaddon] = new AreaDefinition(AreaLevel.Abaddon, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.FrigidHighlands)
                });
            Areas[(int)AreaLevel.PitOfAcheron] = new AreaDefinition(AreaLevel.PitOfAcheron, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.ArreatPlateau)
                });
            Areas[(int)AreaLevel.InfernalPit] = new AreaDefinition(AreaLevel.InfernalPit, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.FrozenTundra)
                });
            Areas[(int)AreaLevel.TheWorldStoneKeepLevel1] = new AreaDefinition(AreaLevel.TheWorldStoneKeepLevel1, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.ArreatSummit, new int[] {81}),
                    new AreaLink(AreaLevel.TheWorldStoneKeepLevel2, new int[] {82}),
                });
            Areas[(int)AreaLevel.TheWorldStoneKeepLevel2] = new AreaDefinition(AreaLevel.TheWorldStoneKeepLevel2, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.TheWorldStoneKeepLevel1, new int[] {81}),
                    new AreaLink(AreaLevel.TheWorldStoneKeepLevel3, new int[] {82}),
                }, true); // Has waypoint
            Areas[(int)AreaLevel.TheWorldStoneKeepLevel3] = new AreaDefinition(AreaLevel.TheWorldStoneKeepLevel3, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.TheWorldStoneKeepLevel2, new int[] {81}),
                    new AreaLink(AreaLevel.ThroneOfDestruction, new int[] {82}),
                });
            Areas[(int)AreaLevel.ThroneOfDestruction] = new AreaDefinition(AreaLevel.ThroneOfDestruction, new AreaLink[] 
                {
                    new AreaLink(AreaLevel.TheWorldStoneKeepLevel3, new int[] {81}),
                    new AreaLink(AreaLevel.TheWorldstoneChamber) // PPOOOOORTALS! portals
                });
            Areas[(int)AreaLevel.TheWorldstoneChamber] = new AreaDefinition(AreaLevel.TheWorldstoneChamber, new AreaLink[] 
                {
                });
            #endregion
        }

        /// <summary>
        /// Finds the closest Way Point to the destination area
        /// </summary>
        /// <param name="availableWaypoints">A list of all Way Points available to the Hero</param>
        /// <param name="destination"></param>
        /// <returns>A list of links containing the area and how to get there</returns>
        public static List<AreaLink> GetLinks(List<WaypointDestination> availableWaypoints, Dictionary<QuestType, QuestStanding> quests, AreaLevel start, AreaLevel destination)
        {
            var open = new AGB.Collections.PriorityQueueB<AreaNode>();
            var closed = new AreaNode[140];

            // Start the score at 0, iterate it each time we add neighbors
            int score = 0;

            // We actually start from the destination and then fan out
            open.Push(new AreaNode(destination, score));

            while (open.Count != 0)
            {
                AreaNode node = open.Pop();

                // Is this a waypoint and do we have it, return!  
                // (Or if this is the start area, return, we're not getting any closer)
                if ((Areas[(int)node.AreaLevel].HasWaypoint && availableWaypoints.Contains((WaypointDestination)node.AreaLevel)) || node.AreaLevel == start)
                {
                    List<AreaLink> path = new List<AreaLink>();

                    while (node != null)
                    {
                        AreaLink link = new AreaLink(node.AreaLevel);

                        if (node.Parent != null)
                        {
                            AreaLink nextLink = Areas[(int)node.AreaLevel].FindLinkTo(node.Parent.AreaLevel, availableWaypoints, quests);

                            // A link wasn't available (due to a quest or something), return null
                            if (nextLink == null)
                                return null;

                            // Since we don't want the AreaLink to include the NEXT level, let's make a
                            // new one using the current level and the same exit
                            link.Exits = nextLink.Exits;
                            link.PortalId = nextLink.PortalId;
                            link.AreaLevel = node.AreaLevel;
                        }

                        // Find out how the link really relates to the next area
                        path.Add(link);

                        node = node.Parent;
                    }



                    // add the destination
                    //path.Add(new AreaLink(destination));

                    return path;
                }

                // Increase distance score
                score++;

                // Add neighbors
                foreach (AreaLink neighbor in Areas[(int)node.AreaLevel].Links)
                {
                    if (!neighbor.IsActive(availableWaypoints, quests))
                        continue;

                    if (closed[(int)neighbor.AreaLevel] == null)
                    {
                        AreaNode newNode = new AreaNode(neighbor.AreaLevel, score);
                        newNode.Parent = node;
                        closed[(int)neighbor.AreaLevel] = newNode;
                    }

                    if (!closed[(int)neighbor.AreaLevel].IsOpen)
                    {
                        open.Push(closed[(int)neighbor.AreaLevel]);
                        closed[(int)neighbor.AreaLevel].IsOpen = true;
                    }
                }
            }

            // No link found, in this case, could it be a one-way-portal?
            // (Durance of Hate Level 3 to The Pandemonium Fortress!)
            AreaLink oneWayLink = Areas[(int)start].FindLinkTo(destination, availableWaypoints, quests);

            if (oneWayLink != null)
            {
                List<AreaLink> path = new List<AreaLink>();

                // Since we don't want the AreaLink to include the NEXT level, let's make a
                // new one using the current level and the same exit
                oneWayLink.AreaLevel = start;

                path.Add(oneWayLink);

                return path;
            }

            return null;
        }
    }
}