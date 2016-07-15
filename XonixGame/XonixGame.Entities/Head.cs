using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SoonRemoveStuff;
using System.Collections.Generic;
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
            this.Speed = Config.DefaultHeadSpeed;

            StateSet verticalMovementStrategy = Config.VerticalMovementStrategy;
            StateSet horizontalMovementStrategy = Config.HorizontalMovementStrategy;
            StateSet commonStrategy = Config.HeadCommonStrategy;

            this.Strategy = new Strategy(new List<StateSet>
            {
                verticalMovementStrategy,
                horizontalMovementStrategy,
                commonStrategy,
            });

            this.HeadFlyweight = HeadFlyweight.Instance;
        }

        #endregion Public Constructors

        #region Public Properties

        public HeadFlyweight HeadFlyweight { get; }
        public Position Position { get; set; }
        public Position Speed { get; set; }
        public Texture2D Texture { get; set; }
        public Strategy Strategy { get; }

        #endregion Public Properties

        #region Public Methods

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.Position.ToVector2(), Color.Red);
        }

        public void Move()
        {
            foreach (var state in this.Strategy.GetStates())
            {
                this.HeadFlyweight.CommandsActionBinder[state].Invoke(this);
            }
        }

        public void ReadComands()
        {
            foreach (var keyCommandPair in Config.KeyCommandBinding)
            {
                Keys key = keyCommandPair.Key;
                Commands command = keyCommandPair.Value;

                if (this.KeyboardInputHelper.WasKeyPressed(key))
                {
                    StateSet stratagy = this.Strategy.GetStrategyByCommand(command);

                    stratagy.ApplyCommand(command);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            this.ReadComands();
            this.Move();

            base.Update(gameTime);
        }

        #endregion Public Methods
    }
}