namespace FlockingBirds.Game.Drawable
{
    public interface IDrawableBird
    {
        float PositionX { get; }

        float PositionY { get; }

        float VelocityX { get; }

        float VelocityY { get; }

        int Group { get; }
    }
}
