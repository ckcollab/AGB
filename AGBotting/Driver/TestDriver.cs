using System;
using System.Collections.Generic;
using System.IO;

using AGB;
using AGB.D2;

using D2Packets;

namespace AGB.D2.Modules
{
    public class TestDriver : Module
    {
        private DriverConfig Config;

        public TestDriver()
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

            Game.JoinTCPGame("129.101.27.227", "HammersForLyfe.d2s", "HammersForLyfe", D2Data.CharacterClass.Paladin);
            //Game.JoinTCPGame("192.168.0.101", "KSH-BurnInHell.d2s", "KSH-BurnInHell", D2Data.CharacterClass.Sorceress);
        }
    }
}