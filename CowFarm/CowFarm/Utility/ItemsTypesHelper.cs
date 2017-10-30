using System;
using CowFarm.Entities;
using CowFarm.Entities.Items;
using CowFarm.Entities.Items.Craftables;

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
            ItemType[2] = typeof(CutGrass);
            ItemType[3] = typeof(Rope);

            return ItemType;
        }
    }
}