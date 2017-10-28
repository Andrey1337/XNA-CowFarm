using CowFarm.DrowingSystem;
using CowFarm.ScreenSystem;
using Microsoft.Xna.Framework;
using World = CowFarm.Worlds.World;

namespace CowFarm.Entities
{
    public abstract class Decoration : Entity
    {
        protected AnimatedSprites DecorationMovement;
        protected Rectangle DestRect;       
        protected Rectangle SourceRect;

        protected ObjectMovingType ObjectMovingType;

        protected Decoration(CowGameScreen cowGameScreen, World world, Rectangle destRect, AnimatedSprites decorationMovement) : base(cowGameScreen)
        {            
            this.DestRect = destRect;
            this.DecorationMovement = decorationMovement;
            this.ObjectMovingType = ObjectMovingType.Static;
        }

    }
}