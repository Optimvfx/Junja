using UnityEngine;
using System;

namespace Game.Core
{
    public interface IDamagable
    {
        void ApplayDamage(uint damage);
    }
}