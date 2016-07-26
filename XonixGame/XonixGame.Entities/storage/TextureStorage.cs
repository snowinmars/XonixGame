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
    public static class TextureStorage 
    {

        static TextureStorage()
        {
            binding = new Dictionary<TextureType, Texture2D>();
        }

        public static Texture2D Get(TextureType textureType)
        {
            return binding[textureType];
        }

        public static void Set(TextureType textureType, Texture2D texture)
        {
            binding.Add(textureType, texture);
        }

        public static void Init(GraphicsDevice graphicsDevice)
        {
            binding.Add(TextureType.Empty, GameContentManager.Instance.Load(TextureType.Empty));
            binding.Add(TextureType.Head, GameContentManager.Instance.Load(TextureType.Head));
        }

        private static IDictionary<TextureType, Texture2D> binding { get; }
    }
}
