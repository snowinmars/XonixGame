using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using SoonRemoveStuff;
using Microsoft.Xna.Framework;

namespace XonixGame.Entities
{
    public class TextureStorage : ITextureStorage
    {

        public TextureStorage()
        {
            this.binding = new Dictionary<TextureType, Texture2D>();
        }

        public Texture2D Get(TextureType textureType)
        {
            return this.binding[textureType];
        }

        public void Init(GraphicsDevice graphicsDevice)
        {
            this.binding.Add(TextureType.Head, graphicsDevice.Generate(10, 10, Color.Red));
        }

        private IDictionary<TextureType, Texture2D> binding { get; }
    }
}
