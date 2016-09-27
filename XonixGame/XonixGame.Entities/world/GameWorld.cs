using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SandS.Algorithm.Library.PositionNamespace;
using System.Linq;
using XonixGame.ContentMemoryStorageNamespace;
using XonixGame.Enums;

namespace XonixGame.Entities
{
    public class GameWorld : World
    {
        public GameWorld(Player player) : base()
        {
            this.Player = player;
            this.PolygonWrapper = new PolygonWrapper();
        }

        private PolygonWrapper PolygonWrapper { get; set; }

        public Player Player { get; private set; }

        public Texture2D Texture { get; private set; }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var pass in this.BasicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();

                spriteBatch.Draw(this.Texture, 
                    position: Vector2.Zero, 
                    origin: new Vector2(-spriteBatch.GraphicsDevice.Viewport.Width/ 2,
                                        -spriteBatch.GraphicsDevice.Viewport.Height / 2));
                this.PolygonWrapper.Draw(spriteBatch);
                this.Player.Draw(spriteBatch);
            }
        }

        public override void Update()
        {
            this.Player.Update();
            this.PolygonWrapper.Update(this.Player.Position);
        }

        private Matrix WorldMatrix { get; set; }
        private Matrix ViewMatrix { get; set; }
        private Matrix ProjectionMatrix { get; set; }
        private BasicEffect BasicEffect { get; set; }

        public override void LoadContent(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            this.LoadContent(graphicsDevice);

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

            //this.BasicEffect.CurrentTechnique.Passes.First().Apply(); // I suppose, that I will not have any other effects.
        }

        private void LoadMatrixes(GraphicsDevice graphicsDevice)
        {
            this.WorldMatrix = Matrix.CreateWorld(new Vector3(0f, 0f, 0f), new Vector3(0, 0, -1), Vector3.Up); ;
            this.ViewMatrix = Matrix.CreateLookAt(new Vector3(0, 0, -3), Vector3.Zero, Vector3.Up);
            this.ProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
                                                                        graphicsDevice.DisplayMode.AspectRatio,
                                                                        1.0f,
                                                                        100.0f);
        }

        private void LoadContent(GraphicsDevice graphicsDevice)
        {
            this.Texture = TextureStorage.Get(TextureType.World);

            this.Player.LoadContent();
            this.PolygonWrapper.LoadContent(graphicsDevice);
        }

        public override void Initialize()
        {
        }
    }
}