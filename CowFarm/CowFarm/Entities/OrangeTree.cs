using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using CowFarm.DrowingSystem;
using CowFarm.ScreenSystem;
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
        public OrangeTree(CowGameScreen cowGameScreen, World world, Vector2 position) : base(cowGameScreen, new Rectangle((int)position.X, (int)position.Y, 155, 261), new AnimatedSprites(cowGameScreen.GameTextures["orangeTreeMovement"], 1, 0))
        {
            world.AddStaticEntity(this);
            float width = (float)14 / 100;
            float height = (float)1 / 100;
            float x = (float)(DestRect.X + DestRect.Width - 80) / 100;
            float y = (float)(DestRect.Y + DestRect.Height - 22) / 100;
            Body = BodyFactory.CreateRectangle(world, width, height, 0f, new Vector2(x, y));

            Body.BodyType = BodyType.Static;
            Body.CollisionCategories = Category.All;
            Body.CollidesWith = Category.All;
        }

        public override void Update(GameTime gameTime)
        {
            SourceRect = PlantMovement.Animate(gameTime, ObjectMovingType);
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
