using System;
using System.Collections.Generic;
using System.Linq;
using CowFarm.Containers;
using CowFarm.Entities.Items;
using CowFarm.ScreenSystem;
using CowFarm.Utility;
using CowFarm.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CowFarm.Inventory
{
    public class Inventory
    {
        private readonly StaticConainer[] _containers;

        private readonly CowGameScreen _cowGameScreen;
        private readonly Type[] _typesIds;

        private int _indexOnFocus;
        public Vector2 StartPosition { get; }
        public Vector2 EndPosition { get; }

        private HashSet<Container> _containersOnFocus;
        private HashSet<Container> _prevContainersOnFocus;

        public SwapContainer SwapContainer;

        public Inventory(CowGameScreen cowGameScreen)
        {
            _cowGameScreen = cowGameScreen;
            _typesIds = ItemsTypesHelper.GetItemsTypes();
            _indexOnFocus = 0;
            StartPosition = new Vector2(330, 827);
            EndPosition = new Vector2(StartPosition.X + _cowGameScreen.GameTextures["inventoryPanel"].Width, StartPosition.Y);

            _containersOnFocus = new HashSet<Container>();
            _prevContainersOnFocus = new HashSet<Container>();

            var pos = new Vector2(StartPosition.X + 25, StartPosition.Y + 9);

            _containers = new StaticConainer[9];
            for (var i = 0; i < _containers.Length; i++)
            {
                var rect = new Rectangle((int)pos.X, (int)pos.Y, 42, 42);
                _containers[i] = new StaticConainer(rect, cowGameScreen.GameTextures["cleanTexture"]);
                pos.X += 13 + rect.Width;
            }

            SwapContainer = new SwapContainer(Rectangle.Empty);
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
                if (!container.ItemStack.IsEmpty()) continue;
                container.Add(item);
                return;
            }
        }

        private MouseState _prevMouseState;

        public void Update()
        {
            var mouseState = Mouse.GetState();
            var mousePoint = new Point(mouseState.X, mouseState.Y);

            for (var i = 0; i < _containers.Length; i++)
            {
                if (_containers[i].Position.Contains(mousePoint))
                {
                    _containers[i].OnFocus = true;
                    _containersOnFocus.Add(_containers[i]);


                    if (mouseState.LeftButton != ButtonState.Pressed ||
                        _prevMouseState.LeftButton == ButtonState.Pressed) continue;

                    _indexOnFocus = i;

                    SwapContainer.Swap(_containers[i]);
                }
            }
            _prevContainersOnFocus.ToList().Where(container => !_containersOnFocus.Contains(container)).ToList().ForEach(container => container.OnFocus = false);

            _prevMouseState = mouseState;
            _prevContainersOnFocus = _containersOnFocus;
            _containersOnFocus = new HashSet<Container>();
        }

        public void ItemOnFocus(int index)
        {
            _indexOnFocus = index;
        }

        public void Drop(World world, Vector2 position)
        {
            if (_indexOnFocus == -1)
                return;
            _containers[_indexOnFocus].Drop(world, position, _typesIds, _cowGameScreen);
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.Draw(_cowGameScreen.GameTextures["inventoryPanel"], StartPosition, Color.White);
            var pos = StartPosition;
            pos.X += 26;
            pos.Y += 10;
            if (_indexOnFocus != -1)
                spriteBatch.Draw(_cowGameScreen.GameTextures["selectionTexture"], new Vector2(pos.X - 5 + (_indexOnFocus * 40) + (_indexOnFocus * 15), pos.Y - 5), Color.White);


            foreach (var container in _containers)
            {
                container.Draw(spriteBatch, font);
            }

            SwapContainer?.Draw(spriteBatch, font);
        }


    }
}