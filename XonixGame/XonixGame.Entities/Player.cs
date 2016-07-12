using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using SoonRemoveStuff;

namespace XonixGame.Entities
{
    public class Player
    {
        public Player(Head head)
        {
            this.Head = head;
        }

        public Head Head { get; set; }

        public Position Position { get; set; }
    }
}
