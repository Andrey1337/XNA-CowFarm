using System;
using System.Collections.Generic;
using System.Diagnostics;
using CowFarm.DrowingSystem;
using CowFarm.Entities;
using CowFarm.Generators;
using CowFarm.ScreenSystem;
using FarseerPhysics.Factories;
using FarseerPhysics.Samples.ScreenSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.Worlds
{
    public class FirstWorld : World
    {
        private readonly Texture2D _background;

        public FirstWorld(CowGameScreen cowGameScreen, GraphicsDeviceManager graphics, Dictionary<string, Texture2D> gameTextures,
            ScreenManager screenManager)
            : base(graphics, gameTextures, screenManager)
        {
            _background = gameTextures["firstWorldBackGround"];

            new Grass(graphics, this, new Vector2(480, 440), gameTextures);
            new Grass(graphics, this, new Vector2(540, 250), gameTextures);

            new GreenTree(cowGameScreen, this, graphics, new Vector2(644, 164), gameTextures);
            new GreenTree(cowGameScreen, this, graphics, new Vector2(437, 5), gameTextures);
            new GreenTree(cowGameScreen, this, graphics, new Vector2(1000, 550), gameTextures);
            new GreenTree(cowGameScreen, this, graphics, new Vector2(150, 450), gameTextures);
            new GreenTree(cowGameScreen, this, graphics, new Vector2(900, 55), gameTextures);


            new Bush(this, graphics, new Vector2(100, 150), gameTextures);
            new Bush(this, graphics, new Vector2(830, 200), gameTextures);
            new Bush(this, graphics, new Vector2(400, 450), gameTextures);

            new Rock(this, new Vector2(210, 300), gameTextures);
        }        

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_background, new Rectangle(0, 0, Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight), Color.White);
            base.Draw(gameTime, spriteBatch);
        }
    }
}

