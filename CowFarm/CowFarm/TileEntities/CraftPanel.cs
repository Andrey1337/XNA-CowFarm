using CowFarm.Containers;
using CowFarm.Entities.Items;
using CowFarm.Inventory;
using CowFarm.ScreenSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CowFarm.TileEntities
{
    public class CraftPanel : TileEntity
    {
        private readonly Vector2 _drawPos;

        public CraftPanel(CowGameScreen cowGameScreen) : base(cowGameScreen)
        {
            Containers = new Container[2, 2];
            _drawPos = new Vector2(920, 758);
            Containers[0, 0] = new StaticConainer(new Rectangle((int)_drawPos.X + 19, (int)_drawPos.Y + 20, 42, 42), cowGameScreen.GameTextures["cleanTexture"]);
            Containers[0, 1] = new StaticConainer(new Rectangle((int)_drawPos.X + 75, (int)_drawPos.Y + 20, 42, 42), cowGameScreen.GameTextures["cleanTexture"]);
            Containers[1, 0] = new StaticConainer(new Rectangle((int)_drawPos.X + 19, (int)_drawPos.Y + 71, 42, 42), cowGameScreen.GameTextures["cleanTexture"]);
            Containers[1, 1] = new StaticConainer(new Rectangle((int)_drawPos.X + 75, (int)_drawPos.Y + 71, 42, 42), cowGameScreen.GameTextures["cleanTexture"]);

            CraftContainer = new CraftContainer(new Rectangle((int)_drawPos.X + 167, (int)_drawPos.Y + 45, 42, 42),
                cowGameScreen.GameTextures["cleanTexture"], this);
        }



        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.Draw(CowGameScreen.GameTextures["craftPanel"], _drawPos, Color.White);

            foreach (var container in Containers)
            {
                container?.Draw(spriteBatch, font);
            }
            CraftContainer.Draw(spriteBatch, font);
        }
    }
}