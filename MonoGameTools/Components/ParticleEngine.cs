//=============================================================================
// ParticleEngine.cs
//
// Based on the ParticleEngine class by RB Whitaker.
// Source: http://rbwhitaker.wikidot.com/2d-particle-engine-3
//=============================================================================

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Tools.Components {
    public class ParticleEngine {
        /// <summary>
        /// The intial location particles appear from.
        /// </summary>
        public Vector2 Location { get; set; }
        /// <summary>
        /// The maximum amount of particles to generate per update.
        /// </summary>
        public int MaximumParticles { get; set; }
        /// <summary>
        /// Randomized textures to use for particles.
        /// </summary>
        public List<Texture2D> Textures { get; set; }
        /// <summary>
        /// The color mask for the particle texture.
        /// </summary>
        public Color ParticleColor { get; set; }
        /// <summary>
        /// Whether to randomize the particle texture color mask.
        /// </summary>
        public bool RandomizeColor { get; set; }
        /// <summary>
        /// The max amount to randomize the color, from 0 to 255.
        /// </summary>
        public int RandomColorThreshold { get; set; }
        /// <summary>
        /// The minimum size of the particle.
        /// 1.0 is original size; 2.0 is twice as big; etc.
        /// </summary>
        public float ParticleSize { get; set; }
        /// <summary>
        /// Whether to randomize the particle size.
        /// </summary>
        public bool RandomizeSize { get; set; }
        /// <summary>
        /// The maximum random particle size.
        /// 1.0 is original size; 2.0 is twice as big; etc.
        /// </summary>
        public float RandomMaximumSize { get; set; }
        /// <summary>
        /// The minimum particle life span.
        /// </summary>
        public int ParticleLifeSpan { get; set; }
        /// <summary>
        /// Whether to randomize the particle life span.
        /// </summary>
        public bool RandomizeLifeSpan { get; set; }
        /// <summary>
        /// The maximum random particle life span.
        /// </summary>
        public int RandomMaximumLifeSpan { get; set; }
        /// <summary>
        /// The initial angle of each particle.
        /// </summary>
        public float ParticleAngle { get; set; }
        /// <summary>
        /// The speed at which a particle rotates.
        /// </summary>
        public float ParticleAngularVelocity { get; set; }
        /// <summary>
        /// The amount to spread the particles per update.
        /// </summary>
        public float ParticleSpread { get; set; }

        private Random rand;
        private List<Particle> particles;

        /// <summary>
        /// Create the ParticleEngine.
        /// </summary>
        /// <param name="location">The intial location particles appear from</param>
        /// <param name="maximumParticles">The maximum amount of particles to generate per update</param>
        /// <param name="textures">Randomized textures to use for particles</param>
        /// <param name="particleColor">The color mask for the particle texture</param>
        /// <param name="randomizeColor">Whether to randomize the particle texture color mask</param>
        /// <param name="randomColorThreshold">The max amount to randomize the color, from 0 to 255</param>
        /// <param name="particleSize">The minimum size of the particle. 1.0 is original size; 2.0 is twice as big; etc.</param>
        /// <param name="randomizeSize">Whether to randomize the particle size</param>
        /// <param name="randomMaximumSize">The maximum random particle size. 1.0 is original size; 2.0 is twice as big; etc.</param>
        /// <param name="particleLifeSpan">The minimum particle life span</param>
        /// <param name="randomizeLifeSpan">Whether to randomize the particle life span</param>
        /// <param name="randomMaximumLifeSpan">The maximum random particle life span</param>
        /// <param name="particleAngle">The initial angle of each particle</param>
        /// <param name="particleAngularVelocity">The speed at which a particle rotates</param>
        /// <param name="particleSpread">The amount to spread the particles per update</param>
        public ParticleEngine(Vector2 location, int maximumParticles,
            List<Texture2D> textures, Color particleColor,
            bool randomizeColor = false, int randomColorThreshold = 255,
            float particleSize = 1f, bool randomizeSize = false,
            float randomMaximumSize = 1f, int particleLifeSpan = 60,
            bool randomizeLifeSpan = false, int randomMaximumLifeSpan = 60,
            float particleAngle = 0f, float particleAngularVelocity = .1f,
            float particleSpread = 1f) {
            Location = location;
            MaximumParticles = maximumParticles;
            Textures = textures;

            ParticleColor = particleColor;
            RandomizeColor = randomizeColor;
            RandomColorThreshold = randomColorThreshold;

            ParticleSize = particleSize;
            RandomizeSize = randomizeSize;
            RandomMaximumSize = randomMaximumSize;

            ParticleLifeSpan = particleLifeSpan;
            RandomizeLifeSpan = randomizeLifeSpan;
            RandomMaximumLifeSpan = randomMaximumLifeSpan;

            ParticleAngle = particleAngle;
            ParticleAngularVelocity = particleAngularVelocity;

            ParticleSpread = particleSpread;

            rand = new Random();
            particles = new List<Particle>();
        }

        /// <summary>
        /// Update the particle engine status.
        /// </summary>
        public void Update() {
            for(int i = 0; i < MaximumParticles; i++) {
                particles.Add(GenerateParticle());
            }

            for(int i = 0; i < particles.Count; i++) {
                particles[i].Update();

                if(particles[i].LifeSpan <= 0) {
                    particles.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Draw the particles in the engine.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Begin();

            foreach(Particle pt in particles) {
                pt.Draw(spriteBatch);
            }

            spriteBatch.End();
        }

        /// <summary>
        /// Generate a new particle.
        /// </summary>
        /// <returns>A new particle</returns>
        private Particle GenerateParticle() {
            Texture2D texture = Textures[rand.Next(Textures.Count)];
            Vector2 position = Location;
            Vector2 velocity = new Vector2(
                ParticleSpread * (float)(rand.NextDouble() * 2 - 1),
                ParticleSpread * (float)(rand.NextDouble() * 2 - 1));
            float angle = ParticleAngle;
            float angularVelocity = ParticleAngularVelocity * (float)(rand.NextDouble() * 2 - 1);
            float size = ParticleSize;
            int   lifespan = ParticleLifeSpan;
            Color color = ParticleColor;
            int colorThreshold = RandomColorThreshold;

            if(RandomizeSize) {
                size = (float)rand.NextDouble() * (RandomMaximumSize - ParticleSize) + ParticleSize;
            }

            if(RandomizeLifeSpan) {
                lifespan = ParticleLifeSpan + rand.Next(RandomMaximumLifeSpan);
            }

            if(RandomizeColor) {
                int R = rand.Next(color.R - colorThreshold, color.R + colorThreshold);
                int G = rand.Next(color.G - colorThreshold, color.G + colorThreshold);
                int B = rand.Next(color.B - colorThreshold, color.B + colorThreshold);

                if(R >= 255)
                    R = 255;
                else if(R <= 0)
                    R = 0;
                if(G >= 255)
                    G = 255;
                else if(G <= 0)
                    G = 0;
                if(B >= 255)
                    B = 255;
                else if(B <= 0)
                    B = 0;

                color = new Color(R, G, B);
            }

            return new Particle(texture, position, velocity, angle, angularVelocity,
                color, size, lifespan);
        }
    }
}
