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
        /// Check if any keyboard key has been pressed.
        /// </summary>
        /// <param name="keys">The Keys that have been pressed</param>
        /// <returns>If any keys have been pressed</returns>
        public bool AnyKeyPressed(out Keys[] keys) {
            KeyboardInput.GetStates();

            keys = KeyboardInput.CurrentState.GetPressedKeys();

            if(KeyboardInput.CurrentState.GetPressedKeys().Length > 0) {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Check if any keyboard key has been pressed.
        /// </summary>
        /// <returns>If any keys have been pressed</returns>
        public bool AnyKeyPressed() {
            KeyboardInput.GetStates();

            if(KeyboardInput.CurrentState.GetPressedKeys().Length > 0) {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Check if any keyboard key has been released.
        /// </summary>
        /// <param name="keys">The Keys that have been released</param>
        /// <returns>If any keys have been released</returns>
        public bool AnyKeyReleased(out Keys[] keys) {
            List<Keys> list = new List<Keys>();
            KeyboardInput.GetStates();

            foreach(Keys key in Enum.GetValues(typeof(Keys))) {
                if(KeyboardInput.CurrentState.IsKeyUp(key) &&
                   KeyboardInput.PreviousState.IsKeyDown(key)) {
                    list.Add(key);
                }
            }

            keys = list.ToArray();

            if(keys.Length > 0) {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Check if any keyboard key has been released.
        /// </summary>
        /// <returns>If any keys have been released</returns>
        public bool AnyKeyReleased() {
            KeyboardInput.GetStates();

            foreach(Keys key in Enum.GetValues(typeof(Keys))) {
                if(KeyboardInput.CurrentState.IsKeyUp(key) &&
                   KeyboardInput.PreviousState.IsKeyDown(key)) {
                    return true;
                }
            }

            return false;
        }

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
        /// Checks if any GamePad buttons have been pressed.
        /// </summary>
        /// <param name="playerIndex">The index of the controller</param>
        /// <param name="buttons">The Buttons that are pressed</param>
        /// <returns>If any GamePad buttons have been pressed</returns>
        public bool AnyButtonPressed(PlayerIndex playerIndex, out Buttons[] buttons) {
            List<Buttons> list = new List<Buttons>();

            GamePadInput.GetStates(playerIndex);

            buttons = null;

            if(GamePadInput.CurrentState.IsConnected) {
                foreach(Buttons button in Enum.GetValues(typeof(Buttons))) {
                    if(GamePadInput.CurrentState.IsButtonDown(button) &&
                       GamePadInput.PreviousState.IsButtonUp(button)) {
                        list.Add(button);
                    }
                }

                buttons = list.ToArray();

                if(buttons.Length > 0) {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if any GamePad buttons have been pressed.
        /// </summary>
        /// <param name="playerIndex">The index of the controller</param>
        /// <returns>If any GamePad buttons have been pressed</returns>
        public bool AnyButtonPressed(PlayerIndex playerIndex) {
            GamePadInput.GetStates(playerIndex);

            if(GamePadInput.CurrentState.IsConnected) {
                foreach(Buttons button in Enum.GetValues(typeof(Buttons))) {
                    if(GamePadInput.CurrentState.IsButtonDown(button) &&
                       GamePadInput.PreviousState.IsButtonUp(button)) {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if any GamePad buttons have been released.
        /// </summary>
        /// <param name="playerIndex">The index of the controller</param>
        /// <param name="buttons">The Buttons that are released</param>
        /// <returns>If any GamePad buttons have been released</returns>
        public bool AnyButtonReleased(PlayerIndex playerIndex, out Buttons[] buttons) {
            List<Buttons> list = new List<Buttons>();

            GamePadInput.GetStates(playerIndex);

            buttons = null;

            if(GamePadInput.CurrentState.IsConnected) {
                foreach(Buttons button in Enum.GetValues(typeof(Buttons))) {
                    if(GamePadInput.CurrentState.IsButtonUp(button) &&
                       GamePadInput.PreviousState.IsButtonDown(button)) {
                        list.Add(button);
                    }
                }

                buttons = list.ToArray();

                if(buttons.Length > 0) {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if any GamePad buttons have been released.
        /// </summary>
        /// <param name="playerIndex">The index of the controller</param>
        /// <returns>If any GamePad buttons have been released</returns>
        public bool AnyButtonReleased(PlayerIndex playerIndex) {
            GamePadInput.GetStates(playerIndex);

            if(GamePadInput.CurrentState.IsConnected) {
                foreach(Buttons button in Enum.GetValues(typeof(Buttons))) {
                    if(GamePadInput.CurrentState.IsButtonUp(button) &&
                       GamePadInput.PreviousState.IsButtonDown(button)) {
                        return true;
                    }
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
        /// Check if any mouse button has been pressed.
        /// </summary>
        /// <param name="buttons">the MouseButtons that have been pressed</param>
        /// <returns>If any MouseButtons have been pressed</returns>
        public bool AnyMouseButtonPressed(out MouseButton[] buttons) {
            List<MouseButton> list = new List<MouseButton>();
            MouseInput.GetStates();

            foreach(MouseButton button in Enum.GetValues(typeof(MouseButton))) {
                if(MouseInput.CurrentButtonState[button] == ButtonState.Pressed &&
                   MouseInput.PreviousButtonState[button] == ButtonState.Released) {
                    list.Add(button);
                }
            }

            buttons = list.ToArray();

            if(buttons.Length > 0) {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Check if any mouse button has been pressed.
        /// </summary>
        /// <returns>If any MouseButtons have been pressed</returns>
        public bool AnyMouseButtonPressed() {
            MouseInput.GetStates();

            foreach(MouseButton button in Enum.GetValues(typeof(MouseButton))) {
                if(MouseInput.CurrentButtonState[button] == ButtonState.Pressed &&
                   MouseInput.PreviousButtonState[button] == ButtonState.Released) {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Check if any mouse button has been released.
        /// </summary>
        /// <param name="buttons">the MouseButtons that have been released</param>
        /// <returns>If any MouseButtons have been released</returns>
        public bool AnyMouseButtonReleased(out MouseButton[] buttons) {
            List<MouseButton> list = new List<MouseButton>();
            MouseInput.GetStates();

            foreach(MouseButton button in Enum.GetValues(typeof(MouseButton))) {
                if(MouseInput.CurrentButtonState[button] == ButtonState.Released &&
                   MouseInput.PreviousButtonState[button] == ButtonState.Pressed) {
                    list.Add(button);
                }
            }

            buttons = list.ToArray();

            if(buttons.Length > 0) {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Check if any mouse button has been released.
        /// </summary>
        /// <returns>If any MouseButtons have been released</returns>
        public bool AnyMouseButtonReleased() {
            MouseInput.GetStates();

            foreach(MouseButton button in Enum.GetValues(typeof(MouseButton))) {
                if(MouseInput.CurrentButtonState[button] == ButtonState.Released &&
                   MouseInput.PreviousButtonState[button] == ButtonState.Pressed) {
                    return true;
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
        /// Check if a mouse button has been pressed.
        /// </summary>
        /// <param name="position">The position of the mouse cursor</param>
        /// <param name="buttons">The MouseButtons to check</param>
        /// <returns>If the MouseButton has been pressed</returns>
        public bool MouseButtonPressed(out Vector2 position,
            params MouseButton[] buttons) {
            MouseInput.GetStates();
            position = MouseInput.CurrentState.Position.ToVector2();

            foreach(MouseButton button in buttons) {
                if(MouseInput.CurrentButtonState[button] == ButtonState.Pressed &&
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
        /// Check if a mouse button has been released.
        /// </summary>
        /// <param name="position">The position of the mouse cursor</param>
        /// <param name="buttons">The MouseButtons to check</param>
        /// <returns>If the MouseButton has been released</returns>
        public bool MouseButtonReleased(out Vector2 position,
            params MouseButton[] buttons) {
            MouseInput.GetStates();
            position = MouseInput.CurrentState.Position.ToVector2();

            foreach(MouseButton button in buttons) {
                if(MouseInput.CurrentButtonState[button] == ButtonState.Released &&
                    MouseInput.PreviousButtonState[button] == ButtonState.Pressed) {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Get the current position of the mouse cursor.
        /// </summary>
        /// <returns>The position of the mouse cursor</returns>
        public Vector2 GetMousePosition() {
            return Mouse.GetState().Position.ToVector2();
        }
    }
}
