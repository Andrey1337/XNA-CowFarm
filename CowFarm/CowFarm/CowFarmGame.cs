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
using LogoScreen = FarseerPhysics.Samples.ScreenSystem.LogoScreen;
using BackgroundScreen = FarseerPhysics.Samples.ScreenSystem.BackgroundScreen;
using EntryType = FarseerPhysics.Samples.ScreenSystem.EntryType;
using MenuScreen = FarseerPhysics.Samples.ScreenSystem.MenuScreen;
using ScreenManager = FarseerPhysics.Samples.ScreenSystem.ScreenManager;
using SimpleDemo1 = FarseerPhysics.Samples.Demos.SimpleDemo1;

namespace CowFarm
{
    public class CowFarmGame : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Color _backGroundColor;

        private Cow _cow;

        private DateTime _currentTime;

        private SpriteFont _font;

        private FirstWorld _firstWorld;

        private Dictionary<string, Texture2D> _gameTextures;

        private ScreenManager _screenManager;

        public CowFarmGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
        }


        protected override void Initialize()
        {
            _backGroundColor = new Color(57, 172, 57);

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _currentTime = DateTime.Now;

            _screenManager = new ScreenManager(this);

            CowGameScreen firstWorld = new CowGameScreen(Content, _graphics, GraphicsDevice);
            SimpleDemo1 demo = new SimpleDemo1();
            MenuScreen menuScreen = new MenuScreen("Cow Farm Game");

            menuScreen.AddMenuItem("", EntryType.Separator, null);
            menuScreen.AddMenuItem("Igra Epta Blya", EntryType.Screen, demo);
            menuScreen.AddMenuItem("Igra Epta Blya", EntryType.Screen, firstWorld);
            menuScreen.AddMenuItem("", EntryType.Separator, null);
            menuScreen.AddMenuItem("Exit", EntryType.ExitItem, null);


            Components.Add(_screenManager);

            _screenManager.AddScreen(new BackgroundScreen());
            _screenManager.AddScreen(menuScreen);
            _screenManager.AddScreen(new LogoScreen(TimeSpan.FromSeconds(3.0)));
            //screenManager.AddScreen(firstWorld);
            base.Initialize();
        }







        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        //protected override void Update(GameTime gameTime)
        //{
        //    _firstWorld.Update(gameTime);
        //    base.Update(gameTime);
        //}

        //protected override void Draw(GameTime gameTime)
        //{
        //    GraphicsDevice.Clear(_backGroundColor);
        //    _spriteBatch.Begin();

        //    _firstWorld.Draw(gameTime, _spriteBatch);

        //    DrawTime();

        //    _spriteBatch.End();
        //    base.Draw(gameTime);
        //}

        //private void DrawTime()
        //{
        //    var inGametime = DateTime.Now - _currentTime;
        //    _spriteBatch.DrawString(_font, $"Time: {inGametime.ToString(@"mm\:ss\.ff") }", new Vector2(_graphics.PreferredBackBufferWidth - _graphics.PreferredBackBufferWidth / 5, 0), Color.Black);
        //    //spriteBatch.DrawString(_font, $"Cow pos x:{_cow.GetPosition().X + _cow.GetPosition().Width} y:{_cow.GetPosition().Y + _cow.GetPosition().Height}", new Vector2(500, 100), Color.AliceBlue);
        //    //spriteBatch.DrawString(_font, $"Grass pos x:{_grass.GetPosition().X + _grass.GetPosition().Width} y:{_grass.GetPosition().Y + _grass.GetPosition().Height}", new Vector2(500, 150), Color.AliceBlue);
        //    //_spriteBatch.DrawString(_font, DateTime.Now.ToString(@"mm\:ss\.ff"), new Vector2(500, 150), Color.AliceBlue);
        //}
    }
}