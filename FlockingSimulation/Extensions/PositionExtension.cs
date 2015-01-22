namespace FlockingSimulation.Extensions
{
    using Structs;
    using SharpDX;
    using System;

    public static class PositionExtension
    {
        public static bool IsInVisibilityRange(
                                Position position, 
                                Vector2 direction, 
                                Position neighbourhood, 
                                float angle)
        {
            var halfAngle = angle / 2;

            var directionFactor = direction.X / direction.Y;
            var directionAngle = Math.Atan(directionFactor);

            var startingAngle = directionAngle - halfAngle;
            var endingAngle = directionAngle + halfAngle;

            if (startingAngle > endingAngle)
            {
                startingAngle = directionAngle + halfAngle;
                endingAngle = directionAngle - halfAngle;
            }

            var neighbourhoodFactor = neighbourhood.X / neighbourhood.Y;
            var neighbourhoodAngle = Math.Atan(neighbourhoodFactor);

            return (startingAngle < neighbourhoodAngle)
                    && (neighbourhoodAngle < endingAngle);
        }

        public static bool IsCloseEnough(Position position, Position neighbourhood, float distance)
        {
            return Math.Sqrt(Power2(neighbourhood.X - position.X) 
                                + Power2(neighbourhood.Y - neighbourhood.Y)) 
                                    < distance;
        }

        public static double Power2(double arg)
        {
            return arg * arg;
        }
    }
}
