using System;
using System.Collections.Generic;
using System.Text;

namespace EZPower
{
    public class Vector2
    {
        public int x;
        public int y;

        public Vector2()
        {
            this.x = 0;
            this.y = 0;
        }

        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2 operator -(Vector2 vecA, Vector2 vecB)
        {
            return new Vector2(vecA.x - vecB.x, vecA.y - vecB.y);
        }

        public static Vector2 operator +(Vector2 vecA, Vector2 vecB)
        {
            return new Vector2(vecA.x + vecB.x, vecA.y + vecB.y);
        }

        public static Vector2 operator *(Vector2 vecA, Vector2 vecB)
        {
            return new Vector2(vecA.x * vecB.x, vecA.y * vecB.y);
        }

        public static Vector2 operator *(Vector2 vecA, int scaler)
        {
            return new Vector2(vecA.x * scaler, vecA.y * scaler);
        }

        public static Vector2 operator /(Vector2 vecA, int scaler)
        {
            return new Vector2(vecA.x / scaler, vecA.y / scaler);
        }
    }

    public class MathUtil
    {
    }
}
