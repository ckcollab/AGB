using System;
using System.Collections.Generic;
using System.IO;

using AGB;
using AGB.Net;

using AGB.D2;
using AGB.D2.Net;

namespace AGB.D2.Modules
{
    public class DriverConfig
    {
        public string GameName;
        public string GamePassword;

        public DriverConfig()
        {
        }

        public DriverConfig(string fileName)
        {
            if (File.Exists(fileName))
            {
                DriverConfig config = Util.XmlDeserialize<DriverConfig>(fileName);

                if (config != null)
                {
                    GameName = config.GameName;
                    GamePassword = config.GamePassword;
                }
            }
        }

        public void Save(string fileName)
        {
            AGB.Util.XmlSerialize<DriverConfig>(this, fileName);
        }
    }
}
