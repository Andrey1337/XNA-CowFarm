using System;
using System.Collections.Generic;
using CowFarm.DrowingSystem;
using CowFarm.Entities;
using CowFarm.Worlds;
using FarseerPhysics.Common;
using FarseerPhysics.Samples.ScreenSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CowFarm.ScreenSystem
{
    public class CowGameScreen : GameScreen
    {
        private readonly ContentManager _contentManager;
        private readonly GraphicsDeviceManager _graphics;
        private World _world;
        private Cow _cow;
        private Dictionary<string, Texture2D> _gameTextures;
        private SpriteFont _font;
        private readonly GraphicsDevice _graphicsDevice;
        private string _worldSerialize;



        private bool _escapeKeyPressed;

        public CowGameScreen(ContentManager contentManager, GraphicsDeviceManager graphics,
            GraphicsDevice graphicsDevice)
        {
            _contentManager = contentManager;
            _graphics = graphics;
            _graphicsDevice = graphicsDevice;
            HasCursor = false;

            TransitionOnTime = TimeSpan.FromSeconds(0.4);
            TransitionOffTime = TimeSpan.FromSeconds(0.3);


        }

        public override void LoadContent()
        {
            base.LoadContent();
            _escapeKeyPressed = false;
            if (_worldSerialize == null)
            {
                LoadCow();
                PlantLoad();
                LoadFonts();
                _world = new FirstWorld(_graphics, new List<Entity>() { _cow },
                    _gameTextures, ScreenManager, DateTime.Now);
            }
            else
            {

                //_world = Newtonsoft.Json.JsonConvert.DeserializeObject<FirstWorld>(_worldSerialize);
            }
            _world.GameStartedTime = DateTime.Now - _world.PlayTime;

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
            if (!coveredByOtherScreen && !otherScreenHasFocus)
            {
                _world.Update(gameTime);
                //Camera.Update(gameTime);
            }
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            _graphicsDevice.Clear(new Color(57, 172, 57));

            ScreenManager.SpriteBatch.Begin();

            _world.Draw(gameTime, ScreenManager.SpriteBatch);

            DrawTime();

            ScreenManager.SpriteBatch.End();

            base.Draw(gameTime);
        }

        private TimeSpan _timeKeyEscapeWasPressed;
        private void DrawTime()
        {
            var inGametime = DateTime.Now - _world.GameStartedTime;
            if (!_escapeKeyPressed)
            {
                ScreenManager.SpriteBatch.DrawString(_font, $"Time: {inGametime.ToString(@"mm\:ss\.ff") }", new Vector2(_graphics.PreferredBackBufferWidth - _graphics.PreferredBackBufferWidth / 5, 0), Color.Black);
                _timeKeyEscapeWasPressed = inGametime;
            }
            else
            {
                ScreenManager.SpriteBatch.DrawString(_font, $"Time: {_timeKeyEscapeWasPressed.ToString(@"mm\:ss\.ff") }", new Vector2(_graphics.PreferredBackBufferWidth - _graphics.PreferredBackBufferWidth / 5, 0), Color.Black);
            }

            //spriteBatch.DrawString(_font, $"Cow pos x:{_cow.GetPosition().X + _cow.GetPosition().Width} y:{_cow.GetPosition().Y + _cow.GetPosition().Height}", new Vector2(500, 100), Color.AliceBlue);
            //spriteBatch.DrawString(_font, $"Grass pos x:{_grass.GetPosition().X + _grass.GetPosition().Width} y:{_grass.GetPosition().Y + _grass.GetPosition().Height}", new Vector2(500, 150), Color.AliceBlue);
            //_spriteBatch.DrawString(_font, DateTime.Now.ToString(@"mm\:ss\.ff"), new Vector2(500, 150), Color.AliceBlue);
        }


        public override void HandleInput(InputHelper input, GameTime gameTime)
        {
            if (input.IsNewKeyPress(Keys.Escape))
            {
                _worldSerialize = Newtonsoft.Json.JsonConvert.SerializeObject(_world);
                _world.PlayTime = DateTime.Now - _world.GameStartedTime;
                _escapeKeyPressed = true;
                ExitScreen();
            }
            base.HandleInput(input, gameTime);
        }
    }
}