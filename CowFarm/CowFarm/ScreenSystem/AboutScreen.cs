using System;
using FarseerPhysics.Samples.ScreenSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CowFarm.ScreenSystem
{
    public class AboutScreen : GameScreen
    {
        private readonly ContentManager _contentManager;
        public readonly GraphicsDeviceManager Graphics;

        private Texture2D _aboutTexture;
        public AboutScreen(ContentManager contentManager, GraphicsDeviceManager graphics)
        {
            _contentManager = contentManager;
            Graphics = graphics;
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        public override void LoadContent()
        {
            _aboutTexture = _contentManager.Load<Texture2D>("about");
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.SpriteBatch.Begin();
            ScreenManager.SpriteBatch.Draw(_aboutTexture, new Rectangle(0, 0, Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight), Color.White);
            ScreenManager.SpriteBatch.End();
        }

        public override void HandleInput(InputHelper input, GameTime gameTime)
        {
            if (input.KeyboardState.IsKeyDown(Keys.Escape))
                ExitScreen();
        }
    }
}