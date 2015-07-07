using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Tools.Transitions;
using System;
using System.Collections.Generic;

namespace MonoGame.Tools {
    public sealed class ScreenManager {
        private GameScreen currentScreen;
        private Dictionary<string, GameScreen> screens;
        private static ScreenManager instance;
        private ScreenTransition transition;

        /// <summary>
        /// The dimensions of the screen.
        /// </summary>
        public Vector2 Dimensions { get; set; }
        /// <summary>
        /// The GraphicsDevice for the screen.
        /// </summary>
        public GraphicsDevice GraphicsDevice { get; set; }
        /// <summary>
        /// The SpriteBatch used for drawing to the screen.
        /// </summary>
        public SpriteBatch SpriteBatch { get; set; }
        /// <summary>
        /// The ContentManager used for loading content to the screen.
        /// </summary>
        public ContentManager Content { get; private set; }
        /// <summary>
        /// The instance of the ScreenManager.
        /// </summary>
        public static ScreenManager Instance {
            get {
                if(instance == null) {
                    instance = new ScreenManager();
                }

                return instance;
            }
        }

        /// <summary>
        /// Create the ScreenManager.
        /// </summary>
        private ScreenManager() {
            currentScreen = null;
            screens = new Dictionary<string, GameScreen>();
            Dimensions = new Vector2(800, 600);
            GraphicsDevice = null;
            SpriteBatch = null;
            Content = null;
        }

        /// <summary>
        /// Add a screen.
        /// </summary>
        /// <typeparam name="T">A GameScreen child class</typeparam>
        /// <param name="screenName">The screen name</param>
        /// <param name="screen">The screen</param>
        public void AddScreen<T>(string screenName, T screen) where T : GameScreen {
            if(!screens.ContainsKey(screenName)) {
                if(screen == null) {
                    screen = (T)Activator.CreateInstance(typeof(T));
                }

                screens.Add(screenName, screen);

                if(currentScreen == null) {
                    SetScreen(screen);
                }
            }
        }

        /// <summary>
        /// Change the screen.
        /// </summary>
        /// <param name="screenName">The screen name</param>
        /// <param name="transition">The ScreenTransition to use between screens</param>
        /// <returns></returns>
        public void ChangeScreen(string screenName,
            ScreenTransition transition = null) {
            GameScreen screen = null;
            this.transition = transition;

            if(screens.ContainsKey(screenName)) {
                screen = screens[screenName];

                if(transition != null) {
                    transition.Activate(currentScreen, screen);
                }
            }
        }

        /// <summary>
        /// Load the ScreenManager content.
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content) {
            Content = content;

            if(currentScreen != null) {
                currentScreen.LoadContent(content);
            }
        }

        /// <summary>
        /// Unload the ScreenManager content.
        /// </summary>
        public void UnloadContent() {
            currentScreen.UnloadContent();
        }

        /// <summary>
        /// Update the ScreenManager state.
        /// </summary>
        /// <param name="gameTime">The game time TimeSpan</param>
        public void Update(GameTime gameTime) {
            currentScreen.Update(gameTime);

            if(transition != null) {
                transition.Update(gameTime);

                if(transition.IsActive && transition.DoChange) {
                    SetScreen(transition.NextScreen);
                }
            }
        }

        /// <summary>
        /// Draw the ScreenManager content.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch to draw to</param>
        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Begin();
            currentScreen.Draw(spriteBatch);

            if(transition != null) {
                if(transition.IsActive) {
                    transition.Draw(GraphicsDevice, spriteBatch);
                }
            }

            spriteBatch.End();
        }

        /// <summary>
        /// Set the current screen.
        /// </summary>
        /// <param name="screen">A screen</param>
        private void SetScreen(GameScreen screen) {
            currentScreen = screen;
            screen.LoadContent(Content);

            if(transition != null) {
                transition.DoChange = false;
            }
        }
    }
}
