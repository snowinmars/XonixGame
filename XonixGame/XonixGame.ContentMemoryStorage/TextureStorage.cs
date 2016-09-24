using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SandS.Algorithm.CommonNamespace;
using SandS.Algorithm.Extensions.GraphicsDeviceExtensionNamespace;
using SoonRemoveStuff;
using System.Collections.Generic;
using XonixGame.Configuration;
using XonixGame.Enums;

namespace XonixGame.ContentMemoryStorageNamespace
{
    public static class TextureStorage
    {
        private static ContentManager ContentManager { get; set; }

        private static IDictionary<TextureType, Texture2D> TextureDictionary { get; set; }

        public static Texture2D Get(TextureType textureType)
                            => TextureStorage.TextureDictionary[textureType];

        public static void Initialize(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            TextureStorage.ContentManager = contentManager;
            TextureStorage.TextureDictionary = new Dictionary<TextureType, Texture2D>
            {
                {
                    TextureType.Default,
                    graphicsDevice.Generate(1,
                                            1,
                                            Color.Black)
                },
                {
                    TextureType.Player,
                    graphicsDevice.Generate(Config.PlayerSize.X,
                                            Config.PlayerSize.Y,
                                            CommonValues.Random.NextColor())
                },
                {
                    TextureType.World,
                    graphicsDevice.Generate(Config.WorldSize.X,
                                            Config.WorldSize.Y,
                                            Color.Aqua,
                                            borderThick: 2,
                                            borderColor: Color.Blue)
                },
            };
        }
    }
}