using CowFarm.Entities;
using CowFarm.Entities.Animals;
using CowFarm.ScreenSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.StatusBars
{
    public class HealthBar : StatusBar
    {
        public HealthBar(CowGameScreen cowGameScreen, Cow animal) : base(cowGameScreen, animal)
        {
            DrawRect = new Rectangle((int)animal.Inventory.StartPosition.X, (int)animal.Inventory.StartPosition.Y - 19, 18, 18);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            float healthPoint = Animal.HealthPoint;

            var drawRect = DrawRect;

            for (int i = 0; i < 10; i++)
            {
                if (healthPoint > 5)
                    spriteBatch.Draw(CowGameScreen.GameTextures["fullHearthIcon"], drawRect, Color.White);
                else
                {
                    spriteBatch.Draw(
                        healthPoint > 0
                            ? CowGameScreen.GameTextures["halfHearthIcon"]
                            : CowGameScreen.GameTextures["emptyHearthIcon"], drawRect, Color.White);
                }

                healthPoint -= 10;
                drawRect.X += drawRect.Width - 1;
            }
        }
    }
}