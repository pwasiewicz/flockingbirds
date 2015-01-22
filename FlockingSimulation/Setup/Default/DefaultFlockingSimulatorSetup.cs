namespace FlockingSimulation.Setup.Default
{
    using SharpDX;
    using System;

    internal class DefaultFlockingSimulatorSetup : IFlockingSetup
    {
        public DefaultFlockingSimulatorSetup()
        {
            this.DefaultSettings();
        }

        internal int BirdsCount { get; private set; }

        internal int Width { get; private set; }

        internal int Height { get; private set; }

        internal float VisibilityDistance { get; private set; }

        internal bool IsFullScreen { get; private set; }

        internal bool RunAtStart { get; private set; }

        internal float MaxSpeed { get; private set; }

        internal int GroupCount { get; private set; }

        internal float Separation { get; private set; }

        internal float Cohesion { get; private set; }

        internal float Alignment { get; private set; }

        internal bool MouseInteractionEnabled { get; private set; }

        internal float Steer { get; private set; }

        internal Vector2 WindVector { get; set; }

        public IFlockingSetup SetBirdsCount(int count)
        {
            this.BirdsCount = count;
            return this;
        }

        public IFlockingSetup SetEnvironmentSize(int width, int height)
        {
            if (height > 1080 || width > 1920)
            {
                throw new NotSupportedException("Maximum supported resolution is 1920x1080 px.");
            }

            if (height < 240 || width < 320)
            {
                throw new NotSupportedException("Maximum supported resolution is 300x300 px.");
            }

            this.Width = width;
            this.Height = height;

            return this;
        }

        internal void DefaultSettings()
        {
            this.BirdsCount = 50;
            this.Width = 640;
            this.Height = 480;
            this.VisibilityDistance = 50;
            this.IsFullScreen = false;
            this.RunAtStart = false;
            this.MaxSpeed = 1f;
            this.GroupCount = 1;
            this.MouseInteractionEnabled = true;
            this.Separation = 5f;
            this.Steer = 1f;
            this.Cohesion = 4f;
            this.Alignment = 6f;
            this.WindVector = new Vector2(0, 0);
        }

        public IFlockingSetup SetVisibilityDistance(float distance)
        {
            this.VisibilityDistance = distance;

            return this;
        }


        public IFlockingSetup SetFullScreenMode(bool fullscreen)
        {
            this.IsFullScreen = fullscreen;

            return this;
        }


        public IFlockingSetup SetRunAtStart(bool runAtStart)
        {
            this.RunAtStart = runAtStart;

            return this;
        }


        public IFlockingSetup MaximumSpeed(float speed)
        {
            if (speed < 0.5 || speed > 1.5)
            {
                throw new ArgumentOutOfRangeException("speed");
            }

            this.MaxSpeed = speed;

            return this;
        }

        public IFlockingSetup MouseInteraction(bool active)
        {
            this.MouseInteractionEnabled = active;

            return this;
        }


        public IFlockingSetup SetGroups(int count)
        {
            if (count < 1 || count > 3)
            {
                throw new ArgumentOutOfRangeException("count");
            }

            this.GroupCount = count;

            return this;
        }


        public IFlockingSetup BirdsSeparation(float separation)
        {
            if (separation < 1 || separation > 30)
            {
                throw new ArgumentOutOfRangeException("separation");
            }

            this.Separation = separation;

            return this;
        }

        public IFlockingSetup BirdsCohesion(float cohesion)
        {
            this.Cohesion = cohesion;

            return this;
        }

        public IFlockingSetup BirdsAlignment(float alignment)
        {
            this.Alignment = alignment;

            return this;
        }

        public IFlockingSetup MaxSteer(float steer)
        {
            this.Steer = steer;

            return this;
        }


        public IFlockingSetup Wind(float vectorX, float vectorY)
        {
            this.WindVector = new Vector2(vectorX, vectorY);

            return this;
        }
    }
}
