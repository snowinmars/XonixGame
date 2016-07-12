using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace XonixGame.Configuration
{
    public static class Config
    {
        public static Keys[] AllowedKeys = 
            {
                Keys.Up,
                Keys.Down,
                Keys.Left,
                Keys.Right,
            };
    }
}
