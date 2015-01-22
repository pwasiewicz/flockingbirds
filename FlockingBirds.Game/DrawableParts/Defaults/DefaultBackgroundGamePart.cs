namespace FlockingBirds.Game.DrawableParts.Defaults
{
    using FlockingBirds.Game.Textures;
    using FlockingSimulation.Setup;
    using SharpDX;
    using SharpDX.Toolkit;
    using SharpDX.Toolkit.Content;
    using SharpDX.Toolkit.Graphics;

    public class DefaultBackgroundGamePart : IBackgroundGamePart
    {
        private readonly IFlockingBirdsGameTexturesManager textureManager;

        private readonly IFlockingSetupAccessor setupAccessor;

        private GraphicsDevice graphicDeviceManager;

        private SpriteBatch drawingSprite;

        public DefaultBackgroundGamePart(
                        IFlockingBirdsGameTexturesManager textureManager,
                        IFlockingSetupAccessor setupAccessor)
        {
            this.textureManager = textureManager;
            this.setupAccessor = setupAccessor;
        }

        public void Draw(GameTime gameTime)
        {
            this.drawingSprite.Begin();

            var scaleHeight = (float)this.setupAccessor.Height / this.textureManager.BackgroundTexture.Height;
            var scaleWidth = (float)this.setupAccessor.Width / this.textureManager.BackgroundTexture.Width;

            var scale = scaleHeight > scaleWidth ? scaleHeight : scaleWidth;

            this.drawingSprite.Draw(this.textureManager.BackgroundTexture, Vector2.Zero, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 1);

            this.drawingSprite.End();
        }

        public void Update(GameTime gameTime)
        {
        }


        public void Load(ContentManager contentManager, GraphicsDevice deviceManager, Game game)
        {
            this.textureManager.Load(contentManager);

            this.graphicDeviceManager = deviceManager;
            this.drawingSprite = new SpriteBatch(this.graphicDeviceManager);
        }
    }
}
