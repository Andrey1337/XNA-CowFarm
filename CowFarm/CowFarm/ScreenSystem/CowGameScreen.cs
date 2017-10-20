using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using CowFarm.DrowingSystem;
using CowFarm.Entities;
using CowFarm.Enums;
using CowFarm.Interfaces;
using CowFarm.Utility;
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

        public int Score { get; set; }
        public Dictionary<string, Texture2D> GameTextures { get; private set; }
        public Dictionary<string, SpriteFont> GameFonts { get; private set; }

        private string _worldSerialize;
        private bool _escapeKeyPressed;

        public CowGameScreen(ContentManager contentManager, GraphicsDeviceManager graphics)
        {
            _contentManager = contentManager;
            _graphics = graphics;
            HasCursor = true;
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        public override void LoadContent()
        {
            _escapeKeyPressed = false;

            _inGameTime = new TimeSpan();

            GameTextures = ResourceLoader.LoadTextures(_contentManager, _graphics.GraphicsDevice);
            GameFonts = ResourceLoader.LoadFonts(_contentManager);

            FirstWorld = new FirstWorld(this, _graphics, GameTextures, ScreenManager);
            SecondWorld = new SecondWorld(this, _graphics, GameTextures, ScreenManager);

            FirstWorld.RightWorld = SecondWorld;
            SecondWorld.LeftWorld = FirstWorld;
            WordlsList = new List<World> { FirstWorld, SecondWorld };

            WorldOnFocus = SecondWorld;
            CreateCow();

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
            Cow.Inventory.Draw(ScreenManager.SpriteBatch, GameFonts["gameFont"]);
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
            ScreenManager.SpriteBatch.DrawString(GameFonts["gameFont"], "Score: " + Score, new Vector2(100, 16), Color.Black);
            ScreenManager.SpriteBatch.DrawString(GameFonts["gameFont"], _inGameTime.ToString(@"mm\:ss\.ff"), new Vector2(1080, 16), Color.Black);
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

                _escapeKeyPressed = true;
                ExitScreen();
            }
            base.HandleInput(input, gameTime);
        }

        private void CreateCow()
        {
            Cow = new Cow(this, WorldOnFocus, new Vector2(460, 370), GameTextures);
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

            //if (dynamic is Item)
            //{

            //}

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