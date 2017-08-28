using System.Collections.Generic;
using CowFarm.Entities;
using CowFarm.Worlds;
using FarseerPhysics.Samples.ScreenSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.ScreenSystem
{
    public class CowGameScreen : PhysicsGameScreen
    {
        private readonly ContentManager _contentManager;
        private readonly GraphicsDeviceManager _graphics;
        private World _world;
        private Cow _cow;
        private Dictionary<string, Texture2D> _gameTextures;
        private SpriteFont _font;
        private readonly GraphicsDevice _graphicsDevice;


        public CowGameScreen(ContentManager contentManager, GraphicsDeviceManager graphics, GraphicsDevice graphicsDevice)
        {
            _contentManager = contentManager;
            _graphics = graphics;
            _graphicsDevice = graphicsDevice;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            LoadCow();
            PlantLoad();
            LoadFonts();
            _world = new FirstWorld(_graphics, new List<Entity>() { _cow }, _gameTextures, this.ScreenManager);
        }

        private void LoadCow()
        {
            _gameTextures = new Dictionary<string, Texture2D>();
            _gameTextures.Add("cowRightWalk", _contentManager.Load<Texture2D>("cowRightWalk"));
            _gameTextures.Add("cowLeftWalk", _contentManager.Load<Texture2D>("cowLeftWalk"));
            _gameTextures.Add("cowDownWalk", _contentManager.Load<Texture2D>("cowUpWalk"));
            _gameTextures.Add("cowUpWalk", _contentManager.Load<Texture2D>("cowDownWalk"));

            _cow = new Cow(_graphics, new Rectangle(100, 100, 54, 49),
                new AnimatedSprites(_gameTextures["cowRightWalk"], 3, 54, 16),
                new AnimatedSprites(_gameTextures["cowRightWalk"], 3, 54, 16),
                new AnimatedSprites(_gameTextures["cowLeftWalk"], 3, 54, 16),
                new AnimatedSprites(_gameTextures["cowUpWalk"], 3, 54, 16),
                new AnimatedSprites(_gameTextures["cowDownWalk"], 3, 54, 16));
        }

        private void PlantLoad()
        {
            _gameTextures.Add("grassMovement", _contentManager.Load<Texture2D>("grassMovement"));
            _gameTextures.Add("treeMovement", _contentManager.Load<Texture2D>("treeMovement"));
        }

        private void LoadFonts()
        {
            _font = _contentManager.Load<SpriteFont>("gameFont");
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            _world.Update(gameTime);
            Camera.Update(gameTime);
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            _graphicsDevice.Clear(new Color(57, 172, 57));
            ScreenManager.SpriteBatch.Begin(0, null, null, null, null, null);

            _world.Draw(gameTime, ScreenManager.SpriteBatch);

            ScreenManager.SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}