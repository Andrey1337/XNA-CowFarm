using System;
using System.Collections.Generic;
using CowFarm.Entities;
using CowFarm.ScreenSystem;
using CowFarm.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.Inventory
{
    public class Container
    {
        public int ItemId;
        public int ItemsCount;
        private readonly int _maxCount;
        public Texture2D IconTexture;

        public Container(Item item)
        {
            ItemId = item.ItemId;
            IconTexture = item.IconTexture;
            if (item is Apple)
                _maxCount = 1;
        }

        public bool PossibleToAdd(int itemId)
        {
            return ItemId == itemId && ItemsCount < _maxCount;
        }

        public void Add()
        {
            ItemsCount++;
        }

        public void Drop(World world, Vector2 position, Type[] itemsTypes, CowGameScreen cowGameScreen)
        {
            if (itemsTypes[ItemId] == null)
                throw new Exception("No itemId exists");

            object[] args = { cowGameScreen, world, position };
            Activator.CreateInstance(itemsTypes[ItemId], args);

            ItemsCount--;
        }
    }
}