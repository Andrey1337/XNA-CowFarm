using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.ScreenSystem
{
    public class MenuWindow : AlertWindow
    {
        public MenuWindow(CowGameScreen cowGameScreen) : base(cowGameScreen)
        {
            Position = new Rectangle(0, 0, CowGameScreen.GameTextures["menuAlertWindow"].Width, CowGameScreen.GameTextures["menuAlertWindow"].Height);
            CentalizePosition();
            Buttons.Add(new RestartGameButton(cowGameScreen, new Vector2(Position.X + 71, Position.Y + 136)));
            Buttons.Add(new MainMenuButton(cowGameScreen, new Vector2(Position.X + 71, Position.Y + 181)));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(CowGameScreen.GameTextures["menuAlertWindow"], Position, Color.White);
            Buttons.ForEach(button => button.Draw(spriteBatch));
        }
    }
}