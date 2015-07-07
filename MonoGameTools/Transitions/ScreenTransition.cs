//===================================================================
// ScreenTransition.cs
//===================================================================

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Tools.Transitions {
    public abstract class ScreenTransition {
        protected ContentManager content;
        
        /// <summary>
        /// Whether the transition is active.
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// The current screen (before the transition).
        /// </summary>
        public GameScreen CurrentScreen { get; private set; }
        /// <summary>
        /// The next screen (after the transition).
        /// </summary>
        public GameScreen NextScreen { get; private set; }
        /// <summary>
        /// Whether to change the screen. This should occur when the
        /// transition is hiding the current screen.
        /// </summary>
        public bool DoChange { get; set; }

        /// <summary>
        /// Create the ScreenTransition.
        /// </summary>
        public ScreenTransition() {
            IsActive = false;
            content = null;
            CurrentScreen = null;
            NextScreen = null;
            DoChange = false;
        }

        /// <summary>
        /// Load the ScreenTransition content.
        /// </summary>
        /// <param name="content">The ContentManager to load to</param>
        public virtual void LoadContent(ContentManager content) {
            this.content = content;
        }

        /// <summary>
        /// Unload the ScreenTransition content.
        /// </summary>
        public virtual void UnloadContent() { }

        /// <summary>
        /// Activate the ScreenTransition.
        /// </summary>
        /// <param name="currentScreen">The current screen (before the
        /// transition)</param>
        /// <param name="nextScreen">The next screen (after the
        /// transition)</param>
        public virtual void Activate(GameScreen currentScreen,
            GameScreen nextScreen) {
            IsActive = true;
            CurrentScreen = currentScreen;
            NextScreen = nextScreen;
        }

        /// <summary>
        /// Deactivate the ScreenTransition.
        /// </summary>
        public virtual void Deactivate() {
            IsActive = false;
        }

        /// <summary>
        /// Update the ScreenTransition status.
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime) {
            if(IsActive) {
                DoTransition(gameTime);
            }
        }

        /// <summary>
        /// Draw the ScreenTransition.
        /// </summary>
        /// <param name="graphicsDevice">The GraphicsDevice to draw to</param>
        /// <param name="spriteBatch">The SpriteBatch to draw to</param>
        public abstract void Draw(GraphicsDevice graphicsDevice,
            SpriteBatch spriteBatch);
        
        /// <summary>
        /// Run the ScreenTransition.
        /// </summary>
        /// <param name="gameTime">The game time TimeSpan</param>
        protected abstract void DoTransition(GameTime gameTime);
    }
}
