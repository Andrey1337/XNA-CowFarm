using CowFarm.DrowingSystem;
using CowFarm.ScreenSystem;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using World = CowFarm.Worlds.World;

namespace CowFarm.Entities.Plants
{
    internal class OrangeTree : Plant
    {
        public OrangeTree(CowGameScreen cowGameScreen, World world, Vector2 position) : base(cowGameScreen, world, new Rectangle((int)position.X, (int)position.Y, 155, 261), new StaticAnimatedSprites(cowGameScreen.GameTextures["orangeTreeMovement"], 1, 0))
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

        public override Rectangle GetPosition()
        {
            return new Rectangle(DestRect.X, DestRect.Y, PlantMovement.SpriteWidth, PlantMovement.SpriteHeight);
        }
    }
}
