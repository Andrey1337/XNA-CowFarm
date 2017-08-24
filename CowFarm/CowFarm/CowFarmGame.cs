using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using CowFarm.Entities;
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
        private SpriteBatch _spriteBatch;

        private Color _backGroundColor;

        private Cow _cow;

        private DateTime _currentTime;

        private SpriteFont _font;

        private FirstWorld _firstWorld;

        private Dictionary<string, Texture2D> _gameTextures;

        public CowFarmGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.PreferredBackBufferWidth = 800;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _backGroundColor = new Color(57, 172, 57);

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _currentTime = DateTime.Now;

            MenuScreen menuScreen = new MenuScreen("Farseer Samples");


            base.Initialize();
        }

        protected override void LoadContent()
        {

            _gameTextures = new Dictionary<string, Texture2D>();
            LoadCow();
            PlantLoad();
            LoadFonts();

            _firstWorld = new FirstWorld(_graphics, new List<Entity>() { _cow }, _gameTextures);


        }

        private void LoadFonts()
        {
            _font = Content.Load<SpriteFont>("gameFont");
        }

        private void LoadCow()
        {
            _gameTextures.Add("cowRightWalk", Content.Load<Texture2D>("cowRightWalk"));
            _gameTextures.Add("cowLeftWalk", Content.Load<Texture2D>("cowLeftWalk"));
            _gameTextures.Add("cowDownWalk", Content.Load<Texture2D>("cowUpWalk"));
            _gameTextures.Add("cowUpWalk", Content.Load<Texture2D>("cowDownWalk"));

            _cow = new Cow(_graphics, new Rectangle(100, 100, 54, 49),
                new AnimatedSprites(_gameTextures["cowRightWalk"], 3, 54, 16),
                new AnimatedSprites(_gameTextures["cowRightWalk"], 3, 54, 16),
                new AnimatedSprites(_gameTextures["cowLeftWalk"], 3, 54, 16),
                new AnimatedSprites(_gameTextures["cowUpWalk"], 3, 54, 16),
                new AnimatedSprites(_gameTextures["cowDownWalk"], 3, 54, 16));
        }
        private void PlantLoad()
        {
            _gameTextures.Add("grassMovement", Content.Load<Texture2D>("grassMovement"));
            _gameTextures.Add("treeMovement", Content.Load<Texture2D>("treeMovement"));
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        protected override void Update(GameTime gameTime)
        {
            _firstWorld.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(_backGroundColor);
            _spriteBatch.Begin();

            _firstWorld.Draw(gameTime, _spriteBatch);

            DrawTime();

            _spriteBatch.End();
            base.Draw(gameTime);
        }

        private void DrawTime()
        {
            var inGametime = DateTime.Now - _currentTime;
            _spriteBatch.DrawString(_font, $"Time: {inGametime.ToString(@"mm\:ss\.ff") }", new Vector2(_graphics.PreferredBackBufferWidth - _graphics.PreferredBackBufferWidth / 5, 0), Color.Black);
            //spriteBatch.DrawString(_font, $"Cow pos x:{_cow.GetPosition().X + _cow.GetPosition().Width} y:{_cow.GetPosition().Y + _cow.GetPosition().Height}", new Vector2(500, 100), Color.AliceBlue);
            //spriteBatch.DrawString(_font, $"Grass pos x:{_grass.GetPosition().X + _grass.GetPosition().Width} y:{_grass.GetPosition().Y + _grass.GetPosition().Height}", new Vector2(500, 150), Color.AliceBlue);
            //_spriteBatch.DrawString(_font, DateTime.Now.ToString(@"mm\:ss\.ff"), new Vector2(500, 150), Color.AliceBlue);
        }
    }
}