using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.Entities
{
    public abstract class Plant : Entity, IEatable
    {
        protected AnimatedSprites GrassMovement;
        protected Rectangle DestRect;

        protected GraphicsDeviceManager Graphics;
        protected Plant(GraphicsDeviceManager graphics, Rectangle destRect, AnimatedSprites grassMovement)
        {
            this.Graphics = graphics;
            this.DestRect = destRect;
            this.GrassMovement = grassMovement;
        }
    }
}
