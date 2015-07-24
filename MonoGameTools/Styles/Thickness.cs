//=============================================================================
// Thickness.cs
//=============================================================================

using System;

namespace MonoGame.Tools.Styles {
    public class Thickness {
        /// <summary>
        /// The top edge thickness.
        /// </summary>
        public int Top { get; set; }
        /// <summary>
        /// The left edge thickness.
        /// </summary>
        public int Left { get; set; }
        /// <summary>
        /// The bottom edge thickness.
        /// </summary>
        public int Bottom { get; set; }
        /// <summary>
        /// The right edge thickness.
        /// </summary>
        public int Right { get; set; }

        /// <summary>
        /// Create the thickness object.
        /// </summary>
        public Thickness() {
            Top = Left = Bottom = Right = 1;
        }

        /// <summary>
        /// Create the thickness object.
        /// </summary>
        /// <param name="thickness">Edge thickness</param>
        public Thickness(int thickness) {
            Top = Right = Bottom = Left = thickness;
        }

        /// <summary>
        /// Create the Thickness object.
        /// </summary>
        /// <param name="vertical">The top and bottom edge thickness</param>
        /// <param name="horizontal">The left and right edge thickness</param>
        public Thickness(int vertical, int horizontal) {
            Top = Bottom = vertical;
            Left = Right = horizontal;
        }

        /// <summary>
        /// Create the Thickness object.
        /// </summary>
        /// <param name="top">The top edge thickness</param>
        /// <param name="right">The right edge thickness</param>
        /// <param name="bottom">The bottom edge thickness</param>
        /// <param name="left">The ledge thickness</param>
        public Thickness(int top, int right, int bottom, int left) {
            Top = top;
            Right = right;
            Bottom = bottom;
            Left = left;
        }
    }
}
