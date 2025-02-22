using UnityEngine;

namespace ColdWind.Core
{
    public static class VectorExtensions
    {
        public static Vector3 SetValues(
            this Vector3 vector, float x = float.NaN, float y = float.NaN, float z = float.NaN)
        {
            return new Vector3(
                SelectCorrectValue(x, vector.x),
                SelectCorrectValue(y, vector.y),
                SelectCorrectValue(z, vector.z)
            );
        }

        public static Vector2 SetValues(
            this Vector2 vector, float x = float.NaN, float y = float.NaN)
        {
            return new Vector2(
                SelectCorrectValue(x, vector.x),
                SelectCorrectValue(y, vector.y)
            );
        }

        private static float SelectCorrectValue(float newValue, float defaultValue)
        {
            return float.IsNaN(newValue) ? defaultValue : newValue;
        }
    }
}
