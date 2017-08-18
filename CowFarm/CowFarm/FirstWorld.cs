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
        
        public FirstWorld(List<Entity> staticEntities, List<Entity> dynamicEntities)
        {
            this._staticEntities = staticEntities;
            this._dynamicEntities = dynamicEntities;
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
