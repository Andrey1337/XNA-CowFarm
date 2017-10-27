using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CowFarm.DrowingSystem;
using CowFarm.ScreenSystem;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.Entities
{
    public abstract class Plant : Entity
    {
        protected GraphicsDeviceManager Graphics;
        protected AnimatedSprites PlantMovement;
        protected Rectangle DestRect;
        protected Rectangle SourceRect;

        protected ObjectMovingType ObjectMovingType;

        protected Plant(CowGameScreen cowGameScreen, Rectangle destRect, AnimatedSprites plantMovement) : base(cowGameScreen)
        {
            this.Graphics = cowGameScreen.Graphics; 
            this.DestRect = destRect;
            this.PlantMovement = plantMovement;
            this.ObjectMovingType = ObjectMovingType.Static;
        }

    }
}
