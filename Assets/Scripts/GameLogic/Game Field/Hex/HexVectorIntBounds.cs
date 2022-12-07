using UnityEngine;

namespace Game.GameLogic
{
    public struct HexVectorIntBounds
    {
        public readonly HexVectorInt Max;
        public readonly HexVectorInt Min;

        public HexVectorIntBounds(HexVectorInt min, HexVectorInt max)
        {
            Max = max;
            Min = min;
        }
    }
}