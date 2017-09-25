using CowFarm.DrowingSystem;
using Microsoft.Xna.Framework;

namespace CowFarm.Entities
{
    public abstract class NPC : Animal
    {
        protected NPC(GraphicsDeviceManager graphics, Rectangle destRect, AnimatedSprites rightWalk, AnimatedSprites leftWalk, AnimatedSprites downWalk, AnimatedSprites upWalk) : base(graphics, destRect, rightWalk, leftWalk, downWalk, upWalk)
        {
        }


    }
}