using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XonixGame.ContentMemoryStorageNamespace;
using XonixGame.Entities;

namespace XonixGame.Monogame
{
    public class XonixGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private World world;

        public XonixGame()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";

            this.graphics.PreferredBackBufferWidth = 1920;
            this.graphics.PreferredBackBufferHeight = 1020;

            this.graphics.IsFullScreen = true;
            this.IsMouseVisible = true;
        }

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);

            this.spriteBatch.Begin();

            this.world.Draw(this.spriteBatch);

            this.spriteBatch.End();

            base.Draw(gameTime);
        }

        protected override void Initialize()
        {
            TextureStorage.Initialize(this.Content, this.GraphicsDevice);
            FontStorage.Initialize(this.Content);

            this.GraphicsDevice.RasterizerState = new RasterizerState
            {
                CullMode = CullMode.None
            };

            Player player = new Player(0, 0);
            this.world = new GameWorld(player);
            this.world.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);

            this.world.LoadContent(this.Content, this.GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            this.world.Update();

            base.Update(gameTime);
        }
    }
}