using System;
using System.Collections.Generic;
using System.IO;
using CowFarm.DrowingSystem;
using CowFarm.Entities;
using CowFarm.Worlds;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Samples;
using FarseerPhysics.Samples.Demos.Prefabs;
using FarseerPhysics.Samples.DrawingSystem;
using FarseerPhysics.Samples.ScreenSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using World = CowFarm.Worlds.World;

namespace CowFarm.ScreenSystem
{
    public class CowGameScreen : GameScreen
    {
        private readonly ContentManager _contentManager;
        private readonly GraphicsDeviceManager _graphics;
        private readonly GraphicsDevice _graphicsDevice;

        private World _middleWorld;

        //Worlds 
        private World _rightWorld;
        private World _leftWorld;
        private World _upWorld;
        private World _downWorld;


        private Cow _cow;

        private Dictionary<string, Texture2D> _gameTextures;
        private SpriteFont _font;

        private string _worldSerialize;
        private bool _escapeKeyPressed;

        public CowGameScreen(ContentManager contentManager, GraphicsDeviceManager graphics,
            GraphicsDevice graphicsDevice)
        {
            _gameTextures = null;
            _middleWorld = null;

            _rightWorld = null;
            _leftWorld = null;
            _upWorld = null;
            _downWorld = null;

            _contentManager = contentManager;
            _graphics = graphics;
            _graphicsDevice = graphicsDevice;

            HasCursor = true;

            TransitionOnTime = TimeSpan.FromSeconds(0.4);
            TransitionOffTime = TimeSpan.FromSeconds(0.3);
        }


        public override void LoadContent()
        {
            base.LoadContent();

            if (_worldSerialize == null)
            {
                _gameTextures = new Dictionary<string, Texture2D>();
                PlantLoad();
                LoadFonts();
                LoadCow();

                _middleWorld = new FirstWorld(_graphics, _gameTextures, ScreenManager, DateTime.Now);

                CreateCow();

                _middleWorld.AddDynamicEntity(_cow);
            }
            _middleWorld.GameStartedTime = DateTime.Now - _middleWorld.TimeInTheGame;
            _escapeKeyPressed = false;
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            if (!coveredByOtherScreen && !otherScreenHasFocus)
            {
                _middleWorld.Update(gameTime);
            }
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            _graphicsDevice.Clear(new Color(57, 172, 57));

            ScreenManager.SpriteBatch.Begin();

            _middleWorld.Draw(gameTime, ScreenManager.SpriteBatch);

            DrawTime();

            ScreenManager.SpriteBatch.End();

            base.Draw(gameTime);
        }

        private TimeSpan _timeKeyEscapeWasPressed;

        private void DrawTime()
        {
            var inGametime = DateTime.Now - _middleWorld.GameStartedTime;
            if (!_escapeKeyPressed)
            {
                ScreenManager.SpriteBatch.DrawString(_font, $"Time: {inGametime.ToString(@"mm\:ss\.ff") }", new Vector2(_graphics.PreferredBackBufferWidth - _graphics.PreferredBackBufferWidth / 5, 0), Color.Black);
                _timeKeyEscapeWasPressed = inGametime;
                //ScreenManager.SpriteBatch.DrawString(Font, "X: " + Cow.GetPosition().X + " Y: " + Cow.GetPosition().Y, new Vector2(Graphics.PreferredBackBufferWidth - Graphics.PreferredBackBufferWidth / 5, 0), Color.Black);
            }
            else
            {
                ScreenManager.SpriteBatch.DrawString(_font, $"Time: {_timeKeyEscapeWasPressed.ToString(@"mm\:ss\.ff") }", new Vector2(_graphics.PreferredBackBufferWidth - _graphics.PreferredBackBufferWidth / 5, 0), Color.Black);
            }

            //spriteBatch.DrawString(_font, $"Cow pos x:{_cow.GetPosition().X + _cow.GetPosition().Width} y:{_cow.GetPosition().Y + _cow.GetPosition().Height}", new Vector2(500, 100), Color.AliceBlue);
            //spriteBatch.DrawString(_font, $"Grass pos x:{_grass.GetPosition().X + _grass.GetPosition().Width} y:{_grass.GetPosition().Y + _grass.GetPosition().Height}", new Vector2(500, 150), Color.AliceBlue);
            //_spriteBatch.DrawString(_font, DateTime.Now.ToString(@"mm\:ss\.ff"), new Vector2(500, 150), Color.AliceBlue);
        }


        public override void HandleInput(InputHelper input, GameTime gameTime)
        {
            if (input.IsNewKeyPress(Keys.Escape))
            {
                //WorldSerialize = Newtonsoft.Json.JsonConvert.SerializeObject(World);

                _middleWorld.TimeInTheGame = DateTime.Now - _middleWorld.GameStartedTime;
                _escapeKeyPressed = true;
                ExitScreen();
            }
            base.HandleInput(input, gameTime);
        }


        private void CreateCow()
        {
            _cow = new Cow(_middleWorld, _graphics, new Rectangle(100, 100, 54, 49), new AnimatedSprites(_gameTextures["cowRightWalk"], 3, 54, 16), new AnimatedSprites(_gameTextures["cowRightWalk"], 3, 54, 16), new AnimatedSprites(_gameTextures["cowLeftWalk"], 3, 54, 16), new AnimatedSprites(_gameTextures["cowUpWalk"], 3, 54, 16), new AnimatedSprites(_gameTextures["cowDownWalk"], 3, 54, 16));
        }

        private void LoadCow()
        {
            _gameTextures.Add("cowRightWalk", _contentManager.Load<Texture2D>("cowRightWalk"));
            _gameTextures.Add("cowLeftWalk", _contentManager.Load<Texture2D>("cowLeftWalk"));
            _gameTextures.Add("cowDownWalk", _contentManager.Load<Texture2D>("cowUpWalk"));
            _gameTextures.Add("cowUpWalk", _contentManager.Load<Texture2D>("cowDownWalk"));
        }

        private void PlantLoad()
        {
            _gameTextures.Add("grassMovement", _contentManager.Load<Texture2D>("grassMovement"));
            _gameTextures.Add("treeMovement", _contentManager.Load<Texture2D>("treeMovement"));
        }

        private void LoadFonts()
        {
            _font = _contentManager.Load<SpriteFont>("gameFont");
        }

    }
}