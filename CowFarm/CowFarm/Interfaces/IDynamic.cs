using CowFarm.Enums;
using CowFarm.Worlds;

namespace CowFarm
{
    public interface IDynamic
    {
        void ChangeWorld(World world, Direction direction);
    }
}