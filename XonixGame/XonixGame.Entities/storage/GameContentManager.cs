using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using SoonRemoveStuff;
using Microsoft.Xna.Framework;

namespace XonixGame.Entities
{
    public class GameContentManager
    {
        #region singleton

        protected GameContentManager()
        {
            
        }

        public static GameContentManager Instance => SingletonCreator<GameContentManager>.CreatorInstance;

        private sealed class SingletonCreator<S>
            where S : class
        {
            public static S CreatorInstance { get; } = (S)typeof(S).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic,
                                                                                    null,
                                                                                    new Type[0],
                                                                                    new ParameterModifier[0]).Invoke(null);
        }
        #endregion singleton

        public SpriteFont Load(FontType fontType)
        {
            switch (fontType)
            {
            case FontType.Defult:
                return contentManager.Load<SpriteFont>("fonts/PTSans14");
            default:
                throw new ArgumentException();
            }
        }

        public Texture2D Load(TextureType textureType)
        {
            switch (textureType)
            {
            case TextureType.Empty:
                return graphicsDevice.Generate(10, 10, Color.Red);
            case TextureType.Head:
                return graphicsDevice.Generate(10, 10, Color.Red);
            default:
                throw new ArgumentException();
            }
        }

        public static void Init(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            GameContentManager.contentManager = contentManager;
            GameContentManager.graphicsDevice = graphicsDevice;
        }

        private static ContentManager contentManager;
        private static GraphicsDevice graphicsDevice;
    }
}
