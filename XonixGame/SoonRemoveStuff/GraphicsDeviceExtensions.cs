using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SoonRemoveStuff
{
    public static class A
    {
        public static T CantBeMore<T>(this T value, T cutoff)
            where T : IComparable
        {
            if (value.CompareTo(cutoff) > 0)
            {
                value = cutoff;
            }

            return value;
        }

        public static T CantBeLess<T>(this T value, T cutoff)
            where T : IComparable
        {
            if (value.CompareTo(cutoff) < 0)
            {
                value = cutoff;
            }

            return value;
        }
    }

    public static class B
    {
        public static Color NextColor(this Random random)
        {
            return new Color(random.Next(0,255),
                                random.Next(0, 255),
                                random.Next(0, 255),
                                random.Next(0, 255));
        }
    }
}