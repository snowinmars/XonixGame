using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithms.Library;
using Algorithms.Library.Menu;
using Microsoft.Xna.Framework;
using SoonRemoveStuff;

namespace XonixGame.Entities
{
    public class XnaMenu<T>  : Menu<T>
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
    }
}
