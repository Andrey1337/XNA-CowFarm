using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm
{
    public class AnimatedSprites
    {
        public Texture2D Animation { get; }
        public int SpriteWidth { get; }
        public int SpriteHeight { get; }
        private int _framesCounter;
        private readonly int _spaceFromSprites;
        private readonly int _frames;
        private float _elapsed;

        public AnimatedSprites(Texture2D animation, int frames, int spriteWidth, int spaceFromSprites)
        {
            this.Animation = animation;
            this.SpriteWidth = spriteWidth;
            this.SpriteHeight = Animation.Height;
            this._spaceFromSprites = spaceFromSprites;
            this._framesCounter = 0;
            this._frames = frames;
            this._elapsed = 0;
        }

        public Rectangle Animate(GameTime gameTime, float delay)
        {
            _elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (_elapsed >= delay)
            {
                if (_framesCounter >= _frames - 1)
                {
                    _framesCounter = 1;
                }
                else
                {
                    _framesCounter++;
                }
                _elapsed = 0;
            }
            return new Rectangle(SpriteWidth * _framesCounter + _framesCounter * _spaceFromSprites, 0,
                SpriteWidth, Animation.Height);
        }
    }
}