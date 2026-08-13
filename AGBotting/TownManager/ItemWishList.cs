using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AGB.D2.Modules
{
    public class WishListItem
    {
        public D2Data.ItemClass ItemClass = D2Data.ItemClass.None;
        public int Count = 0;
        public WishListItem(D2Data.ItemClass itemClass, int count)
        {
            ItemClass = itemClass;
            Count = count;
        }
    }
}
