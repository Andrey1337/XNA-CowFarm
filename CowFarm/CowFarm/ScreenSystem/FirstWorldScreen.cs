using System;
using System.Collections.Generic;
using CowFarm.DrowingSystem;
using CowFarm.Entities;
using CowFarm.Worlds;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Samples;

namespace CowFarm.ScreenSystem
{
    public class FirstWorldScreen : CowGameScreen
    {
        private Cow _cow;
        private Grass _grass;
        public FirstWorldScreen(ContentManager contentManager, GraphicsDeviceManager graphics, GraphicsDevice graphicsDevice)
            : base(contentManager, graphics, graphicsDevice)
        {

        }

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
                World.AddDynamicEntity(_cow);
            }



            World.GameStartedTime = DateTime.Now - World.TimeInTheGame;
        }

        private void CreateCow()
        {
            _cow = new Cow(World, Graphics, new Rectangle(100, 100, 54, 49),
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