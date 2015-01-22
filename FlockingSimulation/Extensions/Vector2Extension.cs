namespace FlockingSimulation.Extensions
{
    using SharpDX;
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public static class Vector2Extension
    {
        public static readonly Vector2 Empty;

        private static readonly Random Random;

        static Vector2Extension()
        {
            Empty = new Vector2(0f, 0f);
            Random = new Random();
        }

        public static bool IsAlmostEqual(this Vector2 source, Vector2 target)
        {
            return (Math.Abs(source.X - target.X) < 0.1f
                    && Math.Abs(source.Y - target.Y) < 0.1f);
        }

        public static Vector2 AverageVector(params Vector2[] vectors)
        {
            // cast for call apropriate overload of method
            return AverageVector(vectors as IEnumerable<Vector2>);
        }

        public static Vector2 AverageVector(IEnumerable<Vector2> vectors)
        {
            if (vectors == null)
            {
                throw new ArgumentNullException("vectors");
            }

            var enumerable = vectors as IList<Vector2> ?? vectors.ToList();

            if (!enumerable.Any())
            {
                return Empty;
            }

            var vector = enumerable.First();

            return enumerable.Skip(1).Aggregate(vector, (current, vec) => current + vec);
        }

        public static Vector2 RandomVector(float min, float max)
        {
            return Random.NextVector2(new Vector2(min, min), new Vector2(max, max));
        }

        public static Vector2 Limit(this Vector2 vector, float length)
        {
            var currLength = vector.Length();

            if (!(currLength > length))
            {
                return vector;
            }

            vector.X *= length/currLength;
            vector.Y *= length/currLength;

            return vector;
        }
    }
}
