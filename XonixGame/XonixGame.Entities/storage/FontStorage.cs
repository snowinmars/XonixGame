using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace XonixGame.Entities
{
    public static class FontStorage
    {
        private static  IDictionary<FontType, SpriteFont> binding { get; }

        static FontStorage()
        {
            binding = new Dictionary<FontType, SpriteFont>();
        }

        public static SpriteFont Get(FontType fontType)
        {
            return binding[fontType];
        }

        public static void Set(FontType fontType, SpriteFont spriteFont)
        {
            binding.Add(fontType, spriteFont);
        }

        public static void Init()
        {
            binding.Add(FontType.Defult, GameContentManager.Instance.Load(FontType.Defult));
        }
    }
}
