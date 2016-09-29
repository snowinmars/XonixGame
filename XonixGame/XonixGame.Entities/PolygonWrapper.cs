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
        private PositionVector previousPosition;

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
        private Polygon polygon;
        private RenderTarget2D renderTarget2D;

        public void Draw(SpriteBatch spriteBatch)
        {
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
                        spriteBatch.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, vertexes, 0, vertexes.Length / 3);
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

        public void LoadContent(GraphicsDevice graphicsDevice)
        {
            //this.dotTexture = TextureStorage.Get(TextureType.Default);
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