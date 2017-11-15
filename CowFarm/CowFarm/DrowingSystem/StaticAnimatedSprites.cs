using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.DrowingSystem
{
    public class StaticAnimatedSprites : AnimatedSprites
    {
        public StaticAnimatedSprites(Texture2D animation, int frames, int spaceFromSprites) : base(animation, frames, spaceFromSprites)
        {
            FramesCounter = 0;
        }

        public override Rectangle Animate(GameTime gameTime, float delay = float.MaxValue)
        {
            Elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (!(Elapsed >= delay))
                return new Rectangle(SpriteWidth * FramesCounter + FramesCounter * SpaceFromSprites, 0,
                    SpriteWidth, Animation.Height);

            if (FramesCounter >= Frames - 1)
            {
                FramesCounter = 0;
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