﻿using System.Diagnostics;
using CowFarm.DrowingSystem;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CowFarm.Entities
{
    public class Cat : NPC
    {
        private Rectangle _sourceRect;

        private const float Delay = 200f;

        public Cat(World world, GraphicsDeviceManager graphics, Rectangle destRect, AnimatedSprites rightWalk, AnimatedSprites leftWalk, AnimatedSprites downWalk, AnimatedSprites upWalk) : base(graphics, destRect, rightWalk, leftWalk, downWalk, upWalk)
        {
            CurrentAnim = rightWalk;

            Body = BodyFactory.CreateRectangle(world, 0.28f, 0.05f, 0, new Vector2((float)destRect.X / 100, (float)destRect.Y / 100));
            Body.CollisionCategories = Category.All;
            Body.CollidesWith = Category.All;
            Body.BodyType = BodyType.Dynamic;
            SpeedX = 1.5f;
            SpeedY = 1f;
        }

        public override void Load(ContentManager content)
        {

        }

        public override void Update(GameTime gameTime)
        {
            //if (PositionToGo == new Vector2(-1, -1))
            //    ChoseWay();

            //GoToPosition();

            //KeyboardState input = Keyboard.GetState();

            //if (input.IsKeyDown(Keys.A))
            //{
            //    CurrentAnim = LeftWalk;
            //}
            //if (input.IsKeyDown(Keys.D))
            //{
            //    CurrentAnim = RightWalk;
            //}
            //if (input.IsKeyDown(Keys.S))
            //{
            //    CurrentAnim = DownWalk;
            //}
            //if (input.IsKeyDown(Keys.W))
            //{
            //    CurrentAnim = UpWalk;
            //}
            //_sourceRect = CurrentAnim.Animate(gameTime, Delay, ObjectMovingType);
            _sourceRect = new Rectangle(0, 0, CurrentAnim.SpriteWidth, CurrentAnim.SpriteHeight);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(CurrentAnim.Animation, GetPosition(), _sourceRect, Color.White);
        }

        public override Rectangle GetPosition()
        {
            Vector2 vector = ConvertUnits.ToDisplayUnits(Body.Position);
            vector.X -= (float)CurrentAnim.SpriteWidth / 2;
            vector.Y -= (float)CurrentAnim.SpriteHeight / 2;

            return new Rectangle((int)vector.X, (int)vector.Y, CurrentAnim.SpriteWidth, CurrentAnim.Animation.Height);
        }


        public override void Eat(IEatable entity)
        {
            throw new System.NotImplementedException();
        }
    }
}