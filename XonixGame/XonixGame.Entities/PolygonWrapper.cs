using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Poly2Tri;
using SandS.Algorithm.Library.PositionNamespace;
using System;
using System.Collections.Generic;
using System.Linq;

namespace XonixGame.Entities
{
    public enum PolygonWrapperState
    {
        None,
        RecordStarted,
        RecordFinished,
        TesselationStarted,
        TesselationFinished,
        TextureGeneraingStarted,
        TextureGeneraingFinished,
    }

    internal class PolygonWrapper
    {
        private Polygon polygon;
        private PositionVector previousPosition;
        private RenderTarget2D renderTarget2D;

        public PolygonWrapper(Polygon p, IList<Polygon> holes)
        {
            this.State = PolygonWrapperState.RecordFinished;
            this.polygon = p;

            if (holes == null)
            {
                return;
            }

            foreach (var hole in holes)
            {
                this.polygon.AddHole(hole);
            }
        }

        public PolygonWrapperState State { get; private set; }
        private BasicEffect BasicEffect { get; set; }
        private Matrix ProjectionMatrix { get; set; }
        private Matrix ViewMatrix { get; set; }
        private Matrix WorldMatrix { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var pass in this.BasicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();

                switch (this.State)
                {
                    case PolygonWrapperState.RecordStarted:
                    case PolygonWrapperState.RecordFinished:
                    case PolygonWrapperState.TesselationStarted:
                        {
                            VertexPositionColor[] vertexes = this.polygon
                                .Points
                                .Select(p => new VertexPositionColor(new Vector3((float)p.X,
                                    (float)p.Y,
                                    0),
                                    Color.White))
                                .ToArray();
                            spriteBatch.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, vertexes, 0,
                                vertexes.Length / 3);
                            goto case PolygonWrapperState.None;
                        }
                    case PolygonWrapperState.TesselationFinished:
                        {
                            this.State = PolygonWrapperState.TextureGeneraingStarted;
                            goto case PolygonWrapperState.TextureGeneraingStarted;
                        }
                    case PolygonWrapperState.TextureGeneraingStarted:
                        {
                            this.RenderTrianglesToRenderTarget(spriteBatch);
                            this.State = PolygonWrapperState.TextureGeneraingFinished;
                            goto case PolygonWrapperState.TextureGeneraingFinished;
                        }
                    case PolygonWrapperState.None:
                    case PolygonWrapperState.TextureGeneraingFinished:
                        {
                            spriteBatch.Draw(this.renderTarget2D, Vector2.Zero);
                            break;
                        }
                    default:
                        {
                            throw new ArgumentOutOfRangeException();
                        }
                }
            }
        }

        public void LoadContent(GraphicsDevice graphicsDevice)
        {
            this.LoadMatrixes(graphicsDevice);
            this.LoadEffects(graphicsDevice);

            this.renderTarget2D = new RenderTarget2D(graphicsDevice, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height);
        }

        public void Update(PositionVector pos)
        {
            if (object.ReferenceEquals(this.previousPosition, null))
            {
                this.previousPosition = pos;
            }

            TriangulationPoint actualTriangulationPoint = new TriangulationPoint(pos.X, pos.Y);
            TriangulationPoint previousTriangulationPoint = new TriangulationPoint(this.previousPosition.X, this.previousPosition.Y);

            this.UpdateState(actualTriangulationPoint, previousTriangulationPoint);
            this.HandleState(actualTriangulationPoint);

            this.previousPosition = pos;
        }

        private void HandleState(TriangulationPoint pos)
        {
            switch (this.State)
            {
                case PolygonWrapperState.RecordStarted:
                    {
                        this.polygon.Add(pos);
                        break;
                    }
                case PolygonWrapperState.RecordFinished:
                    {
                        this.State = PolygonWrapperState.TesselationStarted;
                        goto case PolygonWrapperState.TesselationStarted;
                    }
                case PolygonWrapperState.TesselationStarted:
                    {
                        this.State = PolygonWrapperState.TesselationFinished;
                        P2T.Triangulate(this.polygon);
                        goto case PolygonWrapperState.TesselationFinished;
                    }
                case PolygonWrapperState.TesselationFinished:
                case PolygonWrapperState.None:
                case PolygonWrapperState.TextureGeneraingStarted:
                    {
                        return;
                    }
                case PolygonWrapperState.TextureGeneraingFinished:
                    {
                        this.State = PolygonWrapperState.None;
                        break;
                    }
                default:
                    {
                        throw new ArgumentOutOfRangeException();
                    }
            }
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

        private VertexPositionColor[] MapTrianglesDotsToVertex()
        {
            VertexPositionColor[] vertexes = new VertexPositionColor[this.polygon.Triangles.Count * 3];

            for (int trianglesCount = 0;
                    trianglesCount < this.polygon.Triangles.Count;
                    trianglesCount++)
            {
                for (int pointCount = 0;
                        pointCount < 3; // 3 is 3 points in triangle
                        pointCount++)
                {
                    TriangulationPoint point = this.polygon.Triangles[trianglesCount].Points[pointCount];
                    vertexes[trianglesCount * 3 + pointCount] = new VertexPositionColor(new Vector3(-(float)(point.X), -(float)(point.Y), 0), Color.White);
                }
            }

            return vertexes;
        }

        private void RenderPolygon(SpriteBatch spriteBatch)
        {
            VertexPositionColor[] vertexes = this.MapTrianglesDotsToVertex();

            spriteBatch.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, vertexes, 0, vertexes.Length / 3);
        }

        private void RenderTrianglesToRenderTarget(SpriteBatch spriteBatch)
        {
            spriteBatch.GraphicsDevice.SetRenderTarget(this.renderTarget2D);
            spriteBatch.GraphicsDevice.Clear(Color.TransparentBlack);
            this.RenderPolygon(spriteBatch);
            spriteBatch.GraphicsDevice.SetRenderTarget(null);
        }

        private void UpdateState(TriangulationPoint actualTriangulationPoint, TriangulationPoint previousTriangulationPoint)
        {
            bool isOnBorder = this.polygon.Contains(actualTriangulationPoint);
            bool wasOnBorder = this.polygon.Contains(previousTriangulationPoint);

            // inside border zone player doesn't track
            if (isOnBorder && wasOnBorder)
            {
                return;
            }

            // if player steps out
            if (!isOnBorder && wasOnBorder)
            {
                this.State = PolygonWrapperState.RecordStarted;
            }

            if (isOnBorder && !wasOnBorder)
            {
                this.State = PolygonWrapperState.RecordFinished;
            }
        }
    }
}