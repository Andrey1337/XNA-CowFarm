using System;
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
    public abstract class Animal : Entity
    {
        protected AnimatedSprites RightWalk;
        protected AnimatedSprites LeftWalk;
        protected AnimatedSprites DownWalk;
        protected AnimatedSprites UpWalk;
        protected AnimatedSprites CurrentAnim;

        public Body Body;

        protected Rectangle DestRect;
        protected GraphicsDeviceManager Graphics;

        protected ObjectMovingType ObjectMovingType;

        protected Animal( GraphicsDeviceManager graphics, Rectangle destRect, AnimatedSprites currentAnim, AnimatedSprites rightWalk,
            AnimatedSprites leftWalk, AnimatedSprites downWalk, AnimatedSprites upWalk)

        {
            this.CurrentAnim = currentAnim;
            this.DestRect = destRect;
            this.RightWalk = rightWalk;
            this.LeftWalk = leftWalk;
            this.DownWalk = downWalk;
            this.UpWalk = upWalk;
            this.Graphics = graphics;
            this.ObjectMovingType = ObjectMovingType.Dynamic;
        }

        public abstract void Eat();
    }
}
