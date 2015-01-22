
namespace FlockingSimulation
{
    using System;
    using SharpDX.Toolkit.Input;
    using System.Collections.Generic;
    using SharpDX;

    public interface IFlockingSimulatorEnvironment
    {
        void Reset();

        void Update();

        void Run();

        void Stop();

        IEnumerable<FlockingBirds.Game.Drawable.IDrawableBird> Birds { get; }

        void MouseStateProvider(Func<MouseState> stateProvider);

        void ChangeWind(Vector2? wind);
    }
}
