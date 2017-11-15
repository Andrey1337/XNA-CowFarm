using System;
using Microsoft.Xna.Framework;

namespace CowFarm.Interfaces
{
    public interface IAttackable
    {
        Vector2 GetAttackPosition();
        bool OnFocus { get; set; }
        void GetDamage(int damage);
        TimeSpan DamageAnimationTime { get; set; }
        TimeSpan InDamageAnimationTime { get; set; }
        bool InAttack { get; }
    }
}