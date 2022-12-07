using UnityEngine;

namespace Game.Core
{
    [System.Serializable]
    public struct Degree
    {
        public const float MaxDegree = 360f;

        [Range(0f, MaxDegree)]
        [SerializeField] private float _value;

        public float Value => _value;

        public Degree(float value)
        {
            _value = value % MaxDegree;

            if (_value < 0)
                _value += MaxDegree;
        }

        public static implicit operator float(Degree angle) => angle.Value;

        public static implicit operator Degree(float value) => new Degree(value);

        public override string ToString()
        {
            return _value.ToString();
        }
    }
}