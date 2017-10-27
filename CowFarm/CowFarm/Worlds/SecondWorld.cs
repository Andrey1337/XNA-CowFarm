using System;
using System.Collections.Generic;
using System.Diagnostics;
using CowFarm.DrowingSystem;
using CowFarm.Entities;
using CowFarm.Entities.Items;
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
        public SecondWorld(CowGameScreen cowGameScreen, ScreenManager screenManager) : base(cowGameScreen, screenManager)
        {
            new Cat(cowGameScreen, this, new Vector2(100, 100));

            new Grass(cowGameScreen, this, new Vector2(540, 300));

            new BoulderRock(cowGameScreen, this, new Vector2(350, 400));

            new Rock(cowGameScreen, this, new Vector2(900, 600));

            var treeWithAplle = new GreenTree(cowGameScreen, this, new Vector2(700, 164));
            treeWithAplle.CreateApple();
            treeWithAplle = new GreenTree(cowGameScreen, this, new Vector2(240, 50));
            treeWithAplle.CreateApple();

            new CutGrass(cowGameScreen, this, new Vector2(1f, 1f));

            new Rocks(cowGameScreen, this, new Vector2(2.4f, 3.5f));

            new BerryBush(cowGameScreen, this, new Vector2(100, 500));
            new BerryBush(cowGameScreen, this, new Vector2(940, 400));

            new Bush(cowGameScreen, this, new Vector2(590, 290));
            new Bush(cowGameScreen, this, new Vector2(430, 230));

            new OrangeTree(cowGameScreen, this, new Vector2(550, 500));
            new OrangeTree(cowGameScreen, this, new Vector2(1000, 60));

            //border
            BodyFactory.CreateEdge(this, new Vector2((float)Graphics.PreferredBackBufferWidth / 100, 0), new Vector2((float)Graphics.PreferredBackBufferWidth / 100, (float)Graphics.PreferredBackBufferHeight / 100));

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GameTextures["secondWorldBackGround"], new Rectangle(0, 0, Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight), Color.White);
            base.Draw(gameTime, spriteBatch);
        }
    }
}