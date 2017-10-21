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
    public class Container
    {
        public Item Item;
        public int ItemsCount;
        private int _maxCount;
        public Rectangle Position { get; }
        public bool IsPicked;

        private readonly Texture2D _backgroundTexture;

        public bool OnFocus;

        public Container(Rectangle pos, Texture2D background)
        {
            Position = pos;
            _backgroundTexture = background;
        }

        public Container()
        {
        }

        public void Swap(Container container)
        {
            Item = container.Item;
            ItemsCount = container.ItemsCount;
            if (container.Item is Apple)
            {
                _maxCount = 2;
            }
        }

        public bool PossibleToAdd(Item item)
        {
            if (Item == null)
                return false;
            return Item.ItemId == item.ItemId && ItemsCount < _maxCount;
        }

        public bool IsEmpty()
        {
            return Item == null;
        }

        public void Add(Item item)
        {
            if (item is Apple)
                _maxCount = 2;
            Item = item;
            ItemsCount++;
            item.Pick();
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            if (OnFocus)
            {
                spriteBatch.Draw(_backgroundTexture, Position, new Color(183, 183, 183));
            }
            if (Item == null) return;

            if (!IsPicked)
            {
                spriteBatch.Draw(Item.IconTexture, new Rectangle(Position.X + 1, Position.Y + 1, 40, 40), Color.White);
                if (ItemsCount > 1)
                {
                    TextDrawHeleper.DrawText(spriteBatch, font, ItemsCount.ToString(), Color.Black, Color.White, 1,
                        new Vector2(Position.X + 27, Position.Y + 20));
                }
            }
            else
            {
                var mouseState = Mouse.GetState();
                var mousePoint = new Vector2(mouseState.X, mouseState.Y);
                var rect = new Rectangle((int)mousePoint.X - 20, (int)mousePoint.Y - 20, 40, 40);
                spriteBatch.Draw(Item.IconTexture, rect, Color.White);

                if (ItemsCount > 1)
                {
                    TextDrawHeleper.DrawText(spriteBatch, font, ItemsCount.ToString(), Color.Black, Color.White, 1,
                        new Vector2(rect.X + 27, rect.Y + 20));
                }
            }
        }

        public void Drop(World world, Vector2 position, Type[] itemsTypes, CowGameScreen cowGameScreen)
        {
            if (Item == null)
                return;
            if (itemsTypes[Item.ItemId] == null)
                throw new Exception("No itemId exists");

            object[] args = { cowGameScreen, world, position };
            Activator.CreateInstance(itemsTypes[Item.ItemId], args);

            ItemsCount--;
            if (ItemsCount == 0)
                Item = null;
        }
    }
}