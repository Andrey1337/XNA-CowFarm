﻿using System;
using CowFarm.Inventory;
using CowFarm.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CowFarm.Containers
{
    public class SwapContainer : Container
    {
        public new void Swap(Container container)
        {
            var ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.LeftControl))
            {
                if (ItemStack.IsEmpty())
                {
                    int count = Convert.ToInt32((double)container.ItemsCount / 2 + 0.1);
                    for (int i = 0; i < count; i++)
                    {
                        ItemStack.Add(container.Item);
                        container.Remove();
                    }
                    return;
                }

                if ((container.ItemStack.IsEmpty() || container.Item.ItemId == Item.ItemId) && !container.ItemStack.IsFull())
                {
                    container.ItemStack.Add(ItemStack.Item);
                    ItemStack.Remove();
                }
                return;
            }

            if (container.Item == null || Item == null || container.Item.ItemId != Item.ItemId || container.ItemStack.IsFull())
            {
                container.Swap(this);
                return;
            }

            var itemsCount = ItemsCount;
            for (var i = 0; i < itemsCount && !container.ItemStack.IsFull(); i++)
            {
                Remove();
                container.Add();
            }
        }

        

        public override void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            if (Item == null || ItemsCount == 0)
                return;

            var mouseState = Mouse.GetState();
            var mousePoint = new Vector2(mouseState.X, mouseState.Y);
            var rect = new Rectangle((int)mousePoint.X - 20, (int)mousePoint.Y - 20, 40, 40);
            spriteBatch.Draw(Item.IconTexture, rect, Color.White);

            if (ItemsCount > 1)
            {
                TextDrawHeleper.DrawText(spriteBatch, font, ItemsCount.ToString(), Color.Black, Color.White, 1,
                    new Vector2(rect.X + 26, rect.Y + 19));
            }
        }


        public SwapContainer(Rectangle position) : base(Rectangle.Empty)
        {
        }
    }
}