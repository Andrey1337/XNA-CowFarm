using System.Collections.Generic;
using System.Diagnostics;
using CowFarm.DrowingSystem;
using CowFarm.Worlds;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.Entities
{
    public class BoulderRock : Decoration
    {
        public BoulderRock(World world, Rectangle destRect, Dictionary<string, Texture2D> gameTextures) : base(world, destRect, new AnimatedSprites(gameTextures["boulderRockMovement"], 1, 0))
        {
            float x1 = (float)(destRect.X + 20) / 100;
            float x2 = (float)(destRect.X + destRect.Width - 15) / 100;

            float y1 = (float)(destRect.Y + destRect.Height - 40) / 100;
            float y2 = (float)(destRect.Y + destRect.Height - 45) / 100;

            Body = BodyFactory.CreateEdge(world, new Vector2(x1, y1), new Vector2(x2, y2));
            x1 = (float)(destRect.X + 19) / 100;
            x2 = (float)(destRect.X + 60) / 100;
            y2 = (float)(destRect.Y + destRect.Height - 20) / 100;
            Body = BodyFactory.CreateEdge(world, new Vector2(x1, y1), new Vector2(x2, y2));
            x1 = x2;
            y1 = y2;

            x2 = (float)(destRect.X + destRect.Width - 13) / 100;
            y2 = (float)(destRect.Y + destRect.Height - 45) / 100;
            Body = BodyFactory.CreateEdge(world, new Vector2(x1, y1), new Vector2(x2, y2));
            world.AddStaticEntity(this);
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

            return new Rectangle(DestRect.X, DestRect.Y, DecorationMovement.SpriteWidth, DecorationMovement.SpriteHeight - 55);
        }
    }
}