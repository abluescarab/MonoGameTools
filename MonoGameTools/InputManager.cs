//=============================================================================
// InputManager.cs
//
// Based on the MonoGame RPG Made Easy tutorial series by CodingMadeEasy.
// Source: https://www.youtube.com/watch?v=pWPTwj4WdtA
//
// Todo:
//    Implement joystick features
//    Multiplayer input choices
//    Implement multiple input choices
//=============================================================================

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGame.Tools {
    public sealed class InputManager {
        public enum InputType {
            Keyboard,
            Mouse,
            KeyboardAndMouse,
            GamePad
        }

        public enum MouseButton {
            Left,
            Middle,
            Right,
            XButton1,
            XButton2
        }

        private static readonly Lazy<InputManager> instance =
            new Lazy<InputManager>(() => new InputManager());

        /// <summary>
        /// The instance of the InputManager.
        /// </summary>
        public static InputManager Instance {
            get { return instance.Value; }
        }

        /// <summary>
        /// The KeyboardInput state manager.
        /// </summary>
        private static class KeyboardInput {
            public static KeyboardState CurrentState { get; set; }
            public static KeyboardState PreviousState { get; set; }

            public static void GetStates() {
                PreviousState = CurrentState;
                CurrentState = Keyboard.GetState();
            }
        }

        /// <summary>
        /// The MouseInput state manager.
        /// </summary>
        private static class MouseInput {
            private static MouseState currentState;
            private static MouseState previousState;

            /// <summary>
            /// The current state.
            /// </summary>
            public static Dictionary<MouseButton, ButtonState>
                CurrentState = new Dictionary<MouseButton, ButtonState>() {
                    { MouseButton.Left, ButtonState.Released },
                    { MouseButton.Middle, ButtonState.Released },
                    { MouseButton.Right, ButtonState.Released },
                    { MouseButton.XButton1, ButtonState.Released },
                    { MouseButton.XButton2, ButtonState.Released }
                };

            /// <summary>
            /// The previous state.
            /// </summary>
            public static Dictionary<MouseButton, ButtonState>
                PreviousState = new Dictionary<MouseButton, ButtonState>() {
                    { MouseButton.Left, ButtonState.Released },
                    { MouseButton.Middle, ButtonState.Released },
                    { MouseButton.Right, ButtonState.Released },
                    { MouseButton.XButton1, ButtonState.Released },
                    { MouseButton.XButton2, ButtonState.Released }
                };

            /// <summary>
            /// Get the current and previous states.
            /// </summary>
            public static void GetStates() {
                previousState = currentState;
                currentState = Mouse.GetState();

                AssignStates();
            }

            /// <summary>
            /// Assign the current and previous button states.
            /// </summary>
            private static void AssignStates() {
                PreviousState[MouseButton.Left] = previousState.LeftButton;
                PreviousState[MouseButton.Middle] = previousState.MiddleButton;
                PreviousState[MouseButton.Right] = previousState.RightButton;
                PreviousState[MouseButton.XButton1] = previousState.XButton1;
                PreviousState[MouseButton.XButton2] = previousState.XButton2;

                CurrentState[MouseButton.Left] = currentState.LeftButton;
                CurrentState[MouseButton.Middle] = currentState.MiddleButton;
                CurrentState[MouseButton.Right] = currentState.RightButton;
                CurrentState[MouseButton.XButton1] = currentState.XButton1;
                CurrentState[MouseButton.XButton2] = currentState.XButton2;
            }
        }

        /// <summary>
        /// The GamePadInput state manager.
        /// </summary>
        private static class GamePadInput {
            /// <summary>
            /// The current state.
            /// </summary>
            public static GamePadState CurrentState { get; set; }
            /// <summary>
            /// The previous state.
            /// </summary>
            public static GamePadState PreviousState { get; set; }

            /// <summary>
            /// Get the current and previous states.
            /// </summary>
            /// <param name="index">The index of the controller</param>
            public static void GetStates(PlayerIndex index) {
                PreviousState = CurrentState;
                CurrentState = GamePad.GetState(index);
            }
        }

        /// <summary>
        /// Create the InputManager.
        /// </summary>
        private InputManager() { }

        /// <summary>
        /// Update the InputManager state.
        /// </summary>
        public void Update() {
            KeyboardInput.GetStates();
            MouseInput.GetStates();
        }

        /// <summary>
        /// Check if a keyboard key has been pressed.
        /// </summary>
        /// <param name="keys">The Keys to check</param>
        /// <returns>If the key has been pressed</returns>
        public bool KeyPressed(params Keys[] keys) {
            foreach(Keys key in keys) {
                if(KeyboardInput.CurrentState.IsKeyDown(key) &&
                    KeyboardInput.PreviousState.IsKeyUp(key)) {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Check if a keyboard key has been released.
        /// </summary>
        /// <param name="keys">The Keys to check</param>
        /// <returns>If the key has been released</returns>
        public bool KeyReleased(params Keys[] keys) {
            foreach(Keys key in keys) {
                if(KeyboardInput.CurrentState.IsKeyUp(key) &&
                    KeyboardInput.PreviousState.IsKeyDown(key)) {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Check if a GamePad button has been pressed.
        /// </summary>
        /// <param name="playerIndex">The index of the controller</param>
        /// <param name="buttons">The Buttons to check</param>
        /// <returns>If the GamePad button has been pressed</returns>
        public bool ButtonPressed(PlayerIndex playerIndex,
            params Buttons[] buttons) {
            GamePadInput.GetStates(playerIndex);

            if(GamePadInput.CurrentState.IsConnected) {
                foreach(Buttons button in buttons) {
                    if(GamePadInput.CurrentState.IsButtonDown(button) &&
                        GamePadInput.PreviousState.IsButtonUp(button)) {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Check if a GamePad button has been released.
        /// </summary>
        /// <param name="playerIndex">The index of the controller</param>
        /// <param name="buttons">The Buttons to check</param>
        /// <returns>If the GamePad button has been released</returns>
        public bool ButtonReleased(PlayerIndex playerIndex,
            params Buttons[] buttons) {
            GamePadInput.GetStates(playerIndex);

            if(GamePadInput.CurrentState.IsConnected) {
                foreach(Buttons button in buttons) {
                    if(GamePadInput.CurrentState.IsButtonUp(button) &&
                        GamePadInput.PreviousState.IsButtonDown(button)) {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Check if a mouse button has been pressed.
        /// </summary>
        /// <param name="buttons">The MouseButtons to check</param>
        /// <returns>If the MouseButton has been pressed</returns>
        public bool MouseButtonPressed(params MouseButton[] buttons) {
            foreach(MouseButton button in buttons) {
                if(MouseInput.CurrentState[button] == ButtonState.Pressed &&
                    MouseInput.PreviousState[button] == ButtonState.Released) {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Check if a mouse button has been released.
        /// </summary>
        /// <param name="buttons">The MouseButtons to check</param>
        /// <returns>If the MouseButton has been released</returns>
        public bool MouseButtonReleased(params MouseButton[] buttons) {
            foreach(MouseButton button in buttons) {
                if(MouseInput.CurrentState[button] == ButtonState.Released &&
                    MouseInput.PreviousState[button] == ButtonState.Pressed) {
                    return true;
                }
            }

            return false;
        }
    }
}
