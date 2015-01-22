namespace FlockingBirds.Game.Extensions
{
    using SharpDX.Toolkit.Input;

    public static class MouseServiceExtensions
    {
        private static readonly object ResolveMouseLock = new object();

        public static IMouseService Resolve(SharpDX.Toolkit.Game game)
        {
            lock (ResolveMouseLock)
            {
                var mouse = game.Services.GetService(typeof (IMouseService)) as IMouseService;

                return mouse ?? new MouseManager(game);
            }
        }
    }
}
