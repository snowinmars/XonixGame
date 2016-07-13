using Microsoft.Xna.Framework.Input;
using SoonRemoveStuff;
using System.Collections.Generic;

namespace XonixGame.Configuration
{
    public static class Config
    {
        public static IDictionary<Keys, Commands> KeyComandBinding { get; } = new Dictionary<Keys, Commands>
        {
            {Keys.Up, Commands.MoveUp},
            {Keys.Down, Commands.MoveDown },
            {Keys.Left, Commands.MoveLeft },
            {Keys.Right, Commands.MoveRight},
        };

        /// <summary>
        /// Speed in pixels per tick
        /// </summary>
        public static Position DefaultHeadSpeed = new Position(1,1); // px
    }
}