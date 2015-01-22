namespace FlockingSimulation.Engine
{
    using FlockingSimulation.Models;
    using SharpDX.Toolkit;
    using System.Collections.Generic;
    using SharpDX;
    using System;
    using SharpDX.Toolkit.Input;

    public interface IFlockingSimulatorEngine
    {
        bool IsRunning { get; set; }

        IEnumerable<Bird> Birds { get; }

        void Reset();

        void Update();

        void ProvideMouseState(Func<MouseState> stateProvider);

        void ChangeWind(Vector2? direction);
    }
}
