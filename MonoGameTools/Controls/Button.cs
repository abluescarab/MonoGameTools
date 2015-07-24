//=============================================================================
// Button.cs
//
// Todo:
//    Rounded border
//=============================================================================

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Tools.Styles;
using MonoGame.Tools.Events;

namespace MonoGame.Tools.Components {
    public class Button {
        private Rectangle sourceRectangle = Rectangle.Empty;
        private Rectangle borderRectangle = Rectangle.Empty;
        private Texture2D background = null;
        private Texture2D border = null;
        private SpriteFont font = null;
        private bool wasHoveringOver = false;

        /// <summary>
        /// Fires when the button is clicked.
        /// </summary>
        public event MouseEventHandler Click;
        /// <summary>
        /// Fires when the mouse enters the button bounds.
        /// </summary>
        public event MouseEventHandler MouseEnter;
        /// <summary>
        /// Fires when the mouse leaves the button bounds.
        /// </summary>
        public event MouseEventHandler MouseLeave;

        /// <summary>
        /// The size of the button.
        /// </summary>
        public Vector2 Size { get; set; }
        /// <summary>
        /// The position of the button.
        /// </summary>
        public Vector2 Position { get; set; }
        /// <summary>
        /// The text of the button.
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// The text color of the button.
        /// </summary>
        public Color TextColor { get; set; }
        /// <summary>
        /// The background color of the button.
        /// </summary>
        public Color BackgroundColor { get; set; }
        /// <summary>
        /// The alignment of the button text.
        /// </summary>
        public Alignment TextAlignment { get; set; }
        /// <summary>
        /// The border around the button.
        /// </summary>
        public Border Border { get; set; }
        /// <summary>
        /// The font used for the button text.
        /// </summary>
        public string SpriteFont { get; set; }
        /// <summary>
        /// The alpha of the button.
        /// </summary>
        public float Alpha { get; set; }

        /// <summary>
        /// Initialize the Button.
        /// </summary>
        /// <param name="size">The size of the button</param>
        /// <param name="position">The position of the button</param>
        public Button(Vector2 size, Vector2 position) {
            Size = size;
            Position = position;
            Text = string.Empty;
            TextColor = Color.White;
            SpriteFont = string.Empty;
            BackgroundColor = Color.Black;
            TextAlignment = Alignment.MiddleCenter;
            Border = null;
            Alpha = 1.0f;
        }

        /// <summary>
        /// Initialize the Button.
        /// </summary>
        /// <param name="size">The size of the button</param>
        /// <param name="position">The position of the button</param>
        /// <param name="text">The text of the button</param>
        /// <param name="textColor">The text color of the button</param>
        /// <param name="spriteFont">The font used for the button text</param>
        /// <param name="backgroundColor">The background color of the button</param>
        /// <param name="border">The border around the button</param>
        public Button(Vector2 size, Vector2 position, string text,
            Color textColor, string spriteFont, Color backgroundColor,
            Border border = null) {
            Size = size;
            Position = position;
            Text = text;
            TextColor = textColor;
            SpriteFont = spriteFont;
            BackgroundColor = backgroundColor;
            TextAlignment = Alignment.MiddleCenter;
            Border = border;
            Alpha = 1.0f;
        }

        /// <summary>
        /// Load the Button content.
        /// </summary>
        /// <param name="content">The ContentManager to load to</param>
        /// <param name="graphicsDevice">The GraphicsDevice to draw to</param>
        public void LoadContent(ContentManager content,
            GraphicsDevice graphicsDevice) {
            CreateBackground(graphicsDevice);
            CreateBorder(graphicsDevice);

            if(!string.IsNullOrEmpty(SpriteFont)) {
                font = content.Load<SpriteFont>(SpriteFont);
            }
        }

        /// <summary>
        /// Unload the button content.
        /// </summary>
        public void UnloadContent() {
            sourceRectangle = Rectangle.Empty;
            borderRectangle = Rectangle.Empty;
            background = null;
            border = null;
            font = null;
        }

        /// <summary>
        /// Update the Button content.
        /// </summary>
        /// <param name="gameTime">The game time TimeSpan</param>
        public void Update(GameTime gameTime) {
            Vector2 mouse = InputManager.Instance.GetMousePosition();
            MouseButton[] buttons;

            if(IsInBounds(mouse)) {
                if(!wasHoveringOver) {
                    if(this.MouseEnter != null &&
                       this.MouseEnter.GetInvocationList().Length > 0) {
                        this.OnMouseEnter(new MouseEventArgs(MouseButton.None,
                            0, (int)mouse.X, (int)mouse.Y, 0));
                    }

                    wasHoveringOver = true;
                }

                if(InputManager.Instance.AnyMouseButtonPressed(out buttons)) {
                    if(this.Click != null &&
                        this.Click.GetInvocationList().Length > 0) {
                        this.OnClick(new MouseEventArgs(buttons[0],
                            1, (int)mouse.X, (int)mouse.Y, 0));
                    }
                }
            }
            else {
                if(wasHoveringOver) {
                    if(this.MouseLeave != null &&
                       this.MouseLeave.GetInvocationList().Length > 0) {
                        this.OnMouseLeave(new MouseEventArgs(MouseButton.None,
                         0, (int)mouse.X, (int)mouse.Y, 0));
                    }

                    wasHoveringOver = false;
                }
            }
        }

        /// <summary>
        /// Handler for the Click event.
        /// </summary>
        private void OnClick(MouseEventArgs e) {
            MouseEventHandler handler = Click;
            if(handler != null) {
                handler(this, e);
            }
        }

