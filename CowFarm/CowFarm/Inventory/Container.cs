using System;
using System.Collections.Generic;
using System.Diagnostics;
using CowFarm.Entities;
using CowFarm.ScreenSystem;
using CowFarm.Utility;
using CowFarm.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CowFarm.Inventory
{
    public abstract class Container
    {
        public Item Item { get; protected set; }
        public int ItemsCount { get; protected set; }
        public int MaxCount { get; protected set; }
        
                
        public void Swap(Container container)
        {
            Item = container.Item;
            ItemsCount = container.ItemsCount;
            if (container.Item is Apple)
            {
                MaxCount = 3;
            }
        }

        public bool IsFull()
        {
            return ItemsCount == MaxCount;
        }
        public bool IsEmpty()
        {
            return Item == null;
        }

        public bool PossibleToAdd(Item item)
        {
            if (Item == null)
                return false;
            return Item.ItemId == item.ItemId && ItemsCount < MaxCount;
        }

        public void Add(Item item)
        {
            if (item is Apple)
                MaxCount = 3;
            Item = item;
            ItemsCount++;
            item.Pick();
        }

        public void Add(int count)
        {
            ItemsCount += count;
        }

        public void Remove(int count)
        {
            ItemsCount -= count;
            if (ItemsCount == 0)
                Item = null;
        }

        public abstract void Draw(SpriteBatch spriteBatch, SpriteFont font);

        
    }
}