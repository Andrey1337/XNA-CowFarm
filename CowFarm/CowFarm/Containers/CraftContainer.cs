using CowFarm.Inventory;
using CowFarm.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.Containers
{
    public class CraftContainer : Container
    {
        private readonly Texture2D _backgroundTexture;

        public CraftContainer(Rectangle pos, Texture2D background) : base(pos)
        {
            Position = pos;
            _backgroundTexture = background;
        }

        public override void Swap(Container container)
        {
            if (!container.ItemStack.IsEmpty() || ItemStack.IsEmpty())
                return;

            base.Swap(container);
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
    }
}