using System;
using System.Collections.Generic;
using CowFarm.DrowingSystem;
using CowFarm.Interfaces;
using CowFarm.ScreenSystem;
using CowFarm.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.Entities.Animals.NPC
{
    public abstract class Npc : Animal, IAttackable
    {
        protected List<Vector2> WayList;

        protected Vector2 PositionToGo;
        protected Random Rnd;
        protected float SpeedX { get; set; }
        protected float SpeedY { get; set; }

        protected bool HaveWay;
        private bool _standing;
        private TimeSpan _standingTime;

        protected Npc(CowGameScreen cowGameScreen, World world, Rectangle destRect, AnimatedSprites rightWalk, AnimatedSprites leftWalk, AnimatedSprites downWalk, AnimatedSprites upWalk) : base(cowGameScreen, world, destRect, rightWalk, leftWalk, downWalk, upWalk)
        {
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

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(CurrentAnim.Animation, GetPosition(), SourceRect,
                OnFocus ? new Color(209, 209, 224) : Color.White);
        }

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

            if (_standingTime <= TimeSpan.Zero && 2 == Rnd.Next(1000))
            {
                _standingTime += TimeSpan.FromSeconds(Rnd.Next(3, 5));
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

        public bool OnFocus { get; set; }
        public void GetDamage(int damage)
        {
            HealthPoint -= damage;
            if (HealthPoint <= 0)
                CurrentWorld.RemoveDynamicEntity(this);
        }
    }
}