using System;
using System.Collections.Generic;
using CowFarm.DrowingSystem;
using CowFarm.Entities;
using CowFarm.Generators;
using FarseerPhysics.Samples.ScreenSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.Worlds
{
    public class FirstWorld : World
    {
        private Dictionary<string, Texture2D> _gameTextures;



        public FirstWorld(GraphicsDeviceManager graphics, Dictionary<string, Texture2D> gameTextures,
            ScreenManager screenManager, DateTime gameStartedTime)
            : base(graphics, gameTextures, screenManager, gameStartedTime)
        {
            _gameTextures = gameTextures;
            GrassGenerator = new GrassGenerator(this, graphics,
                new AnimatedSprites(gameTextures["grassMovement"], 2, 24, 15), 6, gameStartedTime);
           
            var sprite = new AnimatedSprites(gameTextures["treeMovement"], 1, 155, 0);

            Tree tree = new Tree(this, graphics, new Rectangle(640, 170, sprite.SpriteWidth, sprite.SpriteHeight), sprite);
            this.AddStaticEntity(tree);

            tree = new Tree(this, graphics, new Rectangle(450, 5, sprite.SpriteWidth, sprite.SpriteHeight), sprite);
            this.AddStaticEntity(tree);


            Rock rock = new Rock(this, graphics, new Rectangle(210, 300, 129, 108), new AnimatedSprites(gameTextures["rockMovement"], 1, 129, 0));
            this.AddStaticEntity(rock);

        }

        public override void Load(ContentManager content)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_gameTextures["firstWorldBackGround"], new Rectangle(0, 0, Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight), Color.White);
            base.Draw(gameTime, spriteBatch);
        }
    }
}