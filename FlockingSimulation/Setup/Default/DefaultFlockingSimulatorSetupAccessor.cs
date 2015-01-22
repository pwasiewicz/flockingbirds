namespace FlockingSimulation.Setup.Default
{
    using Setup;
    using SharpDX;
    using System;

    internal class DefaultFlockingSimulatorSetupAccessor
        : IFlockingSetupAccessor
    {
        private DefaultFlockingSimulatorSetup setup;

        public DefaultFlockingSimulatorSetupAccessor(
            Action<IFlockingSetup> setup)
        {
            var builder = new DefaultFlockingSimulatorSetup();

            setup(builder);

            this.setup = builder;
        }

        public int Width
        {
            get
            {
                return this.setup.Width;
            }
        }

        public int Height
        {
            get
            {
                return this.setup.Height;
            }
        }


        public int BirdsCount
        {
            get
            {
                return this.setup.BirdsCount;
            }
        }

        public float BirdVisibilityDistance
        {
            get 
            {
                return this.setup.VisibilityDistance;
            }
        }


        public bool FullScreen
        {
            get 
            {
                return this.setup.IsFullScreen;
            }
        }


        public bool RunAtStart
        {
            get 
            {
                return this.setup.RunAtStart;
            }
        }

        public float MaximumSpeed
        {
            get 
            {
                return this.setup.MaxSpeed;
            }
        }

        public float Separation
        {
            get
            {
                return this.setup.Separation;
            }
        }

        public float Cohesion
        {
            get { return this.setup.Cohesion; }
        }

        public float Alignment
        {
            get { return this.setup.Alignment; }
        }

        public bool MouseInteraction
        {
            get { return this.setup.MouseInteractionEnabled; }
        }

        public float MaxSteer
        {
            get { return this.setup.Steer; }
        }


        public int GroupCount
        {
            get { return this.setup.GroupCount; }
        }


        public Vector2 Wind
        {
            get { return this.setup.WindVector; }
        }
    }
}
