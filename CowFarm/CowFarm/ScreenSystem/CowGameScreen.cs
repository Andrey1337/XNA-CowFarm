using System;
using System.Collections.Generic;
using System.Diagnostics;
using CowFarm.Entities;
using CowFarm.Enums;
using CowFarm.Interfaces;
using CowFarm.ScreenSystem;
using CowFarm.Utility;
using CowFarm.Worlds;
using FarseerPhysics.Samples.ScreenSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using World = CowFarm.Worlds.World;

namespace CowFarm
{
    public class CowGameScreen : GameScreen
    {
        private readonly ContentManager _contentManager;
        public readonly GraphicsDeviceManager Graphics;

        private bool _startNewGame;

        public List<World> WordlsList { get; private set; }

        private World FirstWorld { get; set; }
        private World SecondWorld { get; set; }
        public World WorldOnFocus { get; set; }

        private TimeSpan _inGameTime;
        public Cow Cow;

        public int Score { get; set; }
        public Dictionary<string, Texture2D> GameTextures { get; private set; }
        public Dictionary<string, SpriteFont> GameFonts { get; private set; }
        public Dictionary<string, SoundEffect> GameSounds { get; private set; }

        public AlertWindow AlertWindow;

        private bool _onPause;

        public CowGameScreen(ContentManager contentManager, GraphicsDeviceManager graphics)
        {
            _contentManager = contentManager;
            Graphics = graphics;
            HasCursor = true;
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
            _startNewGame = true;
        }

        public override void LoadContent()
        {
            //_escapeKeyPressed = false;
            _onPause = false;
            if (_startNewGame)
            {
                _inGameTime = new TimeSpan();
                GameTextures = ResourceLoader.LoadTextures(_contentManager, Graphics.GraphicsDevice);
                GameFonts = ResourceLoader.LoadFonts(_contentManager);
                GameSounds = ResourceLoader.LoadSongs(_contentManager);

                _startNewGame = false;
                FirstWorld = new FirstWorld(this);
                SecondWorld = new SecondWorld(this);

                FirstWorld.RightWorld = SecondWorld;
                SecondWorld.LeftWorld = FirstWorld;
                WordlsList = new List<World> { FirstWorld, SecondWorld };

                WorldOnFocus = SecondWorld;
                CreateCow();
            }
            base.LoadContent();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            var mouse = Mouse.GetState();
            var mousePoint = new Vector2(mouse.X, mouse.Y);

            Debug.WriteLine(mousePoint);
            AlertWindow?.Update();
            if (!coveredByOtherScreen && !otherScreenHasFocus && !_onPause)
            {
                _inGameTime += gameTime.ElapsedGameTime;
                Cow.Inventory.Update();
                Cow.CraftPanel.Update();
                WorldOnFocus.Update(gameTime);
            }
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.SpriteBatch.Begin();
            WorldOnFocus.Draw(ScreenManager.SpriteBatch);
            DrawTime();
            DrawSprint();
            Cow.CraftPanel.Draw(ScreenManager.SpriteBatch, GameFonts["gameFont"]);
            Cow.HealthBar.Draw(ScreenManager.SpriteBatch);
            Cow.FoodBar.Draw(ScreenManager.SpriteBatch);
            Cow.Inventory.Draw(ScreenManager.SpriteBatch, GameFonts["gameFont"]);
            if (_onPause)
                ScreenManager.SpriteBatch.Draw(GameTextures["cleanTexture"], new Rectangle(0, 0, Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight), new Color(0, 0, 0, 30));
            AlertWindow?.Draw(ScreenManager.SpriteBatch);
            ScreenManager.SpriteBatch.End();
            base.Draw(gameTime);
        }

        private void DrawSprint()
        {
            float width = Cow.Boost * 300;
            var rect1 = new Rectangle(6, 848, 304, 20);
            var rect2 = new Rectangle(8, 850, (int)width, 16);
            ScreenManager.SpriteBatch.Draw(GameTextures["sprintBorder"], rect1, Color.White);
            ScreenManager.SpriteBatch.Draw(GameTextures["sprintTexture"], rect2, Color.White);
        }

        private void DrawTime()
        {
            ScreenManager.SpriteBatch.Draw(GameTextures["timerTexture"], new Vector2(1000, 5), Color.White);
            ScreenManager.SpriteBatch.DrawString(GameFonts["gameFont"], _inGameTime.ToString(@"mm\:ss\.ff"),
                new Vector2(1080, 16), Color.Black);
        }

        public void FinishGame()
        {
            _onPause = true;
            AlertWindow = new DeadAlertWindow(this);
        }

        public void RestartGame()
        {
            _startNewGame = true;
            AlertWindow = null;
            LoadContent();
        }

        public void ExitToMenu()
        {
            AlertWindow = null;
            ExitScreen();
        }

        public void PauseGame()
        {
            _onPause = true;
            AlertWindow = new MenuWindow(this);
        }

        public void ResumeGame()
        {
            _onPause = false;
            AlertWindow = null;
        }

        public override void HandleInput(InputHelper input, GameTime gameTime)
        {
            if (!_onPause)
            {
                if (input.KeyboardState.IsKeyDown(Keys.Right) && WorldOnFocus.RightWorld != null)
                {
                    WorldOnFocus = WorldOnFocus.RightWorld;

                }
                if (input.KeyboardState.IsKeyDown(Keys.Left) && WorldOnFocus.LeftWorld != null)
                {
                    WorldOnFocus = WorldOnFocus.LeftWorld;
                }
            }
            if (input.IsNewKeyPress(Keys.Escape))
            {
                if (AlertWindow == null)
                {
                    PauseGame();
                }
                else if (AlertWindow is MenuWindow)
                {
                    ResumeGame();
                }
            }
            //base.HandleInput(input, gameTime);
        }

        private void CreateCow()
        {
            Cow = new Cow(this, WorldOnFocus, new Vector2(530, 400));
            WorldOnFocus.AddDynamicEntity(Cow);
        }

        public void ChangeWorld(IDynamic dynamic, Direction direction)
        {
            switch (direction)
            {
                case Direction.Right:
                    WorldOnFocus.RemoveDynamicEntity((Entity)dynamic);
                    dynamic.ChangeWorld(WorldOnFocus.RightWorld, direction);
                    WorldOnFocus.RightWorld.AddDynamicEntity((Entity)dynamic);
                    break;

                case Direction.Left:
                    WorldOnFocus.RemoveDynamicEntity((Entity)dynamic);
                    dynamic.ChangeWorld(WorldOnFocus.LeftWorld, direction);
                    WorldOnFocus.LeftWorld.AddDynamicEntity((Entity)dynamic);
                    break;
            }

            if (!(dynamic is Cow)) return;
            switch (direction)
            {
                case Direction.Right:
                    WorldOnFocus = WorldOnFocus.RightWorld;
                    break;

                case Direction.Left:
                    WorldOnFocus = WorldOnFocus.LeftWorld;
                    break;

            }
        }
    }
}