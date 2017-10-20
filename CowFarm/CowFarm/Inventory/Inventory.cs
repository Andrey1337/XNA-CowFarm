using System;
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
using Microsoft.Xna.Framework.Input;

namespace CowFarm.Inventory
{
    public class Inventory
    {
        private readonly Container[] _containers;

        private readonly CowGameScreen _cowGameScreen;
        private readonly Type[] _typesIds;

        private int _indexOnFocus;

        private readonly Texture2D _selectionTexture;

        public Inventory(CowGameScreen cowGameScreen)
        {
            _cowGameScreen = cowGameScreen;
            _typesIds = ItemsTypesHelper.GetItemsTypes();

            _drawPos = new Vector2(330, 827);

            var pos = new Vector2(_drawPos.X + 25, _drawPos.Y + 9);

            _containers = new Container[9];
            for (int i = 0; i < _containers.Length; i++)
            {
                var rect = new Rectangle((int)pos.X, (int)pos.Y, 42, 42);
                _containers[i] = new Container(rect, cowGameScreen.GameTextures["cleanTexture"]);
                pos.X += 13 + rect.Width;
            }
        }

        public void Add(Item item)
        {
            for (var i = 0; i < _containers.Length; i++)
            {
                if (_containers[i] == null || !_containers[i].PossibleToAdd(item)) continue;
                _containers[i].Add(item);
                item.Pick();
                return;
            }

            for (var i = 0; i < _containers.Length; i++)
            {
                if (!_containers[i].IsEmpty()) continue;
                _containers[i].Add(item);
                item.Pick();
                return;
            }
        }

        private Container containerOnFocus;
        public void Update()
        {

            var mouseState = Mouse.GetState();
            var mousePoint = new Point(mouseState.X, mouseState.Y);

            for (int i = 0; i < _containers.Length; i++)
            {
                if (_containers[i].Position.Contains(mousePoint))
                {
                    if (mouseState.LeftButton == ButtonState.Pressed)
                        _indexOnFocus = i;
                    containerOnFocus = _containers[i];
                    containerOnFocus.OnFocus = true;
                }
                if (containerOnFocus != null && !containerOnFocus.Position.Contains(mousePoint))
                {
                    containerOnFocus.OnFocus = false;
                }
            }
        }

        public void ItemOnFocus(int index)
        {
            _indexOnFocus = index;
        }

        public void Drop(World world, Vector2 position)
        {
            position.X /= 100;
            position.Y /= 100;

            _containers[_indexOnFocus].Drop(world, position, _typesIds, _cowGameScreen);
        }

        readonly Vector2 _drawPos;
        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.Draw(_cowGameScreen.GameTextures["inventoryPanel"], _drawPos, Color.White);
            var pos = _drawPos;
            pos.X += 26;
            pos.Y += 10;

            spriteBatch.Draw(_cowGameScreen.GameTextures["selectionTexture"], new Vector2(pos.X - 5 + (_indexOnFocus * 40) + (_indexOnFocus * 15), pos.Y - 5), Color.White);

            for (var i = 0; i < _containers.Length; i++)
            {
                _containers[i].Draw(spriteBatch, font);
            }

        }


    }
}