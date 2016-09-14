using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XonixGame.Entities
{
    public class Player
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