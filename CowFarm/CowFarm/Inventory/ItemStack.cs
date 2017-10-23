using CowFarm.Entities;

namespace CowFarm.Inventory
{
    public class ItemStack
    {
        public Item Item { get; set; }
        public int ItemsCount { get; set; }
        public int MaxCount { get; set; }

        public ItemStack(Item item, int itemsCount, int maxCount)
        {
            Item = item;
            ItemsCount = itemsCount;
            MaxCount = maxCount;
        }

        public bool IsFull()
        {
            return ItemsCount == MaxCount;
        }
    }
}