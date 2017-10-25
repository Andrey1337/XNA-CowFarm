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
        public ItemStack ItemStack;
        public Item Item => ItemStack.Item;
        public int ItemsCount => ItemStack.ItemsCount;
        
    
        public void Swap(Container container)
        {
            ItemStack temp = ItemStack;
            ItemStack = container.ItemStack;
            container.ItemStack = temp;
        }

        protected Container()
        {
            ItemStack = new ItemStack();
        }       

        public bool PossibleToAdd(Item item)
        {
            if (Item == null)
                return false;
            return Item.ItemId == item.ItemId && ItemsCount < Item.StackCount;
        }

        public void Add(Item item = null)
        {
            ItemStack.Add(item);
            item?.Pick();
        }

        public void Remove()
        {
            ItemStack.ItemsCount--;
            if (ItemsCount == 0)
                ItemStack.Item = null;
        }

        public abstract void Draw(SpriteBatch spriteBatch, SpriteFont font);

    }
}