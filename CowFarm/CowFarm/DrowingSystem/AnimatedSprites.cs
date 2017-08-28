using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.DrowingSystem
{
    public class AnimatedSprites
    {
        public Texture2D Animation { get; }
        public int SpriteWidth { get; }
        public int SpriteHeight { get; }
        private int _framesCounter;
        public int SpaceFromSprites { get; }
        public int Frames { get; }
        private float _elapsed;

        public AnimatedSprites(Texture2D animation, int frames, int spriteWidth, int spaceFromSprites)
        {
            this.Animation = animation;
            this.SpriteWidth = spriteWidth;
            this.SpriteHeight = Animation.Height;
            this.SpaceFromSprites = spaceFromSprites;
            this._framesCounter = 0;
            this.Frames = frames;
            this._elapsed = 100;
        }

        public Rectangle Animate(GameTime gameTime, float delay, ObjectMovingType ogjectType)
        {


            _elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (!(_elapsed >= delay))
                return new Rectangle(SpriteWidth * _framesCounter + _framesCounter * SpaceFromSprites, 0,
                    SpriteWidth, Animation.Height);
            if (_framesCounter >= Frames - 1)
            {
                _framesCounter = (int)ogjectType;
            }
            else
            {
                _framesCounter++;
            }
            _elapsed = 0;
            return new Rectangle(SpriteWidth * _framesCounter + _framesCounter * SpaceFromSprites, 0,
                SpriteWidth, Animation.Height);
        }
    }
}