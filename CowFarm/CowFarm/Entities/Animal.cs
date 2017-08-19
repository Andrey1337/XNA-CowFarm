﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.Entities
{
    public abstract class Animal : Entity
    {
        //protected readonly Texture2D RightWalk;
        //protected readonly Texture2D LeftWalk;
        //protected readonly Texture2D DownWalk;
        //protected readonly Texture2D UpWalk;
        //protected Texture2D CurrentAnim;

        protected readonly AnimatedSprites RightWalk;
        protected readonly AnimatedSprites LeftWalk;
        protected readonly AnimatedSprites DownWalk;
        protected readonly AnimatedSprites UpWalk;
        protected AnimatedSprites CurrentAnim;

        protected Rectangle DestRect;
        protected GraphicsDeviceManager Graphics;

        protected Animal(GraphicsDeviceManager graphics, Rectangle destRect, AnimatedSprites currentAnim, AnimatedSprites rightWalk,
            AnimatedSprites leftWalk, AnimatedSprites downWalk, AnimatedSprites upWalk)
        {
            this.CurrentAnim = currentAnim;
            this.DestRect = destRect;
            this.RightWalk = rightWalk;
            this.LeftWalk = leftWalk;
            this.DownWalk = downWalk;
            this.UpWalk = upWalk;
            this.Graphics = graphics;
        }

        public abstract void Eat();
    }
}
