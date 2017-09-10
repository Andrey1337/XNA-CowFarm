using System;
using System.Collections.Generic;
using System.IO;
using CowFarm.DrowingSystem;
using CowFarm.Entities;
using CowFarm.Worlds;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Samples;
using FarseerPhysics.Samples.Demos.Prefabs;
using FarseerPhysics.Samples.DrawingSystem;
using FarseerPhysics.Samples.ScreenSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using World = CowFarm.Worlds.World;

namespace CowFarm.ScreenSystem
{
    public class CowGameScreen : PhysicsGameScreen
    {
        private readonly ContentManager _contentManager;
        private readonly GraphicsDeviceManager _graphics;
        private readonly GraphicsDevice _graphicsDevice;

        private new World World;


        private Body _rectangle;
        private Sprite _rectangleSprite;

        //Worlds 
        private World _rightWorld;
        private World _leftWorld;
        private World _upWorld;
        private World _downWorld;

        private Cow _cow;
        private Sprite _cowSprite;

        private Dictionary<string, Texture2D> _gameTextures;
        private SpriteFont _font;

        private string _worldSerialize;
        private bool _escapeKeyPressed;

        public CowGameScreen(ContentManager contentManager, GraphicsDeviceManager graphics,
            GraphicsDevice graphicsDevice)
        {
            _gameTextures = null;
            World = null;

            _rightWorld = null;
            _leftWorld = null;
            _upWorld = null;
            _downWorld = null;

            _contentManager = contentManager;
            _graphics = graphics;
            _graphicsDevice = graphicsDevice;

            HasCursor = true;

            TransitionOnTime = TimeSpan.FromSeconds(0.7);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }


        public override void LoadContent()
        {
            if (_worldSerialize == null)
            {
                _gameTextures = new Dictionary<string, Texture2D>();
                PlantLoad();
                LoadFonts();
                LoadCow();

                World = new FirstWorld(_graphics, _gameTextures, ScreenManager, DateTime.Now);

                CreateCow();

                World.AddDynamicEntity(_cow);

                _cow.Body.BodyType = BodyType.Dynamic;
                _cow.Body.CollisionCategories = Category.All;
                _cow.Body.CollidesWith = Category.All;
                SetUserAgent(_cow.Body, 10f, 10f);
            }
            World.GameStartedTime = DateTime.Now - World.TimeInTheGame;
            _escapeKeyPressed = false;
            
            _rectangle = BodyFactory.CreateRectangle(World, 1f, 1f, 1f, new Vector2(1, 1));
            _rectangle.BodyType = BodyType.Static;
            _rectangle.CollisionCategories = Category.All;
            _rectangle.CollidesWith = Category.All;
            _rectangleSprite = new Sprite(ScreenManager.Assets.TextureFromShape(
                _rectangle.FixtureList[0].Shape, MaterialType.Squares, Color.Orange, 1f));

            base.LoadContent();
        }

        private void CreateCow()
        {
            _cow = new Cow(World, _graphics, new Rectangle(2, 1, 54, 49), new AnimatedSprites(_gameTextures["cowRightWalk"], 3, 54, 16), new AnimatedSprites(_gameTextures["cowRightWalk"], 3, 54, 16), new AnimatedSprites(_gameTextures["cowLeftWalk"], 3, 54, 16), new AnimatedSprites(_gameTextures["cowUpWalk"], 3, 54, 16), new AnimatedSprites(_gameTextures["cowDownWalk"], 3, 54, 16));
        }

        private void LoadCow()
        {
            _gameTextures.Add("cowRightWalk", _contentManager.Load<Texture2D>("cowRightWalk"));
            _gameTextures.Add("cowLeftWalk", _contentManager.Load<Texture2D>("cowLeftWalk"));
            _gameTextures.Add("cowDownWalk", _contentManager.Load<Texture2D>("cowUpWalk"));
            _gameTextures.Add("cowUpWalk", _contentManager.Load<Texture2D>("cowDownWalk"));
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
                World.Update(gameTime);
            }
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            _graphicsDevice.Clear(new Color(57, 172, 57));

            ScreenManager.SpriteBatch.Begin();


            ScreenManager.SpriteBatch.Draw(_rectangleSprite.Texture, ConvertUnits.ToDisplayUnits(_rectangle.Position), null, Color.White, _rectangle.Rotation, _rectangleSprite.Origin, 1f, SpriteEffects.None, 0f);

            //ScreenManager.SpriteBatch.Draw(_cowSprite.Texture, ConvertUnits.ToDisplayUnits(_cow.Body.Position), null, Color.White, _cow.Body.Rotation, _cowSprite.Origin, 1f, SpriteEffects.None, 0f);
            //ScreenManager.SpriteBatch.Draw(_rectangleSprite.Texture, ConvertUnits.ToDisplayUnits(_cow.Body.Position), null, Color.White, _cow.Body.Rotation, _rectangleSprite.Origin, 1f, SpriteEffects.None, 0f);
            World.Draw(gameTime, ScreenManager.SpriteBatch);

            DrawTime();

            ScreenManager.SpriteBatch.End();

            base.Draw(gameTime);
        }

        private TimeSpan _timeKeyEscapeWasPressed;

        private void DrawTime()
        {
            var inGametime = DateTime.Now - World.GameStartedTime;
            if (!_escapeKeyPressed)
            {
                ScreenManager.SpriteBatch.DrawString(_font, $"Time: {inGametime.ToString(@"mm\:ss\.ff") }", new Vector2(_graphics.PreferredBackBufferWidth - _graphics.PreferredBackBufferWidth / 5, 0), Color.Black);
                _timeKeyEscapeWasPressed = inGametime;
                //ScreenManager.SpriteBatch.DrawString(Font, "X: " + Cow.GetPosition().X + " Y: " + Cow.GetPosition().Y, new Vector2(Graphics.PreferredBackBufferWidth - Graphics.PreferredBackBufferWidth / 5, 0), Color.Black);
            }
            else
            {
                ScreenManager.SpriteBatch.DrawString(_font, $"Time: {_timeKeyEscapeWasPressed.ToString(@"mm\:ss\.ff") }", new Vector2(_graphics.PreferredBackBufferWidth - _graphics.PreferredBackBufferWidth / 5, 0), Color.Black);
            }
        }


        //public override void HandleInput(InputHelper input, GameTime gameTime)
        //{
        //    if (input.IsNewKeyPress(Keys.Escape))
        //    {
        //        //WorldSerialize = Newtonsoft.Json.JsonConvert.SerializeObject(World);

        //        World.TimeInTheGame = DateTime.Now - World.GameStartedTime;
        //        _escapeKeyPressed = true;
        //        //ExitScreen();
        //    }
        //    base.HandleInput(input, gameTime);
        //}



    }
}