using FlockingBirds.Configurator.ViewModels;

namespace FlockingBirds.Configurator.Dependencies
{
    using Autofac;

    internal class Container
    {
        private readonly IContainer container;

        internal Container()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MainWindowViewModel>().As<IMainWindowViewModel>();
            builder.RegisterType<FlockingBirdsExecuter>().As<IFlockingBirdsExecuter>();
            builder.RegisterType<FlockingArgumentsSettingsStore>().As<IFlockingArgumentsSettingsStore>();
            builder.RegisterType<CompressingService>().As<ICompressingService>();

            this.container = builder.Build();
        }

        internal T Resolve<T>()
        {
            return this.container.Resolve<T>();
        }
    }
}
