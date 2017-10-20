using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters;
using System.Security.Cryptography;
using CowFarm.Entities;
using CowFarm.ScreenSystem;
using CowFarm.Utility;
using CowFarm.Worlds;
using FarseerPhysics.Samples.ScreenSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.Inventory
{
    public class Inventory
    {
        private readonly Container[] _containers;

        private readonly Dictionary<string, Texture2D> _gameTextures;
        private readonly CowGameScreen _cowGameScreen;
        private readonly Type[] _typesIds;

        private readonly Texture2D _inventoryTexture;
        public Inventory(CowGameScreen cowGameScreen)
        {
            _cowGameScreen = cowGameScreen;
            _typesIds = ItemsTypesHelper.GetItemsTypes();
            _gameTextures = cowGameScreen.GameTextures;
            _inventoryTexture = _gameTextures["inventoryPanel"];
            _containers = new Container[9];
            _drawPos = new Vector2(330, 827);

        }

        public void Add(Item item)
        {
            for (var i = 0; i < _containers.Length; i++)
            {
                if (_containers[i] == null || !_containers[i].PossibleToAdd(item.ItemId)) continue;
                _containers[i].Add();
                item.Pick();
                return;
            }

            for (var i = 0; i < _containers.Length; i++)
            {
                if (_containers[i] != null) continue;
                _containers[i] = new Container(item);
                _containers[i].Add();
                item.Pick();
                return;
            }
        }

        public void Drop(World world, Vector2 position, int index)
        {
            position.X /= 100;
            position.Y /= 100;



            if (_containers[index] == null) return;
            _containers[index].Drop(world, position, _typesIds, _cowGameScreen);
            if (_containers[index].ItemsCount == 0)
                _containers[index] = null;
        }

        readonly Vector2 _drawPos;
        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.Draw(_inventoryTexture, _drawPos, Color.White);
            var pos = _drawPos;
            pos.X += 26;
            pos.Y += 10;

            for (var i = 0; i < _containers.Length; i++)
            {
                Rectangle rect = new Rectangle((int)pos.X, (int)pos.Y, 40, 40);
                if (_containers[i] != null)
                {
                    spriteBatch.Draw(_containers[i].IconTexture, rect, Color.White);
                    if (_containers[i].ItemsCount > 1)
                    {
                        TextDrawHeleper.DrawText(spriteBatch, font, _containers[i].ItemsCount.ToString(), Color.Black, Color.White, 1, new Vector2(rect.X + 27, rect.Y + 20));
                    }
                }
                pos.X += 15 + rect.Width;

            }
        }


    }
}