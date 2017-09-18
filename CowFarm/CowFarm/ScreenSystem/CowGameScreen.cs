using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using CowFarm.DrowingSystem;
using CowFarm.Entities;
using CowFarm.Worlds;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
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

        private World _world;

        private Body _rectangle;
        private Sprite _rectangleSprite;

        //Worlds 
        //private World _rightWorld;
        //private World _leftWorld;
        //private World _upWorld;
        //private World _downWorld;

        private Cow _cow;

        private Dictionary<string, Texture2D> _gameTextures;
        private SpriteFont _font;

        private int Score { get; set; }

        private string _worldSerialize;
        private bool _escapeKeyPressed;

        public CowGameScreen(ContentManager contentManager, GraphicsDeviceManager graphics,
            GraphicsDevice graphicsDevice)
        {
            _gameTextures = null;
            _world = null;

            //_rightWorld = null;
            //_leftWorld = null;
            //_upWorld = null;
            //_downWorld = null;

            _contentManager = contentManager;
            _graphics = graphics;
            

            HasCursor = true;

            TransitionOnTime = TimeSpan.FromSeconds(0.7);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }


        public override void LoadContent()
        {
            _escapeKeyPressed = false;
            if (_worldSerialize == null)
            {
                _gameTextures = new Dictionary<string, Texture2D>();
                PlantLoad();
                LoadFonts();
                LoadCow();
                RockLoad();
                _world = new FirstWorld(_graphics, _gameTextures, ScreenManager, DateTime.Now);

                CreateCow();
                _world.AddDynamicEntity(_cow);
                _cow.Body.BodyType = BodyType.Dynamic;
                _cow.Body.CollisionCategories = Category.All;
                _cow.Body.CollidesWith = Category.All;
            }

            _world.GameStartedTime = DateTime.Now - _world.TimeInTheGame;

            base.LoadContent();
        }

        private void CreateCow()
        {
            _cow = new Cow(_world, _graphics, new Rectangle(100, 100, 54, 49), new AnimatedSprites(_gameTextures["cowRightWalk"], 3, 54, 16), new AnimatedSprites(_gameTextures["cowRightWalk"], 3, 54, 16), new AnimatedSprites(_gameTextures["cowLeftWalk"], 3, 54, 16), new AnimatedSprites(_gameTextures["cowUpWalk"], 3, 54, 16), new AnimatedSprites(_gameTextures["cowDownWalk"], 3, 54, 16));
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

            _gameTextures.Add("firstWorldBackGround", _contentManager.Load<Texture2D>("firstWorldBackGround"));

            _gameTextures.Add("bushMovement", _contentManager.Load<Texture2D>("bushMovement"));
        }

        private void RockLoad()
        {
            _gameTextures.Add("rockMovement", _contentManager.Load<Texture2D>("rockMovement"));
        }

        private void LoadFonts()
        {
            _gameTextures.Add("timerTexture", _contentManager.Load<Texture2D>("timerTexture"));
            _font = _contentManager.Load<SpriteFont>("gameFont");
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            if (!coveredByOtherScreen && !otherScreenHasFocus)
            {
                _world.Update(gameTime);
            }
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.SpriteBatch.Begin();

            _world.Draw(gameTime, ScreenManager.SpriteBatch);

            ScreenManager.SpriteBatch.Draw(_gameTextures["timerTexture"], new Vector2(1000, 5), Color.White);
            DrawTime();


            ScreenManager.SpriteBatch.DrawString(_font, "Score: " + _cow.Score, new Vector2(100, 16), Color.Black);
            ScreenManager.SpriteBatch.End();
            base.Draw(gameTime);
        }

        private TimeSpan _timeKeyEscapeWasPressed;

        private void DrawTime()
        {
            var inGametime = DateTime.Now - _world.GameStartedTime;
            if (!_escapeKeyPressed)
            {
                ScreenManager.SpriteBatch.DrawString(_font, inGametime.ToString(@"mm\:ss\.ff"), new Vector2(1080, 16), Color.Black);
                _timeKeyEscapeWasPressed = inGametime;
            }
            else
            {
                ScreenManager.SpriteBatch.DrawString(_font, _timeKeyEscapeWasPressed.ToString(@"mm\:ss\.ff"), new Vector2(1080, 16), Color.Black);
            }
        }


        public override void HandleInput(InputHelper input, GameTime gameTime)
        {
            //_cow.HandleUserAgent(input);

            if (input.IsNewKeyPress(Keys.Escape))
            {
                //_worldSerialize = "Serialized";

                _world.TimeInTheGame = DateTime.Now - _world.GameStartedTime;
                _escapeKeyPressed = true;
                ExitScreen();
            }
            base.HandleInput(input, gameTime);
        }



    }
}