using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SandS.Algorithm.Library.EnumsNamespace;
using SandS.Algorithm.Library.PositionNamespace;
using System;
using System.Collections.Generic;
using Poly2Tri;
using XonixGame.Configuration;
using XonixGame.ContentMemoryStorageNamespace;
using XonixGame.Enums;

namespace XonixGame.Entities
{
    public class Player : AbstractItem
    {
        public Player(int x, int y) : this(new PositionVector(x, y))
        {
        }

        public Player(PositionVector position)
        {
            this.Position = position;

            this.PlayerFlyweight = PlayerFlyweight.Instance;
            this.BodyWrapper = GetBody();

            this.ActualSpeed = new PositionVector();

            this.KeyboardInputHelper.InputKeyPressType = InputKeyPressType.OnDown;
            this.MovementType = MovementType.PressAndHold;
        }

        private PolygonWrapper GetBody()
        {
            IList<PolygonPoint> bodyPoints = new List<PolygonPoint>
            {
                new PolygonPoint(-Config.LeftUpperCorner.X, -Config.LeftUpperCorner.Y),
                new PolygonPoint(-(Config.LeftUpperCorner.X + Config.PlayerSize.X), -Config.LeftUpperCorner.Y),
                new PolygonPoint(-(Config.LeftUpperCorner.X + Config.PlayerSize.X), -(Config.LeftUpperCorner.Y + Config.PlayerSize.Y)),
                new PolygonPoint(-Config.LeftUpperCorner.X, -(Config.LeftUpperCorner.Y + Config.PlayerSize.Y)),
            };

            Polygon body = new Polygon(bodyPoints);

            return new PolygonWrapper(body, null);
        }

        public MovementType MovementType { get; set; }
        public PositionVector Position { get; private set; }
        private PositionVector ActualSpeed { get; set; }
        private BasicEffect BasicEffect { get; set; }
        private PolygonWrapper BodyWrapper { get;  }
        private PlayerFlyweight PlayerFlyweight { get; }
        private Matrix ProjectionMatrix { get; set; }
        private Matrix ViewMatrix { get; set; }
        private Matrix WorldMatrix { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            this.BodyWrapper.Draw(spriteBatch);
        }

        public void LoadContent(GraphicsDevice graphicsDevice)
        {
            this.LoadMatrixes(graphicsDevice);
            this.LoadEffects(graphicsDevice);
            this.BodyWrapper.LoadContent(graphicsDevice);
        }

        public override void Update()
        {
            this.ReadInput();
            this.SpeedCut();
            this.Move();

            this.BodyWrapper.Update(this.Position);

            base.Update();
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

        private void Move()
        {
            this.Position += this.ActualSpeed;
        }

        private void ReadInput()
        {
            foreach (var keyCommandPair in Config.KeyCommandBinding)
            {
                Keys key = keyCommandPair.Key;
                Commands command = keyCommandPair.Value;

                switch (this.MovementType)
                {
                    case MovementType.JustPress:
                        {
                            if (this.KeyboardInputHelper.WasKeyPressed(key))
                            {
                                this.ActualSpeed += this.PlayerFlyweight.CommandDirectionBinder[command];
                            }
                        }
                        break;

                    case MovementType.PressAndHold:
                        {
                            if (this.KeyboardInputHelper.IsKeyDown(key))
                            {
                                this.ActualSpeed += this.PlayerFlyweight.CommandDirectionBinder[command];
                            }

                            if (this.KeyboardInputHelper.WasKeyReleased(key))
                            {
                                this.ActualSpeed -= this.PlayerFlyweight.CommandDirectionBinder[command];
                            }
                        }
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void SpeedCut()
        {
            if (Math.Abs(this.ActualSpeed.X) > Config.MaxSpeedX)
            {
                this.ActualSpeed.X = Math.Sign(this.ActualSpeed.X) * Config.MaxSpeedX;
            }

            if ((Math.Abs(this.ActualSpeed.Y)) > Config.MaxSpeedY)
            {
                this.ActualSpeed.Y = Math.Sign(this.ActualSpeed.Y) * Config.MaxSpeedY;
            }
        }
    }
}