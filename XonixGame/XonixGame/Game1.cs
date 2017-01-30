using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XonixGame.Constants;
using XonixGame.ContentStorage;
using XonixGame.Entities;

namespace XonixGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public Game1()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";
        }

        private Player player;
        private Matrix viewMatrix;
            Matrix projectionMatrix;
        
        protected override void Initialize()
        {
            viewMatrix = Matrix.CreateLookAt(new Vector3(0, 0, 10),
                                                Vector3.Zero,
                                                Vector3.Up);
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
                                                                    (float)this.Window.ClientBounds.Width / (float)this.Window.ClientBounds.Height,
                                                                    1, 
                                                                    100);

            
            effect = new BasicEffect(this.GraphicsDevice);

            this.player = new Player();

            base.Initialize();
        }

            BasicEffect effect;
        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
            Storage.Load(this.Content);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            this.player.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);

            effect.World = this.player.WorldMatrix;
            effect.View = viewMatrix;
            effect.Projection = projectionMatrix;

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                this.spriteBatch.Begin(transformMatrix: this.player.WorldMatrix);
                this.player.Draw(this.spriteBatch);
                this.spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}