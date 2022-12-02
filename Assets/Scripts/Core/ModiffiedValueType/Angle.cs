using UnityEngine;

namespace Game.Core
{
    [System.Serializable]
    public struct Angle
    {
        public const float MaxAngle = 360f;

        [Range(0f, MaxAngle)]
        [SerializeField] private float _value;

        public float Value => _value;

        public Angle(float value)
        {
            _value = Mathf.Abs(value) % MaxAngle;
        }

        public static implicit operator float(Angle angle) => angle.Value;

        public static implicit operator Angle(float value) => new Angle(value);
    }
}