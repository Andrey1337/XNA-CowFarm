using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.DrowingSystem
{
    public abstract class AnimatedSprites
    {
        public Texture2D Animation { get; }
        public int SpriteWidth { get; }
        public int SpriteHeight { get; }
        protected int FramesCounter;
        public int SpaceFromSprites { get; }
        public int Frames { get; }
        protected float Elapsed;

        protected AnimatedSprites(Texture2D animation, int frames, int spaceFromSprites)
        {
            Animation = animation;
            SpriteWidth = (animation.Width - (frames - 1) * spaceFromSprites) / frames;
            SpriteHeight = Animation.Height;
            SpaceFromSprites = spaceFromSprites;
            Frames = frames;
            Elapsed = 100;
        }

        public abstract Rectangle Animate(GameTime gameTime, float delay = float.MaxValue);

    }
}