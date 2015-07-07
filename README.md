# MonoGameTools
MonoGameTools is a library to add some useful classes to the MonoGame framework.

## How to Use
Include the MonoGameTools DLL in your project references. There are three namespaces:
`MonoGame.Tools`, the base namespace; `MonoGame.Tools.Effects`, for image effects; and `MonoGame.Tools.Transitions`, for screen transitions.

## Added Classes

* **AnimatedSprite.cs**: A class that can be used for sprite sheets.
* **GameScreen.cs**: A base class for custom game screens.
* **Image.cs**: An class that can be used for external images.
* **Particle.cs**: A class that can be used for particles and particle systems.
* **ParticleEngine.cs**: A class that implements a particle engine that includes options for randomized particles.
* **ScreenManager.cs**: A singleton used for controlling the game screens.

### Image Effects

* **ImageEffect.cs**: An class that can be inherited from to create image effects.
* **FadeEffect.cs**: A class that can create a fade effect on images.
* **FlashEffect.cs**: A class that can create a flashing effect on images.
* **ZoomEffect.cs**: A class that can create a zoom effect on images.

### Screen Transitions

* **ScreenTransition.cs**: An class that can be inherited from to create screen transitions.
* **FadeTransition.cs**: A class that can create a fade transition.

## Credits
Some classes are based on the wonderful [XNA Tutorials](http://rbwhitaker.wikidot.com/xna-tutorials) by RB Whitaker and the [MonoGame RPG Made Easy](https://www.youtube.com/playlist?list=PLHJE4y54mpC5hrlDv8yFHPfrSNhqFoA0h) series by CodingMadeEasy.
