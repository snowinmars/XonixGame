using Microsoft.Xna.Framework;
using SandS.Algorithm.Library.EnumsNamespace;
using SandS.Algorithm.Library.PositionNamespace;
using System;

namespace SoonRemoveStuff
{
    public static class Extensions
    {
        public static Color NextColor(this Random random)
        {
            return new Color(random.Next(0, 255),
                                random.Next(0, 255),
                                random.Next(0, 255),
                                random.Next(0, 255));
        }
    }
}