using System;

namespace CowFarm
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for thes application.
        /// </summary>
        static void Main(string[] args)
        {
            using (CowFarmGame game = new CowFarmGame())
            {
                game.Run();
            }
        }
    }
#endif
}

