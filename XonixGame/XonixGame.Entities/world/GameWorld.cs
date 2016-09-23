using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SandS.Algorithm.Library.PositionNamespace;
using System.Linq;
using XonixGame.ContentMemoryStorageNamespace;

namespace XonixGame.Entities
{
    public class GameWorld : World
    {
        public GameWorld(Player player, Position size) : base()
        {
            this.Player = player;
            this.PolygonWrapper = new PolygonWrapper();
        }

        private PolygonWrapper PolygonWrapper { get; set; }

        public Player Player { get; private set; }

        public Texture2D Texture { get; private set; }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, Vector2.Zero);
            this.Player.Draw(spriteBatch);
            this.PolygonWrapper.Draw(spriteBatch);
        }

        public override void Update()
        {
            this.Player.Update();
            this.PolygonWrapper.Update();
        }

        private Matrix WorldMatrix { get; set; }
        private Matrix ViewMatrix { get; set; }
        private Matrix ProjectionMatrix { get; set; }
        private BasicEffect BasicEffect { get; set; }

        public override void LoadContent(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            this.LoadContent();

            this.LoadMatrixes(graphicsDevice);

            this.LoadEffects(graphicsDevice);

            graphicsDevice.RasterizerState = RasterizerState.CullNone;
        }

        private void LoadEffects(GraphicsDevice graphicsDevice)
        {
            this.BasicEffect = new BasicEffect(graphicsDevice)
            {
                World = this.WorldMatrix,
                View = this.ViewMatrix,
                Projection = this.ProjectionMatrix,
                VertexColorEnabled = true
            };

            this.BasicEffect.CurrentTechnique.Passes.First().Apply(); // I suppose, that I will not have any other effects.
        }

        private void LoadMatrixes(GraphicsDevice graphicsDevice)
        {
            this.WorldMatrix = Matrix.Identity;
            this.ViewMatrix = Matrix.CreateLookAt(new Vector3(0, 0, -3), Vector3.Zero, Vector3.Up);
            this.ProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
                                                                        graphicsDevice.DisplayMode.AspectRatio,
                                                                        1.0f,
                                                                        100.0f);
        }

        private void LoadContent()
        {
            this.Texture = TextureStorage.Instance.Get(TextureType.World);

            this.Player.LoadContent();
            this.PolygonWrapper.LoadContent();
        }

        public override void Initialize()
        {
        }
    }
}