using System.Collections.Generic;
using CowFarm.ScreenSystem.Buttons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.ScreenSystem.AlertWindows
{
    public abstract class AlertWindow
    {
        protected List<Button> Buttons;
        protected CowGameScreen CowGameScreen;
        protected Rectangle Position;

        protected AlertWindow(CowGameScreen cowGameScreen)
        {
            CowGameScreen = cowGameScreen;
            Buttons = new List<Button>();
        }

        protected void CentalizePosition()
        {
            Position.X = CowGameScreen.Graphics.PreferredBackBufferWidth / 2 - Position.Width / 2;
            Position.Y = CowGameScreen.Graphics.PreferredBackBufferHeight / 2 - Position.Height / 2;
        }

        public void Update()
        {
            Buttons.ForEach(button => button.Update());
        }

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}