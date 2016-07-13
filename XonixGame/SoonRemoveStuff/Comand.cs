using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoonRemoveStuff
{
    public class Comand
    {
        public Comand()
        {
            
        }

        public Comand(Action action)
        {
            this.Action = action;
        }

        public Action Action { get; set; }

        public void Exec()
        {
            this.Action?.Invoke();
        }
    }
}
