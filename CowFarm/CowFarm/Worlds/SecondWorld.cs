using CowFarm.Entities;
using CowFarm.Entities.Items;
using CowFarm.ScreenSystem;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.Worlds
{
    public class SecondWorld : World
    {
        public SecondWorld(CowGameScreen cowGameScreen) : base(cowGameScreen)
        {
            new Cat(cowGameScreen, this, new Vector2(100, 100));

            new Grass(cowGameScreen, this, new Vector2(450, 550));
            new Grass(cowGameScreen, this, new Vector2(390, 600));
            new Grass(cowGameScreen, this, new Vector2(300, 520));
            new Grass(cowGameScreen, this, new Vector2(900, 150));
            new Grass(cowGameScreen, this, new Vector2(850, 620));
            new Grass(cowGameScreen, this, new Vector2(830, 400));
            new Grass(cowGameScreen, this, new Vector2(980, 300));
            new Grass(cowGameScreen, this, new Vector2(200, 300));
            new Grass(cowGameScreen, this, new Vector2(430, 350));

            new Log(cowGameScreen).Drop(this, new Vector2(400, 350));
            new Log(cowGameScreen).Drop(this, new Vector2(700, 400));
            new Log(cowGameScreen).Drop(this, new Vector2(900, 100));

            new BoulderRock(cowGameScreen, this, new Vector2(350, 400));

            new Rock(cowGameScreen, this, new Vector2(900, 600));

            new GreenTree(cowGameScreen, this, new Vector2(700, 164)).CreateApple();
            new GreenTree(cowGameScreen, this, new Vector2(240, 50)).CreateApple();

            new Rocks(cowGameScreen).Drop(this, new Vector2(240, 350));

            new BerryBush(cowGameScreen, this, new Vector2(100, 500));
            new BerryBush(cowGameScreen, this, new Vector2(940, 400));

            new Bush(cowGameScreen, this, new Vector2(590, 290));
            new Bush(cowGameScreen, this, new Vector2(430, 230));
            new Bush(cowGameScreen, this, new Vector2(510, 200));
            new Bush(cowGameScreen, this, new Vector2(760, 70));

            new OrangeTree(cowGameScreen, this, new Vector2(550, 500));
            new OrangeTree(cowGameScreen, this, new Vector2(1000, 60));

            //border
            BodyFactory.CreateEdge(this, new Vector2((float)CowGameScreen.Graphics.PreferredBackBufferWidth / 100, 0), new Vector2((float)CowGameScreen.Graphics.PreferredBackBufferWidth / 100, (float)CowGameScreen.Graphics.PreferredBackBufferHeight / 100));

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(CowGameScreen.GameTextures["secondWorldBackGround"], new Rectangle(0, 0, CowGameScreen.Graphics.PreferredBackBufferWidth, CowGameScreen.Graphics.PreferredBackBufferHeight), Color.White);
            base.Draw(spriteBatch);
        }
    }
}