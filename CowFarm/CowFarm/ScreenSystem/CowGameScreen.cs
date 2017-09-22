using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using CowFarm.DrowingSystem;
using CowFarm.Entities;
using CowFarm.Enums;
using CowFarm.Worlds;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Samples;
using FarseerPhysics.Samples.Demos.Prefabs;
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


        //Worlds 
        private World _world { get; set; }
        private World _rightWorld { get; set; }
        public World WorldOnFocus { get; set; }

        //private World _leftWorld;
        //private World _upWorld;
        //private World _downWorld;

        private Cow _cow;

        private Dictionary<string, Texture2D> _gameTextures;
        private SpriteFont _font;

        private string _worldSerialize;
        private bool _escapeKeyPressed;

        public CowGameScreen(ContentManager contentManager, GraphicsDeviceManager graphics,
            GraphicsDevice graphicsDevice)
        {
            _gameTextures = null;
            _world = null;


            _rightWorld = null;
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

                LoadAll();

                _world = new FirstWorld(_graphics, _gameTextures, ScreenManager, DateTime.Now);
                _rightWorld = new SecondWorld(_graphics, _gameTextures, ScreenManager, DateTime.Now);

                _world.RightWorld = _rightWorld;
                _rightWorld.LeftWorld = _world;

                CreateCow();
                _world.AddDynamicEntity(_cow);


                WorldOnFocus = _world;
            }

            _world.GameStartedTime = DateTime.Now - _world.TimeInTheGame;

            base.LoadContent();
        }


        private void LoadAll()
        {
            PlantLoad();
            LoadFonts();
            LoadCow();
            LoadCat();
            RockLoad();
            BackGroundLoad();
        }

        private void CreateCow()
        {
            _cow = new Cow(this, _world, _graphics, new Rectangle(460, 370, 54, 49), new AnimatedSprites(_gameTextures["cowRightWalk"], 3, 54, 16), new AnimatedSprites(_gameTextures["cowLeftWalk"], 3, 54, 16), new AnimatedSprites(_gameTextures["cowUpWalk"], 3, 54, 16), new AnimatedSprites(_gameTextures["cowDownWalk"], 3, 54, 16));
        }

        private void LoadCow()
        {
            _gameTextures.Add("cowRightWalk", _contentManager.Load<Texture2D>("cowRightWalk"));
            _gameTextures.Add("cowLeftWalk", _contentManager.Load<Texture2D>("cowLeftWalk"));
            _gameTextures.Add("cowDownWalk", _contentManager.Load<Texture2D>("cowUpWalk"));
            _gameTextures.Add("cowUpWalk", _contentManager.Load<Texture2D>("cowDownWalk"));
        }

        private void LoadCat()
        {
            _gameTextures.Add("catRightWalk", _contentManager.Load<Texture2D>("catRightWalk"));
            _gameTextures.Add("catLeftWalk", _contentManager.Load<Texture2D>("catLeftWalk"));
            _gameTextures.Add("catDownWalk", _contentManager.Load<Texture2D>("catUpWalk"));
            _gameTextures.Add("catUpWalk", _contentManager.Load<Texture2D>("catDownWalk"));
        }

        private void PlantLoad()
        {
            _gameTextures.Add("grassMovement", _contentManager.Load<Texture2D>("grassMovement"));
            _gameTextures.Add("treeMovement", _contentManager.Load<Texture2D>("treeMovement"));


            _gameTextures.Add("bushMovement", _contentManager.Load<Texture2D>("bushMovement"));
        }

        private void RockLoad()
        {
            _gameTextures.Add("rockMovement", _contentManager.Load<Texture2D>("rockMovement"));
        }

        private void BackGroundLoad()
        {
            _gameTextures.Add("firstWorldBackGround", _contentManager.Load<Texture2D>("firstWorldBackGround"));
            _gameTextures.Add("secondWorldBackGround", _contentManager.Load<Texture2D>("secondWorldBackGround"));

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
                WorldOnFocus.Update(gameTime);
            }
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.SpriteBatch.Begin();

            WorldOnFocus.Draw(gameTime, ScreenManager.SpriteBatch);

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
            if (input.IsNewKeyPress(Keys.Escape))
            {
                //_worldSerialize = "Serialized";

                _world.TimeInTheGame = DateTime.Now - _world.GameStartedTime;
                _escapeKeyPressed = true;
                ExitScreen();
            }
            base.HandleInput(input, gameTime);
        }

        public void ChangeWorld(Animal animal, Direction direction)
        {
            WorldOnFocus.RemoveDynamicEntity(animal);

            switch (direction)
            {
                case Direction.Right:
                    animal.ChangeWorld(Direction.Right);
                    WorldOnFocus.RightWorld.AddDynamicEntity(animal);
                    WorldOnFocus = WorldOnFocus.RightWorld;
                    break;
                case Direction.Left:
                    animal.ChangeWorld(Direction.Left);
                    WorldOnFocus.LeftWorld.AddDynamicEntity(animal);
                    WorldOnFocus = WorldOnFocus.LeftWorld;
                    break;

            }
        }
    }
}