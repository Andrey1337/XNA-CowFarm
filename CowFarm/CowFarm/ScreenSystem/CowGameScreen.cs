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
        private World RightWorld { get; set; }
        public World WorldOnFocus { get; set; }
        //private World _upWorld;
        //private World _downWorld;
        private TimeSpan _inGameTime;
        private Cow _cow;



        private Texture2D _sprintTexture;

        public int Score { get; set; }

        private Dictionary<string, Texture2D> _gameTextures;
        private SpriteFont _font;

        private string _worldSerialize;
        private bool _escapeKeyPressed;


        public CowGameScreen(ContentManager contentManager, GraphicsDeviceManager graphics)
        {
            _gameTextures = null;
            _world = null;
            _inGameTime = new TimeSpan();

            RightWorld = null;
            //_leftWorld = null;
            //_upWorld = null;
            //_downWorld = null;

            _contentManager = contentManager;
            _graphics = graphics;


            HasCursor = false;

            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }


        public override void LoadContent()
        {
            _escapeKeyPressed = false;
            if (_worldSerialize == null)
            {
                _gameTextures = new Dictionary<string, Texture2D>();

                LoadAll();

                _world = new FirstWorld(this, _graphics, _gameTextures, ScreenManager, DateTime.Now);
                RightWorld = new SecondWorld(this, _graphics, _gameTextures, ScreenManager, DateTime.Now);

                _world.RightWorld = RightWorld;
                RightWorld.LeftWorld = _world;




                WorldOnFocus = RightWorld;
                CreateCow();

            }

            _world.GameStartedTime = DateTime.Now - _world.TimeInTheGame;

            base.LoadContent();
        }
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {

            if (!coveredByOtherScreen && !otherScreenHasFocus && !_escapeKeyPressed)
            {
                _inGameTime += gameTime.ElapsedGameTime;

                WorldOnFocus.Update(gameTime);

            }
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.SpriteBatch.Begin();

            WorldOnFocus.Draw(gameTime, ScreenManager.SpriteBatch);

            DrawTime();

            DrawSprint();

            ScreenManager.SpriteBatch.End();
            base.Draw(gameTime);
        }

        private void DrawSprint()
        {
            float width = _cow.Boost * 300;

            var rect1 = new Rectangle(6, 848, 304, 20);
            var rect2 = new Rectangle(8, 850, (int)width, 16);

            ScreenManager.SpriteBatch.Draw(_gameTextures["sprintBorder"], rect1, Color.White);

            ScreenManager.SpriteBatch.Draw(_sprintTexture, rect2, Color.White);


        }

        private void DrawTime()
        {
            ScreenManager.SpriteBatch.Draw(_gameTextures["timerTexture"], new Vector2(1000, 5), Color.White);
            ScreenManager.SpriteBatch.DrawString(_font, "Score: " + Score, new Vector2(100, 16), Color.Black);
            ScreenManager.SpriteBatch.DrawString(_font, _inGameTime.ToString(@"mm\:ss\.ff"), new Vector2(1080, 16), Color.Black);
        }



        public override void HandleInput(InputHelper input, GameTime gameTime)
        {
            if (input.KeyboardState.IsKeyDown(Keys.Right) && WorldOnFocus.RightWorld != null)
            {
                WorldOnFocus = WorldOnFocus.RightWorld;
            }
            if (input.KeyboardState.IsKeyDown(Keys.Left) && WorldOnFocus.LeftWorld != null)
            {
                WorldOnFocus = WorldOnFocus.LeftWorld;
            }

            if (input.IsNewKeyPress(Keys.Escape))
            {
                _worldSerialize = "Serialized";

                _world.TimeInTheGame = DateTime.Now - _world.GameStartedTime;
                _escapeKeyPressed = true;
                ExitScreen();
            }
            base.HandleInput(input, gameTime);
        }


        private void LoadAll()
        {
            PlantLoad();
            LoadFonts();
            LoadCow();
            LoadCat();
            RockLoad();
            BackGroundLoad();
            LoadButtons();
        }

        private void CreateCow()
        {
            _cow = new Cow(this, WorldOnFocus, new Rectangle(460, 370, 54, 49), _gameTextures);
            WorldOnFocus.AddDynamicEntity(_cow);
        }

        private void LoadCow()
        {
            _gameTextures.Add("cowRightWalk", _contentManager.Load<Texture2D>("AnimalMovements/cowRightWalk"));
            _gameTextures.Add("cowLeftWalk", _contentManager.Load<Texture2D>("AnimalMovements/cowLeftWalk"));
            _gameTextures.Add("cowUpWalk", _contentManager.Load<Texture2D>("AnimalMovements/cowUpWalk"));
            _gameTextures.Add("cowDownWalk", _contentManager.Load<Texture2D>("AnimalMovements/cowDownWalk"));
        }

        private void LoadCat()
        {
            _gameTextures.Add("catRightWalk", _contentManager.Load<Texture2D>("AnimalMovements/catRightWalk"));
            _gameTextures.Add("catLeftWalk", _contentManager.Load<Texture2D>("AnimalMovements/catLeftWalk"));
            _gameTextures.Add("catDownWalk", _contentManager.Load<Texture2D>("AnimalMovements/catUpWalk"));
            _gameTextures.Add("catUpWalk", _contentManager.Load<Texture2D>("AnimalMovements/catDownWalk"));
        }

        private void PlantLoad()
        {
            _gameTextures.Add("grassMovement", _contentManager.Load<Texture2D>("Plants/grassMovement"));

            _gameTextures.Add("greenTreeMovement", _contentManager.Load<Texture2D>("Plants/greenTreeMovement"));
            _gameTextures.Add("orangeTreeMovement", _contentManager.Load<Texture2D>("Plants/orangeTreeMovement"));

            _gameTextures.Add("eatenGrassMovement", _contentManager.Load<Texture2D>("Plants/eatenGrassMovement"));

            _gameTextures.Add("bushMovement", _contentManager.Load<Texture2D>("Plants/bushMovement"));
            _gameTextures.Add("berryBushMovement", _contentManager.Load<Texture2D>("Plants/berryBushMovement"));
        }

        private void RockLoad()
        {
            _gameTextures.Add("rockMovement", _contentManager.Load<Texture2D>("DecorationMovements/rockMovement"));
            _gameTextures.Add("boulderRockMovement", _contentManager.Load<Texture2D>("DecorationMovements/boulderRockMovement"));
        }

        private void BackGroundLoad()
        {
            _gameTextures.Add("firstWorldBackGround", _contentManager.Load<Texture2D>("WorldsBackgrounds/firstWorldBackGround"));
            _gameTextures.Add("secondWorldBackGround", _contentManager.Load<Texture2D>("WorldsBackgrounds/secondWorldBackGround"));

        }

        private void LoadFonts()
        {
            _gameTextures.Add("timerTexture", _contentManager.Load<Texture2D>("timerTexture"));
            _font = _contentManager.Load<SpriteFont>("gameFont");

            _sprintTexture = new Texture2D(_graphics.GraphicsDevice, 1, 1);
            _sprintTexture.SetData(new Color[] { new Color(52, 101, 181), });
            _gameTextures.Add("sprintBorder", _contentManager.Load<Texture2D>("sprintBorder"));
        }

        private void LoadButtons()
        {
            _gameTextures.Add("eButtonMovement", _contentManager.Load<Texture2D>("eButtonMovement"));
        }

        public void ChangeWorld(Animal animal, Direction direction)
        {
            switch (direction)
            {
                case Direction.Right:

                    WorldOnFocus.RemoveDynamicEntity(animal);
                    animal.ChangeWorld(Direction.Right);
                    WorldOnFocus.RightWorld.AddDynamicEntity(animal);
                    WorldOnFocus = WorldOnFocus.RightWorld;

                    break;
                case Direction.Left:

                    WorldOnFocus.RemoveDynamicEntity(animal);
                    animal.ChangeWorld(Direction.Left);
                    WorldOnFocus.LeftWorld.AddDynamicEntity(animal);
                    WorldOnFocus = WorldOnFocus.LeftWorld;

                    break;

            }
        }
    }
}