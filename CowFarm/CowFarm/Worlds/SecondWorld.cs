using System;
using System.Collections.Generic;
using System.Diagnostics;
using CowFarm.DrowingSystem;
using CowFarm.Entities;
using CowFarm.ScreenSystem;
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

        public SecondWorld(CowGameScreen cowGameScreen, GraphicsDeviceManager graphics, Dictionary<string, Texture2D> gameTextures, ScreenManager screenManager) : base(graphics, gameTextures, screenManager)
        {
            AddDynamicEntity(new Cat(this, new Vector2(100, 100), gameTextures));

            new Grass(graphics, this, new Vector2(540, 300), gameTextures);

            new BoulderRock(this, new Vector2(350, 400), gameTextures);

            new Rock(this, new Vector2(900, 600), gameTextures);

            var treeWithAplle = new GreenTree(cowGameScreen, this, graphics, new Vector2(700, 164), gameTextures);
            treeWithAplle.CreateApple();
            treeWithAplle = new GreenTree(cowGameScreen, this, graphics, new Vector2(240, 50), gameTextures);
            treeWithAplle.CreateApple();

            new BerryBush(this, graphics, new Vector2(100, 500), gameTextures);
            new BerryBush(this, graphics, new Vector2(940, 400), gameTextures);

            new Bush(this, graphics, new Vector2(590, 290), gameTextures);
            new Bush(this, graphics, new Vector2(430, 230), gameTextures);

            new OrangeTree(this, graphics, new Vector2(550, 500), gameTextures);
            new OrangeTree(this, graphics, new Vector2(1000, 60), gameTextures);

            //border
            BodyFactory.CreateEdge(this, new Vector2((float)graphics.PreferredBackBufferWidth / 100, 0), new Vector2((float)graphics.PreferredBackBufferWidth / 100, (float)graphics.PreferredBackBufferHeight / 100));

            _gameTextures = gameTextures;
        }
        
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_gameTextures["secondWorldBackGround"], new Rectangle(0, 0, Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight), Color.White);
            base.Draw(gameTime, spriteBatch);
        }
    }
}