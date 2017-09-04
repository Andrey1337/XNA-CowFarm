using System;
using System.Collections.Generic;
using CowFarm.DrowingSystem;
using CowFarm.Entities;
using CowFarm.Worlds;
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
        protected World World;

        protected Generator(World world, GraphicsDeviceManager graphics, AnimatedSprites animatedSprites, int objectsMaxCounter)
        {
            Graphics = graphics;
            Random = new Random();
            World = world;
            AnimatedSpriteMovement = animatedSprites;

            ObjectsMaxCounter = objectsMaxCounter;
            ObjectsCounter = 0;
        }



        public virtual void Generate(List<Entity>[] statiEntities) { }
    }
}