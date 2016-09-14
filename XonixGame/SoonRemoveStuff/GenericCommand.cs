using System;

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