using System.Collections.Generic;
using CowFarm.DrowingSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.Entities
{
    public class BerryBush : Plant
    {
        public BerryBush(GraphicsDeviceManager graphics, Rectangle destRect, Dictionary<string, Texture2D> gameTextures)
            : base(graphics, destRect, new AnimatedSprites(gameTextures["berryBushMovement"], 1, 0))
        {
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
            return new Rectangle(DestRect.X, DestRect.Y, DestRect.Width, DestRect.Height);
        }
    }
}