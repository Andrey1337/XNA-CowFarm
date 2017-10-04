using System;

namespace CowFarm
{
    public class CollideEventArg : EventArgs
    {
        private int BodyIdA { get; set; }
        private int BodyIdB { get; set; }
        public CollideEventArg(int idA, int idB)
        {
            BodyIdA = idA;
            BodyIdB = idB;
        }
    }
}