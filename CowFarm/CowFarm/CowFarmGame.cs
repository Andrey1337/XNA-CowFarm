using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using CowFarm.Entities;
using CowFarm.ScreenSystem;
using CowFarm.Worlds;
using FarseerPhysics.Samples.ScreenSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CowFarm
{
    public class CowFarmGame : Game
    {
        private readonly GraphicsDeviceManager _graphics;

        public ScreenManager ScreenManager { get; set; }

        public CowFarmGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.PreferredBackBufferWidth = 900;
            _graphics.PreferredBackBufferHeight = 700;

            ScreenManager = new ScreenManager(this);
            Components.Add(ScreenManager);

            //FrameRateCounter frameRateCounter = new FrameRateCounter(_screenManager);
            //frameRateCounter.DrawOrder = 101;
            //Components.Add(frameRateCounter);
        }

        protected override void Initialize()
        {
            CowGameScreen firstWorld = new CowGameScreen(Content, _graphics, GraphicsDevice);

            MenuScreen menuScreen = new MenuScreen("Cow Farm Game");

            menuScreen.AddMenuItem("", EntryType.Separator, null);
            menuScreen.AddMenuItem("Start Game", EntryType.Screen, firstWorld);

            menuScreen.AddMenuItem("", EntryType.Separator, null);
            menuScreen.AddMenuItem("Exit", EntryType.ExitItem, null);


            ScreenManager.AddScreen(new BackgroundScreen());
            ScreenManager.AddScreen(menuScreen);
            ScreenManager.AddScreen(new LogoScreen(TimeSpan.FromSeconds(3.0)));

            base.Initialize();
        }


        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

    }
}