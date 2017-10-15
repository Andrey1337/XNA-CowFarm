using System.Diagnostics;
using System.Security.Cryptography;
using CowFarm.Entities;
using FarseerPhysics.Samples.ScreenSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.Inventory
{
    public class Inventory
    {
        private Container[] _containers;

        private readonly Texture2D _inventoryTexture;
        public Inventory(Texture2D inventoryTexture)
        {
            _inventoryTexture = inventoryTexture;
            _containers = new Container[9];
            _drawPos = new Vector2(330, 827);
        }

        public void Add(Item item)
        {
            for (var i = 0; i < _containers.Length; i++)
            {
                if (_containers[i] == null || !_containers[i].PossibleToAdd(item.BodyTypeName)) continue;
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

        public void Drop(int index)
        {
            
        }

        readonly Vector2 _drawPos;
        public void Draw(SpriteBatch spriteBatch)
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
                    spriteBatch.Draw(_containers[i].Item.IconTexture, rect, Color.White);

                }
                pos.X += 15 + rect.Width;

            }
        }
    }
}