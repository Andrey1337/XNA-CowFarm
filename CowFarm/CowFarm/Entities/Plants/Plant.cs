using CowFarm.DrowingSystem;
using CowFarm.ScreenSystem;
using CowFarm.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.Entities.Plants
{
    public abstract class Plant : Entity
    {
        protected GraphicsDeviceManager Graphics;
        protected StaticAnimatedSprites PlantMovement;
        protected Rectangle DestRect;
        protected Rectangle SourceRect;

        protected Plant(CowGameScreen cowGameScreen, World world, Rectangle destRect, StaticAnimatedSprites plantMovement) : base(cowGameScreen, world)
        {
            Graphics = cowGameScreen.Graphics;
            DestRect = destRect;
            PlantMovement = plantMovement;
        }

        public override void Update(GameTime gameTime)
        {
            SourceRect = new Rectangle(0, 0, PlantMovement.SpriteWidth, PlantMovement.SpriteHeight);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(PlantMovement.Animation, DestRect, SourceRect, Color.White);
        }
    }
}
