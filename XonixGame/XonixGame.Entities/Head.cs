using System;
using Algorithms.Library;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SoonRemoveStuff;
using XonixGame.Configuration;
using Color = Microsoft.Xna.Framework.Color;

namespace XonixGame.Entities
{
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
        }

        #endregion Public Constructors

        #region Public Properties

        public Texture2D Texture { get; set; }
        private Position ActualSpeed { get; set; }
        private HeadFlyweight HeadFlyweight { get; }
        private Position Position { get; set; }

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
            foreach (var keyCommandPair in Config.KeyCommandBinding)
            {
                Keys key = keyCommandPair.Key;
                Commands command = keyCommandPair.Value;

                if (this.KeyboardInputHelper.WasKeyPressed(key))
                {
                    this.ActualSpeed += this.HeadFlyweight.CommandDirectionBinder[command];
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

        #endregion Private Methods
    }
}