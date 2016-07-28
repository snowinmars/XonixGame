using Algorithms.Library;
using Algorithms.Library.Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace XonixGame.Entities
{
    public class XnaMenu<T> : Menu<T>
        where T : XnaMenuNode
    {
        internal static XnaMenuNode drawingNode;

        public XnaMenu() : this(new Graph<T>())
        {
        }

        public XnaMenu(IEnumerable<T> nodes) : this(new Graph<T>(nodes))
        {
        }

        protected internal XnaMenu(Graph<T> graph) : base(graph)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (XnaMenu<T>.drawingNode == null)
            {
                return;
            }

            foreach (var node in XnaMenu<T>.drawingNode.Children)
            {
                spriteBatch.DrawString(GameContentManager.Instance.Load(FontType.Defult),
                    node.Text,
                    new Vector2(node.Position.X, node.Position.Y),
                    Microsoft.Xna.Framework.Color.Black);
            }
        }
    }
}