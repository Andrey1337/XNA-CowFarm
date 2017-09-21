using System;
using System.Collections.Generic;
using FarseerPhysics.Samples.ScreenSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.Worlds
{
    public class SecondWorld : World
    {
        private readonly Dictionary<string, Texture2D> _gameTextures;
        public SecondWorld(GraphicsDeviceManager graphics, Dictionary<string, Texture2D> gameTextures, ScreenManager screenManager, DateTime gameStartedTime) : base(graphics, gameTextures, screenManager, gameStartedTime)
        {
            _gameTextures = gameTextures;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_gameTextures["secondWorldBackGround"], new Rectangle(0, 0, Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight), Color.White);
            base.Draw(gameTime, spriteBatch);
        }
    }
}