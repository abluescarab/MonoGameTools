//=============================================================================
// FadeTransition.cs
//=============================================================================

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Tools.Transitions {
    public class FadeTransition : ScreenTransition {
        private float alpha;
        private bool increaseAlpha;

        /// <summary>
        /// The speed of the transition.
        /// </summary>
        public float Speed { get; set; }
        /// <summary>
        /// The color to fade the screen to.
        /// </summary>
        public Color FadeColor { get; set; }
        /// <summary>
        /// The dimensions of the screen.
        /// </summary>
        public Vector2 Dimensions { get; set; }

        /// <summary>
        /// Create the FadeTransition.
        /// </summary>
        public FadeTransition()
            : base() {
            Speed = 1.0f;
            FadeColor = Color.Black;
            Dimensions = Vector2.Zero;
        }

        /// <summary>
        /// Create the FadeTransition.
        /// </summary>
        /// <param name="dimensions">The dimensions of the screen</param>
        /// <param name="fadeColor">The color to fade the screen to</param>
        /// <param name="speed">The speed of the transition</param>
        public FadeTransition(Vector2 dimensions,
            Color fadeColor, float speed = 1.0f) {
            Dimensions = dimensions;
            FadeColor = fadeColor;
            Speed = speed;

            alpha = 0.0f;
            increaseAlpha = true;
        }

        /// <summary>
        /// Run the FadeTransition.
        /// </summary>
        /// <param name="gameTime">The game time TimeSpan</param>
        protected override void DoTransition(GameTime gameTime) {
            // checks if DoChange is false because if it is true, screen
            // content is still loading and we should pause the transition
            if(!DoChange) {
                if(increaseAlpha) {
                    alpha += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                else {
                    alpha -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }

                if(alpha <= 0.0f) {
                    if(!increaseAlpha) {
                        Deactivate();
                    }
                }
                else if(alpha >= 1.0f) {
                    increaseAlpha = false;
                    CurrentScreen.UnloadContent();
                    DoChange = true;
                }
            }
        }

        /// <summary>
        /// Draw the FadeTransition.
        /// </summary>
        /// <param name="graphicsDevice">The GraphicsDevice to draw to</param>
        /// <param name="spriteBatch">The SpriteBatch to draw to</param>
        public override void Draw(GraphicsDevice graphicsDevice,
            SpriteBatch spriteBatch) {
            Texture2D texture = new Texture2D(graphicsDevice,
                1, 1);
            Rectangle rectangle = new Rectangle(0, 0,
                (int)Dimensions.X, (int)Dimensions.Y);
            Color color = new Color(FadeColor, alpha);

            texture.SetData(new[] { color });
            spriteBatch.Draw(texture, rectangle, FadeColor);
        }
    }
}