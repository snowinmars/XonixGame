﻿using Microsoft.Xna.Framework.Input;
using System;

namespace SoonRemoveStuff
{
    public class InputHelper
    {
        #region Private Fields

        private KeyboardState keyboardState;
        private MouseState mouseState;
        private KeyboardState oldKeyboardState;
        private MouseState oldMouseState;

        #endregion Private Fields

        #region Public Constructors

        public InputHelper(KeyboardState oldKeyboardState, MouseState oldMouseState, KeyboardState keyboardState, MouseState mouseState)
        {
            this.oldKeyboardState = oldKeyboardState;
            this.oldMouseState = oldMouseState;

            this.keyboardState = keyboardState;
            this.mouseState = mouseState;

            this.InputKeyPressType = InputKeyPressType.OnUp;
        }

        #endregion Public Constructors

        #region Public Properties

        public InputKeyPressType InputKeyPressType { get; set; }

        #endregion Public Properties

        #region Public Methods

        public Position GetMousePosition()
        {
            return new Position(this.mouseState.X, this.mouseState.Y);
        }

        public bool WasKeyPressed(Keys key)
        {
            switch (this.InputKeyPressType)
            {
                case InputKeyPressType.OnUp:
                    return this.oldKeyboardState.IsKeyDown(key) &&
                            this.keyboardState.IsKeyUp(key);

                case InputKeyPressType.OnDown:
                    return this.oldKeyboardState.IsKeyUp(key) &&
                            this.keyboardState.IsKeyDown(key);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public bool WasMouseButtonPressed(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return this.WasLeftMouseButtonPressed();

                case MouseButton.Middle:
                    return this.WasMiddleMouseButtonPressed();

                case MouseButton.Right:
                    return this.WasRightMouseButtonPressed();

                default:
                    throw new ArgumentOutOfRangeException(nameof(button), button, null);
            }
        }

        #endregion Public Methods

        #region Private Methods

        private bool WasLeftMouseButtonPressed()
        {
            switch (this.InputKeyPressType)
            {
                case InputKeyPressType.OnUp:
                    return this.oldMouseState.LeftButton == ButtonState.Pressed &&
                            this.mouseState.LeftButton == ButtonState.Released;

                case InputKeyPressType.OnDown:
                    return this.oldMouseState.LeftButton == ButtonState.Released &&
                            this.mouseState.LeftButton == ButtonState.Pressed;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private bool WasMiddleMouseButtonPressed()
        {
            switch (this.InputKeyPressType)
            {
                case InputKeyPressType.OnUp:
                    return this.oldMouseState.MiddleButton == ButtonState.Pressed &&
                            this.mouseState.MiddleButton == ButtonState.Released;

                case InputKeyPressType.OnDown:
                    return this.oldMouseState.MiddleButton == ButtonState.Released &&
                            this.mouseState.MiddleButton == ButtonState.Pressed;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private bool WasRightMouseButtonPressed()
        {
            switch (this.InputKeyPressType)
            {
                case InputKeyPressType.OnUp:
                    return this.oldMouseState.RightButton == ButtonState.Pressed &&
                            this.mouseState.RightButton == ButtonState.Released;

                case InputKeyPressType.OnDown:
                    return this.oldMouseState.RightButton == ButtonState.Released &&
                            this.mouseState.RightButton == ButtonState.Pressed;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion Private Methods
    }
}