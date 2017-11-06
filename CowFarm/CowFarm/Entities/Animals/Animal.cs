using CowFarm.DrowingSystem;
using CowFarm.Interfaces;
using CowFarm.ScreenSystem;
using Microsoft.Xna.Framework;
using World = CowFarm.Worlds.World;

namespace CowFarm.Entities.Animals
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

        protected ObjectMovingType ObjectMovingType;

        protected float Delay;

        protected Animal(CowGameScreen cowGameScreen, World world, Rectangle destRect, AnimatedSprites rightWalk,
            AnimatedSprites leftWalk, AnimatedSprites upWalk, AnimatedSprites downWalk) : base(cowGameScreen)
        {
            this.DestRect = destRect;
            this.RightWalk = rightWalk;
            this.LeftWalk = leftWalk;
            this.UpWalk = upWalk;
            this.DownWalk = downWalk;
            this.ObjectMovingType = ObjectMovingType.Dynamic;
        }


        public abstract void Eat(IEatable food);
    }
}
