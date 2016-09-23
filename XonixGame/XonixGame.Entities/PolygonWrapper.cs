using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using XonixGame.ContentMemoryStorageNamespace;

namespace XonixGame.Entities
{
    internal class PolygonWrapper
    {
        private IList<VertexPositionColor> PlayerPositions { get; }
        private Texture2D dotTexture { get; set; }

        public PolygonWrapper()
        {
            this.PlayerPositions = new List<VertexPositionColor>(128);
        }

        public void LoadContent()
        {
            this.dotTexture = TextureStorage.Instance.Get(TextureType.Default);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }

        public void Update()
        {
        }
    }
}