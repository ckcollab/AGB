using System;
using System.Collections.Generic;
using System.IO;

using D2Data;
using AGB.D2;

namespace AGB.D2.Modules
{
    public class PickItCategory
    {
        public string Name;

        public List<PickItRequirement> Items;

        public PickItCategory()
        {

        }
    }

    public class PickItConfig
    {
        public List<PickItCategory> Categories;

        public PickItConfig()
        {
            Categories = new List<PickItCategory>();
        }

        public PickItConfig(string fileName)
        {
            /* Adding an example category

            Categories = new List<PickItCategory>();

            PickItCategory category = new PickItCategory();
            category.Name = "Super awesome uniques";
            category.Items = new List<PickItRequirement>();

            PickItRequirement coa = new PickItRequirement(PickItResult.Keep, "Sockets == 2 && Defense >= 200", null);
            coa.StatOps = null;
            coa.ModOps = null;
            coa.Description = "CoA";
            coa.ItemClass = ItemClass.Corona;
            coa.ItemType = ItemType.Helm;
            coa.Quality = ItemQuality.Unique;

            category.Items.Add(coa);

            Categories.Add(category);

            Save(fileName);
             */

            if (File.Exists(fileName))
            {
                PickItConfig config = Util.XmlDeserialize<PickItConfig>(fileName);

                if (config != null)
                {
                    Categories = config.Categories;
                }
            }
        }

        public void Save(string fileName)
        {
            AGB.Util.XmlSerialize<PickItConfig>(this, fileName);
        }
    }
}
