namespace FlockingSimulation.Setup
{
    using SharpDX;

    public interface IFlockingSetupAccessor
    {
        int Width { get; }

        int Height { get; }

        int BirdsCount { get; }

        float BirdVisibilityDistance { get; }

        int GroupCount { get;  }

        bool FullScreen { get; }

        bool RunAtStart { get; }

        float MaximumSpeed { get; }

        float Separation { get; }

        float Cohesion { get; }

        float Alignment { get; }

        bool MouseInteraction { get; }

        float MaxSteer { get;  }

        Vector2 Wind { get; }
    }
}
