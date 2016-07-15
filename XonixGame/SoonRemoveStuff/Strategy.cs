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
        public Strategy(IEnumerable<StateSet> strategies)
        {
            this.StateSets = strategies;
        }

        public IEnumerable<StateSet> StateSets { get; }

        /// <summary>
        /// Returns all states of all stategies. Return values can repeat.
        /// </summary>
        public IEnumerable<Commands> GetStates()
        {
            return this.StateSets.Select(strategy => strategy.State);
        }

        /// <summary>
        /// Returns first strategy, that has 'command' as available state
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public StateSet GetStrategyByCommand(Commands command)
        {
            // Is Commands.Wait a problem?
            return this.StateSets
                .FirstOrDefault(strategy =>
                                    strategy.AvailableStates.Contains(command));
        }
    }
}
