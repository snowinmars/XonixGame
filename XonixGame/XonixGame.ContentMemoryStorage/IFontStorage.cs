using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace XonixGame.ContentMemoryStorageNamespace
{
    public interface IFontStorage
    {
        void Initialize(ContentManager contentManager);

        SpriteFont Get(FontType fontType);
    }

    public enum FontType
    {
        Default = 0,
    }
}