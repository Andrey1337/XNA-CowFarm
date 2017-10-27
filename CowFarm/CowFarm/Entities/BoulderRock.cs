using System.Collections.Generic;
using CowFarm.DrowingSystem;
using CowFarm.ScreenSystem;
using CowFarm.Worlds;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.Entities
{
    public class BoulderRock : Decoration
    {
        public BoulderRock(CowGameScreen cowGameScreen, World world, Vector2 position)
            : base(cowGameScreen, world, new Rectangle((int)position.X, (int)position.Y, 140, 115), new AnimatedSprites(cowGameScreen.GameTextures["boulderRockMovement"], 1, 0))
        {
            float x1 = (float)(DestRect.X + 20) / 100;
            float x2 = (float)(DestRect.X + DestRect.Width - 15) / 100;

            float y1 = (float)(DestRect.Y + DestRect.Height - 40) / 100;
            float y2 = (float)(DestRect.Y + DestRect.Height - 45) / 100;

            Body = BodyFactory.CreateEdge(world, new Vector2(x1, y1), new Vector2(x2, y2));
            x1 = (float)(DestRect.X + 19) / 100;
            x2 = (float)(DestRect.X + 60) / 100;
            y2 = (float)(DestRect.Y + DestRect.Height - 20) / 100;
            Body = BodyFactory.CreateEdge(world, new Vector2(x1, y1), new Vector2(x2, y2));
            x1 = x2;
            y1 = y2;

            x2 = (float)(DestRect.X + DestRect.Width - 13) / 100;
            y2 = (float)(DestRect.Y + DestRect.Height - 45) / 100;
            Body = BodyFactory.CreateEdge(world, new Vector2(x1, y1), new Vector2(x2, y2));
            world.AddStaticEntity(this);
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