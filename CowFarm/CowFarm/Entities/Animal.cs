using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CowFarm.DrowingSystem;
using CowFarm.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.Entities
{
    public abstract class Animal : Entity
    {
        public AnimatedSprites RightWalk;
        protected AnimatedSprites LeftWalk;
        protected AnimatedSprites DownWalk;
        protected AnimatedSprites UpWalk;
        protected AnimatedSprites CurrentAnim;

        protected Rectangle DestRect;
        protected GraphicsDeviceManager Graphics;

        protected ObjectMovingType ObjectMovingType;

        protected Animal(World world, GraphicsDeviceManager graphics, Rectangle destRect, AnimatedSprites currentAnim, AnimatedSprites rightWalk,
            AnimatedSprites leftWalk, AnimatedSprites downWalk, AnimatedSprites upWalk)
            
        {
            this.CurrentAnim = currentAnim;
            this.DestRect = destRect;
            this.RightWalk = rightWalk;
            this.LeftWalk = leftWalk;
            this.DownWalk = downWalk;
            this.UpWalk = upWalk;
            this.Graphics = graphics;
            //this.ObjectMovingType = ObjectMovingType.Dynamic;
        }

        public abstract void Eat();
    }
}
