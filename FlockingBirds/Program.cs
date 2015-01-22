namespace FlockingBirds
{
    using Autofac;

    using FlockingBirds.Game.Services;

    using Game;
    using Game.DrawableParts;
    using Game.DrawableParts.Defaults;
    using Game.Textures;
    using FlockingSimulation;
    using FlockingSimulation.Defaults;
    using FlockingSimulation.Engine;
    using FlockingSimulation.Setup;
    using FlockingSimulation.Setup.Default;
    using ParametersCore;
    using NLog;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Simple FlockingBirds application using SharpDX.Toolkit.
    /// </summary>
    public class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
#if NETFX_CORE
        [MTAThread]
#else
        [STAThread]
#endif
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            if (ProgramParameters.IsAskingForHelp(args))
            {
                ProgramParameters.PrintHelp();
                return;
            }

            Logger.Info("Preparing dependencies...");


            var builder = new ContainerBuilder();
            IContainer container;

            try
            {
                builder.RegisterType<FlockingSimulatorEngine>()
                       .As<IFlockingSimulatorEngine>()
                       .InstancePerDependency();

                Action<IFlockingSetup> setupAction =
                    setup => GetFlockingSetup(setup, args);

                builder.RegisterType<DefaultSimulatorEnvironment>()
                       .As<IFlockingSimulatorEnvironment>()
                       .SingleInstance();

                builder.RegisterType<DefaultFlockingSimulatorSetupAccessor>()
                       .As<IFlockingSetupAccessor>()
                       .WithParameter("setup", setupAction)
                       .SingleInstance();

                builder.RegisterType<FlockingBirdsGameTexturesManager>()
                       .As<IFlockingBirdsGameTexturesManager>()
                       .SingleInstance();

                builder.RegisterType<FlockingBirdsGame>()
                       .As<IFlockingBirdsGame>();

                builder.RegisterType<DefaultBackgroundGamePart>()
                       .As<IBackgroundGamePart>();

                builder.RegisterType<DefaultDrawableParts>()
                       .As<IDrawableParts>();

                builder.RegisterType<DefaultBirdsGamePart>()
                       .As<IBirdsGamePart>();

                builder.RegisterType<MousePart>().As<IMousePart>();

                builder.RegisterType<ColorService>().As<IColorService>();

                container = builder.Build();
            }
            catch (Exception ex)
            {
                Logger.DebugException("Error while setting AutoFac dependencies.", ex);
                Logger.Fatal("Error while preparing dependencies.");

                throw;
            }

            using (var scope = container.BeginLifetimeScope())
            {
                var game = scope.Resolve<IFlockingBirdsGame>();

                Logger.Info("Starting game...");
                game.Run();
            }
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.Trace("Unhandled exception caught: {0}", e.ExceptionObject);
        }

        [Parameter("fullscreen", false)]
        [ParameterDescription("Determines wheter application should run in fullscreen mode.")]
        public static void FullScreenModeParameter(IFlockingSetup setup, string value)
        {
            setup.SetFullScreenMode(true);
        }

        [Parameter("size")]
        [ParameterDescription("Determines the size of application. Format: widthxheight (like 320x240).")]
        public static void EnvironmentSizeParameter(IFlockingSetup setup, string value)
        {
            var valueCasted = value;
            var values = valueCasted.Split('x');
            if (values.Length != 2)
            {
                Logger.Fatal("Invalid parameter format. Value: {0}", values);

                throw new ArgumentException(
                    "Parameter -size should looks like: \"-size 1280x720\"");
            }

            var width = 0;
            var height = 0;

            try
            {
                width = Convert.ToInt32(values[0]);
                height = Convert.ToInt32(values[1]);
            }
            catch
            {
                Logger.Fatal("Could not cast size paremeter value (\"{0}\") to integer.", width, height);

                throw;
            }

            Logger.Info("Found -size parameter. Width: {0}, Height: {1}", width, height);

            setup.SetEnvironmentSize(width, height);
        }

        [Parameter("runAtStart", false)]
        [ParameterDescription("Determines wheter application should run simulation immeditely.")]
        public static void RunAtStartParameter(IFlockingSetup setup, string value)
        {
            setup.SetRunAtStart(true);
        }

        [Parameter("birdsCount")]
        [ParameterDescription("The quantity of birds in group. Less = better performance.")]
        public static void BirdsCountParameter(IFlockingSetup setup, string value)
        {
            setup.SetBirdsCount(Convert.ToInt32(value));
        }

        [Parameter("visibilityDistance")]
        [ParameterDescription("Determines the range of bird's visibility (a circle, in px).")]
        public static void VisibilityDIstanceParameter(IFlockingSetup setup, string value)
        {
            setup.SetVisibilityDistance(Convert.ToSingle(value));
        }

        [Parameter("groups")]
        [ParameterDescription("The quantity of groups.")]
        public static void GroupsParameter(IFlockingSetup setup, string value)
        {
            setup.SetGroups(Convert.ToInt32(value));
        }

        [Parameter("maxBirdSpeed")]
        [ParameterDescription("Birds maximum speed. Expressed in pixels per frame.")]
        public static void BirdSpeed(IFlockingSetup setup, string value)
        {
            setup.MaximumSpeed(Convert.ToSingle(value));
        }

        [Parameter("birdsSeparation")]
        [ParameterDescription("Modifier for birds separation.")]
        public static void BirdsSeparation(IFlockingSetup setup, string value)
        {
            setup.BirdsSeparation(Convert.ToSingle(value));
        }

        [Parameter("noMouseInteraction", false)]
        [ParameterDescription("When set, birds wouldn't interact with mouse.")]
        public static void NoMouseInteraction(IFlockingSetup setup, string value)
        {
            setup.MouseInteraction(active: false);
        }

        [Parameter("maxSteer")]
        [ParameterDescription("Max bird's steer vector length.")]
        public static void MaxSteer(IFlockingSetup setup, string value)
        {
            setup.MaxSteer(Convert.ToSingle(value));
        }

        [Parameter("birdsCohesion")]
        [ParameterDescription("Modifier for birds cohesion.")]
        public static void Cohesion(IFlockingSetup setup, string value)
        {
            setup.BirdsCohesion(Convert.ToSingle(value));
        }

        [Parameter("birdsAlignment")]
        [ParameterDescription("Modifier for birds alignment.")]
        public static void Alignment(IFlockingSetup setup, string value)
        {
            setup.BirdsAlignment(Convert.ToSingle(value));
        }

        [Parameter("wind")]
        [ParameterDescription("Wind vector. Usage: x;y.")]
        public static void Wind(IFlockingSetup setup, string value)
        {
            var args = value.Split(';');
            setup.Wind(Convert.ToSingle(args[0]), Convert.ToSingle(args[1]));
        }

        private static void GetFlockingSetup(IFlockingSetup setup, IEnumerable<string> args)
        {
            Logger.Info("Resolving program arguments...");

            ProgramParameters.GetSetup(setup, args);
        }
    }
}