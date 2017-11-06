using CowFarm.Entities.Animals;
using CowFarm.ScreenSystem;
using FarseerPhysics.Samples.ScreenSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.StatusBars
{
    public class SprintBar : StatusBar
    {

        public SprintBar(CowGameScreen cowGameScreen, Cow cow) : base(cowGameScreen, cow)
        {
            _drawRect = new Rectangle((int)cow.Inventory.EndPosition.X, (int)cow.Inventory.EndPosition.Y - 19, 18, 18);
            _drawRect.X -= _drawRect.Width + 1;
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            float width = _cow.Boost * 300;
            var rect1 = new Rectangle(6, 848, 304, 20);
            var rect2 = new Rectangle(8, 850, (int)width, 16);
            spriteBatch.Draw(_cowGameScreen.GameTextures["sprintBorder"], rect1, Color.White);
            spriteBatch.Draw(_cowGameScreen.GameTextures["sprintTexture"], rect2, Color.White);
        }
    }
}