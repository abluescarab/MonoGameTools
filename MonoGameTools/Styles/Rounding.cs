//=============================================================================
// Rounding.cs
//=============================================================================

using System;

namespace MonoGame.Tools.Styles {
    public class Rounding {
        /// <summary>
        /// The rounding for the top left corner.
        /// </summary>
        public float TopLeft { get; set; }
        /// <summary>
        /// The rounding for the top right corner.
        /// </summary>
        public float TopRight { get; set; }
        /// <summary>
        /// The rounding for the bottom left corner.
        /// </summary>
        public float BottomLeft { get; set; }
        /// <summary>
        /// The rounding for the bottom right corner.
        /// </summary>
        public float BottomRight { get; set; }

        /// <summary>
        /// Create the Rounding object.
        /// </summary>
        public Rounding() {
            TopLeft = TopRight = BottomLeft = BottomRight = 0.0f;
        }

        /// <summary>
        /// Create the Rounding object.
        /// </summary>
        /// <param name="rounding">The corner rounding</param>
        public Rounding(float rounding) {
            TopLeft = TopRight = BottomLeft = BottomRight = rounding;
        }

        /// <summary>
        /// Create the Rounding object.
        /// </summary>
        /// <param name="topLeft">The top left corner rounding</param>
        /// <param name="topRight">The top right corner rounding</param>
        /// <param name="bottomLeft">The bottom left corner rounding</param>
        /// <param name="bottomRight">The bottom right corner rounding</param>
        public Rounding(float topLeft, float topRight,
            float bottomLeft, float bottomRight) {
            TopLeft = topLeft;
            TopRight = topRight;
            BottomLeft = bottomLeft;
            BottomRight = bottomRight;
        }
    }
}
