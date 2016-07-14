using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithms.Library;

namespace SoonRemoveStuff
{
    public class Strategy
    {
        public Strategy(Scope scope, List<Commands> availableCommands)
        {
            this.Scope = scope;
            this.AvailableCommands = availableCommands;
        }

        public List<Commands> AvailableCommands { get; }

        public Commands State { get; private set; }

        public Scope Scope { get; private set; }

        public void ChangeScope(Scope scope)
        {
            // TODO

            this.Scope = scope;
        }

        public void ApplyCommand(Commands command)
        {
            if (!Bitwise.IsPowerOfTwo((ulong)command))
            {
                throw new ArgumentException("Strategy can not be in conflict", nameof(command));
            }

            if (!this.AvailableCommands.Contains(command))
            {
                throw new ArgumentException("Non available command", nameof(command));
            }

            this.State = command;
        }
    }
}
