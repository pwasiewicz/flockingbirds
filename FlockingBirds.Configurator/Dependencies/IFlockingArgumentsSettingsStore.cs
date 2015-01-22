namespace FlockingBirds.Configurator.Dependencies
{
    using ViewModels;

    public interface IFlockingArgumentsSettingsStore
    {
        void StoreDefault(IFlockingArgumentsViewModel flockingArguments);

        void LoadDefault(IFlockingArgumentsViewModel flockingArguments);

        void StoreUser(IFlockingArgumentsViewModel flockingArguments);

        void LoadUser(IFlockingArgumentsViewModel flockingArguments);

        void ResetAll();
    }
}
