namespace FlockingBirds.Configurator.ViewModels
{
    using System.Windows.Input;
    using System.Windows.Media;

    public interface IMainWindowViewModel : IFlockingArgumentsViewModel
    {
        string CurrentFlockingBirdArguments { get; set; }

        string FlockingBirdsHelp { get; }

        string EngineDescription { get; }

        double ArrowRotationAngle { get; set; }

        ICommand RefreshFlockingBirdArguments { get; }

        ICommand RunFlockingBirds { get; }

        ICommand Close { get; }

        ICommand StoreAsDefault { get; }

        ICommand StoreUser { get; }

        ICommand LoadUser { get; }

        ICommand ResetDefault { get; }
    }
}
