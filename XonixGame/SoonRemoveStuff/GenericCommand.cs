using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoonRemoveStuff
{
    public class GenericCommand<T>
    {
        private Action<T> Action { get; }
        private T Arg { get; }

        public GenericCommand(Action<T> action, T arg)
        {
            this.Action = action;
            this.Arg = arg;
        }

        public void Exec()
        {
            this.Action.Invoke(this.Arg);
        }
    }
}
