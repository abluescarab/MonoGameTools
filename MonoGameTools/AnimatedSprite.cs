//===================================================================
// AnimatedSprite.cs
//
// Based on the AnimatedSprite class by RB Whitaker.
// Source: http://rbwhitaker.wikidot.com/texture-atlases-2
//===================================================================

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Tools {
    public class AnimatedSprite {
        /// <summary>
        /// The sprite texture.
        /// </summary>
        public Texture2D Texture { get; set; }
        /// <summary>
        /// The texture file row count.
        /// </summary>
        public int Rows { get; set; }
        /// <summary>
        /// The texture file column count.
        /// </summary>
        public int Columns { get; set; }
        
        private int currentFrame;
        private int totalFrames;

        /// <summary>
        /// Create the AnimatedSprite.
        /// </summary>
        /// <param name="texture">The texture</param>
        /// <param name="rows">The texture file row count</param>
        /// <param name="columns">The texture file column count</param>
        public AnimatedSprite(Texture2D texture, int rows, int columns) {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = rows * columns;
        }

        /// <summary>
        /// Updates the sprite frame.
        /// </summary>
        public void Update() {
            currentFrame++;
            if(currentFrame == totalFrames)
                currentFrame = 0;
        }

        /// <summary>
        /// Draw the sprite.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch used for drawing textures</param>
        /// <param name="location">The X and Y location on the window</param>
        /// <param name="textureMask">The color mask for the texture</param>
        public void Draw(SpriteBatch spriteBatch, Vector2 location, Color textureMask) {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            Rectangle source = new Rectangle(width * column, height * row, width, height);
            Rectangle destination = new Rectangle((int)location.X, (int)location.Y, width, height);

            spriteBatch.Begin();
            spriteBatch.Draw(Texture, destination, source, textureMask);
            spriteBatch.End();
        }
    }
}
