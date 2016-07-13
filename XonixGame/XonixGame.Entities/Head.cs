using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SoonRemoveStuff;
using System.Collections.Generic;
using XonixGame.Configuration;

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
            this.ComandActionBinder = new Dictionary<Comands, Comand>
            {
                {Comands.MoveUp, new Comand(() => this.Position.Y -= this.Speed.Y)},
                {Comands.MoveDown, new Comand(() => this.Position.Y += this.Speed.Y)},
                {Comands.MoveLeft, new Comand(() => this.Position.X -= this.Speed.X)},
                {Comands.MoveRight, new Comand(() => this.Position.X += this.Speed.X)},
                {Comands.Wait, new Comand(() => { })},
            };
        }

        #endregion Public Constructors

        #region Public Properties

        public IDictionary<Comands, Comand> ComandActionBinder { get; }
        public Comands Comands { get; set; }
        public Position Position { get; set; }
        public Position Speed { get; set; }
        public Texture2D Texture { get; set; }

        #endregion Public Properties

        #region Public Methods

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.Position.ToVector2(), Color.Red);
        }

        public void Move()
        {
            foreach (var comandActionPair in this.ComandActionBinder)
            {
                if (this.Comands.HasFlag(comandActionPair.Key))
                {
                    comandActionPair.Value.Exec();
                }
            }
        }

        public void ReadComands()
        {
            foreach (var keyComandPair in Config.KeyComandBinding)
            {
                if (this.KeyboardInputHelper.WasKeyPressed(keyComandPair.Key))
                {
                    this.Comands |= keyComandPair.Value;
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