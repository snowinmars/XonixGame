using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoonRemoveStuff;

namespace XonixGame.Entities
{
    public class StrategySet
    {
        public StrategySet(IEnumerable<Strategy> strategies)
        {
            this.Strategies = strategies;
        }

        public IEnumerable<Strategy>  Strategies { get; }

        public IEnumerable<Commands> States 
            => this.Strategies.Select(strategy => strategy.State);

        public Strategy GetStrategyByCommand(Commands command)
        {
            return this.Strategies
                .FirstOrDefault(strategy => 
                                    strategy.AvailableCommands.Contains(command));
        }
    }
}
