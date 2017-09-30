using System;
using System.Collections.Generic;
using System.Diagnostics;
using CowFarm.DrowingSystem;
using CowFarm.Entities;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Samples.ScreenSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.Worlds
{
    public class SecondWorld : World
    {
        private readonly Dictionary<string, Texture2D> _gameTextures;

        public SecondWorld(GraphicsDeviceManager graphics, Dictionary<string, Texture2D> gameTextures, ScreenManager screenManager, DateTime gameStartedTime) : base(graphics, gameTextures, screenManager, gameStartedTime)
        {
            Cat cat = new Cat(this, new Rectangle(100, 100, 56, 46), gameTextures);
            AddDynamicEntity(cat);

            var grass1 = new Grass(graphics, new Rectangle(540, 300, 25, 51), gameTextures);
            AddStaticEntity(grass1);

            BoulderRock boulderRock = new BoulderRock(this, new Rectangle(350, 400, 140, 115), gameTextures);

            Rock rock = new Rock(this, new Rectangle(900, 600, 160, 108), gameTextures);

            GreenTree greenTree = new GreenTree(this, graphics, new Rectangle(700, 164, 155, 261), gameTextures);

            BerryBush berryBush = new BerryBush(this, graphics, new Rectangle(100, 500, 130, 120), gameTextures);
            berryBush = new BerryBush(this, graphics, new Rectangle(940, 400, 130, 120), gameTextures);

            Bush bush = new Bush(this, graphics, new Rectangle(590, 290, 84, 87), gameTextures);

            bush = new Bush(this, graphics, new Rectangle(430, 230, 84, 87), gameTextures);

            OrangeTree orangeTree = new OrangeTree(this, graphics, new Rectangle(550, 500, 155, 261), gameTextures);
            orangeTree = new OrangeTree(this, graphics, new Rectangle(1000, 60, 155, 261), gameTextures);
            greenTree = new GreenTree(this, graphics, new Rectangle(244, 54, 155, 261), gameTextures);

            //border
            BodyFactory.CreateEdge(this, new Vector2((float)graphics.PreferredBackBufferWidth / 100, 0), new Vector2((float)graphics.PreferredBackBufferWidth / 100, (float)graphics.PreferredBackBufferHeight / 100));

            _gameTextures = gameTextures;
        }



        public override void Load(ContentManager content)
        {

        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_gameTextures["secondWorldBackGround"], new Rectangle(0, 0, Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight), Color.White);
            base.Draw(gameTime, spriteBatch);
        }
    }
}