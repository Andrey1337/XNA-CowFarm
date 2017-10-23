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
            if (container.Item == null || Item == null || container.Item.ItemId != Item.ItemId || container.ItemStack.IsFull())
            {
                container.Swap(this);
                return;
            }

            for (var i = 0; i <= ItemsCount && !container.ItemStack.IsFull(); i++)
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


    }
}