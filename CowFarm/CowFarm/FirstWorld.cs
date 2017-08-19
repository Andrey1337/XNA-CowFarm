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

        public FirstWorld(GraphicsDeviceManager graphics, List<Entity>[] staticEntities, List<Entity> dynamicEntities)
        {
            //this.StaticEntities = new List<Entity>[graphics.PreferredBackBufferHeight];
            this.StaticEntities = staticEntities;
            this.Graphics = graphics;
            this.DynamicEntities = dynamicEntities;
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
