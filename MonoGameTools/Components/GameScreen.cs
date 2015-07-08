//=============================================================================
// GameScreen.cs
//
// Based on the MonoGame RPG Made Easy tutorial series by CodingMadeEasy.
// Source: https://www.youtube.com/watch?v=CcPb0bKkpeg
//=============================================================================

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Tools.Components {
    public abstract class GameScreen {
        protected ContentManager content;

        /// <summary>
        /// Load the GameScreen content.
        /// </summary>
        /// <param name="content">The ContentManager to load to</param>
        public virtual void LoadContent(ContentManager content) {
            this.content = content;
        }

        /// <summary>
        /// Unload the GameScreen content.
        /// </summary>
        public virtual void UnloadContent() {
            content.Unload();
        }

        /// <summary>
        /// Update the GameScreen status.
        /// </summary>
        /// <param name="gameTime">The game time TimeSpan</param>
        public virtual void Update(GameTime gameTime) { }

        /// <summary>
        /// Draw the GameScreen content.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch to draw to</param>
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
