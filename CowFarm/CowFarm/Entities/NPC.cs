using System;
using CowFarm.DrowingSystem;
using Microsoft.Xna.Framework;

namespace CowFarm.Entities
{
    public abstract class NPC : Animal
    {
        protected Vector2 PositionToGo;
        private readonly Random _rnd;
        protected float SpeedX { get; set; }
        protected float SpeedY { get; set; }

        protected bool HaveWay;

        protected NPC(GraphicsDeviceManager graphics, Rectangle destRect, AnimatedSprites rightWalk, AnimatedSprites leftWalk, AnimatedSprites downWalk, AnimatedSprites upWalk) : base(graphics, destRect, rightWalk, leftWalk, downWalk, upWalk)
        {
            _rnd = new Random();
            PositionToGo = new Vector2(2, 2);

        }

        protected virtual void ChoseWay()
        {
            int x = _rnd.Next(Graphics.PreferredBackBufferWidth);
            int y = _rnd.Next(Graphics.PreferredBackBufferHeight);

            PositionToGo = new Vector2(x, y);
        }

        protected Vector2 Force;
        protected void GoToPosition()
        {
            Force = new Vector2(0, 0);
            if (GetPosition().X != PositionToGo.X)
            {
                if (GetPosition().X < PositionToGo.X)
                {
                    Force += new Vector2(SpeedX, 0);
                }
                else
                {
                    Force += new Vector2(-SpeedX, 0);
                }
            }
            if (GetPosition().Y != PositionToGo.Y)
            {
                if (GetPosition().Y < PositionToGo.Y)
                {
                    Force += new Vector2(0, SpeedY);
                }
                else
                {
                    Force += new Vector2(0, -SpeedY);
                }
            }
            Body.Move(Force);
            Body.ApplyForce(Force);
        }
    }
}