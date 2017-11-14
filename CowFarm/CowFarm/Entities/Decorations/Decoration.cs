using CowFarm.DrowingSystem;
using CowFarm.ScreenSystem;
using Microsoft.Xna.Framework;
using World = CowFarm.Worlds.World;

namespace CowFarm.Entities.Decorations
{
    public abstract class Decoration : Entity
    {
        protected StaticAnimatedSprites DecorationMovement;
        protected Rectangle DestRect;       
        protected Rectangle SourceRect;

        protected ObjectMovingType ObjectMovingType;

        protected Decoration(CowGameScreen cowGameScreen, World world, Rectangle destRect, StaticAnimatedSprites decorationMovement) : base(cowGameScreen)
        {            
            this.DestRect = destRect;
            this.DecorationMovement = decorationMovement;
            this.ObjectMovingType = ObjectMovingType.Static;
        }

    }
}