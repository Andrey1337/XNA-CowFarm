using CowFarm.Entities;
using CowFarm.Entities.Animals;
using CowFarm.ScreenSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.StatusBars
{
    public class FoodBar : StatusBar
    {
        public FoodBar(CowGameScreen cowGameScreen, Cow cow) : base(cowGameScreen, cow)
        {
            _drawRect = new Rectangle((int)cow.Inventory.EndPosition.X - 19, (int)cow.Inventory.EndPosition.Y - 19, 18, 18);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            float healthPoint = _cow.StarvePoint;

            var drawRect = _drawRect;
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