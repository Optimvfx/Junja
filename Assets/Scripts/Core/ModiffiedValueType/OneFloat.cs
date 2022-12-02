using UnityEngine;

namespace Game.Core
{
    [System.Serializable]
    public struct OneFloat
    {
        [Range(-1, 1)]
        [SerializeField] private float _value;

        public float Value => _value;

        public OneFloat(float value)
        {
            _value = Mathf.Clamp(value, -1, 1);
        }

        public static implicit operator float(OneFloat oneFloat) => oneFloat.Value;
        public static implicit operator OneFloat(float value) => new OneFloat(value);
    }
}