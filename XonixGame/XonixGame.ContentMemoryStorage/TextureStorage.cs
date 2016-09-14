﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SandS.Algorithm.CommonNamespace;
using SandS.Algorithm.Extensions.GraphicsDeviceExtensionNamespace;
using System;
using System.Collections.Generic;

namespace XonixGame.ContentMemoryStorageNamespace
{
    public class TextureStorage : ITextureStorage
    {
        #region singleton

        protected TextureStorage()
        {
        }

        private static readonly Lazy<TextureStorage> instance = new Lazy<TextureStorage>(() => new TextureStorage());

        public static TextureStorage Instance => TextureStorage.instance.Value;

        #endregion singleton

        public Texture2D Get(TextureType textureType)
            => this.textureStorage[textureType];

        private IDictionary<TextureType, Texture2D> textureStorage { get; set; }
        private ContentManager ContentManager { get; set; }

        public void Initialize(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            this.ContentManager = contentManager;
            this.textureStorage = new Dictionary<TextureType, Texture2D>
            {
                {
                    TextureType.Default, graphicsDevice.Generate(1, 1, new Color(CommonValues.Random.Next(),
                        CommonValues.Random.Next(),
                        CommonValues.Random.Next(),
                        CommonValues.Random.Next()))
                }
            };
        }
    }
}