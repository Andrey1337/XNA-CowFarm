﻿using System;
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
            Body = BodyFactory.CreateRectangle(world, 0.53f, 0.45f, 0, new Vector2(2, 1));
        }

        public override Rectangle GetPosition()
        {
            Vector2 vector = ConvertUnits.ToDisplayUnits(Body.Position);
            vector.X -= 28;
            vector.Y -= 25;

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
                {
                    position.X -= CowSpeed;
                }
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

            //DestRect = new Rectangle((int)position.X, (int)position.Y, CurrentAnim.SpriteWidth, CurrentAnim.SpriteHeight);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            Vector2 vector = ConvertUnits.ToDisplayUnits(Body.Position);

            vector.X -= 28;
            vector.Y -= 26;

            //spriteBatch.Draw(CurrentAnim.Animation, vector, _sourceRect, Color.White);

            spriteBatch.Draw(CurrentAnim.Animation, GetPosition(), _sourceRect, Color.White);

            //spriteBatch.Draw(CurrentAnim.Animation, DestRect, _sourceRect, Color.White);
        }

        public void HandleUserAgent(InputHelper input)
        {
            Vector2 force = Vector2.Zero;
            //torque = 0;
            float forceAmount = 10f * 0.6f;
            if (input.KeyboardState.IsKeyDown(Keys.A))
                force += new Vector2(-forceAmount, 0);
            if (input.KeyboardState.IsKeyDown(Keys.S))
                force += new Vector2(0, forceAmount);
            if (input.KeyboardState.IsKeyDown(Keys.D))
                force += new Vector2(forceAmount, 0);
            if (input.KeyboardState.IsKeyDown(Keys.W))
                force += new Vector2(0, -forceAmount);
            
            Body.ApplyForce(force);            
        }
    }
}
