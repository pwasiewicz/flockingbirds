namespace FlockingSimulation.Models
{
    using FlockingBirds.Game.Drawable;
    using SharpDX;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Extensions;
    using SharpDX.Toolkit.Input;

    public class Bird : IDrawableBird
    {
        private static readonly Random Random;

        private readonly float setupMaxSpeed;

        private Vector2 position;

        private Vector2 velocity;

        private Vector2 acceleration;

        private float maxSpeed;

        private float neighbourhoodRadius;

        private float maxSteerForce;

        private float sMod;

        private float cMod;

        private float aMod;

        private Point bounds;

        private Vector2 lastCohesion;

        private Vector2 lastAlignment;

        private Vector2 lastSeperation;

        private Vector2 velocitylastDirection;

        private bool mouseInteraction;

        static Bird()
        {
            Random = new Random();
        }

        internal Bird(  
                    float maxSpeed,
                    float neighbourhoodRadius,
                    float maxSteerForce,
                    Point bounds,
                    Vector2 startPosition,
                    int group,
                    Func<MouseState> mouseStateAccessor,
                    bool mouseInteraction,
                    float separation,
                    float cohesion,
                    float alignment,
                    Func<Vector2?> windFactory)
        {
            if (maxSpeed < 0)
            {
                throw new ArgumentException("Max speed cannot be less than 0.", "maxSpeed");
            }

            this.maxSpeed = this.setupMaxSpeed = maxSpeed;
            this.neighbourhoodRadius = neighbourhoodRadius;
            this.bounds = bounds;
            this.maxSteerForce = maxSteerForce;
            this.position = startPosition;
            this.Group = group;
            this.mouseInteraction = mouseInteraction;

            this.WindFactory = windFactory;
            this.MouseStateAccessor = mouseStateAccessor;

            this.sMod = separation;
            this.aMod = alignment;
            this.cMod = cohesion;

            this.velocity = Vector2Extension.RandomVector(-this.maxSpeed, this.maxSpeed);
        }

        public float PositionX
        {
            get { return this.position.X; }
        }

        public float PositionY
        {
            get { return this.position.Y; }
        }

        public float VelocityX
        {
            get { return this.velocity.X; }
        }

        public float VelocityY
        {
            get { return this.velocity.Y; }
        }

        public int Group { get; set; }

        internal Func<MouseState> MouseStateAccessor { get; set; }

        internal Func<Vector2?> WindFactory {get; set;}

        private void CheckBounds()
        {
            if (this.PositionX > this.bounds.X)
            {
                this.position.X = 0;
            }

            if (this.PositionX < 0)
            {
                this.position.X = this.bounds.X;
            }

            if (this.PositionY > this.bounds.Y)
            {
                this.position.Y = 0;
            }

            if (this.PositionY < 0)
            {
                this.position.Y = this.bounds.Y;
            }
        }

        public void Update(IEnumerable<Bird> birds)
        {
            var mouseState = this.MouseStateAccessor();
            var mousePosVector = new Vector2(mouseState.X*this.bounds.X, mouseState.Y*this.bounds.Y);

            var localBirds = birds as IList<Bird> ?? birds.ToList();

            var lastAcceleration = this.acceleration;

            if (this.mouseInteraction)
            {
                this.acceleration += this.Avoid(mousePosVector, weight: true);
            }

            this.lastAlignment = this.Alignment(localBirds);
            this.lastCohesion = this.Cohesion(localBirds);
            this.lastSeperation = this.Seperation(localBirds);

            var mouseDist = Vector2.Distance(this.position, mousePosVector);

            if (mouseDist > 100)
            {
                this.acceleration = Vector2.Add(this.acceleration, Vector2.Multiply(this.lastAlignment, aMod));
                this.acceleration = Vector2.Add(this.acceleration, Vector2.Multiply(this.lastCohesion, cMod));
                this.acceleration = Vector2.Add(this.acceleration, Vector2.Multiply(this.lastSeperation, sMod));
            }

            if (this.mouseInteraction && mouseDist < 50)
            {
                this.maxSpeed = 2*this.setupMaxSpeed;
            }
            else
            {
                this.maxSpeed = this.setupMaxSpeed;
            }

            this.Move();
            this.CheckBounds();
        }

        private void Move()
        {
            this.velocity += this.acceleration;
            this.velocity = this.velocity.Limit(this.maxSpeed);
            this.position += this.velocity;
            this.position += this.GetWind();
            this.acceleration *= 0;
        }

        private Vector2 Avoid(Vector2 target, bool weight)
        {
            var steer = this.position - target;

            if (weight)
            {
                steer *= 1/Vector2.DistanceSquared(this.position, target);
            }

            return steer;
        }

        private Vector2 Seperation(IEnumerable<Bird> birds)
        {
            var positionSum = Vector2Extension.Empty;

            foreach (var bird in  birds)
            {
                var dist = Vector2.Distance(this.position, bird.position);

                if (!(dist > 0) || !(dist <= this.neighbourhoodRadius))
                {
                    continue;
                }

                var repulse = Vector2.Subtract(this.position, bird.position);
                repulse.Normalize();

                repulse = Vector2.Divide(repulse, dist);
                positionSum += repulse;
            }

            return positionSum;
        }

        private Vector2 Alignment(IEnumerable<Bird> birds)
        {
            var velocitySum = Vector2Extension.Empty;

            var count = 0;

            foreach (var bird in from bird in birds
                let dist = Vector2.Distance(this.position, bird.position)
                where dist > 0 && dist <= this.neighbourhoodRadius
                select bird)
            {
                velocitySum += bird.velocity;
                count += 1;
            }

            if (count <= 0)
            {
                return velocitySum;
            }

            velocitySum = Vector2.Divide(velocitySum, count);
            velocitySum = velocitySum.Limit(this.maxSteerForce);

            return velocitySum;
        }

        private Vector2 Cohesion(IEnumerable<Bird> birds)
        {
            var positionSum = Vector2Extension.Empty;

            var count = 0;

            foreach (var bird in from bird in birds
                let dist = Vector2.Distance(this.position, bird.position)
                where dist > 0 && dist <= this.neighbourhoodRadius
                select bird)
            {
                positionSum += bird.position;
                count += 1;
            }

            if (count > 0)
            {
                positionSum /= count;
            }

            var steer = Vector2.Subtract(positionSum, this.position);
            steer = steer.Limit(this.maxSteerForce);

            return steer;
        }

        private Vector2 GetWind()
        {
            if (this.WindFactory == null)
            {
                return Vector2Extension.Empty;
            }

            var wind = this.WindFactory();

            return wind.HasValue ? wind.Value : Vector2Extension.Empty;
        }
    }
}