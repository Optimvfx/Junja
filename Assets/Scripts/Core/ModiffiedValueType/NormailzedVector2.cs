using UnityEngine;

namespace Game.Core
{
    [System.Serializable]
    public struct NormailzedVector2
    {
        [SerializeField] private Vector2 _value;

        public Vector2 Value => GetNormailzedValue(_value);

        public NormailzedVector2(Vector2 value)
        {
            _value = value;
        }

        public static Vector3 GetNormailzedValue(Vector2 value)
        {
            var normalizedValue = new Vector2(0, 0);

            if (!Mathf.Approximately(value.sqrMagnitude, 0))
                normalizedValue = value.normalized;

            return normalizedValue;
        }

        public static implicit operator Vector2(NormailzedVector2 vector) => vector.Value;
        public static implicit operator NormailzedVector2(Vector2 vector) => new NormailzedVector2(vector);
    }

    [System.Serializable]
    public struct NormailzedVector3
    {
        [SerializeField] private Vector3 _value;

        public Vector3 Value => GetNormailzedValue(_value);

        public NormailzedVector3(Vector3 value)
        {
            _value = value;
        }

        public static Vector3 GetNormailzedValue(Vector3 value)
        {
            var normalizedValue = new Vector3(0, 0);

            if (!Mathf.Approximately(value.sqrMagnitude, 0))
                normalizedValue = value.normalized;

            return normalizedValue;
        }

        public static implicit operator Vector3(NormailzedVector3 vector) => vector.Value;
        public static implicit operator NormailzedVector3(Vector3 vector) => new NormailzedVector3(vector);
        public static implicit operator NormailzedVector2(NormailzedVector3 vector) => new NormailzedVector2(vector.Value);
    }
}