﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using CowFarm.Entities;
using CowFarm.Entities.Animals;
using CowFarm.Enums;
using CowFarm.Interfaces;
using CowFarm.ScreenSystem.AlertWindows;
using CowFarm.Utility;
using CowFarm.Worlds;
using FarseerPhysics.Samples.ScreenSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using World = CowFarm.Worlds.World;

namespace CowFarm.ScreenSystem
{
    public class CowGameScreen : GameScreen
    {
        private readonly ContentManager _contentManager;
        public readonly GraphicsDeviceManager Graphics;

        public List<World> WordlsList { get; private set; }
        private World FirstWorld { get; set; }
        private World SecondWorld { get; set; }
        public World WorldOnFocus { get; private set; }

        private TimeSpan _inGameTime;
        private bool _startNewGame;
        public Cow Cow;
        public Dictionary<string, Texture2D> GameTextures { get; private set; }
        public Dictionary<string, SpriteFont> GameFonts { get; private set; }
        public Dictionary<string, SoundEffect> GameSounds { get; private set; }

        public AlertWindow AlertWindow { get; private set; }

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
                WordlsList.ForEach(world => world.Load(_contentManager));
                WorldOnFocus = SecondWorld;
                CreateCow();
            }
            base.LoadContent();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {

            Debug.WriteLine(new Vector2(Mouse.GetState().X, Mouse.GetState().Y));
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
            Cow.CraftPanel.Draw(ScreenManager.SpriteBatch);
            Cow.ListBars.ForEach(statusBar => statusBar.Draw(ScreenManager.SpriteBatch));
            Cow.Inventory.Draw(ScreenManager.SpriteBatch, GameFonts["gameFont"]);
            if (_onPause)
                ScreenManager.SpriteBatch.Draw(GameTextures["cleanTexture"], new Rectangle(0, 0, Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight), new Color(0, 0, 0, 30));
            AlertWindow?.Draw(ScreenManager.SpriteBatch);
            ScreenManager.SpriteBatch.End();
            base.Draw(gameTime);
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