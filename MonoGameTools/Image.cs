//===================================================================
// Image.cs
//
// Based on the MonoGame RPG Made Easy tutorial series by
// CodingMadeEasy.
// Source: https://www.youtube.com/watch?v=jr8sktSwn7E
//===================================================================

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Tools.Effects;

namespace MonoGame.Tools {
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
            float rotation = 0.0f) {
            origin = Vector2.Zero;
            sourceRectangle = Rectangle.Empty;
            content = null;
            effects = new Dictionary<string, ImageEffect>();

            Path = path;
            Position = position;
            Scale = scale;
            Visible = visible;
            Alpha = alpha;
            Rotation = rotation;
            Dimensions = Vector2.Zero;
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
                    Dimensions = new Vector2(0, 0);
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

            if(content != null) {
                content.Unload();
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
            if(Visible) {
                origin = new Vector2(
                    sourceRectangle.Width / 2,
                    sourceRectangle.Height / 2);

                if(Texture != null) {
                    spriteBatch.Draw(Texture, Position + origin, sourceRectangle,
                        Color.White * Alpha, Rotation, origin, Scale,
                        SpriteEffects.None, 0.0f);
                }
            }
        }
    }
}