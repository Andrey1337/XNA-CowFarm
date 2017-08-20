using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.Entities
{
    public class Tree : Plant
    {
        private const float Delay = 5000f;

        public Tree(GraphicsDeviceManager graphics, Rectangle destRect, AnimatedSprites treeMovement) : base(graphics,
            destRect, treeMovement)
        {
            SpriteWidth = treeMovement.SpriteWidth;
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