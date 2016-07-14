using Algorithms.Library;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SoonRemoveStuff;
using System.Collections.Generic;
using XonixGame.Configuration;
using Color = Microsoft.Xna.Framework.Color;

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

            Strategy verticalMovementStrategy = Config.VerticalMovementStrategy;
            Strategy horizontalMovementStrategy = Config.HorizontalMovementStrategy;
            Strategy commonStrategy = Config.HeadCommonStrategy;

            this.Strategies = new StrategySet(new List<Strategy>
            {
                verticalMovementStrategy,
                horizontalMovementStrategy,
                commonStrategy,
            });

            this.CommandsActionBinder = new Dictionary<Commands, Command>
            {
                {Commands.MoveUp, new Command(() => this.Position.Y -= this.Speed.Y)},
                {Commands.MoveDown, new Command(() => this.Position.Y += this.Speed.Y)},
                {Commands.MoveLeft, new Command(() => this.Position.X -= this.Speed.X)},
                {Commands.MoveRight, new Command(() => this.Position.X += this.Speed.X)},
                {Commands.Wait, new Command(() => { })},
            };
        }

        #endregion Public Constructors

        #region Public Properties

        public IDictionary<Commands, Command> CommandsActionBinder { get; }
        public Position Position { get; set; }
        public Position Speed { get; set; }
        public Texture2D Texture { get; set; }
        public StrategySet Strategies { get; }

        #endregion Public Properties

        #region Public Methods

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.Position.ToVector2(), Color.Red);
        }

        public void Move()
        {
            foreach (var state in this.Strategies.States)
            {
                this.CommandsActionBinder[state].Exec();
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
                    Strategy stratagy = this.Strategies.GetStrategyByCommand(command);

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