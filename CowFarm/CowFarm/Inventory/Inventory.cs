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
        private Container[] _containers;

        private readonly CowGameScreen _cowGameScreen;
        private readonly Type[] _typesIds;

        private int _indexOnFocus;

        private Container _containerOnFocus;

        private Container _swapContainer1;
        private Container _swapContainer2;

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

        private void SwapContainers(Container container1, Container container2)
        {
            if (container1 == null)
                return;

            Container temp = new Container(new Rectangle(0, 0, 0, 0), _cowGameScreen.GameTextures["cleanTexture"]);
            temp.Swap(container1);
            container1.Swap(container2);
            container2.Swap(temp);
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

        private MouseState _prevMouseState;
        public void Update()
        {
            var mouseState = Mouse.GetState();
            var mousePoint = new Point(mouseState.X, mouseState.Y);

            for (int i = 0; i < _containers.Length; i++)
            {
                if (_containers[i].Position.Contains(mousePoint))
                {
                    _containerOnFocus = _containers[i];
                    _containerOnFocus.OnFocus = true;
                    if (mouseState.LeftButton == ButtonState.Pressed && _prevMouseState.LeftButton != ButtonState.Pressed)
                    {
                        _indexOnFocus = i;
                        if (_swapContainer1 == null && !_containers[i].IsEmpty())
                        {
                            _swapContainer1 = _containers[i];
                        }
                        else
                        {
                            if (_swapContainer1 != _containers[i])
                            {
                                SwapContainers(_swapContainer1, _containers[i]);
                                _swapContainer1 = null;
                            }

                        }
                    }
                }
                if (_containerOnFocus != null && !_containerOnFocus.Position.Contains(mousePoint))
                {
                    _containerOnFocus.OnFocus = false;
                }
            }
            _prevMouseState = mouseState;
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
            if (_indexOnFocus != -1)
                spriteBatch.Draw(_cowGameScreen.GameTextures["selectionTexture"], new Vector2(pos.X - 5 + (_indexOnFocus * 40) + (_indexOnFocus * 15), pos.Y - 5), Color.White);


            for (var i = 0; i < _containers.Length; i++)
            {
                _containers[i].Draw(spriteBatch, font);
            }

        }


    }
}