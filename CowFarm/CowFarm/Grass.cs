using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm
{
    class Grass : IEntity
    {
        private Texture2D _grassMovement;
        private Rectangle _destRect;
        private Rectangle _sourceRect;

        private float _elapsed;
        private const int SpaceFromSprites = 15;
        private const float Delay = 400f;
        private int _frames;



        public Grass(Rectangle destRect, Texture2D grassMovement)
        {
            this._destRect = destRect;
            this._grassMovement = grassMovement;
        }
        public void Load(ContentManager content)
        {
        }

        public void Update(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            _elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (_elapsed >= Delay)
            {
                if (_frames >= 1)
                {
                    _frames = 1;
                }
                else
                {
                    _frames++;
                }
                _elapsed = 0;
            }
            _sourceRect = new Rectangle(24 * _frames + _frames * SpaceFromSprites, 0, 24, 24);
        }


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_grassMovement, _destRect, _sourceRect, Color.White);
        }
    }
}
