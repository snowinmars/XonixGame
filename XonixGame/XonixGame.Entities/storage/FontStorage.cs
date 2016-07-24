using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace XonixGame.Entities
{
    public class FontStorage : IFontStorage
    {

        private IDictionary<FontType, SpriteFont> binding { get; }

        public FontStorage()
        {
            this.binding = new Dictionary<FontType, SpriteFont>();
        }

        public SpriteFont Get(FontType fontType)
        {
            return this.binding[fontType];
        }

        public void Init()
        {
            this.binding.Add(FontType.Defult, GameContentManager.Instance.Load(FontType.Defult));
        }
    }
}
