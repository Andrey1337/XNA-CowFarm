using CowFarm.DrowingSystem;
using CowFarm.ScreenSystem;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using World = CowFarm.Worlds.World;

namespace CowFarm.Entities.Plants
{
    public class Bush : Plant
    {
        public Bush(CowGameScreen cowGameScreen, World world, Vector2 position)
            : base(cowGameScreen, new Rectangle((int)position.X, (int)position.Y, 84, 87), new AnimatedSprites(cowGameScreen.GameTextures["bushMovement"], 1, 0))
        {
            CurrentWorld = world;
            CurrentWorld.AddStaticEntity(this);

            float x1 = (float)(DestRect.X + 30) / 100;
            float y = (float)(DestRect.Y + DestRect.Height - 18) / 100;
            float x2 = x1 + (float)24 / 100;

            Body = BodyFactory.CreateEdge(world, new Vector2(x1, y), new Vector2(x2, y));
            Body.BodyTypeName = "bush";
            Body.BodyType = BodyType.Static;
            Body.CollisionCategories = Category.All;
            Body.CollidesWith = Category.All;
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
            return new Rectangle(DestRect.X, DestRect.Y, DestRect.Width, DestRect.Height);
        }
    }
}