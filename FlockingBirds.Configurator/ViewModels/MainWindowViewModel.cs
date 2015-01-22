namespace FlockingBirds.Configurator.ViewModels
{
    using Common;
    using Dependencies;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;
    using System.Text;
    using System.Windows.Media.Animation;
    using System.Windows.Media;
    using System;
    using System.Windows.Threading;


    public class MainWindowViewModel : BindableBase, IMainWindowViewModel
    {
        private readonly IFlockingBirdsExecuter flockingBirdsExecuter;

        private readonly IFlockingArgumentsSettingsStore settingsStore;

        private bool fullScreenMode;

        private bool runAtStart;

        private int groups;

        private int birdsCount;

        private int width;

        private int height;

        private int visibilityDistance;

        private float maxBirdSpeed;

        private float maxBirdSteer;

        private string currArguments;

        private bool mouseInteraction;

        private float birdsSeparation;

        private string flockingBirdsHelp;

        private string engineDescriptionCache;

        private float birdsAlignment;

        private float birdsCohesion;

        private float windDirection;

        private float windPower;

        public MainWindowViewModel(IFlockingBirdsExecuter flockingBirdsExecuter, IFlockingArgumentsSettingsStore settingsStore)
        {
            this.flockingBirdsExecuter = flockingBirdsExecuter;
            this.settingsStore = settingsStore;

            this.SetDefaultValues();

            this.settingsStore.LoadDefault(this);

            try
            {
                this.flockingBirdsHelp = "Loading...";
                this.EngineDescription = this.BuildEngineDescription();
                this.flockingBirdsExecuter.GetFlockingBirdHelp(call => {
                    var func = call.AsyncState as Func<string>;
                    try
                    {
                        var result = func.EndInvoke(call);

                        this.flockingBirdsHelp = result;
                    }
                    catch
                    {

                        this.flockingBirdsHelp = null;
                    }

                    this.OnPropertyChanged("FlockingBirdsHelp");
                });
            }
            catch
            {
                this.flockingBirdsHelp = null;
            }
        }

        public int VisibilityDistance
        {
            get
            {
                return this.visibilityDistance;
            }

            set
            {
                this.SetProperty(ref this.visibilityDistance, value);
            }
        }

        public float MaxBirdSpeed
        {
            get
            {
                return this.maxBirdSpeed;
            }

            set
            {
                this.SetProperty(ref this.maxBirdSpeed, value);
            }
        }

        public float BirdsSeparation
        {
            get
            {
                return this.birdsSeparation;
            }

            set
            {
                this.SetProperty(ref this.birdsSeparation, value);
            }
        }

        public float BirdsCohesion
        {
            get { return this.birdsCohesion; }
            set { this.SetProperty(ref this.birdsCohesion, value); }
        }

        public float BirdsAlignment
        {
            get { return this.birdsAlignment; }
            set { this.SetProperty(ref this.birdsAlignment, value); }
        }

        public string CurrentFlockingBirdArguments
        {
            get
            {
                return this.currArguments;
            }

            set
            {
                this.SetProperty(ref this.currArguments, value);
            }
        }

        public bool MouseInteraction
        {
            get
            {
                return this.mouseInteraction;
            }

            set
            {
                this.SetProperty(ref this.mouseInteraction, value);
            }
        }

        public float BirdMaxSteer
        {
            get
            {
                return this.maxBirdSteer;
            }

            set
            {
                this.SetProperty(ref this.maxBirdSteer, value);
            }
        }

        public bool FullScreenMode
        {
            get
            {
                return this.fullScreenMode;
            }
            set
            {
                this.SetProperty(ref this.fullScreenMode, value);
            }
        }

        public bool RunAtStart
        {
            get
            {
                return this.runAtStart;
            }

            set
            {
                this.SetProperty(ref this.runAtStart, value);
            }
        }

        public int Groups
        {
            get
            {
                return this.groups;
            }

            set
            {
                this.SetProperty(ref this.groups, value);
            }
        }

        public int BirdsCount
        {
            get
            {
                return this.birdsCount;
            }

            set
            {
                this.SetProperty(ref this.birdsCount, value);
            }
        }

        public int Width
        {
            get
            {
                return this.width;
            }

            set
            {
                this.SetProperty(ref this.width, value);
            }
        }

        public int Height
        {
            get
            {
                return this.height;
            }

            set
            {
                this.SetProperty(ref this.height, value);
            }
        }

        public string FlockingBirdsHelp
        {
            get
            {
                if (!string.IsNullOrEmpty(this.flockingBirdsHelp))
                {
                    return this.flockingBirdsHelp;
                }

                return "Help unavailable.";
            }
        }



        public float WindDirection
        {
            get
            {
                return this.windDirection;
            }
            set
            {
                this.SetProperty(ref this.windDirection, value);
                this.OnPropertyChanged("ArrowRotationAngle");
            }
        }

        public float WindPower
        {
            get
            {
                return this.windPower;
            }
            set
            {
                this.SetProperty(ref this.windPower, value);
            }
        }

        public double ArrowRotationAngle
        {
            get
            {
                return -this.WindDirection;
            }
            set
            {
                this.WindDirection = Convert.ToSingle(-value);
            }
        }

        public string EngineDescription { get; private set; }

        public ICommand RefreshFlockingBirdArguments
        {
            get
            {
                return new DelegateCommand(this.UpdateFlockingBirdsArgument);
            }
        }

        public ICommand RunFlockingBirds
        {
            get
            {
                return new DelegateCommand(this.ExecuteFlockingBirds);
            }
        }

        public ICommand Close
        {
            get
            {
                return new DelegateCommand(() => Application.Current.Shutdown());
            }
        }

        public ICommand StoreAsDefault
        {
            get
            {
                return new DelegateCommand(() => this.settingsStore.StoreDefault(this));
            }
        }

        public ICommand StoreUser
        {
            get
            {
                return new DelegateCommand(() => this.settingsStore.StoreUser(this));
            }
        }

        public ICommand LoadUser
        {
            get
            {
                return new DelegateCommand(() => this.settingsStore.LoadUser(this));
            }
        }

        public ICommand ResetDefault
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    this.settingsStore.ResetAll();
                    this.SetDefaultValues();
                });
            }
        }

        private void ExecuteFlockingBirds()
        {
            this.flockingBirdsExecuter.Execute(this.BuildExecuterArguments().ToArray());
        }

        private void UpdateFlockingBirdsArgument()
        {
            this.CurrentFlockingBirdArguments =
                this.flockingBirdsExecuter.GenerateExecutableArguments(this.BuildExecuterArguments().ToArray());
        }

        private string BuildEngineDescription()
        {
            if (!string.IsNullOrEmpty(this.engineDescriptionCache))
            {
                return this.engineDescriptionCache;
            }

            var builder = new StringBuilder();

            builder.AppendLine("From Wikipedia, the free encyclopedia.\n");

            builder.AppendLine(
                "Flocking behavior is the behavior exhibited when a group of birds, called a flock, are foraging or in flight. There are parallels with the shoaling behavior of fish, the swarming behavior of insects, and herd behavior of land animals.\n");

            builder.AppendLine("Basic models of flocking behavior are controlled by three simple rules:");
            builder.AppendLine("\t1. Separation - avoid crowding neighbors (short range repulsion)");
            builder.AppendLine("\t2. Alignment - steer towards average heading of neighbors");
            builder.AppendLine("\t3. Cohesion - steer towards average position of neighbors (long range attraction)");

            this.engineDescriptionCache = builder.ToString();

            return this.engineDescriptionCache;
        }

        private void SetDefaultValues()
        {

            this.FullScreenMode = false;
            this.RunAtStart = true;
            this.Groups = 1;
            this.BirdsCount = 50;
            this.Width = 640;
            this.Height = 480;
            this.VisibilityDistance = 100;
            this.MaxBirdSpeed = 1f;
            this.MouseInteraction = true;
            this.BirdsSeparation = 10f;
            this.BirdsAlignment = 6f;
            this.BirdsCohesion = 4f;
            this.BirdMaxSteer = 0.9f;
            this.WindDirection = 0;
            this.WindPower = 0;
        }

        private FlockingBirdsArgument BuildWindArgument()
        {
            var rad = this.DegreeToRadian(this.WindDirection);

            var x = 1;
            var y = 0;

            var cs = Math.Cos(rad);
            var sn = Math.Sin(rad);

            var px = x * cs - y * sn;
            var py = x * sn + y * cs;

            return FlockingBirdsArgument.Wind(Convert.ToSingle(px * this.WindPower), Convert.ToSingle(py * this.WindPower));
        }

        private IEnumerable<FlockingBirdsArgument> BuildExecuterArguments()
        {
            if (this.FullScreenMode)
            {
                yield return FlockingBirdsArgument.FullScreenMode();
            }

            if (this.RunAtStart)
            {
                yield return FlockingBirdsArgument.RunAtStart();
            }

            if (!this.MouseInteraction)
            {
                yield return FlockingBirdsArgument.NoMouseInteraction();
            }

            yield return FlockingBirdsArgument.Groups(this.Groups);
            yield return FlockingBirdsArgument.BirdsCount(this.BirdsCount);
            yield return FlockingBirdsArgument.VisibilityDistance(this.VisibilityDistance);
            yield return FlockingBirdsArgument.MaxBirdSpeed(this.MaxBirdSpeed);
            yield return FlockingBirdsArgument.Size(this.Width, this.Height);
            yield return
                FlockingBirdsArgument.BirdsSeparation(this.BirdsSeparation);
            yield return FlockingBirdsArgument.MaxBirdSteer(this.BirdMaxSteer);
            yield return FlockingBirdsArgument.BirdsCohesion(this.BirdsCohesion);
            yield return FlockingBirdsArgument.BirdsAignment(this.BirdsAlignment);
            yield return this.BuildWindArgument();
        }

        private double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }
    }
}
