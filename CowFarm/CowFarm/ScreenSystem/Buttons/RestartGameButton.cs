using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CowFarm.ScreenSystem.Buttons
{
    public class RestartGameButton : Button
    {
        public RestartGameButton(CowGameScreen cowGameScreen, Vector2 position) : base(cowGameScreen)
        {
            Position = new Rectangle((int)position.X, (int)position.Y, 205, 35);
            ButtonTexture = CowGameScreen.GameTextures["restartButton"];
        }

        public override void Update()
        {
            var mouseState = Mouse.GetState();
            var mousePoint = new Point(mouseState.X, mouseState.Y);
            if (Position.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Pressed)
            {
                CowGameScreen.RestartGame();
            }
        }
    }
}