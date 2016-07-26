using Algorithms.Library.Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XonixGame.Entities
{
    public class MenuWorld : World
    {
        public MenuWorld() : base()
        {
            this.menu = new Menu<XnaMenuNode>();

            XnaMenuNode head = new XnaMenuNode("Head");

            XnaMenuNode start = new XnaMenuNode("Start");
            XnaMenuNode end = new XnaMenuNode("End");

            this.menu.Connect(head, start);
            this.menu.Connect(head, end);

            this.menu.AddNode(head);
        }

        public override Rectangle Rectangle
        {
            get
            {
                return new Rectangle(0, 0, 640, 480);
            }
        }

        private Menu<XnaMenuNode> menu;

        public override void Draw(SpriteBatch spriteBatch)
        {
            int y = 0;

            foreach (var node in this.menu.Nodes)
            {
                if (node.Text == "Head")
                {
                    continue;
                }

                spriteBatch.DrawString(GameContentManager.Instance.Load(FontType.Defult), node.Text, new Vector2(0, y), Color.Black);
                y += 20;
            }
        }

        public override void Init(GraphicsDevice graphicsDevice)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}