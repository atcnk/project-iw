using UnityEngine;

namespace CodeNameIW
{
    public static class Helpers
    {
        private static Matrix4x4 _isometricMatrix4X4 = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));

        public static Vector3 ToVector3XZ(this Vector2 input)
        {
            return new Vector3(input.x, 0f, input.y);
        }
        
        public static Vector3 ToIsometric(this Vector3 input)
        {
            return _isometricMatrix4X4.MultiplyPoint3x4(input);
        }
    }
}