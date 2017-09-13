using CowFarm.DrowingSystem;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using World = CowFarm.Worlds.World;

namespace CowFarm.Entities
{
    public class Tree : Plant
    {
        public Body Body;
        private const float Delay = 5000f;

        public Tree(World world, GraphicsDeviceManager graphics, Rectangle destRect, AnimatedSprites treeMovement)
            : base(graphics, destRect, treeMovement)
        {
            //SpriteWidth = treeMovement.SpriteWidth;

            Body = BodyFactory.CreateRectangle(world, 0.4f, 0.05f, 0f,
                new Vector2(((float)(destRect.X + treeMovement.SpriteWidth) / 100) - (float)treeMovement.SpriteWidth / 2.05f / 100,
                ((float)destRect.Y + treeMovement.SpriteHeight) / 100 - 0.2f));

            Body.BodyType = BodyType.Static;
            Body.CollisionCategories = Category.Cat2;
            Body.CollidesWith = Category.Cat1;
        }

        public override void Load(ContentManager content)
        {

        }

        public override void Update(GameTime gameTime)
        {
            SourceRect = PlantMovement.Animate(gameTime, Delay, ObjectMovingType);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(PlantMovement.Animation, DestRect, SourceRect, Color.White);
        }

        public override Rectangle GetPosition()
        {
            return new Rectangle(DestRect.X, DestRect.Y, PlantMovement.SpriteWidth, PlantMovement.Animation.Height);
        }
    }
}