using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CowFarm.DrowingSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.Entities
{
    public class Grass : Plant
    {
        private const float Delay = 1500f;

        public Grass(GraphicsDeviceManager graphics, Rectangle destRect, AnimatedSprites grassMovement) :
            base(graphics, destRect, grassMovement)
        {
            SpriteWidth = grassMovement.SpriteWidth;
        }

        public override void Load(ContentManager content)
        {

        }

        public override void Update(GameTime gameTime)
        {
            SourceRect = PlantMovement.Animate(gameTime, Delay, ObjectMovingType);
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(PlantMovement.Animation, DestRect, SourceRect, Color.White);
        }

        public override Rectangle GetPosition()
        {
            return new Rectangle(DestRect.X, DestRect.Y, SpriteWidth, PlantMovement.Animation.Height);
        }
    }
}
