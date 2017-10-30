using System;
using CowFarm.Entities;
using CowFarm.Entities.Items;
using CowFarm.ScreenSystem;
using CowFarm.Utility;
using CowFarm.Worlds;
using CowFarm.Inventory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.Containers
{
    public class StaticConainer : Container
    {
        private readonly Texture2D _backgroundTexture;

        public StaticConainer(Rectangle pos, Texture2D background) : base(pos)
        {
            Position = pos;
            _backgroundTexture = background;
        }

        public override void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            if (OnFocus)
            {
                spriteBatch.Draw(_backgroundTexture, Position, new Color(183, 183, 183));
            }
            if (ItemStack == null || Item == null) return;

            spriteBatch.Draw(Item.IconTexture, new Rectangle(Position.X + 1, Position.Y + 1, Position.Width - 2, Position.Height - 2), Color.White);
            if (ItemsCount > 1)
            {
                TextDrawHeleper.DrawText(spriteBatch, font, ItemsCount.ToString(), Color.Black, Color.White, 1, new Vector2(Position.X + 27, Position.Y + 20));
            }
        }

        public void Drop(World world, Vector2 position, Type[] itemsTypes, CowGameScreen cowGameScreen)
        {
            if (Item == null)
                return;
            if (itemsTypes[Item.ItemId] == null)
                throw new Exception("No itemId exists");

            object[] args = { cowGameScreen };
            object item = Activator.CreateInstance(itemsTypes[Item.ItemId], args);
            ((Item)item).Drop(world, position);
            ItemStack.Remove();
        }

    }
}