using System;
using CowFarm.Entities;

namespace CowFarm.Inventory
{
    public class ItemStack
    {
        public Item Item { get; set; }
        public int ItemsCount { get; set; }
        public int MaxCount { get; set; }

        public ItemStack()
        {
            MaxCount = 3;
        }

        public bool IsFull()
        {
            return ItemsCount == MaxCount;
        }

        public bool IsEmpty()
        {
            return Item == null;
        }

        public void Add(Item item = null)
        {
            if (item != null)
                Item = item;
            ItemsCount++;
        }

        public void Remove()
        {
            if (ItemsCount <= 0)
                throw new Exception("Items count minus");
            ItemsCount--;
            if (ItemsCount == 0)
                Item = null;
        }
    }
}