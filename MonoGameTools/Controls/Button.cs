//=============================================================================
// Button.cs
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

        public event MouseEventHandler Click;
        public event MouseEventHandler MouseEnter;
        public event MouseEventHandler MouseLeave;

        public Vector2 Size { get; set; }
        public Vector2 Position { get; set; }
        public string Text { get; set; }
        public Color TextColor { get; set; }
        public Color BackgroundColor { get; set; }
        public Alignment TextAlignment { get; set; }
        public Border Border { get; set; }
        public string SpriteFont { get; set; }
        public float Alpha { get; set; }

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

        public void LoadContent(ContentManager content,
            GraphicsDevice graphicsDevice) {
            CreateBackground(graphicsDevice);
            CreateBorder(graphicsDevice);

            if(!string.IsNullOrEmpty(SpriteFont)) {
                font = content.Load<SpriteFont>(SpriteFont);
            }
        }

        public void UnloadContent() {
            sourceRectangle = Rectangle.Empty;
            borderRectangle = Rectangle.Empty;
            background = null;
            border = null;
            font = null;
        }

        public void Update(GameTime gameTime) {
            // todo: adapt OnMouseDown and such for this

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

        private void OnClick(MouseEventArgs e) {
            MouseEventHandler handler = Click;
            if(handler != null) {
                handler(this, e);
            }
        }

        private void OnMouseEnter(MouseEventArgs e) {
            MouseEventHandler handler = MouseEnter;
            if(handler != null) {
                handler(this, e);
            }
        }

        private void OnMouseLeave(MouseEventArgs e) {
            MouseEventHandler handler = MouseLeave;
            if(handler != null) {
                handler(this, e);
            }
        }

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

        private bool IsInBounds(Vector2 position) {
            float posX = position.X - Position.X;
            float posY = position.Y - Position.Y;

            if((borderRectangle != null && borderRectangle.Contains(posX, posY) ||
               (sourceRectangle != null && sourceRectangle.Contains(posX, posY)))) {
                return true;
            }

            return false;
        }

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