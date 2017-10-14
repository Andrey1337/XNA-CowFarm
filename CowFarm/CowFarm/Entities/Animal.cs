using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CowFarm.DrowingSystem;
using CowFarm.Enums;
using CowFarm.ScreenSystem;
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

        protected Rectangle DestRect;
        protected Rectangle SourceRect;
        protected GraphicsDeviceManager Graphics;

        protected ObjectMovingType ObjectMovingType;

        protected Animal(World world, Rectangle destRect, AnimatedSprites rightWalk,
            AnimatedSprites leftWalk, AnimatedSprites upWalk, AnimatedSprites downWalk)
        {
            this.DestRect = destRect;
            this.RightWalk = rightWalk;
            this.LeftWalk = leftWalk;
            this.UpWalk = upWalk;
            this.DownWalk = downWalk;
            this.Graphics = world.Graphics;
            this.ObjectMovingType = ObjectMovingType.Dynamic;
        }

        //public virtual void ChangeWorld(Direction directionOfWorld)
        //{
        //    if (directionOfWorld == Direction.Right)
        //    {
        //        Body.Position = new Vector2(0, (float)(GetPosition().Y + 1) / 100);
        //    }

        //    if (directionOfWorld == Direction.Left)
        //    {
        //        Body.Position = new Vector2((float)(Graphics.PreferredBackBufferWidth + GetPosition().Width - 100) / 100, (float)(GetPosition().Y + 1) / 100);
        //    }
        //}

        protected Vector2 GetCenterPosition()
        {
            return new Vector2(GetPosition().X + GetPosition().Width / 2, GetPosition().Y + GetPosition().Height / 2);
        }


        public abstract void Eat(IEatable food);
    }
}
