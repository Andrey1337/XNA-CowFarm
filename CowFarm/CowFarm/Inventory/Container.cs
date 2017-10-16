using System.Collections.Generic;
using CowFarm.Entities;
using CowFarm.Worlds;
using Microsoft.Xna.Framework;

namespace CowFarm.Inventory
{
    public class Container
    {
        public Item Item;
        public int ItemsCount;
        private int _maxCount;

        public Container(Item item)
        {
            Item = item;
            if (item is Apple)
                _maxCount = 3;
        }

        public bool PossibleToAdd(string itemName)
        {
            return Item.BodyTypeName == itemName && ItemsCount < _maxCount;
        }

        public void Add()
        {
            ItemsCount++;
        }

        public void Drop(World world, Vector2 position)
        {
            Item.Drop(world, position);
            ItemsCount--;
        }
    }
}