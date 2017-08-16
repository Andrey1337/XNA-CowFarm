using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CowFarm
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class CowFarmGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Texture2D _cowRightWalk;
        private Texture2D _cowLeftWalk;
        private Texture2D _cowDownWalk;
        private Texture2D _cowUpWalk;

        private Texture2D _grassMovement;

        private Cow _cow;
        private Grass _grass;


        private double CurrentTime = 0f;

        private SpriteFont _font;


        public CowFarmGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
        }

        protected override void Initialize()
        {
            CurrentTime = 0;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            LoadCow();
            GrassLoad();
            _font = Content.Load<SpriteFont>("gameFont");
        }

        private void LoadCow()
        {
            _cowRightWalk = Content.Load<Texture2D>("cowRightWalk");
            _cowLeftWalk = Content.Load<Texture2D>("cowLeftWalk");
            _cowDownWalk = Content.Load<Texture2D>("cowDownWalk");
            _cowUpWalk = Content.Load<Texture2D>("cowUpWalk");
            _cow = new Cow(new Rectangle(100, 100, 54, 48), _cowRightWalk, _cowRightWalk, _cowLeftWalk, _cowDownWalk, _cowUpWalk);
        }
        private void GrassLoad()
        {
            _grassMovement = Content.Load<Texture2D>("grassMovement");
            _grass = new Grass(new Rectangle(200, 100, 16, 16), _grassMovement);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            CurrentTime += gameTime.ElapsedGameTime.TotalSeconds;
            _cow.Update(gameTime, graphics);
            _grass.Update(gameTime, graphics);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(57, 172, 57));

            spriteBatch.Begin();

            //spriteBatch.Draw(_grassMovement, new Rectangle(200, 100, 24, 24), new Rectangle(0, 0, 24, 24), Color.White);
            _cow.Draw(gameTime, spriteBatch);
            _grass.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(_font, "Time: " + (int)CurrentTime + " sec", new Vector2(graphics.PreferredBackBufferWidth - graphics.PreferredBackBufferWidth/2, 0), Color.Black);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}