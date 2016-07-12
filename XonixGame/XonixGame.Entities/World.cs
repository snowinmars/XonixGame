using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SoonRemoveStuff;

namespace XonixGame.Entities
{
    public class World : IUpdatable
    {
        private KeyboardInputHelper KeyboardInputHelper { get; }

        public World()
        {
            this.KeyboardInputHelper = new KeyboardInputHelper();
        }

        public void Update(GameTime gameTime)
        {
            // update KeyboardInputHelper before anything else
            this.KeyboardInputHelper.Update(gameTime);
        }
    }
}