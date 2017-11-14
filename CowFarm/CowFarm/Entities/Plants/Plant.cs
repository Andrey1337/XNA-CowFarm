using CowFarm.DrowingSystem;
using CowFarm.ScreenSystem;
using Microsoft.Xna.Framework;

namespace CowFarm.Entities.Plants
{
    public abstract class Plant : Entity
    {
        protected GraphicsDeviceManager Graphics;
        protected StaticAnimatedSprites PlantMovement;
        protected Rectangle DestRect;
        protected Rectangle SourceRect;

        protected ObjectMovingType ObjectMovingType;

        protected Plant(CowGameScreen cowGameScreen, Rectangle destRect, StaticAnimatedSprites plantMovement) : base(cowGameScreen)
        {
            this.Graphics = cowGameScreen.Graphics; 
            this.DestRect = destRect;
            this.PlantMovement = plantMovement;
            this.ObjectMovingType = ObjectMovingType.Static;
        }

    }
}
