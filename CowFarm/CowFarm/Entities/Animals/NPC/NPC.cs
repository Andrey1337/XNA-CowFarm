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

        protected bool HaveWay;
        protected bool Standing;
        protected TimeSpan StandingTime;

        protected Npc(CowGameScreen cowGameScreen, World world, Rectangle destRect, DynamicAnimatedSprites rightWalk, DynamicAnimatedSprites leftWalk, DynamicAnimatedSprites upWalk, DynamicAnimatedSprites downWalk) : base(cowGameScreen, world, destRect, rightWalk, leftWalk, upWalk, downWalk)
        {
            HaveWay = false;
            Standing = false;
            DamageAnimationTime = TimeSpan.FromMilliseconds(100);
            InAttack = false;
        }

        public override void Update(GameTime gameTime)
        {
            GoToPosition(gameTime);
            if (InAttack)
            {
                InDamageAnimationTime += gameTime.ElapsedGameTime;
                if (InDamageAnimationTime >= DamageAnimationTime)
                {
                    InDamageAnimationTime = TimeSpan.Zero;
                    InAttack = false;
                }
            }
            if (!Standing)
                base.Update(gameTime);
            else
                SourceRect = new Rectangle(0, 0, CurrentAnim.SpriteWidth, CurrentAnim.Animation.Height);
        }

        private int _numberOfPosition;
        protected virtual void ChoseWay()
        {
            HaveWay = true;
            PositionToGo = WayList[_numberOfPosition];
            if (_numberOfPosition >= WayList.Count - 1)
                _numberOfPosition = 0;
            else
                _numberOfPosition++;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (InAttack)
                spriteBatch.Draw(CurrentAnim.Animation, GetPosition(), SourceRect, new Color(231, 0, 0));
            else
                spriteBatch.Draw(CurrentAnim.Animation, GetPosition(), SourceRect, OnFocus ? new Color(209, 209, 224) : Color.White);
        }

        protected void GoToPosition(GameTime gameTime)
        {
            if (Standing)
            {
                Body.Stop();
                if (StandingTime <= TimeSpan.Zero)
                {
                    Standing = false;
                }
                else
                {
                    StandingTime -= gameTime.ElapsedGameTime;
                }

                return;
            }

            if (StandingTime <= TimeSpan.Zero && 2 == Rnd.Next(1000))
            {
                StandingTime += TimeSpan.FromSeconds(Rnd.Next(3, 5));
                Standing = true;
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
            InAttack = true;
            Standing = true;
            StandingTime = TimeSpan.FromSeconds(2);
            if (HealthPoint <= 0)
                CurrentWorld.RemoveDynamicEntity(this);
        }

        public TimeSpan DamageAnimationTime { get; set; }
        public TimeSpan InDamageAnimationTime { get; set; }
        public bool InAttack { get; set; }
    }
}