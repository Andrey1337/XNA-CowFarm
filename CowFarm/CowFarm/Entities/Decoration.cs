using CowFarm.DrowingSystem;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using World = CowFarm.Worlds.World;

namespace CowFarm.Entities
{
    public abstract class Decoration : Entity
    {
        protected AnimatedSprites DecorationMovement;
        protected Rectangle DestRect;
        protected GraphicsDeviceManager Graphics;
        protected Rectangle SourceRect;

        protected Body Body;

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