# MonoGameTools
MonoGameTools is a library to add some useful classes to the MonoGame framework.

## How to Use
Include the MonoGameTools DLL in your project references. Add `using MonoGame.Tools` or `using MonoGame.Tools.Effects` to the top of each file where you need the added classes, or access each class by calling its full name.

## Added Classes

* **AnimatedSprite.cs**: A class that can be used for sprite sheets.
* **Image.cs**: An advanced class that can be used for external images.
* **Particle.cs**: A class that can be used for particles and particle systems.
* **ParticleEngine.cs**: A class that implements a particle engine that includes options for randomized particles.

### Image Effects

* **ImageEffect.cs**: An advanced class that can be inherited from to create image effects.
* **FadeEffect.cs**: A class that can create a fade effect on images.
* **FlashEffect.cs**: A class that can create a flashing effect on images.
* **ZoomEffect.cs**: A class that can create a zoom effect on images.

## Credits
Some classes are based on the wonderful [XNA Tutorials](http://rbwhitaker.wikidot.com/xna-tutorials) by RB Whitaker and the [MonoGame RPG Made Easy](https://www.youtube.com/playlist?list=PLHJE4y54mpC5hrlDv8yFHPfrSNhqFoA0h) series by CodingMadeEasy.
