using System;
using System.Collections.Generic;
using System.Linq;
using CowFarm.DrowingSystem;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using World = CowFarm.Worlds.World;

namespace CowFarm.Entities
{
    public class Cow : Animal, IEatable
    {
        private Rectangle _sourceRect;

        private const float Delay = 200f;

        public const float CowSpeed = 2f;

        public Cow(World world, GraphicsDeviceManager graphics, Rectangle destRect, AnimatedSprites currentAnim,
            AnimatedSprites rightWalk, AnimatedSprites leftWalk, AnimatedSprites downWalk, AnimatedSprites upWalk)
            : base(world, graphics, destRect, currentAnim, rightWalk, leftWalk, downWalk, upWalk)
        {
            CollisionCategories = Category.Cat1;
            CollidesWith = Category.Cat2;
        }

        public override Rectangle GetPosition()
        {
            return new Rectangle(DestRect.X, DestRect.Y, CurrentAnim.SpriteWidth, CurrentAnim.Animation.Height);
        }

        public override void Eat()
        {

        }

        public override void Load(ContentManager content)
        {

        }

        private KeyboardState prevState = new KeyboardState();
        private bool isMoving = false;


        public override void Update(GameTime gameTime)
        {
            const int minX = 0;
            const int minY = 0;
            int maxX = Graphics.PreferredBackBufferWidth;
            int maxY = Graphics.PreferredBackBufferHeight;

            KeyboardState ks = Keyboard.GetState();


            var position = new Vector2(DestRect.X, DestRect.Y);
            if (ks.IsKeyDown(Keys.D) || ks.IsKeyDown(Keys.Right))
            {
                if (position.X + CurrentAnim.SpriteWidth < maxX)
                    position.X += CowSpeed;
                CurrentAnim = RightWalk;
                _sourceRect = CurrentAnim.Animate(gameTime, Delay, ObjectMovingType);
            }
            else if (ks.IsKeyDown(Keys.A) || ks.IsKeyDown(Keys.Left))
            {
                if (position.X > minX)
                    position.X -= CowSpeed;
                CurrentAnim = LeftWalk;
                _sourceRect = CurrentAnim.Animate(gameTime, Delay, ObjectMovingType);
            }
            else if (ks.IsKeyDown(Keys.W) || ks.IsKeyDown(Keys.Up))
            {
                if (position.Y > minY)
                    position.Y -= CowSpeed;
                CurrentAnim = UpWalk;
                _sourceRect = CurrentAnim.Animate(gameTime, Delay, ObjectMovingType);
            }
            else if (ks.IsKeyDown(Keys.S) || ks.IsKeyDown(Keys.Down))
            {
                if (position.Y + CurrentAnim.Animation.Height < maxY)
                    position.Y += CowSpeed;
                CurrentAnim = DownWalk;
                _sourceRect = CurrentAnim.Animate(gameTime, Delay, ObjectMovingType);
            }
            else
            {
                _sourceRect = new Rectangle(0, 0, CurrentAnim.SpriteWidth, CurrentAnim.Animation.Height);
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
