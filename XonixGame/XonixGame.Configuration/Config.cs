using Microsoft.Xna.Framework.Input;
using SoonRemoveStuff;
using System.Collections.Generic;

namespace XonixGame.Configuration
{
    public static class Config
    {
        public static IDictionary<Keys, Comands> KeyComandBinding { get; } = new Dictionary<Keys, Comands>
        {
            {Keys.Up, Comands.MoveUp},
            {Keys.Down, Comands.MoveDown },
            {Keys.Left, Comands.MoveLeft },
            {Keys.Right, Comands.MoveRight},
        };

        /// <summary>
        /// Speed in pixels per tick
        /// </summary>
        public static Position DefaultHeadSpeed = new Position(1,1); // px
    }
}