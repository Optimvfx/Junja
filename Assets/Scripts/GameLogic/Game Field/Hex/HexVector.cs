using Game.Core;
using System;
using UnityEngine;

namespace Game.GameLogic
{
    [System.Serializable]
    public struct HexVectorInt : ISerializationCallbackReceiver
    {
        public static HexVectorInt Zero => new HexVectorInt(0, 0, 0);

        public static HexVectorInt PositiveR => new HexVectorInt(0, -1, 1);
        public static HexVectorInt PositiveS => new HexVectorInt(1, 0, -1);
        public static HexVectorInt PositiveQ => new HexVectorInt(-1, 1, 0);

        public static HexVectorInt NegativeR => new HexVectorInt(0, 1, -1);
        public static HexVectorInt NegativeS => new HexVectorInt(-1, 0, 1);
        public static HexVectorInt NegativeQ => new HexVectorInt(1, -1, 0);

        [SerializeField] private int _r;
        [SerializeField] private int _s;
        [SerializeField] private int _q;

        public int R => _r;
        public int S => _s;
        public int Q => _q;

        public HexVectorInt(int r, int s, int q)
        {
            if (IsValid(r, s, q) == false)
                throw new ArgumentException();

            _r = r;
            _s = s;
            _q = q;
        }

        public static bool IsValid(int r, int s, int q)
        {
            var sum = r + s + q;

            return sum == 0;
        }

        public static HexVectorInt operator +(HexVectorInt first, HexVectorInt second)
        {
            return new HexVectorInt(first.R + second.R, first.S + second.S, first.Q + second.Q);
        }

        public static HexVectorInt operator -(HexVectorInt first, HexVectorInt second)
        {
            return new HexVectorInt(first.R - second.R, first.S - second.S, first.Q - second.Q);
        }

        public static HexVectorInt operator *(HexVectorInt first, HexVectorInt second)
        {
            return new HexVectorInt(first.R * second.R, first.S * second.S, first.Q * second.Q);
        }

        public static HexVectorInt operator *(HexVectorInt first, int second)
        {
            return new HexVectorInt(first.R * second, first.S * second, first.Q * second);
        }
        public override string ToString()
        {
            return $"{{ {_r} : {_s} : {_q} }}";
        }

        public void OnBeforeSerialize()
        {
            if (IsValid(R, S, Q) == false)
                throw new InvalidOperationException();
        }

        public void OnAfterDeserialize()
        {
            if (IsValid(R, S, Q) == false)
                throw new InvalidOperationException();
        }
    }
}