        /// <summary>
        /// Handler for the MouseEnter event.
        /// </summary>
        private void OnMouseEnter(MouseEventArgs e) {
            MouseEventHandler handler = MouseEnter;
            if(handler != null) {
                handler(this, e);
            }
        }

        /// <summary>
        /// Handler for the MouseLeave event.
        /// </summary>
        private void OnMouseLeave(MouseEventArgs e) {
            MouseEventHandler handler = MouseLeave;
            if(handler != null) {
                handler(this, e);
            }
        }

        /// <summary>
        /// Draw the Button.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch to draw to</param>
        public void Draw(SpriteBatch spriteBatch) {
            Vector2 addBorder = Vector2.Zero;

            if(Border != null) {
                addBorder = new Vector2(Border.Thickness.Left,
                    Border.Thickness.Top);
            }

            if(border != null) {
                spriteBatch.Draw(border, Position, borderRectangle,
                    Color.White * Alpha, 0.0f, Vector2.Zero, Vector2.One,
                    SpriteEffects.None, 0.0f);
            }

            if(background != null) {
                spriteBatch.Draw(background, Position + addBorder,
                    sourceRectangle, Color.White * Alpha, 0.0f,
                    Vector2.Zero, Vector2.One, SpriteEffects.None, 0.0f);
            }

            if(!string.IsNullOrEmpty(Text) && font != null) {
                spriteBatch.DrawString(font, Text, Position + addBorder +
                    GetTextPosition(), TextColor);
            }
        }

        /// <summary>
        /// Check if the specified position is within the button's bounds.
        /// </summary>
        /// <param name="position">A position on the screen</param>
        /// <returns>If the position is in the button bounds</returns>
        private bool IsInBounds(Vector2 position) {
            float posX = position.X - Position.X;
            float posY = position.Y - Position.Y;

            if((borderRectangle != null && borderRectangle.Contains(posX, posY) ||
               (sourceRectangle != null && sourceRectangle.Contains(posX, posY)))) {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Create the background texture.
        /// </summary>
        /// <param name="graphicsDevice">The GraphicsDevice to draw to</param>
        private void CreateBackground(GraphicsDevice graphicsDevice) {
            Color[] backgroundColor;

            if(sourceRectangle == Rectangle.Empty) {
                sourceRectangle = new Rectangle(0, 0, (int)Size.X,
                    (int)Size.Y);
            }

            backgroundColor = new Color[sourceRectangle.Width * sourceRectangle.Height];

            for(int i = 0; i < backgroundColor.Length; i++) {
                backgroundColor[i] = BackgroundColor;
            }

            background = new Texture2D(graphicsDevice, sourceRectangle.Width,
                sourceRectangle.Height);
            background.SetData(backgroundColor);
        }

        /// <summary>
        /// Create the border texture.
        /// </summary>
        /// <param name="graphicsDevice">The GraphicsDevice to draw to</param>
        private void CreateBorder(GraphicsDevice graphicsDevice) {
            if(Border != null) {
                Color[] borderColor;

                if(borderRectangle == Rectangle.Empty) {
                    borderRectangle = new Rectangle(0, 0,
                        (int)(Size.X + Border.Thickness.Left + Border.Thickness.Right),
                        (int)(Size.Y + Border.Thickness.Top + Border.Thickness.Bottom));
                }

                borderColor = new Color[borderRectangle.Width * borderRectangle.Height];

                for(int i = 0; i < borderColor.Length; i++) {
                    borderColor[i] = Border.Color;
                }

                border = new Texture2D(graphicsDevice, borderRectangle.Width,
                    borderRectangle.Height);
                border.SetData(borderColor);
            }
        }

        /// <summary>
        /// Get the position of the text within the button.
        /// </summary>
        /// <returns>The position of the text within the button</returns>
        private Vector2 GetTextPosition() {
            Vector2 position = Vector2.Zero;
            Vector2 fontSize = font.MeasureString(Text);

            switch(TextAlignment) {
                case Alignment.TopLeft:
                    position = Vector2.Zero;
                    break;
                case Alignment.TopCenter:
                    position = new Vector2(
                        (int)(Size.X / 2 - fontSize.X / 2),
                        0);
                    break;
                case Alignment.TopRight:
                    position = new Vector2(
                        (int)(Size.X - fontSize.X),
                        0);
                    break;
                case Alignment.MiddleLeft:
                    position = new Vector2(
                        0,
                        (int)(Size.Y / 2 - fontSize.Y / 2));
                    break;
                case Alignment.MiddleCenter:
                    position = new Vector2(
                        (int)(Size.X / 2 - fontSize.X / 2),
                        (int)(Size.Y / 2 - fontSize.Y / 2));
                    break;
                case Alignment.MiddleRight:
                    position = new Vector2(
                        (int)(Size.X - fontSize.X),
                        (int)(Size.Y / 2 - fontSize.Y / 2));
                    break;
                case Alignment.BottomLeft:
                    position = new Vector2(
                        0,
                        (int)(Size.Y - fontSize.Y));
                    break;
                case Alignment.BottomCenter:
                    position = new Vector2(
                        (int)(Size.X / 2 - fontSize.X / 2),
                        (int)(Size.Y - fontSize.Y));
                    break;
                case Alignment.BottomRight:
                    position = new Vector2(
                        (int)(Size.X - fontSize.X),
                        (int)(Size.Y - fontSize.Y));
                    break;
            }

            return position;
        }
    }
}