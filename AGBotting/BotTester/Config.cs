using System;
using System.Collections.Generic;
using System.IO;

using AGB;
using AGB.D2;
using AGB.Net;

namespace BotTester
{
    public class BotTesterConfig
    {
        public string AgbUsername;
        public string AgbPassword;

        public List<Profile> Profiles;

        public List<CdKeySet> CdKeys = new List<CdKeySet>();
        public List<Proxy> Proxies = new List<Proxy>();

        public BotTesterConfig()
        {
        }

        public BotTesterConfig(string fileName)
        {
            //string configString = AGB.Util.FileRead(fileName);

            if (File.Exists(fileName))
            {
                BotTesterConfig config = Util.XmlDeserialize<BotTesterConfig>(fileName);

                if (config != null)
                {
                    AgbUsername = config.AgbUsername;
                    AgbPassword = config.AgbPassword;

                    Profiles = config.Profiles;

                    CdKeys = config.CdKeys;
                    Proxies = config.Proxies;
                }
            }
        }

        public void Save(string fileName)
        {
            AGB.Util.XmlSerialize<BotTesterConfig>(this, fileName);
        }
    }
}
