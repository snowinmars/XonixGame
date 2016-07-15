using Algorithms.Library;
using Microsoft.Xna.Framework.Input;
using SoonRemoveStuff;
using System.Collections.Generic;
using Scope = SoonRemoveStuff.Scope;

namespace XonixGame.Configuration
{
    public static class Config
    {
        public static IDictionary<Keys, Commands> KeyCommandBinding { get; } = new Dictionary<Keys, Commands>
        {
            {Keys.Up, Commands.MoveUp},
            {Keys.Down, Commands.MoveDown },
            {Keys.Left, Commands.MoveLeft },
            {Keys.Right, Commands.MoveRight},
        };

        public static StateSet HeadCommonStrategy { get; } = new StateSet(new Scope(0),
                                                                            new List<Commands>
                                                                            {
                                                                                Commands.Wait,
                                                                            });

        public static StateSet HorizontalMovementStrategy { get; } = new StateSet(new Scope(1),
                                                                            new List<Commands>
                                                                            {
                                                                                Commands.MoveLeft,
                                                                                Commands.MoveRight,
                                                                                Commands.Wait,
                                                                            });

        public static StateSet VerticalMovementStrategy { get; } = new StateSet(new Scope(2),
                                                                            new List<Commands>
                                                                            {
                                                                                Commands.MoveUp,
                                                                                Commands.MoveDown,
                                                                                Commands.Wait,
                                                                            });

        /// <summary>
        /// Speed in pixels per tick
        /// </summary>
        public static Position DefaultHeadSpeed = new Position(1, 1); // px
    }
}