using CowFarm.DrowingSystem;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using World = CowFarm.Worlds.World;

namespace CowFarm.Entities
{
    public class Bush : Plant
    {
        public Bush(World world, GraphicsDeviceManager graphics, Rectangle destRect, AnimatedSprites plantMovement)
            : base(graphics, destRect, plantMovement)
        {
            float x1 = (float)(destRect.X + 30) / 100;
            float y = (float)(destRect.Y + destRect.Height - 18) / 100;
            float x2 = x1 + (float)24 / 100;

            Body = BodyFactory.CreateEdge(world, new Vector2(x1, y), new Vector2(x2, y));

            Body.BodyType = BodyType.Static;
            Body.CollisionCategories = Category.All;
            Body.CollidesWith = Category.All;
        }


        public override void Load(ContentManager content)
        {

        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(PlantMovement.Animation, DestRect, Color.White);
        }

        public override Rectangle GetPosition()
        {
            return new Rectangle(DestRect.X, DestRect.Y, PlantMovement.SpriteWidth, PlantMovement.Animation.Height);
        }        
    }
}