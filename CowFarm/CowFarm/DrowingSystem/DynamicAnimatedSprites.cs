using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.DrowingSystem
{
    public class DynamicAnimatedSprites : AnimatedSprites
    {
        public DynamicAnimatedSprites(Texture2D animation, int frames, int spaceFromSprites) : base(animation, frames, spaceFromSprites)
        {
            FramesCounter = 1;
        }

        public override Rectangle Animate(GameTime gameTime, float delay = float.MaxValue)
        {
            Elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (!(Elapsed >= delay))
                return new Rectangle(SpriteWidth * FramesCounter + FramesCounter * SpaceFromSprites, 0,
                    SpriteWidth, Animation.Height);

            if (FramesCounter >= Frames - 1)
            {
                FramesCounter = 1;
            }
            else
            {
                FramesCounter++;
            }
            Elapsed = 0;
            return new Rectangle(SpriteWidth * FramesCounter + FramesCounter * SpaceFromSprites, 0,
                SpriteWidth, Animation.Height);
        }
    }
}