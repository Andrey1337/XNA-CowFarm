using CowFarm.Entities.Items;
using CowFarm.Inventory;
using CowFarm.TileEntities;
using CowFarm.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.Containers
{
    public class CraftContainer : Container
    {
        private readonly Texture2D _backgroundTexture;
        private readonly TileEntity _tileEntity;

        public CraftContainer(Rectangle pos, Texture2D background, TileEntity tileEntity) : base(pos)
        {
            Position = pos;
            _backgroundTexture = background;
            _tileEntity = tileEntity;
        }

        public override void Swap(Container container)
        {
            if (ItemStack.IsEmpty())
                return;

            if (!container.ItemStack.IsEmpty() && container.ItemStack.Item.ItemId != ItemStack.Item.ItemId)
                return;

            int count = ItemStack.ItemsCount;
            for (int i = 0; i < count && !container.ItemStack.IsFull(); i++)
            {
                container.Add(ItemStack.Item);
                ItemStack.Remove();
            }

            _tileEntity.Craft();
            //base.Swap(container);
        }

        public void Result(Item item, int count = 1)
        {
            ItemStack.Item = item;
            ItemStack.ItemsCount = count;
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