using UnityEngine;

namespace Game.Core
{
    public static class MathExtenstions
    {
        public static bool InBounds(int value, int min, int max)
        {
            return value >= min && value <= max;
        }
    }
}