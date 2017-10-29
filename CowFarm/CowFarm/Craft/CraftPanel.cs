using CowFarm.Inventory;
using CowFarm.ScreenSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CowFarm.Craft
{
    public class CraftPanel
    {
        private StaticConainer[] _containers;
        private readonly CowGameScreen _cowGameScreen;
        private readonly Vector2 _drawPos;

        public CraftPanel(CowGameScreen cowGameScreen)
        {
            _containers = new StaticConainer[4];

            _drawPos = new Vector2(920, 758);
            _cowGameScreen = cowGameScreen;
            _containers[0] = new StaticConainer(new Rectangle((int)_drawPos.X + 19, (int)_drawPos.Y + 20, 42, 42), cowGameScreen.GameTextures["cleanTexture"]);
            _containers[1] = new StaticConainer(new Rectangle((int)_drawPos.X + 75, (int)_drawPos.Y + 20, 42, 42), cowGameScreen.GameTextures["cleanTexture"]);
            _containers[2] = new StaticConainer(new Rectangle((int)_drawPos.X + 19, (int)_drawPos.Y + 71, 42, 42), cowGameScreen.GameTextures["cleanTexture"]);
            _containers[3] = new StaticConainer(new Rectangle((int)_drawPos.X + 75, (int)_drawPos.Y + 71, 42, 42), cowGameScreen.GameTextures["cleanTexture"]);
        }

        private MouseState _prevMouseState;
        private StaticConainer _containerOnFocus;
        public void Update()
        {
            var mouseState = Mouse.GetState();
            var mousePoint = new Point(mouseState.X, mouseState.Y);

            for (var i = 0; i < _containers.Length; i++)
            {
                if (_containers[i].Position.Contains(mousePoint))
                {
                    _containerOnFocus = _containers[i];
                    _containerOnFocus.OnFocus = true;

                }
            }

            if (_containerOnFocus != null && !_containerOnFocus.Position.Contains(mousePoint))
            {
                _containerOnFocus.OnFocus = false;
            }
            _prevMouseState = mouseState;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.Draw(_cowGameScreen.GameTextures["craftPanel"], _drawPos, Color.White);
            var pos = _drawPos;
            pos.X += 26;
            pos.Y += 10;

            foreach (var container in _containers)
            {
                container?.Draw(spriteBatch, font);
            }

        }
    }
}