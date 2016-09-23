using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace XonixGame.Entities
{
    public abstract class World
    {
        protected World()
        {
        }

        public abstract void Draw(SpriteBatch spriteBatch);

        public abstract void Update();

        public abstract void LoadContent(ContentManager contentManager, GraphicsDevice graphicsDevice);

        public abstract void Initialize();
    }
}