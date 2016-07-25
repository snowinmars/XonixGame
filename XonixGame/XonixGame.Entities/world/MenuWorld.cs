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
    public class MenuWorld : World
    {
        public MenuWorld(ITextureStorage textureStorage, IFontStorage fontStorage) : base(textureStorage, fontStorage)
        {
            this.menu = new Menu();

            MenuNode start = new MenuNode("Start");
            MenuNode end = new MenuNode("End");

            this.menu.Head.Connect(start);
            this.menu.Head.Connect(end);
        }

        public override Rectangle Rectangle
        {
            get
            {
                return new Rectangle(0, 0, 640, 480);
            }
        }

        private Menu menu;

        public override void Draw(SpriteBatch spriteBatch)
        {
             spriteBatch.DrawString(GameContentManager.Instance.Load(FontType.Defult), ((MenuNode)this.menu.Head).Text, new Vector2 (10,10),Color.Black);
        }

        public override void Init(GraphicsDevice graphicsDevice)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
