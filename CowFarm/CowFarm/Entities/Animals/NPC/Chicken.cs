using System;
using System.Collections.Generic;
using CowFarm.DrowingSystem;
using CowFarm.Entities.Items;
using CowFarm.Interfaces;
using CowFarm.ScreenSystem;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using World = CowFarm.Worlds.World;

namespace CowFarm.Entities.Animals.NPC
{
    public class Chicken : Npc
    {
        public Chicken(CowGameScreen cowGameScreen, World world, float healthPoint, Vector2 position)
            : base(cowGameScreen, world, healthPoint, new Rectangle((int)position.X, (int)position.Y, 56, 46),
            new DynamicAnimatedSprites(cowGameScreen.GameTextures["whiteChickenRightWalk"], 3, 0),
            new DynamicAnimatedSprites(cowGameScreen.GameTextures["whiteChickenLeftWalk"], 3, 0),
            new DynamicAnimatedSprites(cowGameScreen.GameTextures["whiteChickenUpWalk"], 3, 0),
            new DynamicAnimatedSprites(cowGameScreen.GameTextures["whiteChickenDownWalk"], 3, 0))
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
            WayList = new List<Vector2> { new Vector2(100, 500), new Vector2(800, 500) };
        }

        protected override void Die()
        {
            CurrentWorld.RemoveDynamicEntity(this);
            new ChickenLeg(CowGameScreen).Drop(CurrentWorld, new Vector2(GetPosition().X + GetPosition().Width / 2f, GetPosition().Y + GetPosition().Height / 2f));
        }
    }
}