using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using CowFarm.DrowingSystem;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using World = CowFarm.Worlds.World;

namespace CowFarm.Entities
{
    class OrangeTree : Plant
    {
        private const float Delay = float.MaxValue;
        public OrangeTree(World world, GraphicsDeviceManager graphics, Rectangle destRect, Dictionary<string, Texture2D> gameTextures) : base(graphics, destRect, new AnimatedSprites(gameTextures["orangeTreeMovement"], 1, 0))
        {
            world.AddStaticEntity(this);
            float width = (float)14 / 100;
            float height = (float)1 / 100;

            float x = (float)(destRect.X + destRect.Width - 80) / 100;
            float y = (float)(destRect.Y + destRect.Height - 22) / 100;

            Body = BodyFactory.CreateRectangle(world, width, height, 0f, new Vector2(x, y));

            Body.BodyType = BodyType.Static;
            Body.CollisionCategories = Category.All;
            Body.CollidesWith = Category.All;
        }

        public override void Load(ContentManager content)
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            SourceRect = PlantMovement.Animate(gameTime, Delay, ObjectMovingType);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(PlantMovement.Animation, DestRect, SourceRect, Color.White);
        }

        public override Rectangle GetPosition()
        {
            return new Rectangle(DestRect.X, DestRect.Y, PlantMovement.SpriteWidth, PlantMovement.SpriteHeight);
        }
    }
}
