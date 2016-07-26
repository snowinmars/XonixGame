﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XonixGame.Entities
{
    public abstract class World
    {
        protected World()
        {
        }

        public abstract Rectangle Rectangle { get; }

        public abstract void Draw(SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime);

        public abstract void Init(GraphicsDevice graphicsDevice);
    }
}