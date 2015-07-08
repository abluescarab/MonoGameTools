//=============================================================================
// Image.cs
//
// Based on the MonoGame RPG Made Easy tutorial series by CodingMadeEasy.
// Source: https://www.youtube.com/watch?v=jr8sktSwn7E
//=============================================================================

using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Tools.Effects;

namespace MonoGame.Tools.Components {
    public class Image {
        private Vector2 origin;
        private Rectangle sourceRectangle;
        private ContentManager content;
        private Dictionary<string, ImageEffect> effects;

        /// <summary>
        /// The image path.
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// The image position in the window.
        /// </summary>
        public Vector2 Position { get; set; }
        /// <summary>
        /// The image scale.
        /// </summary>
        public Vector2 Scale { get; set; }
        /// <summary>
        /// Whether the image is visible.
        /// </summary>
        public bool Visible { get; set; }
        /// <summary>
        /// The image's opacity, from 0.0 to 1.0.
        /// </summary>
        public float Alpha { get; set; }
        /// <summary>
        /// The image's rotation in radians.
        /// </summary>
        public float Rotation { get; set; }
        /// <summary>
        /// The image's texture.
        /// </summary>
        public Texture2D Texture { get; private set; }
        /// <summary>
        /// The image's dimensions.
        /// </summary>
        public Vector2 Dimensions { get; private set; }
        /// <summary>
        /// Whether to center the origin or put it in the top left.
        /// </summary>
        public bool CenterOrigin { get; set; }

        /// <summary>
        /// Create the Image.
        /// </summary>
        public Image() {
            origin = Vector2.Zero;
            sourceRectangle = Rectangle.Empty;
            content = null;
            effects = new Dictionary<string, ImageEffect>();

            Path = string.Empty;
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Visible = true;
            Alpha = 1.0f;
            Rotation = 0.0f;
            Dimensions = Vector2.Zero;
            CenterOrigin = true;
        }

        /// <summary>
        /// Create the Image.
        /// </summary>
        /// <param name="path">The image path</param>
        /// <param name="position">The image position in the window</param>
        /// <param name="scale">The image scale</param>
        /// <param name="visible">Whether the image is visible</param>
        /// <param name="alpha">The image's opacity, from 0.0 to 1.0</param>
        /// <param name="rotation">The image's rotation in radians</param>
        public Image(string path, Vector2 position, Vector2 scale,
            bool visible = true, float alpha = 1.0f,
            float rotation = 0.0f, bool centerOrigin = true) {
            origin = Vector2.Zero;
            sourceRectangle = Rectangle.Empty;
            content = null;
            effects = new Dictionary<string, ImageEffect>();
            Dimensions = Vector2.Zero;

            Path = path;
            Position = position;
            Scale = scale;
            Visible = visible;
            Alpha = alpha;
            Rotation = rotation;
            CenterOrigin = centerOrigin;
        }

        /// <summary>
        /// Add an ImageEffect.
        /// </summary>
        /// <typeparam name="T">An ImageEffect child class</typeparam>
        /// <param name="effectName">The effect name</param>
        /// <param name="effect">The effect</param>
        public void AddEffect<T>(string effectName, T effect) where T : ImageEffect {
            if(!effects.ContainsKey(effectName)) {
                if(effect == null) {
                    effect = (T)Activator.CreateInstance(typeof(T));
                }
                else {
                    (effect as ImageEffect).IsActive = false;
                    (effect as ImageEffect).LoadContent(this);
                }

                effects.Add(effectName, effect as ImageEffect);
            }
            else {
                effects[effectName].IsActive = false;
                effects[effectName].LoadContent(this);
            }
        }

        /// <summary>
        /// Remove an ImageEffect.
        /// </summary>
        /// <param name="effectName">The effect name</param>
        public void RemoveEffect(string effectName) {
            if(effects.ContainsKey(effectName)) {
                DeactivateEffect(effectName);
                effects.Remove(effectName);
            }
        }

        /// <summary>
        /// Find an ImageEffect by its name.
        /// </summary>
        /// <param name="effectName">The effect name</param>
        /// <returns>The ImageEffect with the specified name</returns>
        public ImageEffect FindEffectByName(string effectName) {
            ImageEffect effect = null;
            
            if(effects.ContainsKey(effectName)) {
                effect = effects[effectName];
            }

            return effect;
        }

        /// <summary>
        /// Find ImageEffects of type.
        /// </summary>
        /// <typeparam name="T">The ImageEffect child class</typeparam>
        /// <returns>An array of ImageEffects of the specified type</returns>
        public ImageEffect[] FindEffectsByType<T>() where T : ImageEffect {
            return effects.Values.OfType<T>().ToArray();
        }

        /// <summary>
        /// Activate an ImageEffect.
        /// </summary>
        /// <param name="effectName">The effect name</param>
        public void ActivateEffect(string effectName) {
            if(effects.ContainsKey(effectName)) {
                effects[effectName].Activate(this);
            }
        }

        /// <summary>
        /// Deactivate an ImageEffect.
        /// </summary>
        /// <param name="effectName">The effect name</param>
        public void DeactivateEffect(string effectName) {
            if(effects.ContainsKey(effectName)) {
                effects[effectName].Deactivate();
            }
        }

        /// <summary>
        /// Load the image content into a ContentManager.
        /// </summary>
        /// <param name="content">The ContentManager to load to</param>
        public void LoadContent(ContentManager content) {
            this.content = content;

            if(!string.IsNullOrEmpty(Path)) {
                Texture = this.content.Load<Texture2D>(Path);

                if(Texture != null) {
                    Dimensions = new Vector2(
                        Texture.Width,
                        Texture.Height);
                }
                else {
                    Dimensions = Vector2.Zero;
                }
            }

            if(sourceRectangle == Rectangle.Empty) {
                sourceRectangle = new Rectangle(0, 0,
                    (int)Dimensions.X, (int)Dimensions.Y);
            }
        }

        /// <summary>
        /// Unload the image content from the ContentManager.
        /// </summary>
        public void UnloadContent() {
            foreach(ImageEffect effect in effects.Values) {
                effect.Deactivate();
            }
            
            Texture = null;
            Dimensions = Vector2.Zero;
            sourceRectangle = Rectangle.Empty;

            if(content != null) {
                content.Unload();
                content = null;
            }
        }

        /// <summary>
        /// Update the image.
        /// </summary>
        /// <param name="gameTime">The game time TimeSpan</param>
        public void Update(GameTime gameTime) {
            foreach(ImageEffect effect in effects.Values) {
                effect.Update(gameTime);
            }
        }

        /// <summary>
        /// Draw the image content.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch to draw to</param>
        public void Draw(SpriteBatch spriteBatch) {
            if(Visible && Texture != null) {
                if(CenterOrigin) {
                    origin = new Vector2(
                        sourceRectangle.Width / 2,
                        sourceRectangle.Height / 2);
                }
                else {
                    origin = new Vector2(0, 0);
                }

                spriteBatch.Draw(Texture, Position, sourceRectangle,
                    Color.White * Alpha, Rotation, origin, Scale,
                    SpriteEffects.None, 0.0f);
            }
        }
    }
}