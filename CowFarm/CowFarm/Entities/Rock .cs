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
        public Body Body;
        public Rock(World world, GraphicsDeviceManager graphics, Rectangle destRect, AnimatedSprites decorationMovement) :
            base(graphics, destRect, decorationMovement)
        {
            Body = BodyFactory.CreateEdge(world, new Vector2(6.03f, 2.3f), new Vector2(7.29f, 2.2f));
        }

        public override void Load(ContentManager content)
        {

        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(DecorationMovement.Animation, DestRect, Color.White);
        }

        public override Rectangle GetPosition()
        {
            return new Rectangle(DestRect.X, DestRect.Y, DecorationMovement.SpriteWidth, DecorationMovement.Animation.Height);
        }
    }
}