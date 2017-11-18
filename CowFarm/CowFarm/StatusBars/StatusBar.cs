using CowFarm.Entities.Animals;
using CowFarm.ScreenSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.StatusBars
{
    public abstract class StatusBar
    {
        protected readonly CowGameScreen CowGameScreen;
        protected readonly Animal Animal;
        protected Rectangle DrawRect;
        protected StatusBar(CowGameScreen cowGameScreen, Animal animal)
        {
            CowGameScreen = cowGameScreen;
            Animal = animal;
        }

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}