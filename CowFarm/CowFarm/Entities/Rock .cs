using CowFarm.DrowingSystem;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using World = CowFarm.Worlds.World;

namespace CowFarm.Entities
{
    public class Rock : Decoration
    {
        public Rock(World world, GraphicsDeviceManager graphics, Rectangle destRect, AnimatedSprites decorationMovement) :
            base(graphics, destRect, decorationMovement)
        {
            //float x1 = (float)(destRect.X + 20) / 100;
            //float x2 = (float)(destRect.X + destRect.Width - 12) / 100;

            float x1 = (float)(destRect.X + 22) / 100;
            float x2 = (float)(destRect.X + destRect.Width - 23) / 100;

            float y1 = (float)(destRect.Y + destRect.Height - 30) / 100;
            float y2 = (float)(destRect.Y + destRect.Height - 35) / 100;

            Body = BodyFactory.CreateEdge(world, new Vector2(x1, y1), new Vector2(x2, y2));

            x2 = (float)(destRect.X + destRect.Width - 50) / 100;
            y2 = (float)(destRect.Y + destRect.Height - 20) / 100;

            Body = BodyFactory.CreateEdge(world, new Vector2(x1, y1), new Vector2(x2, y2));

            x1 = x2;
            y1 = y2;
            x2 = (float)(destRect.X + destRect.Width - 22) / 100;
            y2 = (float)(destRect.Y + destRect.Height - 35) / 100;

            Body = BodyFactory.CreateEdge(world, new Vector2(x1, y1), new Vector2(x2, y2));
            Body.CollidesWith = Category.All;
            Body.CollisionCategories = Category.All;
        }

        public override void Load(ContentManager content)
        {

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