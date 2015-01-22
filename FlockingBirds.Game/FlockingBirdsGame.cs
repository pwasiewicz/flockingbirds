namespace FlockingBirds.Game
{
    using SharpDX;
    using FlockingBirds.Game.DrawableParts;
    using FlockingBirds.Game.Textures;
    using FlockingSimulation.Setup;
    using SharpDX.Toolkit;
    using SharpDX.Toolkit.Graphics;
    // Use these namespaces here to override SharpDX.Direct3D11

    /// <summary>
    /// Simple FlockingBirds game using SharpDX.Toolkit.
    /// </summary>
    public class FlockingBirdsGame : Game, IFlockingBirdsGame
    {
        private readonly GraphicsDeviceManager graphicsDeviceManager;

        private BasicEffect basicEffect;

        private readonly IFlockingSetupAccessor setup;

        private readonly IFlockingBirdsGameTexturesManager texturesManager;

        private readonly IDrawableParts drawableParts;

        public FlockingBirdsGame(
                    IFlockingSetupAccessor setup,
                    IFlockingBirdsGameTexturesManager texturesManager,
                    IDrawableParts drawableParts)
        {
            this.setup = setup;
            this.texturesManager = texturesManager;
            this.drawableParts = drawableParts;

            // Creates a graphics manager. This is mandatory.
            graphicsDeviceManager = new GraphicsDeviceManager(this);

            // Setup the relative directory to the executable directory
            // for loading contents with the ContentManager
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // Modify the title of the window
            Window.Title = "Flocking Birds";

            this.graphicsDeviceManager.PreferredBackBufferHeight =
                this.setup.Height;
            this.graphicsDeviceManager.PreferredBackBufferWidth =
                this.setup.Width;

            this.graphicsDeviceManager.IsFullScreen = this.setup.FullScreen;

            this.graphicsDeviceManager.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {

            // Creates a basic effect
            basicEffect = ToDisposeContent(new BasicEffect(GraphicsDevice));
            basicEffect.EnableDefaultLighting();

            this.texturesManager.Load(this.Content);
            this.drawableParts.LoadAll(this.Content, this.GraphicsDevice, this);

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            this.drawableParts.Birds.Update(gameTime);
            this.drawableParts.MouseRepresenter.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // Clears the screen with the Color.CornflowerBlue
            GraphicsDevice.Clear(Color.CornflowerBlue);

            this.drawableParts.GameBackground.Draw(gameTime);

            this.drawableParts.Birds.Draw(gameTime);
            this.drawableParts.MouseRepresenter.Draw(gameTime);

            base.Draw(gameTime);
        }

        void IFlockingBirdsGame.Run()
        {
            this.Run();
        }

        void System.IDisposable.Dispose()
        {
            this.Dispose();
        }
    }
}
