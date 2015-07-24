//=============================================================================
// Cursor.cs
//
// Todo:
//    Mouse smoothing
//=============================================================================

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Tools.Components;

namespace MonoGame.Tools {
    public class Cursor {
        private string imagePath = string.Empty;
        private Image image = null;

        /// <summary>
        /// The position of the cursor.
        /// </summary>
        public Vector2 Position { get; private set; }
        /// <summary>
        /// Whether the cursor is visible.
        /// </summary>
        public bool Visible { get; set; }
        /// <summary>
        /// The scale of the cursor.
        /// </summary>
        public Vector2 Scale { get; set; }

        /// <summary>
        /// Initialize the cursor.
        /// </summary>
        /// <param name="image">The image to use for the cursor</param>
        public Cursor(string image) {
            imagePath = image;
            Visible = true;
            Scale = Vector2.One;
        }

        /// <summary>
        /// Initialize the cursor.
        /// </summary>
        /// <param name="image">The image to use for the cursor</param>
        /// <param name="position">The initial location of the cursor</param>
        public Cursor(string image, Vector2 position) {
            imagePath = image;
            Mouse.SetPosition((int)position.X, (int)position.Y);
            Visible = true;
            Scale = Vector2.One;
        }

        /// <summary>
        /// Load the Cursor content.
        /// </summary>
        /// <param name="content">The ContentManager to load to</param>
        public void LoadContent(ContentManager content) {
            if(!string.IsNullOrEmpty(imagePath)) {
                image = new Image(imagePath, Position, Scale, centerOrigin: false);
            }

            if(image != null) {
                image.LoadContent(content);
            }
        }

        /// <summary>
        /// Unload the Cursor content.
        /// </summary>
        public void UnloadContent() {
            if(image != null) {
                image.UnloadContent();
            }
        }

        /// <summary>
        /// Update the Cursor content.
        /// </summary>
        /// <param name="gameTime">The game time TimeSpan</param>
        public void Update(GameTime gameTime) {
            if(image != null) {
                Vector2 pos = Mouse.GetState().Position.ToVector2();
                int x = (int)pos.X;
                int y = (int)pos.Y;

                int minX = 0;
                int minY = 0;
                int maxX = (int)ScreenManager.Instance.Dimensions.X;
                int maxY = (int)ScreenManager.Instance.Dimensions.Y;

                if(pos.X < minX) {
                    x = minX;
                }
                else if(pos.X > maxX) {
                    x = maxX;
                }

                if(pos.Y < minY){
                    y = minY;
                }
                else if(pos.Y > maxY) {
                    y = maxY;
                }

                Position = new Vector2(x, y);
                image.Position = Position;
            }
        }

        /// <summary>
        /// Draw the Cursor.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch to draw to</param>
        public void Draw(SpriteBatch spriteBatch) {
            if(image != null) {
                image.Draw(spriteBatch);
            }
        }
    }
}
