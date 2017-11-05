using CowFarm.Entities;
using CowFarm.Entities.Animals;
using CowFarm.ScreenSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.StatusBars
{
    public class HealthBar : StatusBar
    {
        public HealthBar(CowGameScreen cowGameScreen, Cow cow) : base(cowGameScreen, cow)
        {
            _drawRect = new Rectangle((int)cow.Inventory.StartPosition.X, (int)cow.Inventory.StartPosition.Y - 19, 18, 18);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            float healthPoint = _cow.HealthPoint;

            var drawRect = _drawRect;

            for (int i = 0; i < 10; i++)
            {
                if (healthPoint > 5)
                    spriteBatch.Draw(_cowGameScreen.GameTextures["fullHearthIcon"], drawRect, Color.White);
                else
                {
                    spriteBatch.Draw(
                        healthPoint > 0
                            ? _cowGameScreen.GameTextures["halfHearthIcon"]
                            : _cowGameScreen.GameTextures["emptyHearthIcon"], drawRect, Color.White);
                }

                healthPoint -= 10;
                drawRect.X += drawRect.Width - 1;
            }
        }
    }
}