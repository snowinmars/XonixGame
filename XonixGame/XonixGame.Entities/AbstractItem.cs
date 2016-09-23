using Microsoft.Xna.Framework;
using SandS.Algorithm.Library.OtherNamespace;

namespace XonixGame.Entities
{
    public abstract class AbstractItem
    {
        protected KeyboardInputHelper KeyboardInputHelper { get; }

        protected AbstractItem()
        {
            this.KeyboardInputHelper = new KeyboardInputHelper();
        }

        public virtual void Update()
        {
            this.KeyboardInputHelper.Update(null); // TODO ?
        }
    }
}