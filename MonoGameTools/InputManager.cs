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
            public static KeyboardState CurrentState { get; private set; }
            public static KeyboardState PreviousState { get; private set; }

            public static void GetStates() {
                PreviousState = CurrentState;
                CurrentState = Keyboard.GetState();
            }
        }

        /// <summary>
        /// The MouseInput state manager.
        /// </summary>
        private static class MouseInput {
            public static MouseState CurrentState { get; private set; }
            public static MouseState PreviousState { get; private set; }

            /// <summary>
            /// The current state.
            /// </summary>
            public static Dictionary<MouseButton, ButtonState>
                CurrentButtonState = new Dictionary<MouseButton, ButtonState>() {
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
                PreviousButtonState = new Dictionary<MouseButton, ButtonState>() {
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
                PreviousState = CurrentState;
                CurrentState = Mouse.GetState();

                AssignStates();
            }

            /// <summary>
            /// Assign the current and previous button states.
            /// </summary>
            private static void AssignStates() {
                PreviousButtonState[MouseButton.Left] = PreviousState.LeftButton;
                PreviousButtonState[MouseButton.Middle] = PreviousState.MiddleButton;
                PreviousButtonState[MouseButton.Right] = PreviousState.RightButton;
                PreviousButtonState[MouseButton.XButton1] = PreviousState.XButton1;
                PreviousButtonState[MouseButton.XButton2] = PreviousState.XButton2;

                CurrentButtonState[MouseButton.Left] = CurrentState.LeftButton;
                CurrentButtonState[MouseButton.Middle] = CurrentState.MiddleButton;
                CurrentButtonState[MouseButton.Right] = CurrentState.RightButton;
                CurrentButtonState[MouseButton.XButton1] = CurrentState.XButton1;
                CurrentButtonState[MouseButton.XButton2] = CurrentState.XButton2;
            }
        }

        /// <summary>
        /// The GamePadInput state manager.
        /// </summary>
        private static class GamePadInput {
            /// <summary>
            /// The current state.
            /// </summary>
            public static GamePadState CurrentState { get; private set; }
            /// <summary>
            /// The previous state.
            /// </summary>
            public static GamePadState PreviousState { get; private set; }

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
        /// Check if a keyboard key has been pressed.
        /// </summary>
        /// <param name="keys">The Keys to check</param>
        /// <returns>If the key has been pressed</returns>
        public bool KeyPressed(params Keys[] keys) {
            KeyboardInput.GetStates();

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
            KeyboardInput.GetStates();

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
            MouseInput.GetStates();

            foreach(MouseButton button in buttons) {
                if(MouseInput.CurrentButtonState[button] == ButtonState.Pressed &&
                    MouseInput.PreviousButtonState[button] == ButtonState.Released) {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Check if a mouse button has been pressed at a location.
        /// </summary>
        /// <param name="x">The x-coordinate to check</param>
        /// <param name="y">The y-coordinate to check</param>
        /// <param name="buttons">The MouseButtons to check</param>
        /// <returns>If the MouseButton has been pressed</returns>
        public bool MouseButtonPressed(int x, int y, params MouseButton[] buttons) {
            MouseInput.GetStates();

            foreach(MouseButton button in buttons) {
                if(MouseInput.CurrentState.X == x &&
                   MouseInput.CurrentState.Y == y &&
                   MouseInput.CurrentButtonState[button] == ButtonState.Pressed &&
                   MouseInput.PreviousButtonState[button] == ButtonState.Released) {
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
            MouseInput.GetStates();

            foreach(MouseButton button in buttons) {
                if(MouseInput.CurrentButtonState[button] == ButtonState.Released &&
                    MouseInput.PreviousButtonState[button] == ButtonState.Pressed) {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Check if a mouse button has been released at a location.
        /// </summary>
        /// <param name="x">The x-coordinate to check</param>
        /// <param name="y">The y-coordinate to check</param>
        /// <param name="buttons">The MouseButtons to check</param>
        /// <returns>If the MouseButton has been released</returns>
        public bool MouseButtonReleased(int x, int y, params MouseButton[] buttons) {
            MouseInput.GetStates();

            foreach(MouseButton button in buttons) {
                if(MouseInput.CurrentState.X == x &&
                   MouseInput.CurrentState.Y == y &&
                   MouseInput.CurrentButtonState[button] == ButtonState.Released &&
                   MouseInput.PreviousButtonState[button] == ButtonState.Pressed) {
                       return true;
                }
            }

            return false;
        }
    }
}
