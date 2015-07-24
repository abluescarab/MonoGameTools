using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Tools;
using MonoGame.Tools.Components;
using MonoGame.Tools.Effects;
using MonoGame.Tools.Transitions;
using MonoGame.Tools.Styles;
using MonoGame.Tools.Events;

namespace MonoGameToolsDemo {
    public class DemoScreen : GameScreen {
        public Dictionary<string, Image> Images { get; private set; }
        private Button button;
        private Cursor cursor;

        public DemoScreen() {
            Images = new Dictionary<string, Image>();

            Image mainImage = new Image("MonoGameTools", new Vector2(10, 10),
                Vector2.One, centerOrigin: false);
            Image fadeImage = new Image("FadeEffect", new Vector2(30, 60),
                Vector2.One, false, centerOrigin: false);
            Image flashImage = new Image("FlashEffect", new Vector2(30, 110),
                Vector2.One, false, centerOrigin: false);
            Image zoomImage = new Image("ZoomEffect", new Vector2(30, 160),
                Vector2.One, false, centerOrigin: false);

            fadeImage.AddEffect<FadeEffect>("fadeEffect", new FadeEffect(
                repeat: true));
            flashImage.AddEffect<FlashEffect>("flashEffect", new FlashEffect(
                0.5f, minimumAlpha: .25f, repeat: true));
            zoomImage.AddEffect<ZoomEffect>("zoomEffect", new ZoomEffect(
                new Vector2(0.25f, 0.25f), new Vector2(1.0f, 1.0f),
                repeat: true));

            Images.Add("mainImage", mainImage);
            Images.Add("fadeImage", fadeImage);
            Images.Add("flashImage", flashImage);
            Images.Add("zoomImage", zoomImage);

            button = new Button(new Vector2(200, 25), new Vector2(400, 10),
                "Test Button", Color.Black, "Arial", Color.White, new Border(Color.Black,
                    new Thickness(2)));
            button.TextAlignment = Alignment.MiddleCenter;

            cursor = new Cursor("cursor");

            button.Click += button_Click;
            button.MouseEnter += button_MouseEnter;
            button.MouseLeave += button_MouseLeave;
        }

        void button_Click(object sender, MouseEventArgs e) {
            ((Button)sender).Text = "Mouse Clicked";
        }

        void button_MouseLeave(object sender, MouseEventArgs e) {
            ((Button)sender).Text = "Mouse Left";
        }

        void button_MouseEnter(object sender, MouseEventArgs e) {
            ((Button)sender).Text = "Mouse Entered";
        }

        public override void LoadContent(ContentManager content) {
            base.LoadContent(content);

            foreach(Image image in Images.Values) {
                image.LoadContent(content);
            }

            button.LoadContent(content, ScreenManager.Instance.GraphicsDevice);

            if(cursor != null) {
                cursor.LoadContent(content);
            }
        }

        public override void UnloadContent() {
            button.UnloadContent();

            foreach(Image image in Images.Values) {
                image.UnloadContent();
            }

            Images["fadeImage"].Visible = false;
            Images["flashImage"].Visible = false;
            Images["zoomImage"].Visible = false;
            button.Text = "Test Button";

            cursor.UnloadContent();
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            foreach(Image image in Images.Values) {
                image.Update(gameTime);
            }

            button.Update(gameTime);
            cursor.Update(gameTime);

            if(!ScreenManager.Instance.IsTransitioning) {
                if(InputManager.Instance.KeyPressed(Keys.Enter) ||
                    InputManager.Instance.ButtonPressed(PlayerIndex.One,
                    Buttons.A) || InputManager.Instance.MouseButtonPressed(
                    MouseButton.Right)) {
                    if(!Images["fadeImage"].Visible) {
                        Images["fadeImage"].Visible = true;
                        Images["fadeImage"].ActivateEffect("fadeEffect");
                    }
                    else {
                        if(!Images["flashImage"].Visible) {
                            Images["flashImage"].Visible = true;
                            Images["flashImage"].ActivateEffect("flashEffect");
                        }
                        else {
                            if(!Images["zoomImage"].Visible) {
                                Images["zoomImage"].Visible = true;
                                Images["zoomImage"].ActivateEffect("zoomEffect");
                            }
                            else {
                                ScreenManager.Instance.ChangeScreen(this,
                                    new FadeTransition(ScreenManager.Instance.Dimensions,
                                        Color.Black));
                            }
                        }
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch) {
            button.Draw(spriteBatch);

            foreach(Image image in Images.Values) {
                if(image.Visible) {
                    image.Draw(spriteBatch);
                }
            }

            cursor.Draw(spriteBatch);
        }
    }
}
