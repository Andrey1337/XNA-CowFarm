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
namespace CowFarm.Entities
{
    public class Cow : Animal, IEatable
    {
        private Rectangle _sourceRect;

        private const float Delay = 200f;

        private const int SpaceFromSprites = 16;
        public const float CowSpeed = 2f;
        private const int SpriteWidth = 54;

        public Cow(GraphicsDeviceManager graphics, Rectangle destRect, AnimatedSprites currentAnim,
            AnimatedSprites rightWalk, AnimatedSprites leftWalk, AnimatedSprites downWalk, AnimatedSprites upWalk) : base(graphics, destRect, currentAnim, rightWalk, leftWalk, downWalk, upWalk) { }

        public override Rectangle GetPosition()
        {
            return new Rectangle(DestRect.X, DestRect.Y, SpriteWidth, CurrentAnim.Animation.Height);
        }

        //private void Animate(GameTime gameTime)
        //{
        //    _elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

        //    if (_elapsed >= _delay)
        //    {
        //        if (_frames >= 2)
        //        {
        //            _frames = 1;
        //        }
        //        else
        //        {
        //            _frames++;
        //        }
        //        _elapsed = 0;
        //    }
        //    _sourceRect = new Rectangle(SpriteWidth * _frames + _frames * SpaceFromSprites, 0,
        //        SpriteWidth, CurrentAnim.Animation.Height);
        //}

        public override void Eat()
        {

        }

        public override void Load(ContentManager content)
        {

        }

        private KeyboardState prevState = new KeyboardState();
        private bool isMoving = false;
        private string directory = "none";

        public override void Update(GameTime gameTime)
        {
            int MinX = 0;
            int MinY = 0;
            int MaxX = Graphics.PreferredBackBufferWidth;
            int MaxY = Graphics.PreferredBackBufferHeight;

            KeyboardState ks = Keyboard.GetState();


            var position = new Vector2(DestRect.X, DestRect.Y);
            if (ks.IsKeyDown(Keys.D) || ks.IsKeyDown(Keys.Right))
            {
                if (position.X + SpriteWidth < MaxX)
                    position.X += CowSpeed;
                CurrentAnim = RightWalk;
                _sourceRect = CurrentAnim.Animate(gameTime, Delay);
            }
            else if (ks.IsKeyDown(Keys.A) || ks.IsKeyDown(Keys.Left))
            {
                if (position.X > MinX)
                    position.X -= CowSpeed;
                CurrentAnim = LeftWalk;
                _sourceRect = CurrentAnim.Animate(gameTime, Delay);
            }
            else if (ks.IsKeyDown(Keys.W) || ks.IsKeyDown(Keys.Up))
            {
                if (position.Y > MinY)
                    position.Y -= CowSpeed;
                CurrentAnim = UpWalk;
                _sourceRect = CurrentAnim.Animate(gameTime, Delay);
            }
            else if (ks.IsKeyDown(Keys.S) || ks.IsKeyDown(Keys.Down))
            {
                if (position.Y + CurrentAnim.Animation.Height < MaxY)
                    position.Y += CowSpeed;
                CurrentAnim = DownWalk;
                _sourceRect = CurrentAnim.Animate(gameTime, Delay);
            }
            else
            {
                _sourceRect = new Rectangle(0, 0, SpriteWidth, CurrentAnim.Animation.Height);
            }

            DestRect = new Rectangle((int)position.X, (int)position.Y, CurrentAnim.SpriteWidth,
                CurrentAnim.SpriteHeight);

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(CurrentAnim.Animation, DestRect, _sourceRect, Color.White);
        }
    }
}
