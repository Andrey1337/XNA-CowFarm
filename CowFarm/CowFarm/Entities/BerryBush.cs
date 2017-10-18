using System.Collections.Generic;
using CowFarm.DrowingSystem;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using World = CowFarm.Worlds.World;

namespace CowFarm.Entities
{
    public class BerryBush : Plant
    {
        public BerryBush(World world, GraphicsDeviceManager graphics, Vector2 position, IDictionary<string, Texture2D> gameTextures)
            : base(graphics, new Rectangle((int)position.X, (int)position.Y, 130, 120), new AnimatedSprites(gameTextures["berryBushMovement"], 1, 0))
        {
            float x1 = (float)(DestRect.X + 47) / 100;
            float y = (float)(DestRect.Y + DestRect.Height - 17) / 100;

            float x2 = (float)(DestRect.Width + DestRect.X - 60) / 100;

            Body = BodyFactory.CreateEdge(world, new Vector2(x1, y), new Vector2(x2, y));
            world.AddStaticEntity(this);
        }

        public override void Load(ContentManager content)
        {

        }

        public override void Update(GameTime gameTime)
        {
            SourceRect = new Rectangle(0, 0, PlantMovement.SpriteWidth, PlantMovement.SpriteHeight);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(PlantMovement.Animation, DestRect, Color.White);
        }

        public override Rectangle GetPosition()
        {
            return new Rectangle(DestRect.X, DestRect.Y, DestRect.Width, DestRect.Height + 1);
        }
    }
}