namespace FlockingBirds.Game.Textures
{
    using SharpDX.Toolkit.Content;
    using SharpDX.Toolkit.Graphics;

    public class FlockingBirdsGameTexturesManager : IFlockingBirdsGameTexturesManager
    {
        private static readonly object lockObject = new object();

        private static bool loaded = false;

        private const string BackgroundTexturePath = "Background/Background.dds";

        private const string MouseTexturePath = "Mouse.dds";

        private const string PrimitiveBirdTexture = "PrimitiveBird.dds";

        public Texture2D BackgroundTexture
        {
            get;
            private set;
        }

        public Texture2D MouseTexture
        {
            get;
            private set;
        }

        public Texture2D PrimitiveBird
        {
            get;
            private set;
        }

        public void Load(ContentManager contentManager)
        {
            if (loaded)
            {
                return;
            }

            lock (lockObject)
            {
                if (loaded)
                {
                    return;
                }

                this.BackgroundTexture = contentManager.Load<Texture2D>(BackgroundTexturePath);
                this.MouseTexture = contentManager.Load<Texture2D>(MouseTexturePath);
                this.PrimitiveBird =
                    contentManager.Load<Texture2D>(PrimitiveBirdTexture);

                loaded = true;
            }
        }
    }
}
