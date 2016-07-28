using Algorithms.Library.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithms.Library;
using Microsoft.Xna.Framework;
using Color = Algorithms.Library.Color;
using SoonRemoveStuff;

namespace XonixGame.Entities
{
    public class XnaMenuNode : MenuNode
    {
        public XnaMenuNode() : base()
        {
        }

        public XnaMenuNode(Position position)
        {
            this.Position = position;
        }

        public XnaMenuNode(string text, IEnumerable<GraphNode> connections = null, Color color = Color.White)
            : base(text, connections, color)
        {
        }

        public Position Position { get; set; }

        public Rectangle Rectangle
        {
            get
            {
                Point textSize = FontStorage.Get(FontType.Defult).MeasureString(this.Text).ToPoint();
                return new Rectangle(this.Position.X, this.Position.Y, textSize.X, textSize.Y);
            }
        }

        public new IList<XnaMenuNode> Children { get; }

        public void Click()
        {
            XnaMenu<XnaMenuNode>.drawingNode = this;
        }
    }
}
