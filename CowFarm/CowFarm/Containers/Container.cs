using CowFarm.Entities.Items;
using CowFarm.Inventory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.Containers
{
    public abstract class Container
    {
        public ItemStack ItemStack;
        public Item Item => ItemStack.Item;
        public int ItemsCount => ItemStack.ItemsCount;

        public Rectangle Position;
        public bool OnFocus;

        public virtual void Swap(Container container)
        {
            ItemStack temp = ItemStack;
            ItemStack = container.ItemStack;
            container.ItemStack = temp;
        }

        protected Container(Rectangle position)
        {
            ItemStack = new ItemStack();
            Position = position;
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