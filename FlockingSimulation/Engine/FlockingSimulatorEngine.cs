namespace FlockingSimulation.Engine
{
    using Extensions;
    using Models;
    using Setup;
    using SharpDX;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using SharpDX.Toolkit.Input;

    internal class FlockingSimulatorEngine : IFlockingSimulatorEngine
    {
        private static readonly Random Random;

        private readonly IFlockingSetupAccessor setupAccessor;

        private ILookup<int, Bird> birds;

        private Func<MouseState> mouseStateAccessor;

        static FlockingSimulatorEngine()
        {
            Random = new Random();
        }

        public FlockingSimulatorEngine(IFlockingSetupAccessor setupAccessor)
        {
            this.setupAccessor = setupAccessor;
            this.IsRunning = setupAccessor.RunAtStart;
        }


        public IEnumerable<Bird> Birds
        {
            get
            {
                return this.birds.SelectMany(b => b);
            }
        }

        public bool IsRunning { get; set; }

        public void Reset()
        {
            this.InitializeBirdsCollection(this.setupAccessor.BirdsCount, this.setupAccessor.GroupCount);
        }

        public void Update()
        {
            if (!this.IsRunning)
            {
                return;
            }

            foreach (var group in this.birds)
            {
                var birds = this.birds[group.Key];

                foreach (var bird in birds)
                {
                    bird.Update(birds);
                }
            }
        }

        public void ProvideMouseState(Func<MouseState> stateProvider)
        {
            this.mouseStateAccessor = stateProvider;

            foreach (var bird in this.Birds)
            {
                bird.MouseStateAccessor = this.mouseStateAccessor;
            }
        }

        public void ChangeWind(Vector2? direction)
        {
            foreach (var bird in this.Birds)
            {
                bird.WindFactory = () => direction;
            }
        }

        private void InitializeBirdsCollection(int birdsCount, int groupsCount = 1)
        {
            if (groupsCount < 1)
            {
                throw new ArgumentException("Groups count should be min. 1.");
            }

            var birdsSequence = EnumerableExtension.GenerateSequence(
                        birdsCount,
                        () => this.CreateBird(group: 1),
                        typeof(LinkedList<Bird>));

            for (var i = 2; i <= groupsCount; i++)
            {
                var iLocal = i;
                birdsSequence = birdsSequence.Concat(EnumerableExtension.GenerateSequence(
                    birdsCount,
                    () => this.CreateBird(group: iLocal),
                    typeof(LinkedList<Bird>)));
            }

            this.birds = birdsSequence.ToLookup(b => b.Group, b => b);

            this.ChangeWind(this.setupAccessor.Wind);
        }

        private Bird CreateBird(int group)
        {
            return new Bird(
                this.setupAccessor.MaximumSpeed,
                this.setupAccessor.BirdVisibilityDistance,
                this.setupAccessor.MaxSteer,
                new Point(this.setupAccessor.Width, this.setupAccessor.Height),
                new Vector2(
                    (float) this.setupAccessor.Width/2,
                    (float) this.setupAccessor.Height/2),
                group,
                this.mouseStateAccessor,
                this.setupAccessor.MouseInteraction,
                this.setupAccessor.Separation,
                this.setupAccessor.Cohesion,
                this.setupAccessor.Alignment,
                () => Vector2Extension.Empty);
        }
    }
}
