using CowFarm.DrowingSystem;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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


        protected Decoration(GraphicsDeviceManager graphics, Rectangle destRect, AnimatedSprites decorationMovement)
        {
            this.Graphics = graphics;
            this.DestRect = destRect;
            this.DecorationMovement = decorationMovement;
            this.ObjectMovingType = ObjectMovingType.Static;
        }
    }
}