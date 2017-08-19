using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace CowFarm
{
    public class Cow : Animal, IEatable
    {
        private readonly Texture2D _rightWalk;
        private readonly Texture2D _leftWalk;
        private readonly Texture2D _downWalk;
        private readonly Texture2D _upWalk;
        private Texture2D _currentAnim;
        private Rectangle _sourceRect;
        private Rectangle _destRect;
        private float _elapsed;
        private const float Delay = 200f;
        private int _frames;

        private const int SpaceFromSprites = 16;
        public const float CowSpeed = 2f;

        private const int SpriteWidth = 54;


        public Cow(Rectangle destRect, Texture2D currentAnim, Texture2D rightWalk, Texture2D leftWalk, Texture2D downWalk, Texture2D upWalk)
        {
            this._currentAnim = currentAnim;
            this._destRect = destRect;
            this._rightWalk = rightWalk;
            this._leftWalk = leftWalk;
            this._downWalk = downWalk;
            this._upWalk = upWalk;
        }

        public override Rectangle GetPosition()
        {
            return new Rectangle(_destRect.X, _destRect.Y, SpriteWidth, _currentAnim.Height);
        }

        private void Animate(GameTime gameTime)
        {
            _elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (_elapsed >= Delay)
            {
                if (_frames >= 2)
                {
                    _frames = 1;
                }
                else
                {
                    _frames++;
                }
                _elapsed = 0;
            }
            _sourceRect = new Rectangle(SpriteWidth * _frames + _frames * SpaceFromSprites, 0, SpriteWidth, _currentAnim.Height);
        }

        public override void Eat()
        {

        }

        public override void Load(ContentManager content)
        {

        }

        private KeyboardState prevState = new KeyboardState();
        private bool isMoving = false;
        private string directory = "none";

        public override void Update(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            int MinX = 0;
            int MinY = 0;
            int MaxX = graphics.PreferredBackBufferWidth;
            int MaxY = graphics.PreferredBackBufferHeight;

            KeyboardState ks = Keyboard.GetState();


            var position = new Vector2(_destRect.X, _destRect.Y);
            if (ks.IsKeyDown(Keys.D) || ks.IsKeyDown(Keys.Right))
            {
                if (position.X + SpriteWidth < MaxX)
                    position.X += CowSpeed;
                _currentAnim = _rightWalk;
                Animate(gameTime);
            }
            else if (ks.IsKeyDown(Keys.A) || ks.IsKeyDown(Keys.Left))
            {
                if (position.X > MinX)
                    position.X -= CowSpeed;
                _currentAnim = _leftWalk;
                Animate(gameTime);
            }
            else if (ks.IsKeyDown(Keys.W) || ks.IsKeyDown(Keys.Up))
            {
                if (position.Y > MinY)
                    position.Y -= CowSpeed;
                _currentAnim = _upWalk;
                Animate(gameTime);
            }
            else if (ks.IsKeyDown(Keys.S) || ks.IsKeyDown(Keys.Down))
            {
                if (position.Y + _currentAnim.Height < MaxY)
                    position.Y += CowSpeed;
                _currentAnim = _downWalk;
                Animate(gameTime);
            }
            else
            {
                _sourceRect = new Rectangle(0, 0, SpriteWidth, _currentAnim.Height);
            }

            _destRect = new Rectangle((int)position.X, (int)position.Y, SpriteWidth, _currentAnim.Height);
            prevState = ks;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_currentAnim, _destRect, _sourceRect, Color.White);
        }
    }
}
