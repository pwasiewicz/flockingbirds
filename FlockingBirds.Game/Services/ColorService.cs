namespace FlockingBirds.Game.Services
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using SharpDX;

    public class ColorService : IColorService
    {
        private static readonly Random RandomInstance;
        private static readonly ConcurrentDictionary<int, Color> Colors;
        private static readonly ConcurrentBag<Color> AvailableColors; 

        static ColorService()
        {
            RandomInstance = new Random();
            Colors = new ConcurrentDictionary<int, Color>();
            AvailableColors = new ConcurrentBag<Color>();
        }

        public Color GetColor(int id, ColorModes mode)
        {
            if (this.StaticPerShell(mode))
            {
                return this.HandleStaticPerShell(id, mode);
            }

            throw new NotImplementedException();
        }

        private Color HandleStaticPerShell(int id, ColorModes mode)
        {
            return Colors.GetOrAdd(id, addedId => GetColor(mode));
        }

        private Color GetColor(ColorModes mode)
        {
            var allColors = AvailableColors.ToArray();

            var colorsToGetFrom = allColors as IEnumerable<Color>;

            if (this.UniqeColor(mode))
            {
                colorsToGetFrom = colorsToGetFrom.Except(Colors.Values);
                if (!colorsToGetFrom.Any())
                {
                    throw new InvalidOperationException("All color are used.");
                }
            }

            var colors = colorsToGetFrom.ToList();

            var elementAt = RandomInstance.Next(colors.Count);

            return colors[elementAt];
        }

        public void AssignColor(int id, Color color)
        {
            Colors.AddOrUpdate(id, color, (i, oldColor) => color);
        }

        public void RegisterColor(Color color)
        {
            AvailableColors.Add(color);
        }

        private bool StaticPerShell(ColorModes mode)
        {
            return (mode & ColorModes.ConstPerShell) == ColorModes.ConstPerShell;
        }

        private bool UniqeColor(ColorModes mode)
        {
            return (mode & ColorModes.Uninqe) == ColorModes.Uninqe;
        }
    }
}
