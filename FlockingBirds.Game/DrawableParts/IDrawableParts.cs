namespace FlockingBirds.Game.DrawableParts
{
    using SharpDX.Toolkit.Content;
    using SharpDX.Toolkit.Graphics;

    public interface IDrawableParts
    {
        IBackgroundGamePart GameBackground { get; }

        IBirdsGamePart Birds { get; }

        IMousePart MouseRepresenter { get; }

        void LoadAll(ContentManager contentManager, GraphicsDevice graphicsDevice, SharpDX.Toolkit.Game game);
    }
}
