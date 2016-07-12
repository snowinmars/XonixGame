using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SoonRemoveStuff;
using XonixGame.Configuration;
using IDrawable = SoonRemoveStuff.IDrawable;

namespace XonixGame.Entities
{
    public class Head : IMoveable, IDrawable, IUpdatable
    {
        public Direction Direction { get; set; }

        public Position Position { get; set; }

        private IDictionary<Keys, Command> KeyFuncBinding { get; }

        private Position Speed { get; }

        public Head(Position speed, Position position)
        {
            this.Speed = speed;
            this.Position = position;

            this.KeyFuncBinding = new Dictionary<Keys, Command>
            {
                { Keys.Up, new Command(() => this.Direction = Direction.Up)},
                { Keys.Down, new Command(() => this.Direction = Direction.Down)},
                { Keys.Left, new Command(() => this.Direction = Direction.Left)},
                { Keys.Right, new Command(() => this.Direction = Direction.Right)},

            };
        }

        public void ReadDirection(KeyboardInputHelper keyboardInputHelper)
        {
            foreach (var allowedKey in Config.AllowedKeys)
            {
                if (keyboardInputHelper.WasKeyPressed(allowedKey) &&
                    this.KeyFuncBinding.ContainsKey(allowedKey))
                {
                    this.KeyFuncBinding[allowedKey].Exec();
                }
            }
        }

        public void Draw(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
            switch (this.Direction)
            {
                case Direction.Up:
                    this.Position.Y -= this.Speed.Y;
                    break;
                case Direction.Right:
                    this.Position.X += this.Speed.X;
                    break;
                case Direction.Down:
                    this.Position.Y += this.Speed.Y;
                    break;
                case Direction.Left:
                    this.Position.X -= this.Speed.X;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
