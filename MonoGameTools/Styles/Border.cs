//=============================================================================
// Border.cs
//=============================================================================

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Tools.Styles {
    public class Border {
        /// <summary>
        /// The border color.
        /// </summary>
        public Color Color { get; set; }
        /// <summary>
        /// The border thickness.
        /// </summary>
        public Thickness Thickness { get; set; }
        /// <summary>
        /// The border's corner rounding.
        /// </summary>
        public Rounding Rounding { get; set; }

        /// <summary>
        /// Create the Border object.
        /// </summary>
        public Border() {
            Color = Color.White;
            Thickness = new Thickness(1);
            Rounding = new Rounding(0.0f);
        }

        /// <summary>
        /// Create the Border object.
        /// </summary>
        /// <param name="color">The border color</param>
        public Border(Color color) {
            Color = color;
            Thickness = new Thickness(1);
            Rounding = new Rounding(0.0f);
        }

        /// <summary>
        /// Create the Border object.
        /// </summary>
        /// <param name="color">The border color</param>
        /// <param name="thickness">The border thickness</param>
        public Border(Color color, Thickness thickness) {
            Color = color;
            Thickness = thickness;
            Rounding = new Rounding(0.0f);
        }

        /// <summary>
        /// Create the Border object.
        /// </summary>
        /// <param name="color">The border color</param>
        /// <param name="thickness">The border thickness</param>
        /// <param name="rounding">The border's corner rounding</param>
        public Border(Color color, Thickness thickness,
            Rounding rounding) {
            Color = color;
            Thickness = thickness;
            Rounding = rounding;
        }
    }
}
