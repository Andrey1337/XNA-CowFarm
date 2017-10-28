using System;
using CowFarm.ScreenSystem;
using FarseerPhysics.Samples.ScreenSystem;
using Microsoft.Xna.Framework;

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
            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 900;

            ScreenManager = new ScreenManager(this);
            Components.Add(ScreenManager);

            FrameRateCounter frameRateCounter = new FrameRateCounter(ScreenManager);
            frameRateCounter.DrawOrder = 101;
            Components.Add(frameRateCounter);
        }

        protected override void Initialize()
        {
            CowGameScreen cowGameScreen = new CowGameScreen(Content, _graphics);

            MenuScreen menuScreen = new MenuScreen("Cow Farm Game");

            menuScreen.AddMenuItem("", EntryType.Separator, null);
            menuScreen.AddMenuItem("Start Game", EntryType.Screen, cowGameScreen);

            menuScreen.AddMenuItem("", EntryType.Separator, null);
            menuScreen.AddMenuItem("Exit", EntryType.ExitItem, null);


            ScreenManager.AddScreen(new BackgroundScreen());
            ScreenManager.AddScreen(menuScreen);
            ScreenManager.AddScreen(new LogoScreen(TimeSpan.FromSeconds(2.0)));

            base.Initialize();
        }


        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

    }
}