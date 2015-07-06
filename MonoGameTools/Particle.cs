//===================================================================
// Particle.cs
//
// Based on the Particle class by RB Whitaker.
// Source: http://rbwhitaker.wikidot.com/2d-particle-engine-2
//===================================================================

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Tools {
    public class Particle {
        /// <summary>
        /// The particle texture.
        /// </summary>
        public Texture2D Texture { get; set; }
        /// <summary>
        /// The particle initial position.
        /// </summary>
        public Vector2 Position { get; set; }
        /// <summary>
        /// The distance to move every update.
        /// </summary>
        public Vector2 Velocity { get; set; }
        /// <summary>
        /// The particle initial angle.
        /// </summary>
        public float Angle { get; set; }
        /// <summary>
        /// The amount to rotate every update.
        /// </summary>
        public float AngularVelocity { get; set; }
        /// <summary>
        /// The color mask of the particle texture.
        /// </summary>
        public Color Color { get; set; }
        /// <summary>
        /// The amount to fade the texture every update.
        /// </summary>
        //public byte ColorFade { get; set; }
        /// <summary>
        /// The particle size.
        /// </summary>
        public float Size { get; set; }
        /// <summary>
        /// The particle lifespan.
        /// </summary>
        public int LifeSpan { get; set; }

        /// <summary>
        /// Create the Particle.
        /// </summary>
        /// <param name="texture">The texture</param>
        /// <param name="position">The initial position</param>
        /// <param name="velocity">The distance to move every update</param>
        /// <param name="angle">The initial angle</param>
        /// <param name="angularVelocity">The amount to rotate every update</param>
        /// <param name="color">The color mask of the particle texture</param>
        /// <param name="colorFade">The amount to fade the texture every update</param>
        /// <param name="size">The particle size</param>
        /// <param name="lifeSpan">The particle lifespan</param>
        public Particle(Texture2D texture, Vector2 position, Vector2 velocity,
            float angle, float angularVelocity, Color color, float size,
            int lifeSpan) {
            Texture = texture;
            Position = position;
            Velocity = velocity;
            Angle = angle;
            AngularVelocity = angularVelocity;
            Color = color;
            Size = size;
            LifeSpan = lifeSpan;
        }

        /// <summary>
        /// Updates the particle position and lifespan.
        /// </summary>
        public void Update() {
            LifeSpan--;
            Position += Velocity;
            Angle += AngularVelocity;
        }

        /// <summary>
        /// Draw the particle.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch) {
            Rectangle source = new Rectangle(0, 0, Texture.Width, Texture.Height);
            Vector2 origin = new Vector2(Texture.Width / 2, Texture.Height / 2);

            spriteBatch.Draw(Texture, Position, source, Color, Angle, origin,
                Size, SpriteEffects.None, 0f);
        }
    }
}
