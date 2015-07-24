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

        public Vector2 Position { get; private set; }
        public bool Visible { get; set; }
        public Vector2 Scale { get; set; }

        public Cursor(string image) {
            imagePath = image;
            Visible = true;
            Scale = Vector2.One;
        }

        public Cursor(string image, Vector2 position) {
            imagePath = image;
            Mouse.SetPosition((int)position.X, (int)position.Y);
            Visible = true;
            Scale = Vector2.One;
        }

        public void LoadContent(ContentManager content) {
            if(!string.IsNullOrEmpty(imagePath)) {
                image = new Image(imagePath, Position, Scale, centerOrigin: false);
            }

            if(image != null) {
                image.LoadContent(content);
            }
        }

        public void UnloadContent() {
            if(image != null) {
                image.UnloadContent();
            }
        }

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

        public void Draw(SpriteBatch spriteBatch) {
            if(image != null) {
                image.Draw(spriteBatch);
            }
        }
    }
}
