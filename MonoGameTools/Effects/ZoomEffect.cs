//===================================================================
// ZoomEffect.cs
//===================================================================

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Tools.Effects {
    public class ZoomEffect : ImageEffect {
        /// <summary>
        /// The zoom speed.
        /// </summary>
        public float Speed { get; set; }
        /// <summary>
        /// The image's minimum scale.
        /// </summary>
        public Vector2 MinimumScale { get; set; }
        /// <summary>
        /// The image's maximum scale.
        /// </summary>
        public Vector2 MaximumScale { get; set; }
        /// <summary>
        /// Whether to zoom out rather than in.
        /// </summary>
        public bool ZoomOut { get; set; }

        /// <summary>
        /// Create the ZoomEffect.
        /// </summary>
        public ZoomEffect()
            : base() {
            Speed = 1.0f;
            MinimumScale = Vector2.Zero;
            MaximumScale = Vector2.One;
        }

        /// <summary>
        /// Create the ZoomEffect.
        /// </summary>
        /// <param name="minimumScale">The image's minimum scale</param>
        /// <param name="maximumScale">The image's maximum scale</param>
        /// <param name="speed">The zoom speed</param>
        /// <param name="zoomOut">Whether to zoom out rather than in</param>
        /// <param name="repeat">Whether to repeat the effect after it ends</param>
        public ZoomEffect(Vector2 minimumScale, Vector2 maximumScale,
            float speed = 1.0f, bool zoomOut = false,
            bool repeat = false)
            : base(repeat) {
            MinimumScale = minimumScale;
            MaximumScale = maximumScale;
            Speed = speed;
            ZoomOut = zoomOut;
        }

        /// <summary>
        /// Load the ImageEffect content.
        /// </summary>
        /// <param name="image">The image to attach the effect to</param>
        public override void LoadContent(Image image) {
            base.LoadContent(image);

            if(!ZoomOut)
                image.Scale = MinimumScale;
            else
                image.Scale = MaximumScale;
        }

        /// <summary>
        /// Run the effect.
        /// </summary>
        /// <param name="gameTime">The game time TimeSpan</param>
        protected override void DoEffect(GameTime gameTime) {
            float scaleX = image.Scale.X;
            float scaleY = image.Scale.Y;

            if(MinimumScale.X < MaximumScale.X ||
               MinimumScale.Y < MaximumScale.Y) {
                if(!ZoomOut) {
                    if(scaleX < MaximumScale.X)
                        scaleX += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if(scaleY < MaximumScale.Y)
                        scaleY += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                else {
                    if(scaleX > MinimumScale.X)
                        scaleX -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if(scaleY > MinimumScale.Y)
                        scaleY -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }

                image.Scale = new Vector2(scaleX, scaleY);
            }

            if((scaleX <= MinimumScale.X && scaleY <= MinimumScale.Y)) {
                if(Repeat) {
                    image.Scale = MaximumScale;
                }
                else {
                    Deactivate();
                }
            }
            else if(scaleX >= MaximumScale.X && scaleY >= MaximumScale.Y) {
                if(Repeat) {
                    image.Scale = MinimumScale;
                }
                else {
                    Deactivate();
                }
            }
        }
    }
}
