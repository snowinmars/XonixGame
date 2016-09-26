using Microsoft.Xna.Framework;
using System;
using SandS.Algorithm.Library.EnumsNamespace;

namespace SoonRemoveStuff
{
    public static class B
    {
        public static Color NextColor(this Random random)
        {
            return new Color(random.Next(0, 255),
                                random.Next(0, 255),
                                random.Next(0, 255),
                                random.Next(0, 255));
        }
 
        public static bool IsOpposed(this Direction lhs, Direction rhs)
        {
            return (lhs == Direction.Left && rhs == Direction.Right) ||
                   (lhs == Direction.Up && rhs == Direction.Down) ||
                   rhs.IsOpposed(lhs);
        }

        public static bool IsAdjacent(this Direction lhs, Direction rhs)
        {
            return false;
        }
    }
}