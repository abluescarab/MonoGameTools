//===================================================================
// FlashEffect.cs
//===================================================================

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Tools.Effects {
    public class FlashEffect : ImageEffect {
        private float lastRun = 0.0f;
        
        /// <summary>
        /// The speed at which the image flashes, in seconds.
        /// </summary>
        public float Speed { get; set; }
        /// <summary>
        /// The image's maximum alpha.
        /// </summary>
        public float MaximumAlpha { get; set; }
        /// <summary>
        /// The image's minimum alpha.
        /// </summary>
        public float MinimumAlpha { get; set; }

        /// <summary>
        /// Create the FlashEffect.
        /// </summary>
        public FlashEffect() : base() {
            Speed = 1.0f;
            MaximumAlpha = 1.0f;
            MinimumAlpha = 0.0f;
        }

        /// <summary>
        /// Create the FlashEffect.
        /// </summary>
        /// <param name="speed">The speed at which the image flashes, in seconds</param>
        /// <param name="minimumAlpha">The image's minimum alpha</param>
        /// <param name="maximumAlpha">The image's maximum alpha</param>
        /// <param name="repeat">Whether to repeat the effect after it ends</param>
        public FlashEffect(float speed = 1.0f, float minimumAlpha = 0.0f,
            float maximumAlpha = 1.0f, bool repeat = false) : base(repeat) {
                Speed = speed;
                MinimumAlpha = minimumAlpha;
                MaximumAlpha = maximumAlpha;
        }
        
        /// <summary>
        /// Run the effect.
        /// </summary>
        /// <param name="gameTime">The game time TimeSpan</param>
        protected override void DoEffect(GameTime gameTime) {
            lastRun += (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if(MaximumAlpha > MinimumAlpha) {
                    float max = Math.Min(MaximumAlpha, 1.0f);
                    float min = Math.Max(MinimumAlpha, 0.0f);
                    
                    if(lastRun >= Speed) {
                        image.Alpha = FlipValue(image.Alpha, min, max);
                        lastRun = 0.0f;

                        if(!Repeat) {
                            Deactivate();
                        }
                    }
            }
        }

        /// <summary>
        /// Flip a float between two float values.
        /// </summary>
        /// <param name="current">The float to change</param>
        /// <param name="min">The minimum float value</param>
        /// <param name="max">The maximum float value</param>
        /// <returns>The flipped float</returns>
        private float FlipValue(float current, float min, float max) {
            float result = 0.0f;

            if(float.Equals(current, min)) {
                result = max;
            }
            else if(float.Equals(current, max)) {
                result = min;
            }
            else {
                float minDist = Math.Abs(current - min);
                float maxDist = Math.Abs(current - max);

                if(minDist < maxDist) {
                    result = min;
                }
                else if(maxDist < minDist) {
                    result = max;
                }
            }

            return result;
        }
    }
}
