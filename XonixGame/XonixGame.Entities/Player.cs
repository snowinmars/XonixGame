﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SandS.Algorithm.Library.EnumsNamespace;
using SandS.Algorithm.Library.PositionNamespace;
using System;
using XonixGame.Configuration;
using XonixGame.ContentMemoryStorageNamespace;
using XonixGame.Enums;

namespace XonixGame.Entities
{
    public class Player : AbstractItem
    {
        public Player(int x, int y) : this(new Position(x, y))
        {
        }

        public Player(Position position)
        {
            this.Position = position;

            this.PlayerFlyweight = PlayerFlyweight.Instance;

            this.ActualSpeed = new Position();

            this.KeyboardInputHelper.InputKeyPressType = InputKeyPressType.OnDown;
            this.MovementType = MovementType.PressAndHold;
        }

        public MovementType MovementType { get; set; }

        public Position Position { get; private set; }

        private Position ActualSpeed { get; set; }

        private PlayerFlyweight PlayerFlyweight { get; }

        private Texture2D Texture { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.Position.ToVector2(), Color.Red);
        }

        public void LoadContent()
        {
            this.Texture = TextureStorage.Get(TextureType.Player);
        }

        public override void Update()
        {
            this.ReadInput();
            this.SpeedCut();
            this.Move();

            base.Update();
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