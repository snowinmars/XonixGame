using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SoonRemoveStuff
{
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