using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.Entities
{
    public class Grass : Plant
    {
        private readonly Texture2D _grassMovement;
        private Rectangle _destRect;
        private Rectangle _sourceRect;

        private float _elapsed;
        private const int SpaceFromSprites = 15;
        private const float Delay = 1500f;
        private int _frames;

        private const int SpriteWidth = 24;

        private bool _isAvaibleToEat;
        public Grass(Rectangle destRect, Texture2D grassMovement)
        {
            this._destRect = destRect;
            this._grassMovement = grassMovement;
            this._isAvaibleToEat = true;
        }


        public override Rectangle GetPosition()
        {
            return new Rectangle(_destRect.X, _destRect.Y, SpriteWidth, _grassMovement.Height);
        }

        public override void Load(ContentManager content)
        {

        }

        public override void Update(GameTime gameTime)
        {
            _elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (_elapsed >= Delay)
            {
                if (_frames == 1)
                    _frames = 0;
                else
                    _frames++;
                _elapsed = 0;
            }
            _sourceRect = new Rectangle(SpriteWidth * _frames + _frames * SpaceFromSprites, 0, SpriteWidth, _grassMovement.Height);
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_grassMovement, _destRect, _sourceRect, Color.White);
        }
    }
}
