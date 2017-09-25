using System.Collections;
using System.Collections.Generic;
using CowFarm.Entities;

namespace CowFarm.Comparables
{
    public class PositionYComparer : IComparer<Entity>
    {
        public int Compare(Entity x, Entity y)
        {
            return x.GetPosition().Y + x.GetPosition().Height - (y.GetPosition().Y + y.GetPosition().Height);

        }
    }
}