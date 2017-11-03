using CowFarm.Entities;
using CowFarm.ScreenSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.StatusBars
{
    public class HealthBar
    {
        private readonly CowGameScreen _cowGameScreen;       
        private readonly Cow _cow;
        private readonly Rectangle _hearthRect;
        public HealthBar(CowGameScreen cowGameScreen, Cow cow)
        {
            _cowGameScreen = cowGameScreen;            
            _hearthRect = new Rectangle((int)cow.Inventory.StartPosition.X, (int)cow.Inventory.StartPosition.Y -19, 18, 18);            
            _cow = cow;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            float healthPoint = _cow.HealthPoint;

            var drawRect = _hearthRect;

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