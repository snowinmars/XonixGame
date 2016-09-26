﻿using Microsoft.Xna.Framework;
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
        private Position previousPosition;

        public PolygonWrapper()
        {
            IList<PolygonPoint> bound = new List<PolygonPoint>
            {
                new PolygonPoint(0,0),
                new PolygonPoint(0, Config.WorldSize.Y),
                new PolygonPoint(Config.WorldSize.X, Config.WorldSize.Y),
                new PolygonPoint(Config.WorldSize.X, 0),
            };

            IList<PolygonPoint> hole = new List<PolygonPoint>
            {
                new PolygonPoint(5, 5),
                new PolygonPoint(Config.WorldSize.X - 5, 5),
                new PolygonPoint(5, Config.WorldSize.Y - 5),
                new PolygonPoint(Config.WorldSize.X - 5, Config.WorldSize.Y - 5),
            };

            this.polygon = new Polygon(bound);

            //this.polygon.AddHole(new Polygon(hole));

            this.State = PolygonWrapperState.RecordFinished;
        }

        public PolygonWrapperState State { get; private set; }
        private Texture2D dotTexture { get; set; }
        private Polygon polygon { get; set; }
        private RenderTarget2D renderTarget2D { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (this.State)
            {
                case PolygonWrapperState.RecordStarted:
                case PolygonWrapperState.RecordFinished:
                case PolygonWrapperState.TesselationStarted:
                    {
                        VertexPositionColor[] a = this.polygon.Points.Select(p => new VertexPositionColor(new Vector3((float)p.X, (float)p.Y, 0), Color.Black)).ToArray();
                        spriteBatch.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineStrip, a, 0, a.Length - 1);
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
                        this.State = PolygonWrapperState.RecordFinished;
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
            this.dotTexture = TextureStorage.Get(TextureType.Default);
            this.renderTarget2D = new RenderTarget2D(graphicsDevice, Config.WorldSize.X, Config.WorldSize.Y);
        }

        public void Update(Position pos)
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
                        //this.polygon.Simplify();
                        goto case PolygonWrapperState.TesselationStarted;
                    }
                case PolygonWrapperState.TesselationStarted:
                    {
                        this.State = PolygonWrapperState.TesselationFinished;
                        P2T.Triangulate(TriangulationAlgorithm.DTSweep, this.polygon);
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

        private VertexPosition[] MapTrianglesDotsToVertex()
        {
            VertexPosition[] vertexes = new VertexPosition[this.polygon.Triangles.Count * 3];

            for (int trianglesCount = 0;
                    trianglesCount < this.polygon.Triangles.Count;
                    trianglesCount++)
            {
                for (int pointCount = 0;
                    pointCount < 3; // 3 is 3 points in triangle
                    pointCount++)
                {
                    TriangulationPoint point = this.polygon.Triangles[trianglesCount].Points[pointCount];
                    vertexes[trianglesCount * 3 + pointCount] = new VertexPosition(new Vector3((float)point.X, (float)point.Y, 0));
                }
            }

            return vertexes;
        }

        private void RenderPolygon(SpriteBatch spriteBatch)
        {
            VertexPosition[] vertexes = this.MapTrianglesDotsToVertex();

            spriteBatch.GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineStrip, vertexes, 0, vertexes.Length - 1);
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