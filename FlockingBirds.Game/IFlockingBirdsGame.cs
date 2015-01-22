using System;

namespace FlockingBirds.Game
{
    public interface IFlockingBirdsGame : IDisposable
    {
        void Run();
    }
}
