namespace FlockingBirds.Game.Extensions
{
    using SharpDX.Toolkit.Input;

    public static class KeyboardServiceExtensions
    {
        private static readonly object ResolveKeboardLock = new object();

        public static IKeyboardService Resolve(SharpDX.Toolkit.Game game)
        {
            lock (ResolveKeboardLock)
            {
                var mouse = game.Services.GetService(typeof(IKeyboardService)) as IKeyboardService;

                return mouse ?? new KeyboardManager(game);
            }
        }
    }
}
