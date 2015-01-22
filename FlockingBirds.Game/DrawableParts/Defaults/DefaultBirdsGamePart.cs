
using FlockingBirds.Game.Extensions;
using SharpDX.Toolkit.Input;

namespace FlockingBirds.Game.DrawableParts.Defaults
{
    using FlockingBirds.Game.Services;

    using FlockingSimulation;
    using FlockingSimulation.Setup;

    using SharpDX;
    using SharpDX.Toolkit;
    using SharpDX.Toolkit.Content;
    using SharpDX.Toolkit.Graphics;
    using System;
    using Textures;

    public class DefaultBirdsGamePart : IBirdsGamePart
    {
        private readonly IFlockingSimulatorEnvironment simulator;

        private readonly IFlockingSetupAccessor setup;

        private readonly IFlockingBirdsGameTexturesManager textureManager;

        private readonly IColorService colorService;

        private IMouseService mouseService;

        private GraphicsDevice graphicsDevice;

        private int frameRate = 40;

        private TimeSpan lastFrame;

        public DefaultBirdsGamePart(
                        IFlockingSimulatorEnvironment simulator,
                        IFlockingSetupAccessor setup,
                        IFlockingBirdsGameTexturesManager textureManager, IColorService colorService)
        {
            this.simulator = simulator;
            this.setup = setup;
            this.textureManager = textureManager;
            this.colorService = colorService;

            this.simulator.Reset();
            this.simulator.MouseStateProvider(() => mouseService.GetState());

            this.PrepareColors();

            // TODO: remove this
            this.simulator.Run();
        }

        public void Draw(GameTime gameTime)
        {
            var sprite = new SpriteBatch(this.graphicsDevice);
            sprite.Begin();

            foreach (var bird in this.simulator.Birds)
            {
                var group = bird.Group;
                var color = this.colorService.GetColor(
                    group,
                    ColorModes.ConstPerShell | ColorModes.Uninqe);

                var rotation =
                    Convert.ToSingle(Math.Atan2(bird.VelocityY, bird.VelocityX));
                sprite.Draw(
                    this.textureManager.PrimitiveBird,
                    new Rectangle(
                        (int)Math.Round(bird.PositionX),
                        (int)Math.Round(bird.PositionY),
                        20,
                        10),
                    null,
                    color,
                    rotation,
                    Vector2.Zero,
                    SpriteEffects.None,
                    1);

            }

            sprite.End();
        }

        public void Update(GameTime gameTime)
        {
            this.simulator.Update();
        }

        public void Load(ContentManager contentManager, GraphicsDevice deviceManager, Game game)
        {
            this.graphicsDevice = deviceManager;
            this.mouseService = MouseServiceExtensions.Resolve(game);
        }

        private void PrepareColors()
        {
            this.colorService.RegisterColor(Color.White);
            this.colorService.RegisterColor(Color.Red);
            this.colorService.RegisterColor(Color.Blue);
        }
    }
}
