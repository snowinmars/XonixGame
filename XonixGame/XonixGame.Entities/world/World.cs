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

        public abstract Rectangle Rectangle { get; }

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime);

        public abstract void LoadContent(ContentManager contentManager, GraphicsDevice graphicsDevice);

        public abstract void Initialize();
    }
}