//=============================================================================
// ScreenManager.cs
//
// Based on the MonoGame RPG Made Easy tutorial series by CodingMadeEasy.
// Source: https://www.youtube.com/watch?v=agt9-J9RPZ0
//=============================================================================

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Tools.Transitions;

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
        /// Whether the screen is currently transitioning.
        /// </summary>
        public bool IsTransitioning { get; set; }
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
            IsTransitioning = false;
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
                    IsTransitioning = true;
                }
            }
        }

        /// <summary>
        /// Find a list of GameScreens by their type.
        /// </summary>
        /// <typeparam name="T">The GameScreen child class</typeparam>
        /// <returns>A list of screens of the specified type</returns>
        public GameScreen[] FindScreensByType<T>() where T : GameScreen {
            List<GameScreen> screensByType = new List<GameScreen>();

            foreach(GameScreen scr in screens.Values) {
                if(scr.GetType() == typeof(T)) {
                    screensByType.Add(scr);
                }
            }

            return screensByType.ToArray();
        }

        /// <summary>
        /// Find a screen by its name.
        /// </summary>
        /// <param name="screenName">The screen name</param>
        /// <returns>The screen with the specified name</returns>
        public GameScreen FindScreenByName(string screenName) {
            GameScreen screen = null;

            if(screens.ContainsKey(screenName)) {
                screen = screens[screenName];
            }

            return screen;
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
                else if(!transition.IsActive) {
                    IsTransitioning = false;
                    transition = null;
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
