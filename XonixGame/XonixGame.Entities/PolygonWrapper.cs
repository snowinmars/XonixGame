using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Poly2Tri;
using SandS.Algorithm.Library.PositionNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using XonixGame.Configuration;
using XonixGame.ContentMemoryStorageNamespace;
using XonixGame.Enums;

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
        private Texture2D dotTexture { get; set; }
        private RenderTarget2D renderTarget2D { get; set; }
        public PolygonWrapperState State { get; private set; }
        private Polygon polygon { get; set; }

        public PolygonWrapper()
        {
            this.State = PolygonWrapperState.None;

            IList<PolygonPoint> points = new List<PolygonPoint>
            {
                new PolygonPoint(0,0),
                new PolygonPoint(5,5),
                new PolygonPoint(Config.WorldSize.X, 0),
                new PolygonPoint(Config.WorldSize.X - 5, 5),
                new PolygonPoint(0, Config.WorldSize.Y),
                new PolygonPoint(5, Config.WorldSize.Y - 5),
                new PolygonPoint(Config.WorldSize.X, Config.WorldSize.Y),
                new PolygonPoint(Config.WorldSize.X - 5, Config.WorldSize.Y - 5),
            };

            this.polygon = new Polygon(points);
        }

        public void LoadContent(GraphicsDevice graphicsDevice)
        {
            this.dotTexture = TextureStorage.Get(TextureType.Default);
            this.renderTarget2D = new RenderTarget2D(graphicsDevice, Config.WorldSize.X, Config.WorldSize.Y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (this.State)
            {
            case PolygonWrapperState.RecordStarted:
            case PolygonWrapperState.RecordFinished:
            case PolygonWrapperState.TesselationStarted:
                {
                    VertexPosition[] a = this.polygon.Points.Select(p => new VertexPosition(new Vector3((float)p.X, (float)p.Y, 0))).ToArray();
                    spriteBatch.GraphicsDevice.DrawUserPrimitives<VertexPosition>(PrimitiveType.LineStrip, a, 0, a.Length / 3);
                    break;
                }
            case PolygonWrapperState.TesselationFinished:
                {
                    this.State = PolygonWrapperState.TextureGeneraingStarted;
                    goto case PolygonWrapperState.TextureGeneraingStarted;
                    break;
                }
            case PolygonWrapperState.TextureGeneraingStarted:
                {
                    spriteBatch.GraphicsDevice.SetRenderTarget(this.renderTarget2D);
                    RenderPolygon(spriteBatch);
                    spriteBatch.GraphicsDevice.SetRenderTarget(null);
                    this.State = PolygonWrapperState.RecordFinished;
                    goto case PolygonWrapperState.TextureGeneraingFinished;
                    break;
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

        private void RenderPolygon(SpriteBatch spriteBatch)
        {
            VertexPosition[] asd = new VertexPosition[this.polygon.Triangles.Count * 3];

            for (int trianglesCount = 0;
                trianglesCount < this.polygon.Triangles.Count;
                trianglesCount++)
            {
                for (int pointCount = 0; 
                    pointCount < 3; // 3 is 3 points in triangle
                    pointCount++)
                {
                    TriangulationPoint asdasd = this.polygon.Triangles[trianglesCount].Points[pointCount];
                    asd[trianglesCount * 3 + pointCount] = new VertexPosition(new Vector3((float)asdasd.X, (float)asdasd.Y, 0));
                }
            }

            spriteBatch.GraphicsDevice.DrawUserPrimitives<VertexPosition>(PrimitiveType.LineStrip, asd, 0, asd.Length);
        }

        private Position previousPosition;

        public void Update(Position pos)
        {
            if (object.ReferenceEquals(previousPosition, null))
            {
                previousPosition = pos;
            }

            TriangulationPoint actualTriangulationPoint = new TriangulationPoint(pos.X, pos.Y);
            TriangulationPoint previousTriangulationPoint = new TriangulationPoint(previousPosition.X, previousPosition.Y);

            UpdateState(actualTriangulationPoint, previousTriangulationPoint);
            HandleState(actualTriangulationPoint);

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
                    this.polygon.Simplify();
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