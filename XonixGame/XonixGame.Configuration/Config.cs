using Microsoft.Xna.Framework.Input;
using SoonRemoveStuff;
using System.Collections.Generic;
using Algorithms.Library;

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

        public static Strategy HeadCommonStrategy { get; } = new Strategy(new Scope(0),
                                                                            new List<Commands>
                                                                            {
                                                                                Commands.Wait,
                                                                            });
        public static Strategy HorizontalMovementStrategy { get; } = new Strategy(new Scope(1),
                                                                            new List<Commands>
                                                                            {
                                                                                Commands.MoveLeft,
                                                                                Commands.MoveRight,
                                                                                Commands.Wait,
                                                                            });
        public static Strategy VerticalMovementStrategy { get; } = new Strategy(new Scope(2),
                                                                            new List<Commands>
                                                                            {
                                                                                Commands.MoveUp,
                                                                                Commands.MoveDown,
                                                                                Commands.Wait,
                                                                            });

        /// <summary>
        /// Speed in pixels per tick
        /// </summary>
        public static Position DefaultHeadSpeed = new Position(1,1); // px
    }
}