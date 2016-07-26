﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XonixGame.Entities
{
    public interface IFontStorage
    {
        SpriteFont Get(FontType fontType);

        void Init();
    }
}