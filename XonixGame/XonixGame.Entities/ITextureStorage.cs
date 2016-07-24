using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XonixGame.Entities
{
    public enum TextureType
    {
        Empty = 0,
        Head = 1,S
    }

    public interface ITextureStorage
    {
        Texture2D Get(TextureType textureType);
        void Init(GraphicsDevice graphicsDevice);
    }
}
