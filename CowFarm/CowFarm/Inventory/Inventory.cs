using System;
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
        private readonly InventoryConainer[] _containers;

        private readonly CowGameScreen _cowGameScreen;
        private readonly Type[] _typesIds;

        private int _indexOnFocus;

        public SwapContainer SwapContainer;

        public Inventory(CowGameScreen cowGameScreen)
        {
            _cowGameScreen = cowGameScreen;
            _typesIds = ItemsTypesHelper.GetItemsTypes();

            _drawPos = new Vector2(330, 827);

            var pos = new Vector2(_drawPos.X + 25, _drawPos.Y + 9);

            _containers = new InventoryConainer[9];
            for (var i = 0; i < _containers.Length; i++)
            {
                var rect = new Rectangle((int)pos.X, (int)pos.Y, 42, 42);
                _containers[i] = new InventoryConainer(rect, cowGameScreen.GameTextures["cleanTexture"]);
                pos.X += 13 + rect.Width;
            }

            SwapContainer = new SwapContainer();
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
        private InventoryConainer _containerOnFocus;
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

                    SwapContainer.Swap(_containers[i]);
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


            _containers[_indexOnFocus].Drop(world, position, _typesIds, _cowGameScreen);
        }

        private readonly Vector2 _drawPos;
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

            SwapContainer?.Draw(spriteBatch, font);
        }


    }
}