//===================================================================
// ImageEffect.cs
// 
// Based on the MonoGame RPG Made Easy tutorial series by
// CodingMadeEasy.
// Source: https://www.youtube.com/watch?v=krQZqPO0arQ
//===================================================================

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Tools.Effects {
    public abstract class ImageEffect {
        /// <summary>
        /// Whether the image effect is currently active.
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// Whether to repeat the effect after it ends.
        /// </summary>
        public bool Repeat { get; set; }

        protected Image image;

        /// <summary>
        /// Create the ImageEffect.
        /// </summary>
        public ImageEffect() {
            IsActive = false;
            Repeat = false;
        }

        /// <summary>
        /// Create the ImageEffect.
        /// </summary>
        /// <param name="repeat">Whether to repeat the effect after it ends</param>
        public ImageEffect(bool repeat = false) {
            IsActive = false;
            Repeat = repeat;
        }

        /// <summary>
        /// Load the ImageEffect content.
        /// </summary>
        /// <param name="image">The image to attach the effect to</param>
        public virtual void LoadContent(Image image) {
            this.image = image;
        }

        /// <summary>
        /// Unload the ImageEffect content.
        /// </summary>
        public virtual void UnloadContent() { }

        /// <summary>
        /// Activate the effect.
        /// </summary>
        /// <param name="image">The image to attach the effect to</param>
        public virtual void Activate(Image image) {
            this.IsActive = true;
            LoadContent(image);
        }

        /// <summary>
        /// Deactivate the effect.
        /// </summary>
        public virtual void Deactivate() {
            this.IsActive = false;
            UnloadContent();
        }

        /// <summary>
        /// Update the ImageEffect content.
        /// </summary>
        /// <param name="gameTime">The game time TimeSpan</param>
        public virtual void Update(GameTime gameTime) {
            if(this.IsActive && image.Visible) {
                DoEffect(gameTime);
            }
        }

        protected abstract void DoEffect(GameTime gameTime);
    }
}
