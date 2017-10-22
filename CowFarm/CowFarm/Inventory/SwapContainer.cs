using CowFarm.Entities;
using CowFarm.Utility;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CowFarm.Inventory
{
    public class SwapContainer
    {
        public Item Item;
        public int ItemsCount;
        private int _maxCount;

        public SwapContainer()
        {
        }

        public void Swap()
        {
            
        }
        

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            var mousePoint = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            spriteBatch.Draw(Item.IconTexture, new Rectangle((int)mousePoint.X + 1, (int)mousePoint.Y + 1, 40, 40), Color.White);
            if (ItemsCount > 1)
            {
                TextDrawHeleper.DrawText(spriteBatch, font, ItemsCount.ToString(), Color.Black, Color.White, 1,
                    new Vector2(mousePoint.X + 27, mousePoint.Y + 20));
            }
        }
    }
}