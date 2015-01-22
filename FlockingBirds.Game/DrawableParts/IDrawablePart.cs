namespace FlockingBirds.Game.DrawableParts
{
    using SharpDX.Toolkit;
    using SharpDX.Toolkit.Content;
    using SharpDX.Toolkit.Graphics;

    public interface IDrawablePart
    {
        void Draw(GameTime gameTime);

        void Update(GameTime gameTime);

        void Load(
                ContentManager contentManager, 
                GraphicsDevice deviceManager,
                Game game);
    }
}
