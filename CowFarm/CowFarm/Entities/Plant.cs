using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CowFarm.DrowingSystem;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.Entities
{
    public abstract class 
        Plant : Entity
    {
        protected AnimatedSprites PlantMovement;
        protected Rectangle DestRect;
        protected GraphicsDeviceManager Graphics;
        protected Rectangle SourceRect;

        protected Body Body;

        protected ObjectMovingType ObjectMovingType;

        protected Plant(GraphicsDeviceManager graphics, Rectangle destRect, AnimatedSprites plantMovement)
        {
            this.Graphics = graphics;
            this.DestRect = destRect;
            this.PlantMovement = plantMovement;
            this.ObjectMovingType = ObjectMovingType.Static;
        }

    }
}
