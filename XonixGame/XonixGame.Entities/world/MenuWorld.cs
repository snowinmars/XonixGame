using Algorithms.Library.Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SoonRemoveStuff;

namespace XonixGame.Entities
{
    public class MenuWorld : World
    {
        public MenuWorld() : base()
        {
            this.menu = new XnaMenu<XnaMenuNode>();

            XnaMenuNode head = new XnaMenuNode("Head");

            XnaMenuNode start = new XnaMenuNode("Start");
            XnaMenuNode end = new XnaMenuNode("End");

            start.Position = new Position(50, 0);
            end.Position = new Position(50, 20);

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

        private XnaMenu<XnaMenuNode> menu;

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var node in this.menu.Nodes)
            {
                if (node.Text == "Head")
                {
                    continue;
                }

                spriteBatch.DrawString(GameContentManager.Instance.Load(FontType.Defult),
                                        node.Text, 
                                        new Vector2(node.Position.X, node.Position.Y), 
                                        Color.Black);
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