using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CowFarm.DrowingSystem;
using CowFarm.Enums;
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

        public Body Body { get; set; }

        protected Rectangle DestRect;
        protected GraphicsDeviceManager Graphics;

        protected ObjectMovingType ObjectMovingType;

        protected Animal(GraphicsDeviceManager graphics, Rectangle destRect, AnimatedSprites rightWalk,
            AnimatedSprites leftWalk, AnimatedSprites downWalk, AnimatedSprites upWalk)

        {

            this.DestRect = destRect;
            this.RightWalk = rightWalk;
            this.LeftWalk = leftWalk;
            this.DownWalk = downWalk;
            this.UpWalk = upWalk;
            this.Graphics = graphics;
            this.ObjectMovingType = ObjectMovingType.Dynamic;
        }

        public virtual void ChangeWorld(Direction directionOfWorld)
        {
            if (directionOfWorld == Direction.Right)
            {
                Body.Position = new Vector2(0, (float)(GetPosition().Y + 1) / 100);
            }

            if (directionOfWorld == Direction.Left)
            {
                Body.Position = new Vector2((float)(Graphics.PreferredBackBufferWidth + GetPosition().Width - 2) / 100, (float)(GetPosition().Y + 1) / 100);
            }
        }

        public abstract void Eat(IEatable entity);
    }
}
