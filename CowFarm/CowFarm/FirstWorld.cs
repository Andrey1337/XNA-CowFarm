using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                gameTextures["grassMovement"], 24);
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
