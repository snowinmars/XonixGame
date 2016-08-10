using Microsoft.Xna.Framework;
using SandS.Algorithm.Library.OtherNamespace;
using IUpdatable = SoonRemoveStuff.IUpdatable;

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