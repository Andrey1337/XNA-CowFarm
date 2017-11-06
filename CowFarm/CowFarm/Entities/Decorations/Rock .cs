using CowFarm.DrowingSystem;
using CowFarm.ScreenSystem;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using World = CowFarm.Worlds.World;

namespace CowFarm.Entities.Decorations
{
    public class Rock : Decoration
    {
        public Rock(CowGameScreen cowGameScreen, World world, Vector2 position) :
            base(cowGameScreen, world, new Rectangle((int)position.X, (int)position.Y, 160, 108), new AnimatedSprites(cowGameScreen.GameTextures["rockMovement"], 1, 0))
        {
            world.AddStaticEntity(this);
            float x1 = (float)(DestRect.X + 22) / 100;
            float x2 = (float)(DestRect.X + DestRect.Width - 23) / 100;

            float y1 = (float)(DestRect.Y + DestRect.Height - 30) / 100;
            float y2 = (float)(DestRect.Y + DestRect.Height - 35) / 100;

            Body = BodyFactory.CreateEdge(world, new Vector2(x1, y1), new Vector2(x2, y2));

            x2 = (float)(DestRect.X + DestRect.Width - 50) / 100;
            y2 = (float)(DestRect.Y + DestRect.Height - 20) / 100;

            Body = BodyFactory.CreateEdge(world, new Vector2(x1, y1), new Vector2(x2, y2));

            x1 = x2;
            y1 = y2;
            x2 = (float)(DestRect.X + DestRect.Width - 22) / 100;
            y2 = (float)(DestRect.Y + DestRect.Height - 35) / 100;

            Body = BodyFactory.CreateEdge(world, new Vector2(x1, y1), new Vector2(x2, y2));
            Body.CollidesWith = Category.All;
            Body.CollisionCategories = Category.All;
        }



        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(DecorationMovement.Animation, DestRect, Color.White);
        }

        public override Rectangle GetPosition()
        {
            return new Rectangle(DestRect.X, DestRect.Y, DecorationMovement.SpriteWidth, DecorationMovement.SpriteHeight - 40);
        }
    }
}