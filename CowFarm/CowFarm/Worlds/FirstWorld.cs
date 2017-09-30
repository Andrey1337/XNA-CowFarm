using System;
using System.Collections.Generic;
using System.Diagnostics;
using CowFarm.DrowingSystem;
using CowFarm.Entities;
using CowFarm.Generators;
using FarseerPhysics.Factories;
using FarseerPhysics.Samples.ScreenSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.Worlds
{
    public class FirstWorld : World
    {
        //private readonly Dictionary<string, Texture2D> _gameTextures;

        private readonly Texture2D _background;

        public FirstWorld(GraphicsDeviceManager graphics, Dictionary<string, Texture2D> gameTextures,
            ScreenManager screenManager, DateTime gameStartedTime)
            : base(graphics, gameTextures, screenManager, gameStartedTime)
        {
            _background = gameTextures["firstWorldBackGround"];

            Grass grass1 = new Grass(graphics, new Rectangle(480, 440, 25, 51), gameTextures);
            AddStaticEntity(grass1);

            grass1 = new Grass(graphics, new Rectangle(540, 250, 25, 51), gameTextures);
            AddStaticEntity(grass1);

            GreenTree greenTree = new GreenTree(this, graphics, new Rectangle(644, 164, 155, 261), gameTextures);
            greenTree = new GreenTree(this, graphics, new Rectangle(437, 5, 155, 261), gameTextures);
            greenTree = new GreenTree(this, graphics, new Rectangle(1000, 550, 155, 261), gameTextures);
            greenTree = new GreenTree(this, graphics, new Rectangle(150, 450, 155, 261), gameTextures);
            greenTree = new GreenTree(this, graphics, new Rectangle(900, 55, 155, 261), gameTextures);


            var bush = new Bush(this, graphics, new Rectangle(100, 150, 84, 87), gameTextures);
            bush = new Bush(this, graphics, new Rectangle(830, 200, 84, 87), gameTextures);
            bush = new Bush(this, graphics, new Rectangle(400, 450, 84, 87), gameTextures);


            var rock = new Rock(this, new Rectangle(210, 300, 160, 108), gameTextures);

        }



        public override void Load(ContentManager content)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_background, new Rectangle(0, 0, Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight), Color.White);
            base.Draw(gameTime, spriteBatch);
        }
    }
}

