namespace FlockingBirds.Game.DrawableParts.Defaults
{
    using SharpDX.Toolkit.Content;
    using SharpDX.Toolkit.Graphics;

    public class DefaultDrawableParts : IDrawableParts
    {
        private readonly IBackgroundGamePart gameBackground;

        private readonly IBirdsGamePart birds;

        private readonly IMousePart mousePresenter;

        public DefaultDrawableParts(
                        IBackgroundGamePart gameBackgroundPart,
                        IBirdsGamePart birds, IMousePart mousePresenter)
        {
            this.gameBackground = gameBackgroundPart;
            this.birds = birds;
            this.mousePresenter = mousePresenter;
        }

        public IBackgroundGamePart GameBackground
        {
            get { return this.gameBackground; }
        }

        public IMousePart MouseRepresenter
        {
            get { return this.mousePresenter; }
        }

        public void LoadAll(ContentManager contentManager, GraphicsDevice graphicsDevice, SharpDX.Toolkit.Game game)
        {
            this.gameBackground.Load(contentManager, graphicsDevice, game);
            this.birds.Load(contentManager, graphicsDevice, game);
            this.mousePresenter.Load(contentManager, graphicsDevice, game);
        }


        public IBirdsGamePart Birds
        {
            get { return this.birds; }
        }

    }
}
