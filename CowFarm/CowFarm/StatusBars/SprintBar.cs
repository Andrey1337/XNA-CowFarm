using CowFarm.Entities.Animals;
using CowFarm.ScreenSystem;
using FarseerPhysics.Samples.ScreenSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.StatusBars
{
    public class SprintBar : StatusBar
    {
        private readonly Cow _cow;
        public SprintBar(CowGameScreen cowGameScreen, Cow cow) : base(cowGameScreen, cow)
        {
            DrawRect = new Rectangle((int)cow.Inventory.EndPosition.X, (int)cow.Inventory.EndPosition.Y - 19, 18, 18);
            DrawRect.X -= DrawRect.Width + 1;
            _cow = cow;
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            float width = _cow.Boost * 300;
            var rect1 = new Rectangle(6, 848, 304, 20);
            var rect2 = new Rectangle(8, 850, (int)width, 16);
            spriteBatch.Draw(CowGameScreen.GameTextures["sprintBorder"], rect1, Color.White);
            spriteBatch.Draw(CowGameScreen.GameTextures["sprintTexture"], rect2, Color.White);
        }
    }
}