using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SoonRemoveStuff;
using System;
using SandS.Algorithm.Library.EnumsNamespace;
using SandS.Algorithm.Library.PositionNamespace;
using XonixGame.Configuration;
using Color = Microsoft.Xna.Framework.Color;
using Commands = SoonRemoveStuff.Commands;

namespace XonixGame.Entities
{
    public enum MovementType
    {
        JustPress = 0,
        PressAndHold = 1,
    }

    public class Head : AbstractItem, SoonRemoveStuff.IDrawable, IUpdatable
    {
        #region Public Constructors

        public Head(int x, int y) : this(new Position(x, y))
        {
        }

        public Head(Position position)
        {
            this.Position = position;

            this.HeadFlyweight = HeadFlyweight.Instance;

            this.ActualSpeed = new Position();

            this.KeyboardInputHelper.InputKeyPressType = InputKeyPressType.OnDown;
            this.MovementType = MovementType.PressAndHold;
        }

        #endregion Public Constructors

        #region Public Properties

        public MovementType MovementType { get; set; }
        public Texture2D Texture { get; set; }
        private Position ActualSpeed { get; set; }
        private HeadFlyweight HeadFlyweight { get; }
        public Position Position { get; set; }
        public Rectangle Rectangle => new Rectangle(this.Position.X, this.Position.Y, 10, 10);

        #endregion Public Properties

        #region Public Methods

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.Position.ToVector2(), Color.Red);
        }

        public override void Update(GameTime gameTime)
        {
            this.ReadInput();
            this.SpeedCut();
            this.Move();

            base.Update(gameTime);
        }

        #endregion Public Methods

        #region Private Methods

        private void Move()
        {
            this.Position += this.ActualSpeed;
        }

        private void ReadInput()
        {
            bool wasKeyPressed = false;

            foreach (var keyCommandPair in Config.KeyCommandBinding)
            {
                Keys key = keyCommandPair.Key;
                Commands command = keyCommandPair.Value;

                switch (this.MovementType)
                {
                    case MovementType.JustPress:
                        if (this.KeyboardInputHelper.WasKeyPressed(key))
                        {
                            this.ActualSpeed += this.HeadFlyweight.CommandDirectionBinder[command];
                        }
                        break;
                    case MovementType.PressAndHold:
                        if (this.KeyboardInputHelper.WasKeyPressed(key))
                        {
                            this.ActualSpeed += this.HeadFlyweight.CommandDirectionBinder[command];
                            wasKeyPressed = true;
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            if (!wasKeyPressed)
            {
                this.ActualSpeed.SetZero();
            }
        }

        private void SpeedCut()
        {
            if (Math.Abs(this.ActualSpeed.X) > Config.MaxSpeedX)
            {
                this.ActualSpeed.X = Math.Sign(this.ActualSpeed.X)*Config.MaxSpeedX;
            }

            if ((Math.Abs(this.ActualSpeed.Y)) > Config.MaxSpeedY)
            {
                this.ActualSpeed.Y = Math.Sign(this.ActualSpeed.Y)*Config.MaxSpeedY;
            }
        }

        #endregion Private Methods
    }
}