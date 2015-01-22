namespace FlockingBirds.Configurator.Dependencies
{
    using ViewModels;

    public class ViewModelsLocator
    {
        private readonly Container container;

        public ViewModelsLocator()
        {
            this.container = new Container();
        }

        public IMainWindowViewModel MainWindowViewModel
        {
            get { return this.container.Resolve<IMainWindowViewModel>(); }
        }
    }
}
