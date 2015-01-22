namespace FlockingBirds.Game.Services
{
    using System;

    using SharpDX;

    public interface IColorService
    {
        Color GetColor(int id, ColorModes mode);

        void AssignColor(int id, Color color);

        void RegisterColor(Color color);
    }

    [Flags]
    public enum ColorModes
    {
        ConstPerShell = 0x0,
        Uninqe = 0x1
    }
}
