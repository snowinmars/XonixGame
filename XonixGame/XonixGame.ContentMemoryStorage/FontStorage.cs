using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using XonixGame.Enums;

namespace XonixGame.ContentMemoryStorageNamespace
{
    public static class FontStorage
    {
        private static ContentManager ContentManager { get; set; }
        private static IDictionary<FontType, SpriteFont> FontDictionary { get; set; }

        public static SpriteFont Get(FontType fontType)
            => FontStorage.FontDictionary[fontType];

        public static void Initialize(ContentManager contentManager)
        {
            FontStorage.ContentManager = contentManager;
            FontStorage.FontDictionary = new Dictionary<FontType, SpriteFont>();

            FontStorage.FontDictionary.Add(FontType.Default, FontStorage.ContentManager.Load<SpriteFont>("fonts/PTSans14"));
        }
    }
}