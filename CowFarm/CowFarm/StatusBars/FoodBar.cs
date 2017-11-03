using CowFarm.Entities;
using CowFarm.ScreenSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.StatusBars
{
    public class FoodBar
    {
        private readonly CowGameScreen _cowGameScreen;
        private readonly Cow _cow;
        private readonly Rectangle _foodRect;
        public FoodBar(CowGameScreen cowGameScreen, Cow cow)
        {
            _cowGameScreen = cowGameScreen;
            _cow = cow;
            _foodRect = new Rectangle((int)cow.Inventory.EndPosition.X, (int)cow.Inventory.EndPosition.Y - 19, 18, 18);
            _foodRect.X -= _foodRect.Width + 1;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            float healthPoint = _cow.StarvePoint;

            var drawRect = _foodRect;
            for (int i = 0; i < 10; i++)
            {
                if (healthPoint > 5)
                    spriteBatch.Draw(_cowGameScreen.GameTextures["fullFoodIcon"], drawRect, Color.White);
                else
                {
                    spriteBatch.Draw(
                        healthPoint > 0
                            ? _cowGameScreen.GameTextures["halfFoodIcon"]
                            : _cowGameScreen.GameTextures["emptyFoodIcon"], drawRect, Color.White);
                }

                healthPoint -= 10;
                drawRect.X -= drawRect.Width + 1;
            }
        }
    }
}