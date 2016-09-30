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

        public static Position WorldSize { get; set; } = new Position(5, 2);
        public static Position PlayerSize { get; set; } = new Position(10, 10);
        public static int MaxSpeedX { get; set; } = 7;
        public static int MaxSpeedY { get; set; } = 7;

        public static Position LeftUpperCorner
        {
            get
            {
                return new Position(Config.WorldSize.X / 2, Config.WorldSize.Y / 2);
            }
        }

        /// <summary>
        /// Speed in pixels per tick
        /// </summary>
        public static PositionVector DefaultHeadSpeed { get; }= new PositionVector(0.05f, 0.05f);
    }
}