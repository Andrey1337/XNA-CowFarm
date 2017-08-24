using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CowFarm.Entities;

namespace CowFarm
{
    public class EntityYPositionComparer : IComparer<Entity>
    {
        public int Compare(Entity x, Entity y)
        {
            if (y != null && x != null && y.GetPosition().Y + y.GetPosition().Height >= x.GetPosition().Y + x.GetPosition().Height)
            {
                return -1;
            }
            return 1;
        }
    }
}
