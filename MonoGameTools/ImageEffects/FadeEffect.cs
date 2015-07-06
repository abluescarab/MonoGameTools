//===================================================================
// FadeEffect.cs
//
// Based on the MonoGame RPG Made Easy tutorial series by
// CodingMadeEasy.
// Source: https://www.youtube.com/watch?v=krQZqPO0arQ
//===================================================================

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Tools.ImageEffects {
    public class FadeEffect : ImageEffect {
        /// <summary>
        /// The image fade speed.
        /// </summary>
        public float Speed { get; set; }
        /// <summary>
        /// Whether to fade the image in rather than out.
        /// </summary>
        public bool FadeIn { get; set; }

        /// <summary>
        /// Create the FadeEffect.
        /// </summary>
        public FadeEffect()
            : base() {
            Speed = 1.0f;
            FadeIn = false;
        }

        /// <summary>
        /// Create the FadeEffect.
        /// </summary>
        /// <param name="speed">The image fade speed</param>
        /// <param name="fadeIn">Whether to fade the image in rather than out</param>
        /// <param name="repeat">Whether to repeat the effect after it ends</param>
        public FadeEffect(float speed = 1.0f, bool fadeIn = false,
            bool repeat = false)
            : base(repeat) {
            Speed = speed;
            FadeIn = fadeIn;
            Repeat = repeat;
        }

        /// <summary>
        /// Run the effect.
        /// </summary>
        /// <param name="gameTime">The game time TimeSpan</param>
        protected override void DoEffect(GameTime gameTime) {
            if(!FadeIn) {
                image.Alpha -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else {
                image.Alpha += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if(image.Alpha < 0.0f || image.Alpha > 1.0f) {
                if(Repeat) {
                    if(image.Alpha < 0.0f && !FadeIn) {
                        image.Alpha = 1.0f;
                    }
                    else if(image.Alpha > 1.0f && FadeIn) {
                        image.Alpha = 0.0f;
                    }
                }
                else {
                    Deactivate();
                }
            }
        }
    }
}
