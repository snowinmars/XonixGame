using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SandS.Algorithm.Library.MenuNamespace;

namespace XonixGame.Entities
{
    public abstract class World : ICanLoadContent, IInitializable
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