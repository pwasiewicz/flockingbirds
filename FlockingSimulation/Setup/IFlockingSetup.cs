namespace FlockingSimulation.Setup
{
    public interface IFlockingSetup
    {
        IFlockingSetup SetBirdsCount(int count);

        IFlockingSetup SetEnvironmentSize(int width, int height);

        IFlockingSetup SetVisibilityDistance(float distance);

        IFlockingSetup SetFullScreenMode(bool fullscreen);

        IFlockingSetup SetRunAtStart(bool runAtStart);

        IFlockingSetup SetGroups(int count);

        IFlockingSetup MaximumSpeed(float speed);

        IFlockingSetup MouseInteraction(bool active);

        IFlockingSetup BirdsSeparation(float separation);

        IFlockingSetup BirdsCohesion(float cohesion);

        IFlockingSetup BirdsAlignment(float alignment);

        IFlockingSetup MaxSteer(float steer);

        IFlockingSetup Wind(float vectorX, float vectorY);
    }
}
