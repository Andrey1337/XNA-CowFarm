using System;
using System.Collections.Generic;
using System.Diagnostics;
using CowFarm.Entities;
using CowFarm.ScreenSystem;
using CowFarm.Utility;
using CowFarm.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.Inventory
{
    public class Container
    {
        private Item _item;
        public int ItemsCount;
        private int _maxCount;
        public Texture2D IconTexture;
        public Rectangle Position { get; }
        private Texture2D _backgroundTexture;

        public bool OnFocus;

        public Container(Rectangle pos, Texture2D background)
        {
            Position = pos;
            _backgroundTexture = background;
        }

        public bool PossibleToAdd(Item item)
        {
            if (_item == null)
                return false;
            return _item.ItemId == item.ItemId && ItemsCount <= _maxCount;
        }

        public bool IsEmpty()
        {
            return _item == null;
        }

        public void Add(Item item)
        {
            if (item is Apple)
                _maxCount = 3;
            _item = item;
            IconTexture = _item.IconTexture;
            ItemsCount++;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            if (OnFocus)
            {
                spriteBatch.Draw(_backgroundTexture, Position, new Color(183, 183, 183));                
            }
            if (_item == null) return;
           
            

            spriteBatch.Draw(IconTexture, new Rectangle(Position.X + 1, Position.Y + 1, 40, 40), Color.White);

            if (ItemsCount > 1)
            {
                TextDrawHeleper.DrawText(spriteBatch, font, ItemsCount.ToString(), Color.Black, Color.White, 1, new Vector2(Position.X + 27, Position.Y + 20));
            }
        }

        public void Drop(World world, Vector2 position, Type[] itemsTypes, CowGameScreen cowGameScreen)
        {
            if (_item == null)
                return;
            if (itemsTypes[_item.ItemId] == null)
                throw new Exception("No itemId exists");

            object[] args = { cowGameScreen, world, position };
            Activator.CreateInstance(itemsTypes[_item.ItemId], args);

            ItemsCount--;
            if (ItemsCount == 0)
                _item = null;
        }
    }
}