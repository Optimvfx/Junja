using UnityEngine;

namespace Game.Core
{
    public interface IDamager<Damagable>
        where Damagable : IDamagable
    {
        protected abstract void Attack(Damagable damagable);
    }
}
