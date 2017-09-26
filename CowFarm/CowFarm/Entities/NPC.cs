using System;
using System.Collections.Generic;
using CowFarm.DrowingSystem;
using Microsoft.Xna.Framework;

namespace CowFarm.Entities
{
    public abstract class NPC : Animal
    {
        protected List<Vector2> WayList;

        protected Vector2 PositionToGo;
        private readonly Random _rnd;
        protected float SpeedX { get; set; }
        protected float SpeedY { get; set; }

        protected bool HaveWay;

        protected NPC(GraphicsDeviceManager graphics, Rectangle destRect, AnimatedSprites rightWalk, AnimatedSprites leftWalk, AnimatedSprites downWalk, AnimatedSprites upWalk) : base(graphics, destRect, rightWalk, leftWalk, downWalk, upWalk)
        {
            _rnd = new Random();
            HaveWay = false;

            WayList = new List<Vector2> { new Vector2(100, 100), new Vector2(500, 100) };
        }

        private int _index;
        protected virtual void ChoseWay()
        {
            //int x = _rnd.Next(Graphics.PreferredBackBufferWidth);
            //int y = _rnd.Next(Graphics.PreferredBackBufferHeight);

            HaveWay = true;
            PositionToGo = WayList[_index];
            if (_index >= WayList.Count - 1)
                _index = 0;
            else
                _index++;
        }

        protected Vector2 Force;

        
        protected void GoToPosition()
        {
            int positionX = GetPosition().X;
            int positionY = GetPosition().Y;

            if (Math.Abs(positionX - PositionToGo.X) < 5 && Math.Abs(positionY - PositionToGo.Y) < 5)
            {
                HaveWay = false;
            }

            if (HaveWay)
            {
                Force = new Vector2(0, 0);
                if (Math.Abs(positionX - PositionToGo.X) >= 5)
                {
                    if (positionX < PositionToGo.X)
                    {
                        Force += new Vector2(SpeedX, 0);
                    }
                    else
                    {
                        Force += new Vector2(-SpeedX, 0);
                    }
                }
                if (Math.Abs(positionY - PositionToGo.Y) >= 5)
                {
                    if (positionY < PositionToGo.Y)
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
}