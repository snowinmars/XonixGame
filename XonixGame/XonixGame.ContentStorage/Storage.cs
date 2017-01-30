using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SandS.Algorithm.Library.EnumsNamespace;
using System.Collections.Generic;
using XonixGame.Constants;

namespace XonixGame.ContentStorage
{
    public static class Storage
    {
        public static IDictionary<string, Texture2D> Textures { get; }

        public static IDictionary<Keys, Commands> KeysCommandBinding { get; private set; }

        public static Keys[] AllKeyboardKeys { get; private set; }

        static Storage()
        {
            Storage.Textures = new Dictionary<string, Texture2D>(16);

            Storage.KeysCommandBinding = new Dictionary<Keys, Commands>
            {
                {Keys.W, Commands.MoveUp },
                {Keys.D, Commands.MoveRight },
                {Keys.S, Commands.MoveDown },
                { Keys.A, Commands.MoveLeft},
                {Keys.Space, Commands.Wait },
            };

            Storage.AllKeyboardKeys = new Keys[255];

            for (int i = 0; i < 255; i++)
            {
                Keys key = (Keys)i;

                Storage.AllKeyboardKeys[i] = key;
            }
        }

        public static void Load(ContentManager contentManager)
        {
            const string texturesFolderPath = "textures";

            Storage.Textures.Add(Const.TexturePlayerName, contentManager.Load<Texture2D>($"{texturesFolderPath}/{Const.TexturePlayerName}"));
        }
    }
}