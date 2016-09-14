using Microsoft.Xna.Framework.Input;
using SandS.Algorithm.Library.EnumsNamespace;
using SandS.Algorithm.Library.PositionNamespace;
using System.Collections.Generic;

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

        public static int MaxSpeedX { get; set; } = 1;
        public static int MaxSpeedY { get; set; } = 1;
        public static Position PositionEpsilon { get; set; } = new Position(4, 4);

        /// <summary>
        /// Speed in pixels per tick
        /// </summary>
        public static Position DefaultHeadSpeed = new Position(1, 1); // px
    }
}