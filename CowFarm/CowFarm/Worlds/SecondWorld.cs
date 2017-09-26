using System;
using System.Collections.Generic;
using CowFarm.DrowingSystem;
using CowFarm.Entities;
using FarseerPhysics.Samples.ScreenSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.Worlds
{
    public class SecondWorld : World
    {
        private readonly Dictionary<string, Texture2D> _gameTextures;
        public SecondWorld(GraphicsDeviceManager graphics, Dictionary<string, Texture2D> gameTextures, ScreenManager screenManager, DateTime gameStartedTime) : base(graphics, gameTextures, screenManager, gameStartedTime)
        {
            Cat cat = new Cat(this, graphics, new Rectangle(100, 100, 56, 46), new AnimatedSprites(gameTextures["catRightWalk"], 3, 56, 0), new AnimatedSprites(gameTextures["catLeftWalk"], 3, 56, 0), new AnimatedSprites(gameTextures["catUpWalk"], 3, 56, 0), new AnimatedSprites(gameTextures["catDownWalk"], 3, 56, 0));

            AddDynamicEntity(cat);


            var sprite = new AnimatedSprites(gameTextures["treeMovement"], 1, 155, 0);

            Tree tree = new Tree(this, graphics, new Rectangle(700, 164, sprite.SpriteWidth, sprite.SpriteHeight), sprite);
            AddStaticEntity(tree);

            sprite = new AnimatedSprites(gameTextures["treeMovement"], 1, 155, 0);

            tree = new Tree(this, graphics, new Rectangle(244, 54, sprite.SpriteWidth, sprite.SpriteHeight), sprite);
            AddStaticEntity(tree);

            Rock rock = new Rock(this, graphics, new Rectangle(300, 400, 160, 108), new AnimatedSprites(gameTextures["rockMovement"], 1, 129, 0));
            AddStaticEntity(rock);

            _gameTextures = gameTextures;
        }


        public override void Load(ContentManager content)
        {

        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_gameTextures["secondWorldBackGround"], new Rectangle(0, 0, Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight), Color.White);
            base.Draw(gameTime, spriteBatch);
        }
    }
}