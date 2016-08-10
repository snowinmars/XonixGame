using System;
using SandS.Algorithm.Library.Bitwise;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoonRemoveStuff
{
    /// <summary>
    /// Simple Strategy pattern
    /// </summary>
    public class StateSet
    {
        public StateSet(Scope scope, List<Commands> availableCommands)
        {
            this.Scope = scope;
            this.AvailableStates = availableCommands;
        }

        public List<Commands> AvailableStates { get; }

        /// <summary>
        /// Strategy can be only in one state. This state have to be choosen from AvailableStates
        /// </summary>
        public Commands State { get; private set; }

        /// <summary>
        /// Different strategies have to be in different scopes
        /// </summary>
        public Scope Scope { get; private set; }

        public void ChangeScope(Scope scope)
        {
            // TODO

            this.Scope = scope;
        }

        public void ApplyCommand(Commands command)
        {
            if (!BitwiseOperation.IsPowerOfTwo((ulong)command))
            {
                throw new ArgumentException("Strategy can not be in conflict", nameof(command));
            }

            if (!this.AvailableStates.Contains(command))
            {
                throw new ArgumentException("Non available command", nameof(command));
            }

            this.State = command;
        }
    }
}
