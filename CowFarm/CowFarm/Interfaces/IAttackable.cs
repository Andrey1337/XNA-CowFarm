using Microsoft.Xna.Framework;

namespace CowFarm.Interfaces
{
    public interface IAttackable
    {
        Vector2 GetAttackPosition();
        bool OnFocus { get; set; }
    }
}