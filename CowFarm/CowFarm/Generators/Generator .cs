using System;
using System.Collections.Generic;
using CowFarm.DrowingSystem;
using CowFarm.Entities;
using Microsoft.Xna.Framework;

namespace CowFarm.Generators
{
    public abstract class Generator
    {
        protected readonly GraphicsDeviceManager Graphics;
        protected readonly AnimatedSprites AnimatedSpriteMovement;   
             
        protected int ObjectsCounter;
        protected readonly int ObjectsMaxCounter;
        protected readonly Random Random;

        protected Generator(GraphicsDeviceManager graphics, AnimatedSprites animatedSprites,
            int objectsMaxCounter)
        {
            this.Graphics = graphics;
            this.Random = new Random();

            this.AnimatedSpriteMovement = animatedSprites;
            
            this.ObjectsMaxCounter = objectsMaxCounter;
            this.ObjectsCounter = 0;
        }



        public abstract void Generate(List<Entity>[] statiEntities);

    }
}