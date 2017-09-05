﻿using System;
using System.Collections.Generic;
using CowFarm.DrowingSystem;
using CowFarm.Entities;
using CowFarm.Worlds;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Samples;
using FarseerPhysics.Samples.Demos.Prefabs;
using FarseerPhysics.Samples.DrawingSystem;

namespace CowFarm.ScreenSystem
{
    public class FirstWorldScreen : CowGameScreen
    {        
        public FirstWorldScreen(ContentManager contentManager, GraphicsDeviceManager graphics, GraphicsDevice graphicsDevice)
            : base(contentManager, graphics, graphicsDevice) { }

        public override void LoadContent()
        {
            base.LoadContent();

            GameTextures = new Dictionary<string, Texture2D>();
            if (WorldSerialize == null)
            {
                PlantLoad();
                LoadFonts();
                LoadCow();

                World = new FirstWorld(Graphics, GameTextures, ScreenManager, DateTime.Now);

                CreateCow();

                World.AddDynamicEntity(Cow);
            }

            //Rectangle = BodyFactory.CreateRectangle(World, 2, 2, 100);

            //Rectangle.CollisionCategories = Category.Cat2;
            //Rectangle.CollidesWith = Category.Cat1;
            //Rectangle.BodyType = BodyType.Dynamic;

            //RectangleSprite = new Sprite(ScreenManager.Assets.TextureFromShape(Rectangle.FixtureList[0].Shape, MaterialType.Squares, Color.Orange, 1f));


            World.Gravity = Vector2.Zero;
            Border = new Border(World, ScreenManager, Camera);

            Rectangle = BodyFactory.CreateRectangle(World, 5f, 5f, 1f);
            Rectangle.BodyType = BodyType.Dynamic;

            SetUserAgent(Rectangle, 100f, 100f);
            RectangleSprite = new Sprite(ScreenManager.Assets.TextureFromShape(Rectangle.FixtureList[0].Shape, MaterialType.Squares, Color.Orange, 1f));


            World.GameStartedTime = DateTime.Now - World.TimeInTheGame;
        }

        private void CreateCow()
        {
            Cow = new Cow(World, Graphics, new Rectangle(300, 100, 54, 49),
                new AnimatedSprites(GameTextures["cowRightWalk"], 3, 54, 16),
                new AnimatedSprites(GameTextures["cowRightWalk"], 3, 54, 16),
                new AnimatedSprites(GameTextures["cowLeftWalk"], 3, 54, 16),
                new AnimatedSprites(GameTextures["cowUpWalk"], 3, 54, 16),
                new AnimatedSprites(GameTextures["cowDownWalk"], 3, 54, 16));

        }
        private void LoadCow()
        {
            GameTextures.Add("cowRightWalk", ContentManager.Load<Texture2D>("cowRightWalk"));
            GameTextures.Add("cowLeftWalk", ContentManager.Load<Texture2D>("cowLeftWalk"));
            GameTextures.Add("cowDownWalk", ContentManager.Load<Texture2D>("cowUpWalk"));
            GameTextures.Add("cowUpWalk", ContentManager.Load<Texture2D>("cowDownWalk"));


        }
        private void PlantLoad()
        {
            GameTextures.Add("grassMovement", ContentManager.Load<Texture2D>("grassMovement"));
            GameTextures.Add("treeMovement", ContentManager.Load<Texture2D>("treeMovement"));
        }

        private void LoadFonts()
        {
            Font = ContentManager.Load<SpriteFont>("gameFont");
        }       
    }
}