using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CowFarm.ScreenSystem
{
    public abstract class Button
    {
        protected CowGameScreen CowGameScreen;
        protected Texture2D ButtonTexture;
        protected Rectangle Position;
        protected bool OnFocus;
        protected Button(CowGameScreen cowGameScreen)
        {
            CowGameScreen = cowGameScreen;
        }

        public abstract void Update();

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            var mouseState = Mouse.GetState();
            var mousePoint = new Point(mouseState.X, mouseState.Y);
            spriteBatch.Draw(ButtonTexture, Position,
                Position.Contains(mousePoint) ? new Color(200, 200, 200) : Color.White);
        }
    }
}