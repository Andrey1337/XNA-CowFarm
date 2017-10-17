using System;
using CowFarm.Entities;

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
            return ItemType;
        }
    }
}