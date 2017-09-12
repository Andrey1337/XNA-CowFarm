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
            GrassGenerator = new GrassGenerator(this, graphics,
                new AnimatedSprites(gameTextures["grassMovement"], 2, 24, 15), 6, gameStartedTime);

            TreeGenerator treeGenerator = new TreeGenerator(this, graphics,
                new AnimatedSprites(gameTextures["treeMovement"], 2, 146, 30), 2);
            treeGenerator.Generate(StaticEntities);

            Rock rock = new Rock(this, graphics, new Rectangle(600, 150, 131, 108), new AnimatedSprites(gameTextures["rockMovement"], 1, 139, 0));
            this.AddStaticEntity(rock);

        }

        public override void Load(ContentManager content)
        {

        }




    }
}