using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using D2Data;

namespace AGB.D2.Modules
{
    public class Town
    {
        public AreaLevel AreaLevel;

        public NPCClass Healer;
        public NPCClass Repairer;
        public NPCClass PortalSeller;
        public NPCClass MercenaryReviver;

        /// <summary>
        /// Closest preset unit to where Tps are thrown up
        /// </summary>
        public GameObjectClass TownPortalArea; 
    }
}
