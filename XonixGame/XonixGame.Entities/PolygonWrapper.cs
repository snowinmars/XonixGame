using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
            foreach (var playerPosition in this.PlayerPositions)
            {
                spriteBatch.Draw(this.dotTexture, new Vector2(playerPosition.Position.X, playerPosition.Position.Y));
            }

            if (this.PlayerPositions.Count > 2)
            {
                spriteBatch.GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineStrip, this.PlayerPositions.ToArray(), 0, this.PlayerPositions.Count / 2);
            }
        }

        public void Update()
        {
            
        }
    }
}
