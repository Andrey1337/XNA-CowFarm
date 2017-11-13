﻿using System;
using CowFarm.DrowingSystem;
using CowFarm.Interfaces;
using CowFarm.ScreenSystem;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using World = CowFarm.Worlds.World;

namespace CowFarm.Entities.Animals.NPC
{
    public class Chicken : Npc, IAttackable
    {
        public Chicken(CowGameScreen cowGameScreen, World world, Vector2 position)
            : base(cowGameScreen, world, new Rectangle((int)position.X, (int)position.Y, 56, 46),
            new AnimatedSprites(cowGameScreen.GameTextures["whiteChickenRightWalk"], 3, 0),
            new AnimatedSprites(cowGameScreen.GameTextures["whiteChickenLeftWalk"], 3, 0),
            new AnimatedSprites(cowGameScreen.GameTextures["whiteChickenUpWalk"], 3, 0),
            new AnimatedSprites(cowGameScreen.GameTextures["whiteChickenDownWalk"], 3, 0))
        {
            Body = BodyFactory.CreateRectangle(world, 0.22f, 0.05f, 0, position / 100);
            Body.CollisionCategories = Category.All;
            Body.CollidesWith = Category.All;
            Body.BodyType = BodyType.Dynamic;
            Body.BodyTypeName = "chicken";
            Delay = 400f;
            SpeedX = 0.7f;
            SpeedY = 0.6f;

            Rnd = new Random(2);
            CurrentAnim = RightWalk;
            CurrentWorld = world;
            world.AddDynamicEntity(this);
        }

        public override void Update(GameTime gameTime)
        {
            GoToPosition(gameTime);

            if (Body.GetVelocity() == Vector2.Zero)
            {
                SourceRect = new Rectangle(0, 0, CurrentAnim.SpriteWidth, CurrentAnim.Animation.Height);
            }
            else
            {
                if (Force.Y < 0)
                {
                    CurrentAnim = DownWalk;
                }
                if (Force.Y > 0)
                {
                    CurrentAnim = UpWalk;
                }
                if (Force.X > 0)
                {
                    CurrentAnim = RightWalk;
                }
                if (Force.X < 0)
                {
                    CurrentAnim = LeftWalk;
                }
                SourceRect = CurrentAnim.Animate(gameTime, ObjectMovingType, Delay);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(CurrentAnim.Animation, GetPosition(), SourceRect,
                OnFocus ? new Color(209, 209, 224) : Color.White);
        }

        public override Rectangle GetPosition()
        {
            Vector2 vector = ConvertUnits.ToDisplayUnits(Body.Position);
            vector.X -= (float)CurrentAnim.SpriteWidth / 2;
            vector.Y -= (float)CurrentAnim.SpriteHeight / 2;

            return new Rectangle((int)vector.X, (int)vector.Y, CurrentAnim.SpriteWidth, CurrentAnim.Animation.Height);
        }

        public override void Eat(IEatable food)
        {
            throw new System.NotImplementedException();
        }

        public Vector2 GetAttackPosition()
        {
            return new Vector2(GetPosition().X + GetPosition().Width / 2, GetPosition().Y + (float)(GetPosition().Height / 2));
        }

        public bool OnFocus { get; set; }
    }
}