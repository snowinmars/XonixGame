using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SoonRemoveStuff;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Algorithms.Library;
using XonixGame.Configuration;
using Color = Microsoft.Xna.Framework.Color;
using Strategy = SoonRemoveStuff.Strategy;

namespace XonixGame.Entities
{
    public class Head : AbstractItem, IMoveable, SoonRemoveStuff.IDrawable, IUpdatable
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

        public HeadFlyweight HeadFlyweight { get; }
        public Position Position { get; set; }
        public Texture2D Texture { get; set; }
        public Position ActualSpeed { get; set; }

        #endregion Public Properties

        #region Public Methods

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.Position.ToVector2(), Color.Red);
        }

        public void Move()
        {
            this.Position.X += this.ActualSpeed.X;
            this.Position.Y += this.ActualSpeed.Y;
        }

        public void ReadInput()
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

            if (this.ActualSpeed.X > Config.MaxSpeedX)
            {
                this.ActualSpeed.X = Config.MaxSpeedX;
            }

            if (this.ActualSpeed.Y > Config.MaxSpeedY)
            {
                this.ActualSpeed.Y = Config.MaxSpeedY;
            }
        }

        public override void Update(GameTime gameTime)
        {
            this.ReadInput();
            this.Move();

            base.Update(gameTime);
        }

        #endregion Public Methods
    }
}