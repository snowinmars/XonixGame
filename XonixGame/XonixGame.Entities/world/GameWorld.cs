using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Poly2Tri;
using System.Collections.Generic;
using XonixGame.Configuration;

namespace XonixGame.Entities
{
    public class GameWorld : World
    {
        public GameWorld(Player player) : base()
        {
            this.Player = player;

            this.BoundaryPolygon = this.InitBoundaries();
            this.FieldPolygon = this.InitField();
        }

        private PolygonWrapper InitField()
        {
            int x = Config.LeftUpperCorner.X;
            int y = Config.LeftUpperCorner.Y;

            IList<PolygonPoint> field = new List<PolygonPoint>
            {
                new PolygonPoint(-x, -y),
                new PolygonPoint(-x, y),
                new PolygonPoint(x,y),
                new PolygonPoint(x,-y),
            };

            Polygon fieldPolygon = new Polygon(field);

            return new PolygonWrapper(fieldPolygon, null);
        }

        private PolygonWrapper InitBoundaries()
        {
            int x = Config.LeftUpperCorner.X;
            int y = Config.LeftUpperCorner.Y;

            IList<PolygonPoint> boundary = new List<PolygonPoint>
            {
                new PolygonPoint(-x, -y),
                new PolygonPoint(-x, y),
                new PolygonPoint(x,y),
                new PolygonPoint(x,-y),
            };

            const double offset = 0.1;

            IList<PolygonPoint> boundaryHole = new List<PolygonPoint>
            {
                new PolygonPoint(-x + offset, -y + offset),
                new PolygonPoint(-x + offset, y - offset),
                new PolygonPoint(x - offset, y - offset),
                new PolygonPoint(x - offset, -y + offset),
            };

            Polygon boundaryPolygon = new Polygon(boundary);
            Polygon boundaryHolePolygon = new Polygon(boundaryHole);

            return new PolygonWrapper(boundaryPolygon, new[] { boundaryHolePolygon });
        }

        private PolygonWrapper BoundaryPolygon { get; set; }
        private PolygonWrapper FieldPolygon { get; set; }

        public Player Player { get; private set; }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var pass in this.BasicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();

                this.FieldPolygon.Draw(spriteBatch);
                this.BoundaryPolygon.Draw(spriteBatch);
                this.Player.Draw(spriteBatch);
            }
        }

        public override void Update()
        {
            this.Player.Update();
            this.FieldPolygon.Update(this.Player.Position);
            this.BoundaryPolygon.Update(this.Player.Position);
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
                VertexColorEnabled = true,
            };
        }

        private void LoadMatrixes(GraphicsDevice graphicsDevice)
        {
            this.WorldMatrix = Matrix.CreateWorld(new Vector3(0f, 0f, 0f), new Vector3(0, 0, -1), Vector3.Up);
            this.ViewMatrix = Matrix.CreateLookAt(new Vector3(0, 0, 3), Vector3.Zero, Vector3.Up);
            this.ProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
                                                                        graphicsDevice.DisplayMode.AspectRatio,
                                                                        1.0f,
                                                                        100.0f);
        }

        private void LoadContent(GraphicsDevice graphicsDevice)
        {
            this.Player.LoadContent();
            this.FieldPolygon.LoadContent(graphicsDevice);
            this.BoundaryPolygon.LoadContent(graphicsDevice);
        }

        public override void Initialize()
        {
        }
    }
}