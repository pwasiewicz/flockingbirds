
namespace FlockingBirds.Configurator.ViewModels
{
    public interface IFlockingArgumentsViewModel
    {
        bool FullScreenMode { get; set; }

        bool RunAtStart { get; set; }

        int Groups { get; set; }

        int BirdsCount { get; set; }

        int Width { get; set; }

        int Height { get; set; }

        int VisibilityDistance { get; set; }

        float MaxBirdSpeed { get; set; }

        float BirdsSeparation { get; set; }

        float BirdsAlignment { get; set; }

        float BirdsCohesion { get; set; }

        bool MouseInteraction { get; set; }

        float BirdMaxSteer { get; set; }

        float WindDirection { get; set; }

        float WindPower { get; set; }
    }
}
