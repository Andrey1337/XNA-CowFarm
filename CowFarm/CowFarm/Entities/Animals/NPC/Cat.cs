using System;
using System.Collections.Generic;
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
    public class Cat : Npc
    {
        public Cat(CowGameScreen cowGameScreen, World world, Vector2 position)
            : base(cowGameScreen, world, new Rectangle((int)position.X, (int)position.Y, 56, 46),
                  new DynamicAnimatedSprites(cowGameScreen.GameTextures["catRightWalk"], 3, 0),
                  new DynamicAnimatedSprites(cowGameScreen.GameTextures["catLeftWalk"], 3, 0),
                  new DynamicAnimatedSprites(cowGameScreen.GameTextures["catUpWalk"], 3, 0),
                  new DynamicAnimatedSprites(cowGameScreen.GameTextures["catDownWalk"], 3, 0))
        {
            Rnd = new Random(100);
            CurrentWorld = world;
            CurrentAnim = RightWalk;

            Body = BodyFactory.CreateRectangle(CurrentWorld, 0.28f, 0.05f, 0, position / 100);
            Body.CollisionCategories = Category.All;
            Body.CollidesWith = Category.All;
            Body.BodyType = BodyType.Dynamic;
            Body.BodyTypeName = "cat";
            Delay = 500f;
            SpeedX = 0.8f;
            SpeedY = 0.7f;
            CurrentWorld.AddDynamicEntity(this);
            WayList = new List<Vector2> { new Vector2(100, 100), new Vector2(800, 100) };
        }
    }
}