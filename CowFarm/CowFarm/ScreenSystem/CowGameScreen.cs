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
        public List<World> WordlsList { get; private set; }
        private World FirstWorld { get; set; }
        private World SecondWorld { get; set; }
        public World WorldOnFocus { get; set; }

        private TimeSpan _inGameTime;
        public Cow Cow;

        private Texture2D _sprintTexture;

        public int Score { get; set; }
        public Dictionary<string, Texture2D> GameTextures { get; private set; }
        private SpriteFont _font;

        private string _worldSerialize;
        private bool _escapeKeyPressed;


        public CowGameScreen(ContentManager contentManager, GraphicsDeviceManager graphics)
        {
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
                _inGameTime = new TimeSpan();
                GameTextures = new Dictionary<string, Texture2D>();

                LoadAll();

                FirstWorld = new FirstWorld(this, _graphics, GameTextures, ScreenManager, DateTime.Now);
                SecondWorld = new SecondWorld(this, _graphics, GameTextures, ScreenManager, DateTime.Now);

                FirstWorld.RightWorld = SecondWorld;
                SecondWorld.LeftWorld = FirstWorld;
                WordlsList = new List<World> { FirstWorld, SecondWorld };

                WorldOnFocus = SecondWorld;
                CreateCow();

            }
            FirstWorld.GameStartedTime = DateTime.Now - FirstWorld.TimeInTheGame;

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
            float width = Cow.Boost * 300;

            var rect1 = new Rectangle(6, 848, 304, 20);
            var rect2 = new Rectangle(8, 850, (int)width, 16);

            ScreenManager.SpriteBatch.Draw(GameTextures["sprintBorder"], rect1, Color.White);

            ScreenManager.SpriteBatch.Draw(_sprintTexture, rect2, Color.White);


        }

        private void DrawTime()
        {
            ScreenManager.SpriteBatch.Draw(GameTextures["timerTexture"], new Vector2(1000, 5), Color.White);
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
                //_worldSerialize = "Serialized";

                FirstWorld.TimeInTheGame = DateTime.Now - FirstWorld.GameStartedTime;
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
            LoadFood();
            RockLoad();
            BackGroundLoad();
            LoadButtons();
        }

        private void CreateCow()
        {
            Cow = new Cow(this, WorldOnFocus, new Rectangle(460, 370, 54, 49), GameTextures);
            WorldOnFocus.AddDynamicEntity(Cow);
        }

        private void LoadCow()
        {
            GameTextures.Add("cowRightWalk", _contentManager.Load<Texture2D>("AnimalMovements/cowRightWalk"));
            GameTextures.Add("cowLeftWalk", _contentManager.Load<Texture2D>("AnimalMovements/cowLeftWalk"));
            GameTextures.Add("cowUpWalk", _contentManager.Load<Texture2D>("AnimalMovements/cowUpWalk"));
            GameTextures.Add("cowDownWalk", _contentManager.Load<Texture2D>("AnimalMovements/cowDownWalk"));
        }

        private void LoadCat()
        {
            GameTextures.Add("catRightWalk", _contentManager.Load<Texture2D>("AnimalMovements/catRightWalk"));
            GameTextures.Add("catLeftWalk", _contentManager.Load<Texture2D>("AnimalMovements/catLeftWalk"));
            GameTextures.Add("catDownWalk", _contentManager.Load<Texture2D>("AnimalMovements/catUpWalk"));
            GameTextures.Add("catUpWalk", _contentManager.Load<Texture2D>("AnimalMovements/catDownWalk"));
        }

        private void PlantLoad()
        {
            GameTextures.Add("grassMovement", _contentManager.Load<Texture2D>("Plants/grassMovement"));

            GameTextures.Add("greenTreeMovement", _contentManager.Load<Texture2D>("Plants/greenTreeMovement"));
            GameTextures.Add("orangeTreeMovement", _contentManager.Load<Texture2D>("Plants/orangeTreeMovement"));

            GameTextures.Add("eatenGrassMovement", _contentManager.Load<Texture2D>("Plants/eatenGrassMovement"));

            GameTextures.Add("bushMovement", _contentManager.Load<Texture2D>("Plants/bushMovement"));
            GameTextures.Add("berryBushMovement", _contentManager.Load<Texture2D>("Plants/berryBushMovement"));
        }

        private void RockLoad()
        {
            GameTextures.Add("rockMovement", _contentManager.Load<Texture2D>("DecorationMovements/rockMovement"));
            GameTextures.Add("boulderRockMovement", _contentManager.Load<Texture2D>("DecorationMovements/boulderRockMovement"));
        }

        private void BackGroundLoad()
        {
            GameTextures.Add("firstWorldBackGround", _contentManager.Load<Texture2D>("WorldsBackgrounds/firstWorldBackGround"));
            GameTextures.Add("secondWorldBackGround", _contentManager.Load<Texture2D>("WorldsBackgrounds/secondWorldBackGround"));

        }

        private void LoadFood()
        {
            GameTextures.Add("appleMovement", _contentManager.Load<Texture2D>("Food/appleMovement"));
            GameTextures.Add("eatenAppleMovement", _contentManager.Load<Texture2D>("Food/eatenAppleMovement"));


        }

        private void LoadFonts()
        {
            GameTextures.Add("timerTexture", _contentManager.Load<Texture2D>("timerTexture"));
            _font = _contentManager.Load<SpriteFont>("gameFont");

            _sprintTexture = new Texture2D(_graphics.GraphicsDevice, 1, 1);
            _sprintTexture.SetData(new Color[] { new Color(52, 101, 181), });
            GameTextures.Add("sprintBorder", _contentManager.Load<Texture2D>("sprintBorder"));
        }

        private void LoadButtons()
        {
            GameTextures.Add("eButtonMovement", _contentManager.Load<Texture2D>("eButtonMovement"));
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