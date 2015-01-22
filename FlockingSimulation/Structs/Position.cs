namespace FlockingSimulation.Structs
{

    using System;

    public struct Position : IEquatable<Position>
    {
        public static readonly Position Zero = new Position(x: 0, y: 0);

        public Position(float x, float y) : this()
        {
            this.X = x;
            this.Y = y;
        }

        public float X { get; set; }

        public float Y { get; set; }

        public bool Equals(Position other)
        {
            return other.X.Equals(this.X)
                    && other.Y.Equals(this.Y);
        }
    }
}
