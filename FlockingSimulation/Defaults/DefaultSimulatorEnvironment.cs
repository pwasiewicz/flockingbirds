namespace FlockingSimulation.Defaults
{
    using Engine;
    using Setup;
    using System.Collections.Generic;
    using System;
    using System.Windows.Forms;
    using SharpDX.Toolkit.Input;
    using SharpDX;

    internal class DefaultSimulatorEnvironment : IFlockingSimulatorEnvironment
    {
        private readonly IFlockingSetupAccessor setupAccessor;

        private readonly IFlockingSimulatorEngine flockingEngine;

        public DefaultSimulatorEnvironment(
            IFlockingSetupAccessor setupAccessor,
            IFlockingSimulatorEngine flockingEngine)
        {
            this.flockingEngine = flockingEngine;
            this.setupAccessor = setupAccessor;
        }

        public IEnumerable<FlockingBirds.Game.Drawable.IDrawableBird> Birds
        {
            get
            {
                return this.flockingEngine.Birds;
            }
        }

        public void MouseStateProvider(Func<MouseState> stateProvider)
        {
            this.flockingEngine.ProvideMouseState(stateProvider);
        }

        public void Reset()
        {
            this.flockingEngine.Reset();
            this.flockingEngine.IsRunning = false;
        }

        public void Update()
        {
            this.flockingEngine.Update();
        }


        public void Run()
        {
            this.flockingEngine.IsRunning = true;
        }

        public void Stop()
        {
            this.flockingEngine.IsRunning = false;
        }

        public void ChangeWind(Vector2? wind)
        {
            this.flockingEngine.ChangeWind(wind);
        }
    }
}
