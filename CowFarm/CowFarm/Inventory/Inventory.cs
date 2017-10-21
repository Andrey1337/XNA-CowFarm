using System;
using System.Diagnostics;
using System.Linq;
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

        private Container _swapContainer;

        public Inventory(CowGameScreen cowGameScreen)
        {
            _cowGameScreen = cowGameScreen;
            _typesIds = ItemsTypesHelper.GetItemsTypes();

            _drawPos = new Vector2(330, 827);

            var pos = new Vector2(_drawPos.X + 25, _drawPos.Y + 9);

            _containers = new Container[9];
            for (var i = 0; i < _containers.Length; i++)
            {
                var rect = new Rectangle((int)pos.X, (int)pos.Y, 42, 42);
                _containers[i] = new Container(rect, cowGameScreen.GameTextures["cleanTexture"]);
                pos.X += 13 + rect.Width;
            }
        }

        private void SwapContainers(Container container1, Container container2)
        {
            if (_swapContainer == null)
            {
                return;
            }
            if (container1 == container2)
            {
                _swapContainer.IsPicked = false;
                _swapContainer = null;
                return;
            }

            _swapContainer.IsPicked = false;
            Container temp = new Container();
            temp.Swap(container1);
            container1.Swap(container2);
            container2.Swap(temp);

            if (!container1.IsEmpty())
            {
                _swapContainer = container1;
                _swapContainer.IsPicked = true;
            }
            else
            {
                _swapContainer = null;
            }
        }

        public void Add(Item item)
        {
            foreach (var container in _containers)
            {
                if (container == null || !container.PossibleToAdd(item)) continue;
                container.Add(item);
                return;
            }

            foreach (var container in _containers)
            {
                if (!container.IsEmpty()) continue;
                container.Add(item);
                return;
            }
        }

        private MouseState _prevMouseState;
        private Container _containerOnFocus;
        public void Update()
        {
            var mouseState = Mouse.GetState();
            var mousePoint = new Point(mouseState.X, mouseState.Y);

            for (var i = 0; i < _containers.Length; i++)
            {
                if (_containers[i].Position.Contains(mousePoint))
                {
                    _containerOnFocus = _containers[i];
                    _containerOnFocus.OnFocus = true;

                    if (mouseState.LeftButton != ButtonState.Pressed ||
                        _prevMouseState.LeftButton == ButtonState.Pressed) continue;

                    _indexOnFocus = i;


                    if (_swapContainer == null)
                    {
                        if (!_containers[i].IsEmpty())
                        {
                            _swapContainer = _containers[i];
                            _swapContainer.IsPicked = true;
                        }
                    }
                    else
                    {
                        SwapContainers(_swapContainer, _containers[i]);
                    }
                }
            }

            if (_containerOnFocus != null && !_containerOnFocus.Position.Contains(mousePoint))
            {
                _containerOnFocus.OnFocus = false;
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


            foreach (var container in _containers)
            {
                container.Draw(spriteBatch, font);
            }

            _swapContainer?.Draw(spriteBatch, font);
        }


    }
}