using System;
using System.Collections.Generic;
using System.Diagnostics;
using CowFarm.DrowingSystem;
using CowFarm.Entities;
using CowFarm.Entities.Decorations;
using CowFarm.Entities.Plants;
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
        public FirstWorld(CowGameScreen cowGameScreen) : base(cowGameScreen)
        {
            new Grass(cowGameScreen, this, new Vector2(480, 440));
            new Grass(cowGameScreen, this, new Vector2(540, 250));

            new GreenTree(cowGameScreen, this, new Vector2(644, 164));
            new GreenTree(cowGameScreen, this, new Vector2(437, 5));
            new GreenTree(cowGameScreen, this, new Vector2(1000, 550));
            new GreenTree(cowGameScreen, this, new Vector2(150, 450));
            new GreenTree(cowGameScreen, this, new Vector2(900, 55));

            new Bush(cowGameScreen, this, new Vector2(100, 150));
            new Bush(cowGameScreen, this, new Vector2(830, 200));
            new Bush(cowGameScreen, this, new Vector2(400, 450));

            new Rock(cowGameScreen, this, new Vector2(210, 300));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(CowGameScreen.GameTextures["firstWorldBackGround"], new Rectangle(0, 0, CowGameScreen.Graphics.PreferredBackBufferWidth, CowGameScreen.Graphics.PreferredBackBufferHeight), Color.White);
            base.Draw(spriteBatch);
        }
    }
}

