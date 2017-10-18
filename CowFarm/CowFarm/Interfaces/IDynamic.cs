using CowFarm.Enums;
using CowFarm.Worlds;

namespace CowFarm.Interfaces
{
    public interface IDynamic
    {
        void ChangeWorld(World world, Direction direction);
    }
}