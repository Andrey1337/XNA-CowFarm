using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private List<Entity>[] _staticEntities;

        private Rectangle _sourceRect;

        private const float Delay = 200f;

        public const float CowSpeed = 2f;

        public Cow(World world, GraphicsDeviceManager graphics, Rectangle destRect, AnimatedSprites currentAnim, AnimatedSprites rightWalk, AnimatedSprites leftWalk, AnimatedSprites downWalk, AnimatedSprites upWalk)
        : base(graphics, destRect, currentAnim, rightWalk, leftWalk, downWalk, upWalk)
        {
            //Body = BodyFactory.CreateRectangle(world, 0.54f, 0.24f, 0, new Vector2((float)destRect.X / 100, (float)destRect.X / 100));
            Body = BodyFactory.CreateRectangle(world, 0.54f, 0.15f, 0, new Vector2((float)destRect.X / 100, (float)destRect.Y / 100));
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

        public void SetStaticEntity(List<Entity>[] staticEntities)
        {
            _staticEntities = staticEntities;
        }

        //public bool NearbyFood()
        //{
        //    int cowX = GetPosition().X + (int)GetPosition().Width / 2;
        //    int cowY = GetPosition().Y + (int)GetPosition().Height / 2;

        //    int distanceX = 0;
        //    int distanceY = 0;

        //    Rectangle rectangle = new Rectangle(0, 0, 0, 0);
        //    if (CurrentAnim == RightWalk)
        //    {
        //        rectangle = new Rectangle(GetPosition().X + GetPosition().Width, GetPosition().Y, 40, 40);
        //    }
        //    if (CurrentAnim == LeftWalk)
        //    {
        //        rectangle = new Rectangle(GetPosition().X - 40, GetPosition().Y, 40, 40);
        //    }
        //    if (CurrentAnim == UpWalk)
        //    {
        //        rectangle = new Rectangle(GetPosition().X, GetPosition().Y - 40, 40, 40);
        //    }
        //    if (CurrentAnim == DownWalk)
        //    {
        //        rectangle = new Rectangle(GetPosition().X, GetPosition().Y + GetPosition().Height, 40, 40);
        //    }
        //    //int i = 5;
        //    for (int i = rectangle.X; i < rectangle.X + rectangle.Width; i++)
        //    {
        //        if (i < 0)
        //            continue;
        //        for (int j = rectangle.Y; j < rectangle.X + rectangle.Width; j++)
        //        {
        //            if(j < 0)
        //                continue;
        //            if(_staticEntities[i][j].)
        //        }
        //    }
        //    return false;
        //}

        public override void Load(ContentManager content)
        {

        }
        private KeyboardState previousKs = Keyboard.GetState();
        public override void Update(GameTime gameTime)
        {
            HandleUserAgent();
            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.D) || ks.IsKeyDown(Keys.Right))
            {
                if (_force.X == 0)
                {
                    if (CurrentAnim == RightWalk)
                        _sourceRect = new Rectangle(0, 0, CurrentAnim.SpriteWidth, CurrentAnim.Animation.Height);
                    else
                    {
                        if (ks.IsKeyDown(Keys.A))
                        {
                            CurrentAnim = LeftWalk;
                            _sourceRect = new Rectangle(0, 0, CurrentAnim.SpriteWidth, CurrentAnim.Animation.Height);
                        }
                    }
                }
                else
                {
                    CurrentAnim = RightWalk;
                    _sourceRect = CurrentAnim.Animate(gameTime, Delay, ObjectMovingType);
                }
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

        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(CurrentAnim.Animation, GetPosition(), _sourceRect, Color.White);
        }

        Vector2 _force = new Vector2(0, 0);
        public void HandleUserAgent()
        {
            _force = new Vector2(0, 0);

            float forceAmountX = 1.5f;
            float forceAmountY = 1f;

            KeyboardState input = Keyboard.GetState();

            if (input.IsKeyDown(Keys.D))
            {
                _force += new Vector2(forceAmountX, 0);
            }
            if (input.IsKeyDown(Keys.A))
            {
                _force += new Vector2(-forceAmountX, 0);
            }
            if (input.IsKeyDown(Keys.W))
            {
                _force += new Vector2(0, -forceAmountY);
            }
            if (input.IsKeyDown(Keys.S))
            {
                _force += new Vector2(0, forceAmountY);
            }

            if (input.IsKeyUp(Keys.A) && input.IsKeyUp(Keys.S) &&
               input.IsKeyUp(Keys.W) && input.IsKeyUp(Keys.D))
            {
                Body.Stop();
            }

            Body.Move(_force);
            Body.ApplyForce(_force);
        }
    }
}
