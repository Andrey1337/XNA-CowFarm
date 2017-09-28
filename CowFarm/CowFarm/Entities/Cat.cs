using System.Collections.Generic;
using System.Diagnostics;
using CowFarm.DrowingSystem;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using World = CowFarm.Worlds.World;

namespace CowFarm.Entities
{
    public class Cat : NPC
    {
        private Rectangle _sourceRect;

        private const float Delay = 900f;

        public Cat(World world, Rectangle destRect, Dictionary<string, Texture2D> gameTextures)
            : base(world, destRect,
                  new AnimatedSprites(gameTextures["catRightWalk"], 3, 0),
                  new AnimatedSprites(gameTextures["catLeftWalk"], 3, 0),
                  new AnimatedSprites(gameTextures["catUpWalk"], 3, 0),
                  new AnimatedSprites(gameTextures["catDownWalk"], 3, 0))
        {
            CurrentAnim = RightWalk;
            world.AddDynamicEntity(this);
            Body = BodyFactory.CreateRectangle(world, 0.28f, 0.05f, 0, new Vector2((float)destRect.X / 100, (float)destRect.Y / 100));
            Body.CollisionCategories = Category.All;
            Body.CollidesWith = Category.All;
            Body.BodyType = BodyType.Dynamic;
            SpeedX = 0.8f;
            SpeedY = 0.7f;
        }

        public override void Load(ContentManager content)
        {

        }

        public override void Update(GameTime gameTime)
        {
            GoToPosition(gameTime);

            if (Body.GetVelocity() == Vector2.Zero)
            {
                _sourceRect = new Rectangle(0, 0, CurrentAnim.SpriteWidth, CurrentAnim.Animation.Height);
            }
            else
            {
                if (Force.Y < 0)
                {
                    CurrentAnim = UpWalk;
                }
                if (Force.Y > 0)
                {
                    CurrentAnim = DownWalk;
                }
                if (Force.X > 0)
                {
                    CurrentAnim = RightWalk;
                }
                if (Force.X < 0)
                {
                    CurrentAnim = LeftWalk;
                }
                _sourceRect = CurrentAnim.Animate(gameTime, Delay, ObjectMovingType);
            }

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