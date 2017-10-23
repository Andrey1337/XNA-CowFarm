using System;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters;
using CowFarm.Entities;
using CowFarm.Utility;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CowFarm.Inventory
{
    public class SwapContainer : Container
    {
        public SwapContainer() { }

        public new void Swap(Container container)
        {
            var ks = Keyboard.GetState();           

            if (container.Item == null || Item == null || container.Item.GetType() != Item.GetType() || container.IsFull())
            {
                Container temp = new SwapContainer();
                temp.Swap(this);
                base.Swap(container);
                container.Swap(temp);
                return;
            }

            int space = container.MaxCount - container.ItemsCount;
            if (ItemsCount <= space)
            {
                container.Add(ItemsCount);
                Remove(ItemsCount);
            }
            else
            {
                container.Add(space);
                Remove(space);
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


    }
}