﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CowFarm.DrowingSystem;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using World = CowFarm.Worlds.World;

namespace CowFarm.Entities
{
    public abstract class Plant : Entity, IEatable
    {
        
        protected AnimatedSprites PlantMovement;
        protected Rectangle DestRect;
        protected GraphicsDeviceManager Graphics;
        protected Rectangle SourceRect;

        protected ObjectMovingType ObjectMovingType;
        protected int SpriteWidth;


        protected Plant(World world, GraphicsDeviceManager graphics, Rectangle destRect, AnimatedSprites plantMovement)
        {
            this.Graphics = graphics;
            this.DestRect = destRect;
            this.PlantMovement = plantMovement;
            this.ObjectMovingType = ObjectMovingType.Static;
            this.SpriteWidth = plantMovement.SpriteWidth;
        }
        public Texture2D GetTexture()
        {
            return PlantMovement.Animation;
        }
    }
}
