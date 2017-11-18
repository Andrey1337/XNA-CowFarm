using CowFarm.Entities;
using CowFarm.Entities.Animals;
using CowFarm.ScreenSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.StatusBars
{
    public class FoodBar : StatusBar
    {
        private Cow _cow;
        public FoodBar(CowGameScreen cowGameScreen, Cow animal) : base(cowGameScreen, animal)
        {
            DrawRect = new Rectangle((int)animal.Inventory.EndPosition.X - 19, (int)animal.Inventory.EndPosition.Y - 19, 18, 18);
            _cow = animal;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            float healthPoint = _cow.StarvePoint;

            var drawRect = DrawRect;
            for (int i = 0; i < 10; i++)
            {
                if (healthPoint > 5)
                    spriteBatch.Draw(CowGameScreen.GameTextures["fullFoodIcon"], drawRect, Color.White);
                else
                {
                    spriteBatch.Draw(
                        healthPoint > 0
                            ? CowGameScreen.GameTextures["halfFoodIcon"]
                            : CowGameScreen.GameTextures["emptyFoodIcon"], drawRect, Color.White);
                }

                healthPoint -= 10;
                drawRect.X -= drawRect.Width + 1;
            }
        }
    }
}