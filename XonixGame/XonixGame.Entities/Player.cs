using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SoonRemoveStuff;

namespace XonixGame.Entities
{
    public class Player : SoonRemoveStuff.IDrawable, IUpdatable
    {
        public Player(Head head)
        {
            this.Head = head;
        }

        public Head Head { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            this.Head.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            this.Head.Update(gameTime);
        }
    }
}