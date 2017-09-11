using System;
using System.Collections.Generic;
using System.Linq;
using CowFarm.DrowingSystem;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Samples.DrawingSystem;
using FarseerPhysics.Samples.ScreenSystem;
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

        public Cow(World world, GraphicsDeviceManager graphics, Rectangle destRect, AnimatedSprites currentAnim, AnimatedSprites rightWalk, AnimatedSprites leftWalk, AnimatedSprites downWalk, AnimatedSprites upWalk)
        : base(graphics, destRect, currentAnim, rightWalk, leftWalk, downWalk, upWalk)
        {
            Body = BodyFactory.CreateRectangle(world, 0.54f, 0.49f, 0, new Vector2((float)destRect.X / 100, (float)destRect.X / 100));
        }

        public override Rectangle GetPosition()
        {
            Vector2 vector = ConvertUnits.ToDisplayUnits(Body.Position);
            vector.X -= 27;
            vector.Y -= 27;

            return new Rectangle((int)vector.X, (int)vector.Y, CurrentAnim.SpriteWidth, CurrentAnim.Animation.Height);
        }

        public override void Eat()
        {

        }

        public override void Load(ContentManager content)
        {

        }


        //private KeyboardState prevState = new KeyboardState();
        //private bool isMoving = false;

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.D) || ks.IsKeyDown(Keys.Right))
            {
                CurrentAnim = RightWalk;
                _sourceRect = CurrentAnim.Animate(gameTime, Delay, ObjectMovingType);
            }
            else if (ks.IsKeyDown(Keys.A) || ks.IsKeyDown(Keys.Left))
            {
                CurrentAnim = LeftWalk;
                _sourceRect = CurrentAnim.Animate(gameTime, Delay, ObjectMovingType);
            }
            else if (ks.IsKeyDown(Keys.W) || ks.IsKeyDown(Keys.Up))
            {
                CurrentAnim = UpWalk;
                _sourceRect = CurrentAnim.Animate(gameTime, Delay, ObjectMovingType);
            }
            else if (ks.IsKeyDown(Keys.S) || ks.IsKeyDown(Keys.Down))
            {
                CurrentAnim = DownWalk;
                _sourceRect = CurrentAnim.Animate(gameTime, Delay, ObjectMovingType);
            }
            else
            {
                _sourceRect = new Rectangle(0, 0, CurrentAnim.SpriteWidth, CurrentAnim.Animation.Height);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            Vector2 vector = ConvertUnits.ToDisplayUnits(Body.Position);
            vector.X -= 27;
            vector.Y -= 26;

            spriteBatch.Draw(CurrentAnim.Animation, GetPosition(), _sourceRect, Color.White);
        }

        public void HandleUserAgent(InputHelper input)
        {
            Vector2 force = Vector2.Zero;

            float forceAmount = 4f;
            if (input.KeyboardState.IsKeyDown(Keys.A))
            {
                force += new Vector2(-forceAmount, 0);
            }
            if (input.KeyboardState.IsKeyDown(Keys.S))
            {
                force += new Vector2(0, forceAmount);
            }
            if (input.KeyboardState.IsKeyDown(Keys.D))
            {
                force += new Vector2(forceAmount, 0);
            }
            if (input.KeyboardState.IsKeyDown(Keys.W))
            {
                force += new Vector2(0, -forceAmount);
            }
            if (input.KeyboardState.IsKeyUp(Keys.A) && input.KeyboardState.IsKeyUp(Keys.S) &&
                input.KeyboardState.IsKeyUp(Keys.W) && input.KeyboardState.IsKeyUp(Keys.D))
            {
                Body.Zero();
            }

            Body.ApplyForce(force);
        }
    }
}
