using UnityEngine;

namespace Game.Core
{
    public static class MathExtenstions
    {
        public static bool InBounds(int value, int min, int max)
        {
            return value >= min && value <= max;
        }

        public static int RoundToWhole(int value, int whole)
        {
            var extraValue = 0;

            if (value % whole > 0)
                extraValue = whole;

            if (value % whole < 0)
                extraValue = -whole;

            return value + extraValue;
        }
    }
}