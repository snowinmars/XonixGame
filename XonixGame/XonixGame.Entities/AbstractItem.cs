using Microsoft.Xna.Framework;
using SoonRemoveStuff;

namespace XonixGame.Entities
{
    public abstract class AbstractItem : IUpdatable
    {
        protected KeyboardInputHelper KeyboardInputHelper { get; }

        protected AbstractItem()
        {
            this.KeyboardInputHelper = new KeyboardInputHelper();
        }

        public virtual void Update(GameTime gameTime)
        {
            this.KeyboardInputHelper.Update(gameTime);
        }
    }
}