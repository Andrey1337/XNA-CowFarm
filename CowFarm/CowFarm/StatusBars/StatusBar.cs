using CowFarm.Entities.Animals;
using CowFarm.ScreenSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.StatusBars
{
    public abstract class StatusBar
    {
        protected readonly CowGameScreen _cowGameScreen;
        protected readonly Cow _cow;
        protected Rectangle _drawRect;
        protected StatusBar(CowGameScreen cowGameScreen, Cow cow)
        {
            _cowGameScreen = cowGameScreen;
            _cow = cow;
        }

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}