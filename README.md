# MonoGameTools
MonoGameTools is a library that adds some useful classes to the MonoGame framework.

## How to Use
Include the MonoGameTools DLL in your project references, then add the desired namespace to any files.

You can check out the demo project for examples on inheriting the GameScreen class, creating screens and images, and using image effects and screen transitions.

## Added Namespaces

### `MonoGame.Tools` (Manager Classes)

* **`InputManager`**: A singleton used for controlling game input.
* **`ScreenManager`**: A singleton used for controlling the game screens.

### `MonoGame.Tools.Components` (Component Classes)

* **`AnimatedSprite`**: A class that can be used for sprite sheets.
* **`Cursor`**: A class that can be used for a custom cursor with an image.
* **`GameScreen`**: A base class for custom game screens.
* **`Image`**: An class that can be used for external images.
* **`Particle`**: A class that can be used for particles and particle systems.
* **`ParticleEngine`**: A class that implements a particle engine that includes options for randomized particles.

### `MonoGame.Tools.Controls` (Controls)

* **`Button`**: A class that can be used to create a clickable button.

### `MonoGame.Tools.Effects` (Image Effects)

* **`ImageEffect`**: A class that can be inherited from to create image effects.
* **`FadeEffect`**: A class that can create a fade effect on images.
* **`FlashEffect`**: A class that can create a flashing effect on images.
* **`ZoomEffect`**: A class that can create a zoom effect on images.

### `MonoGame.Tools.Events` (Custom EventHandlers and EventArgs)

* **`MouseEventArgs`**: A class based on the WinForms MouseEventArgs.
* **`MouseEventHandler`**: A class based on the WinForms MouseEventHandler.

### `MonoGame.Tools.Transitions` (Screen Transitions)

* **`ScreenTransition`**: An class that can be inherited from to create screen transitions.
* **`FadeTransition`**: A class that can create a fade transition.

## Credits
Some classes are based on the wonderful [XNA Tutorials](http://rbwhitaker.wikidot.com/xna-tutorials) by RB Whitaker, the [MonoGame RPG Made Easy](https://www.youtube.com/playlist?list=PLHJE4y54mpC5hrlDv8yFHPfrSNhqFoA0h) series by CodingMadeEasy, and the .NET library by Microsoft.
