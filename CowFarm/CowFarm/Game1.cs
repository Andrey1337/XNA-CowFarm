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
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Texture2D rightWalk, leftWalk, downWalk, upWalk, currentAnim;

        Rectangle destRect;
        Rectangle sourceRect;
        float elapsed;
        float delay = 200f;
        int frames = 0;
        private KeyboardState ks;
        private float cowSpeed = 2f;

        Vector2 position = Vector2.Zero;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
        }

        protected override void Initialize()
        {
            destRect = new Rectangle(100, 100, 54, 48);

            base.Initialize();
        }

        private void Animate(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsed >= delay)
            {
                if (frames >= 2)
                {
                    frames = 1;
                }
                else
                {
                    frames++;
                }
                elapsed = 0;
            }
            sourceRect = new Rectangle(54 * frames + frames * spaceFromSprite, 0, 54, 54);
        }



        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            rightWalk = Content.Load<Texture2D>("cowRightWalk");
            leftWalk = Content.Load<Texture2D>("cowLeftWalk");
            downWalk = Content.Load<Texture2D>("cowDownWalk");
            upWalk = Content.Load<Texture2D>("cowUpWalk");
            currentAnim = rightWalk;
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        int spaceFromSprite = 16;
        protected override void Update(GameTime gameTime)
        {
            ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.D))
            {
                position.X += cowSpeed;
                currentAnim = rightWalk;
                Animate(gameTime);
            }
            else if (ks.IsKeyDown(Keys.A))
            {
                position.X -= cowSpeed;
                currentAnim = leftWalk;
                Animate(gameTime);
            }
            else if (ks.IsKeyDown(Keys.W))
            {
                position.Y -= cowSpeed;
                currentAnim = upWalk;
                Animate(gameTime);
            }
            else if (ks.IsKeyDown(Keys.S))
            {
                position.Y += cowSpeed;
                currentAnim = downWalk;
                Animate(gameTime);
            }
            else
            {
                sourceRect = new Rectangle(0, 0, 54, 54);
            }


            destRect = new Rectangle((int)position.X, (int)position.Y, 54, 54);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LimeGreen);

            spriteBatch.Begin();
            spriteBatch.Draw(currentAnim, destRect, sourceRect, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}