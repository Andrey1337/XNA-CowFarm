using CowFarm.DrowingSystem;
using CowFarm.ScreenSystem;
using CowFarm.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.Entities.Items
{
    public class Twigs : Item
    {
        public Twigs(CowGameScreen cowGameScreen, World world, Vector2 position) : base(world, new Rectangle((int)position.X, (int)position.Y, 30, 23), new AnimatedSprites(cowGameScreen.GameTextures["rocksMovement"], 1, 0), cowGameScreen.GameTextures["rocksIcon"])
        {

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