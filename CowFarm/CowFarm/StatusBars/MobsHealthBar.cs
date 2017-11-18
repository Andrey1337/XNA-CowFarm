using CowFarm.Entities.Animals;
using CowFarm.ScreenSystem;
using CowFarm.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.StatusBars
{
    public class MobsHealthBar : StatusBar
    {
        private readonly Texture2D _borderTexture;
        private readonly Texture2D _healthTexture;
        private readonly Texture2D _spaceTexture;

        public MobsHealthBar(CowGameScreen cowGameScreen, Animal animal) : base(cowGameScreen, animal)
        {
            _borderTexture = TextureHelper.CreateBorderTexture(cowGameScreen.Graphics.GraphicsDevice, 1, 1);
            _healthTexture = TextureHelper.CreateBorderTexture(cowGameScreen.Graphics.GraphicsDevice, 1, 1);
            _spaceTexture = TextureHelper.CreateBorderTexture(cowGameScreen.Graphics.GraphicsDevice, 1, 1);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_borderTexture, new Rectangle(Animal.GetPosition().X + Animal.GetPosition().Width / 2 - (int)(Animal.MaxHealthPoint / 2) / 2, Animal.GetPosition().Y - 10, (int)(Animal.MaxHealthPoint / 2) + 2, 10), Color.Black);
            spriteBatch.Draw(_spaceTexture, new Rectangle(Animal.GetPosition().X + Animal.GetPosition().Width / 2 - (int)(Animal.MaxHealthPoint / 2) / 2 + 1, Animal.GetPosition().Y - 10 + 1, (int)Animal.MaxHealthPoint / 2, 8), new Color(255, 26, 26));
            spriteBatch.Draw(_healthTexture, new Rectangle(Animal.GetPosition().X + Animal.GetPosition().Width / 2 - (int)(Animal.MaxHealthPoint / 2) / 2 + 1, Animal.GetPosition().Y - 10 + 1, (int)Animal.HealthPoint / 2, 8), new Color(77, 255, 77));
        }
    }
}