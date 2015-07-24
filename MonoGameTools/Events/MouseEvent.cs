//=============================================================================
// MouseEvent.cs
//
// Based on the WinForms MouseEventArgs class.
// Source: http://referencesource.microsoft.com/#System.Windows.Forms/winforms/Managed/System/WinForms/MouseEvent.cs
//=============================================================================

using System;
using Microsoft.Xna.Framework;

namespace MonoGame.Tools.Events {
    public class MouseEventArgs : EventArgs {
        /// <summary>
        /// Which button generated this event (if applicable).
        /// </summary>
        private readonly MouseButton button;
        /// <summary>
        /// If the user has clicked more than once, this contains the count
        /// of clicks so far.
        /// </summary>
        private readonly int clicks;
        /// <summary>
        /// The x portion of the coordinate where this event occurred.
        /// </summary>
        private readonly int x;
        /// <summary>
        /// The y portion of the coordinate where this event occurred.
        /// </summary>
        private readonly int y;

        private readonly int delta;

        /// <summary>
        /// Initializes a new instance of the MouseEventArgs class.
        /// </summary>
        public MouseEventArgs(MouseButton button, int clicks, int x, int y,
            int delta) {
                System.Diagnostics.Debug.Assert((button & (MouseButton.Any | 
                    MouseButton.Left | MouseButton.Middle | 
                    MouseButton.Right | MouseButton.XButton1 | 
                    MouseButton.XButton2)) == button,
                    "Invalid information passed into MouseEventArgs constructor!");
                this.button = button;
                this.clicks = clicks;
                this.x = x;
                this.y = y;
                this.delta = delta;
        }

        /// <summary>
        /// Gets which mouse button was pressed.
        /// </summary>
        public MouseButton Button {
            get { return button; }
        }

        /// <summary>
        /// Gets the number of times the mouse button was pressed and released.
        /// </summary>
        public int Clicks {
            get { return clicks; }
        }

        /// <summary>
        /// Gets the x-coordinate of a mouse click.
        /// </summary>
        public int X {
            get { return x; }
        }

        /// <summary>
        /// Gets the y-coordinate of a mouse click.
        /// </summary>
        public int Y {
            get { return y; }
        }

        /// <summary>
        /// Gets a signed count of the number of detents the mouse wheel has
        /// rotated.
        /// </summary>
        public int Delta {
            get { return delta; }
        }

        /// <summary>
        /// Gets the location of the mouse during MouseEvent.
        /// </summary>
        public Vector2 Location {
            get { return new Vector2(x, y); }
        } 
    }
}
