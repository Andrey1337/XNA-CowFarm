using CowFarm.DrowingSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.Entities
{
    public class Bush : Plant
    {
        public Bush(GraphicsDeviceManager graphics, Rectangle destRect, AnimatedSprites plantMovement) : base(graphics, destRect, plantMovement)
        {
        }

        public override void Load(ContentManager content)
        {
            throw new System.NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            throw new System.NotImplementedException();
        }

        public override Rectangle GetPosition()
        {
            throw new System.NotImplementedException();
        }
    }
}