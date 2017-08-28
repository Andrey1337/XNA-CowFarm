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



        private DateTime _currentTime;


        private ScreenManager _screenManager;

        public CowFarmGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.PreferredBackBufferWidth = 900;
            _graphics.PreferredBackBufferHeight = 700;
        }


        protected override void Initialize()
        {
            _currentTime = DateTime.Now;

            _screenManager = new ScreenManager(this);

            CowGameScreen firstWorld = new CowGameScreen(Content, _graphics, GraphicsDevice);
            MenuScreen menuScreen = new MenuScreen("Cow Farm Game");

            menuScreen.AddMenuItem("", EntryType.Separator, null);
            menuScreen.AddMenuItem("Start The Game", EntryType.Screen, firstWorld);
            menuScreen.AddMenuItem("", EntryType.Separator, null);
            menuScreen.AddMenuItem("Exit", EntryType.ExitItem, null);


            Components.Add(_screenManager);

            _screenManager.AddScreen(new BackgroundScreen());
            _screenManager.AddScreen(menuScreen);
            _screenManager.AddScreen(new LogoScreen(TimeSpan.FromSeconds(3.0)));

            base.Initialize();
        }



        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

    }
}