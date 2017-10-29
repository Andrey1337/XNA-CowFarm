using CowFarm.DrowingSystem;
using CowFarm.ScreenSystem;
using CowFarm.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.Entities.Items.Craftables
{
    public class Rope : Item
    {
        public Rope(CowGameScreen cowGameScreen, World world, Vector2 position)
            : base(cowGameScreen, world,
                new Rectangle((int)position.X, (int)position.Y, 20, 20), new AnimatedSprites(cowGameScreen.GameTextures["ropeMovement"], 1, 0), cowGameScreen.GameTextures["ropeIcon"])
        {
        }

        public override void Update(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            throw new System.NotImplementedException();
        }

        public override Rectangle GetPosition()
        {
            throw new System.NotImplementedException();
        }
    }
}