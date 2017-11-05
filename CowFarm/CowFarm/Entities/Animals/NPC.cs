using System;
using System.Collections.Generic;
using CowFarm.DrowingSystem;
using CowFarm.Worlds;
using Microsoft.Xna.Framework;

namespace CowFarm.Entities.Animals
{
    public abstract class Npc : Animal
    {
        protected List<Vector2> WayList;

        protected Vector2 PositionToGo;
        private readonly Random _rnd;
        protected float SpeedX { get; set; }
        protected float SpeedY { get; set; }

        protected bool HaveWay;
        private bool _standing;
        private TimeSpan _standingTime;

        protected Npc(CowGameScreen cowGameScreen, World world, Rectangle destRect, AnimatedSprites rightWalk, AnimatedSprites leftWalk, AnimatedSprites downWalk, AnimatedSprites upWalk) : base(cowGameScreen, world, destRect, rightWalk, leftWalk, downWalk, upWalk)
        {
            _rnd = new Random();
            HaveWay = false;
            _standing = false;
            WayList = new List<Vector2> { new Vector2(100, 100), new Vector2(800, 100) };
        }

        private int _index;
        protected virtual void ChoseWay()
        {
            HaveWay = true;
            PositionToGo = WayList[_index];
            if (_index >= WayList.Count - 1)
                _index = 0;
            else
                _index++;
        }
        protected Vector2 Force;

        protected void GoToPosition(GameTime gameTime)
        {
            if (_standing)
            {
                Body.Stop();
                if (_standingTime <= TimeSpan.Zero)
                {
                    _standing = false;
                }
                else
                {
                    _standingTime -= gameTime.ElapsedGameTime;
                }

                return;
            }

            if (_standingTime <= TimeSpan.Zero && 2 == _rnd.Next(1000))
            {
                _standingTime += TimeSpan.FromSeconds(_rnd.Next(3, 5));
                _standing = true;
            }

            int positionX = GetPosition().X;
            int positionY = GetPosition().Y;

            if (Math.Abs(positionX - PositionToGo.X) < 5 && Math.Abs(positionY - PositionToGo.Y) < 5)
            {
                HaveWay = false;
            }

            if (!HaveWay) ChoseWay();

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