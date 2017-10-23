using System;
using CowFarm.Entities;
using CowFarm.Entities.Items;

namespace CowFarm.Utility
{
    public static class ItemsTypesHelper
    {
        public static Type[] ItemType;

        public static Type[] GetItemsTypes()
        {
            if (ItemType != null)
            {
                return ItemType;
            }
            ItemType = new Type[9];

            ItemType[0] = typeof(Apple);
            ItemType[1] = typeof(Rocks);
            return ItemType;
        }
    }
}