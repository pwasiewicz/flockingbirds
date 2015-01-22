namespace FlockingBirds.Game.DrawableParts.Defaults
{
    using Textures;
    using SharpDX;
    using SharpDX.Toolkit;
    using SharpDX.Toolkit.Content;
    using SharpDX.Toolkit.Graphics;
    using Extensions;
    using FlockingSimulation.Setup;
    using SharpDX.Toolkit.Input;

    public class MousePart : IMousePart
    {
        private readonly IFlockingBirdsGameTexturesManager textureManager;

        private readonly IFlockingSetupAccessor setup;

        private IMouseService mouseService;

        private GraphicsDevice graphicsDevice;

        private Vector2 lastPosition;

        private Game game;

        public MousePart(IFlockingBirdsGameTexturesManager textureManager, IFlockingSetupAccessor setup)
        {
            this.textureManager = textureManager;
            this.setup = setup;
            
        }

        public void Draw(GameTime gameTime)
        {
            var sprite = new SpriteBatch(graphicsDevice);

            sprite.Begin();

            sprite.Draw(
                this.textureManager.MouseTexture,
                new Rectangle((int) this.lastPosition.X, (int) this.lastPosition.Y, 25, 25),
                Color.White);

            sprite.End();
        }

        public void Update(GameTime gameTime)
        {
           var state = this.mouseService.GetState();

            this.lastPosition = new Vector2(state.X * this.setup.Width, state.Y * this.setup.Height);
        }

        public void Load(ContentManager contentManager, GraphicsDevice deviceManager, Game game)
        {
            textureManager.Load(contentManager);

            this.graphicsDevice = deviceManager;
            this.game = game;

            this.mouseService = MouseServiceExtensions.Resolve(this.game);
        }

    }
}
