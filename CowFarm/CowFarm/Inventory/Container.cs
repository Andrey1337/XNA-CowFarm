using System.Collections.Generic;
using CowFarm.Entities;

namespace CowFarm.Inventory
{
    public class Container
    {
        public Item Item;
        private int _itemsCount;
        private int _maxCount;

        public Container(Item item)
        {
            Item = item;

            if (item is Apple)
                _maxCount = 3;
        }

        public bool PossibleToAdd(string itemName)
        {
            return Item.BodyTypeName == itemName && _itemsCount <= _maxCount;
        }

        public void Add()
        {
            _itemsCount++;
        }
    }
}