using UnityEngine;

namespace Game.GameLogic
{
    [System.Serializable]
    public struct HexVector
    {
        public static HexVector Zero => new HexVectorInt(0, 0, 0);

        public static HexVector PositiveR => new HexVectorInt(1, 0, 0);
        public static HexVector PositiveS => new HexVectorInt(0, 1, 0);
        public static HexVector PositiveQ => new HexVectorInt(0, 0, 1);

        public static HexVector NegativeR => new HexVectorInt(-1, 0, 0);
        public static HexVector NegativeS => new HexVectorInt(0, -1, 0);
        public static HexVector NegativeQ => new HexVectorInt(0, 0, -1);

        [SerializeField] private float _r;
        [SerializeField] private float _s;
        [SerializeField] private float _q;

        public HexVector(float r, float s, float q)
        {
            _r = r;
            _s = s;
            _q = q;
        }

        public float R => _r;
        public float S => _s;
        public float Q => _q;
    }

    [System.Serializable]
    public struct HexVectorInt
    {
        public static HexVectorInt Zero => new HexVectorInt(0, 0, 0);

        public static HexVectorInt PositiveR => new HexVectorInt(1, 0, 0);
        public static HexVectorInt PositiveS => new HexVectorInt(0, 1, 0);
        public static HexVectorInt PositiveQ => new HexVectorInt(0, 0, 1);

        public static HexVectorInt NegativeR => new HexVectorInt(-1, 0, 0);
        public static HexVectorInt NegativeS => new HexVectorInt(0, -1, 0);
        public static HexVectorInt NegativeQ => new HexVectorInt(0, 0, -1);

        [SerializeField] private int _r;
        [SerializeField] private int _s;
        [SerializeField] private int _q;

        public HexVectorInt(int r, int s, int q)
        {
            _r = r;
            _s = s;
            _q = q;
        }

        public int R => _r;
        public int S => _s;
        public int Q => _q;

        public static HexVectorInt operator +(HexVectorInt first, HexVectorInt second)
        {
            return new HexVectorInt(first.R + second.R, first.S + second.S, first.Q + second.Q);
        }

        public static implicit operator HexVector(HexVectorInt vector) => new HexVector(vector.R, vector.S, vector.Q);
    }
}
