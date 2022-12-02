using UnityEngine;
using System;

namespace Game.Core
{
    public class Health : IDamagable, IValidatable
    {
        private int _value;
        private int _maxValue;

        public bool IsDie => _value <= 0;

        public Health(int value, int maxValue)
        {
            if (value < 0 || maxValue < 0 || maxValue < value)
                throw new ArgumentException();

            _value = value;
            _maxValue = maxValue;
        }

        public void Validate()
        {
            _maxValue = Mathf.Max(_maxValue, 0);
            _value = Mathf.Clamp(_value, 0, _maxValue);
        }

        public void ApplayDamage(uint damage)
        {
            if (IsDie)
                return;

            _value -= (int)damage;  
        }
    }
}