using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CowFarm.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm
{
    public class FirstWorld : World
    {

        public FirstWorld(GraphicsDeviceManager graphics, List<Entity> dynamicEntities, Dictionary<string, Texture2D> gameTextures)
        {
            this.StaticEntities = new List<Entity>[graphics.PreferredBackBufferHeight];
            this.Graphics = graphics;
            this.DynamicEntities = dynamicEntities;
            this.GameTextures = gameTextures;
            this.GrassGenerator = new GrassGenerator(graphics,
                new AnimatedSprites(gameTextures["grassMovement"], 2, 24, 15), 6);

            TreeGenerator treeGenerator = new TreeGenerator(graphics, new AnimatedSprites(gameTextures["treeMovement"], 2, 104, 30), 3);
            treeGenerator.Generate(StaticEntities);
        }

        public override void Load(ContentManager content)
        {

        }

        public override Rectangle GetPosition()
        {
            throw new NotImplementedException();
        }

    }
}
