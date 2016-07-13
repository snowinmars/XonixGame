using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SoonRemoveStuff;

namespace XonixGame.Entities
{
    public class Player : AbstractItem/*, IMoveable, SoonRemoveStuff.IDrawable, IUpdatable*/
    {
        public Player(Head head)
        {
            this.Head = head;
        }

        public Head Head { get; set; }

        public Position Position { get; set; }
    }
}
