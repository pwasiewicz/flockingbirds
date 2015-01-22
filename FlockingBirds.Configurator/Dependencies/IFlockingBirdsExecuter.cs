namespace FlockingBirds.Configurator.Dependencies
{
    using Models;
    using System;

    public interface IFlockingBirdsExecuter
    {
        void Execute(params FlockingBirdsArgument[] arguments);

        string GenerateExecutableArguments(params FlockingBirdsArgument[] arguments);

        string GetFlockingBirdHelp();

        void GetFlockingBirdHelp(Action<IAsyncResult> callback);
    }
}
