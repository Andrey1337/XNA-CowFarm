using System;
using System.Collections.Generic;
using System.IO;
using CowFarm.DrowingSystem;
using CowFarm.Entities;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
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
    public class CowGameScreen : PhysicsGameScreen
    {
        protected readonly ContentManager ContentManager;
        protected readonly GraphicsDeviceManager Graphics;
        protected readonly GraphicsDevice GraphicsDevice;

        protected new World World;

        protected Cow Cow;

        protected Border Border;
        protected Body Rectangle;
        protected Sprite RectangleSprite;


        protected Dictionary<string, Texture2D> GameTextures;
        protected SpriteFont Font;

        protected string WorldSerialize;

        private bool _escapeKeyPressed;

        public CowGameScreen(ContentManager contentManager, GraphicsDeviceManager graphics,
            GraphicsDevice graphicsDevice)
        {
            ContentManager = contentManager;
            Graphics = graphics;
            GraphicsDevice = graphicsDevice;
            HasCursor = true;
            TransitionOnTime = TimeSpan.FromSeconds(0.4);
            TransitionOffTime = TimeSpan.FromSeconds(0.3);
        }


        public override void LoadContent()
        {
            base.LoadContent();


            _escapeKeyPressed = false;
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            if (!coveredByOtherScreen && !otherScreenHasFocus)
            {
                World.Update(gameTime);
            }
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(57, 172, 57));

            ScreenManager.SpriteBatch.Begin();

            World.Draw(gameTime, ScreenManager.SpriteBatch);


            ScreenManager.SpriteBatch.Draw(RectangleSprite.Texture, ConvertUnits.ToDisplayUnits(Rectangle.Position), null, Color.White, Rectangle.Rotation, RectangleSprite.Origin, 1f, SpriteEffects.None, 0f);

            DrawTime();

            ScreenManager.SpriteBatch.End();

            base.Draw(gameTime);
        }

        private TimeSpan _timeKeyEscapeWasPressed;

        private void DrawTime()
        {
            var inGametime = DateTime.Now - World.GameStartedTime;
            if (!_escapeKeyPressed)
            {
                //ScreenManager.SpriteBatch.DrawString(Font, $"Time: {inGametime.ToString(@"mm\:ss\.ff") }", new Vector2(Graphics.PreferredBackBufferWidth - Graphics.PreferredBackBufferWidth / 5, 0), Color.Black);
                _timeKeyEscapeWasPressed = inGametime;
                ScreenManager.SpriteBatch.DrawString(Font, "X: " + Cow.GetPosition().X + " Y: " + Cow.GetPosition().Y, new Vector2(Graphics.PreferredBackBufferWidth - Graphics.PreferredBackBufferWidth / 5, 0), Color.Black);

            }
            else
            {
                ScreenManager.SpriteBatch.DrawString(Font, $"Time: {_timeKeyEscapeWasPressed.ToString(@"mm\:ss\.ff") }", new Vector2(Graphics.PreferredBackBufferWidth - Graphics.PreferredBackBufferWidth / 5, 0), Color.Black);
            }

            //spriteBatch.DrawString(_font, $"Cow pos x:{_cow.GetPosition().X + _cow.GetPosition().Width} y:{_cow.GetPosition().Y + _cow.GetPosition().Height}", new Vector2(500, 100), Color.AliceBlue);
            //spriteBatch.DrawString(_font, $"Grass pos x:{_grass.GetPosition().X + _grass.GetPosition().Width} y:{_grass.GetPosition().Y + _grass.GetPosition().Height}", new Vector2(500, 150), Color.AliceBlue);
            //_spriteBatch.DrawString(_font, DateTime.Now.ToString(@"mm\:ss\.ff"), new Vector2(500, 150), Color.AliceBlue);
        }


        public override void HandleInput(InputHelper input, GameTime gameTime)
        {
            if (input.IsNewKeyPress(Keys.Escape))
            {
                WorldSerialize = Newtonsoft.Json.JsonConvert.SerializeObject(World);

                World.TimeInTheGame = DateTime.Now - World.GameStartedTime;
                _escapeKeyPressed = true;
                ExitScreen();
            }
            base.HandleInput(input, gameTime);
        }







    }
}