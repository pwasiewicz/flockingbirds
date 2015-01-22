namespace FlockingBirds.Game.Textures
{
    using SharpDX.Toolkit.Content;
    using SharpDX.Toolkit.Graphics;

    public interface IFlockingBirdsGameTexturesManager
    {
        Texture2D BackgroundTexture { get; }

        Texture2D MouseTexture { get; }

        Texture2D PrimitiveBird { get; }

        void Load(ContentManager contentManager);
    }
}