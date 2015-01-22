namespace FlockingBirds.Configurator.Models
{
    public class FlockingBirdsArgument
    {
        internal FlockingBirdsArgumentType ArgumentType;

        internal string ArgumentValue;

        private FlockingBirdsArgument()
        {
        }

        public static FlockingBirdsArgument FullScreenMode()
        {
            return new FlockingBirdsArgument
            {
                ArgumentType = FlockingBirdsArgumentType.FullScreenMode,
                ArgumentValue = "-fullscreen"
            };
        }

        public static FlockingBirdsArgument Groups(int count)
        {
            return new FlockingBirdsArgument
            {
                ArgumentType = FlockingBirdsArgumentType.Groups,
                ArgumentValue = string.Format("-groups {0}", count)
            };
        }

        public static FlockingBirdsArgument BirdsCount(int count)
        {
            return new FlockingBirdsArgument
            {
                ArgumentType = FlockingBirdsArgumentType.BirdsCount,
                ArgumentValue = string.Format("-birdsCount {0}", count)
            };
        }

        public static FlockingBirdsArgument RunAtStart()
        {
            return new FlockingBirdsArgument
            {
                ArgumentType = FlockingBirdsArgumentType.RunAtStart,
                ArgumentValue = "-runAtStart"
            };
        }

        public static FlockingBirdsArgument Size(int width, int height)
        {
            return new FlockingBirdsArgument
            {
                ArgumentType = FlockingBirdsArgumentType.Size,
                ArgumentValue = string.Format("-size {0}x{1}", width, height)
            };
        }

        public static FlockingBirdsArgument VisibilityDistance(int distance)
        {
            return new FlockingBirdsArgument
            {
                ArgumentType = FlockingBirdsArgumentType.VisibilityDistance,
                ArgumentValue = string.Format("-visibilityDistance {0}", distance)
            };
        }

        public static FlockingBirdsArgument MaxBirdSpeed(float speed)
        {
            return new FlockingBirdsArgument
            {
                ArgumentType = FlockingBirdsArgumentType.MaxBirdSpeed,
                ArgumentValue = string.Format("-maxBirdSpeed {0}", speed)
            };
        }

        public static FlockingBirdsArgument NoMouseInteraction()
        {
            return new FlockingBirdsArgument
            {
                ArgumentType = FlockingBirdsArgumentType.NoMouseInteraction,
                ArgumentValue = "-noMouseInteraction"
            };
        }

        public static FlockingBirdsArgument BirdsSeparation(float value)
        {
            return new FlockingBirdsArgument
            {
                ArgumentType = FlockingBirdsArgumentType.BirdsSeparation,
                ArgumentValue = string.Format("-birdsSeparation {0}", value)
            };
        }

        public static FlockingBirdsArgument MaxBirdSteer(float value)
        {
            return new FlockingBirdsArgument
            {
                ArgumentType = FlockingBirdsArgumentType.MaxBirdSteer,
                ArgumentValue = string.Format("-maxSteer {0}", value)
            };
        }

        public static FlockingBirdsArgument BirdsCohesion(float value)
        {
            return new FlockingBirdsArgument
            {
                ArgumentType = FlockingBirdsArgumentType.BirdsCohesion,
                ArgumentValue = string.Format("-birdsCohesion {0}", value)
            };
        }

        public static FlockingBirdsArgument BirdsAignment(float value)
        {
            return new FlockingBirdsArgument
            {
                ArgumentType = FlockingBirdsArgumentType.BirdsAlignment,
                ArgumentValue = string.Format("-birdsAlignment {0}", value)
            };
        }

        public static FlockingBirdsArgument Wind(float x, float y)
        {
            return new FlockingBirdsArgument
            {
                ArgumentType = FlockingBirdsArgumentType.Wind,
                ArgumentValue = string.Format("-wind {0};{1}", x, y)
            };
        }
    }

    internal enum FlockingBirdsArgumentType
    {
        FullScreenMode,
        Groups,
        BirdsCount,
        RunAtStart,
        Size,
        VisibilityDistance,
        MaxBirdSpeed,
        BirdsSeparation,
        NoMouseInteraction,
        MaxBirdSteer,
        BirdsCohesion,
        BirdsAlignment,
        Wind
    }
}
