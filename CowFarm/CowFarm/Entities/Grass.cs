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
        private readonly AnimatedSprites _grassMovement;
        private Rectangle _destRect;
        private Rectangle _sourceRect;
                
        private const float Delay = 1500f;
        

        private const int SpriteWidth = 24;

        public Grass(GraphicsDeviceManager graphics, Rectangle destRect, AnimatedSprites grassMovement) :
            base(graphics, destRect, grassMovement)
        {
            this._destRect = destRect;
            this._grassMovement = grassMovement;            
        }


        public override Rectangle GetPosition()
        {
            return new Rectangle(_destRect.X, _destRect.Y, SpriteWidth, _grassMovement.Animation.Height);
        }

        public override void Load(ContentManager content)
        {

        }

        public override void Update(GameTime gameTime)
        {            
            _sourceRect = _grassMovement.Animate(gameTime, Delay);
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_grassMovement.Animation, _destRect, _sourceRect, Color.White);
        }
    }
}
