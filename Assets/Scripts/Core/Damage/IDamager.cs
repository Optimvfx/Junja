using UnityEngine;

namespace Game.Core
{
    public interface IDamager<Damagable>
        where Damagable : IDamagable
    {
        void Attack(Damagable damagable);
    }
}
