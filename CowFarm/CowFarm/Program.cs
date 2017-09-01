using System;

namespace CowFarm
{
#if WINDOWS || XBOX

    internal static class Program
    {
        /// <summary>
        /// The main entry point for thes application.
        /// </summary>
        private static void Main(string[] args)
        {
            using (var game = new CowFarmGame())
            {
                game.Run();
            }
        }
    }
#endif
}

