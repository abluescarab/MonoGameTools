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

namespace MonoGameToolsDemo {
    public class DemoScreen : GameScreen {
        public Dictionary<string, Image> Images { get; private set; }

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
        }

        public override void LoadContent(ContentManager content) {
            base.LoadContent(content);

            foreach(Image image in Images.Values) {
                image.LoadContent(content);
            }
        }

        public override void UnloadContent() {
            base.UnloadContent();

            Images["fadeImage"].Visible = false;
            Images["flashImage"].Visible = false;
            Images["zoomImage"].Visible = false;

            foreach(Image image in Images.Values) {
                image.UnloadContent();
            }
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            foreach(Image image in Images.Values) {
                image.Update(gameTime);
            }

            if(!ScreenManager.Instance.IsTransitioning) {
                if(InputManager.Instance.KeyPressed(Keys.Enter) ||
                    InputManager.Instance.ButtonPressed(PlayerIndex.One,
                    Buttons.A) || InputManager.Instance.MouseButtonPressed(
                    InputManager.MouseButton.Right)) {
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
            foreach(Image image in Images.Values) {
                if(image.Visible) {
                    image.Draw(spriteBatch);
                }
            }
        }
    }
}
