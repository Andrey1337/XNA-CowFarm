using System;
using System.Collections.Generic;
using System.Diagnostics;
using CowFarm.DrowingSystem;
using CowFarm.Entities;
using CowFarm.Generators;
using FarseerPhysics.Factories;
using FarseerPhysics.Samples.ScreenSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.Worlds
{
    public class FirstWorld : World
    {
        private readonly Dictionary<string, Texture2D> _gameTextures;

        public FirstWorld(GraphicsDeviceManager graphics, Dictionary<string, Texture2D> gameTextures,
            ScreenManager screenManager, DateTime gameStartedTime)
            : base(graphics, gameTextures, screenManager, gameStartedTime)
        {
            _gameTextures = gameTextures;

            Grass grass1 = new Grass(graphics, new Rectangle(480, 440, 25, 51), new AnimatedSprites(gameTextures["grassMovement"], 1, 25, 10), new AnimatedSprites(gameTextures["eatenGrassMovement"], 1, 25, 10));
            AddStaticEntity(grass1);
            Grass grass2 = new Grass(graphics, new Rectangle(540, 250, 25, 51), new AnimatedSprites(gameTextures["grassMovement"], 1, 25, 10), new AnimatedSprites(gameTextures["eatenGrassMovement"], 1, 25, 10));
            AddStaticEntity(grass2);

            Tree tree = new Tree(this, graphics, new Rectangle(644, 164, 155, 261), gameTextures);
            this.AddStaticEntity(tree);

            tree = new Tree(this, graphics, new Rectangle(437, 5, 155, 261), gameTextures);
            this.AddStaticEntity(tree);

            tree = new Tree(this, graphics, new Rectangle(1000, 550, 155, 261), gameTextures);
            this.AddStaticEntity(tree);

            tree = new Tree(this, graphics, new Rectangle(150, 450, 155, 261), gameTextures);
            this.AddStaticEntity(tree);

            tree = new Tree(this, graphics, new Rectangle(900, 55, 155, 261), gameTextures);
            this.AddStaticEntity(tree);

            var bushSprite = new AnimatedSprites(_gameTextures["bushMovement"], 1, 84, 0);
            Bush bush = new Bush(this, graphics, new Rectangle(100, 150, bushSprite.SpriteWidth, bushSprite.SpriteHeight), bushSprite);
            this.AddStaticEntity(bush);

            bushSprite = new AnimatedSprites(_gameTextures["bushMovement"], 1, 84, 0);
            bush = new Bush(this, graphics, new Rectangle(830, 200, bushSprite.SpriteWidth, bushSprite.SpriteHeight), bushSprite);
            this.AddStaticEntity(bush);

            bushSprite = new AnimatedSprites(_gameTextures["bushMovement"], 1, 84, 0);
            bush = new Bush(this, graphics, new Rectangle(400, 450, bushSprite.SpriteWidth, bushSprite.SpriteHeight), bushSprite);
            this.AddStaticEntity(bush);

            Rock rock = new Rock(this, new Rectangle(210, 300, 160, 108), gameTextures);
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

