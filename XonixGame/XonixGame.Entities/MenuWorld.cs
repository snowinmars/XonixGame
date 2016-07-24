using SoonRemoveStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Algorithms.Library.Menu;

namespace XonixGame.Entities
{
    public class MenuWorld : World, SoonRemoveStuff.IDrawable, IUpdatable
    {
        private Menu menu { get; }

        public MenuWorld(ITextureStorage textureStorage) : base(textureStorage)
        {
            this.menu = new Menu();
            
            MenuNode start = new MenuNode("Start");
            MenuNode exit = new MenuNode("Exit");

            this.menu.Head.Connect(start);
            this.menu.Head.Connect(exit);
        }

        public override Rectangle Rectangle
        {
            get
            {
                return new Rectangle(0, 0, 640, 480);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var node in this.menu.Nodes)
            {
                spriteBatch.DrawString(this.fontStorage, node.Text);
            }
        }

        public override void Init(GraphicsDevice graphicsDevice)
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
