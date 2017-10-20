using CowFarm.DrowingSystem;
using Microsoft.Xna.Framework;
using World = CowFarm.Worlds.World;

namespace CowFarm.Entities
{
    public abstract class Decoration : Entity
    {
        protected AnimatedSprites DecorationMovement;
        protected Rectangle DestRect;
        protected GraphicsDeviceManager Graphics;
        protected Rectangle SourceRect;

        protected ObjectMovingType ObjectMovingType;

        protected Decoration(World world, Rectangle destRect, AnimatedSprites decorationMovement)
        {
            this.Graphics = world.Graphics;
            this.DestRect = destRect;
            this.DecorationMovement = decorationMovement;
            this.ObjectMovingType = ObjectMovingType.Static;
        }

    }
}