using UnityEngine;

namespace Game.Core
{
    public static class VectorMathExtenstions
    {
        public static NormailzedVector2 RadianToVector2(float radian)
        {
            return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
        }

        public static Vector2 RadianToVector2(float radian, float length = 1)
        {
            return RadianToVector2(radian).Value * length;
        }
        
        public static NormailzedVector2 DegreeToVector2(Degree degree)
        {
            return RadianToVector2(degree * Mathf.Deg2Rad);
        }

        public static Vector2 DegreeToVector2(Degree degree, float length = 1)
        {
            return RadianToVector2(degree * Mathf.Deg2Rad).Value * length;
        }

        public static float Vector2ToRadian(NormailzedVector2 vector)
        {
            var vectorValue = vector.Value;

            return Mathf.Acos(vectorValue.x) + Mathf.Asin(vectorValue.y);
        }

        public static Degree Vector2ToDegree(NormailzedVector2 vector)
        {
            return Vector2ToRadian(vector) * Mathf.Rad2Deg;
        }
    }
}
