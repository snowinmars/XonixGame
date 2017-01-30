using Microsoft.Xna.Framework;
using SandS.Algorithm.Library.PositionNamespace;
using XonixGame.Constants;

namespace XonixGame.ContentStorage
{
    public abstract class MovableObject
    {
        public Position Position { get; private set; }
        public Position Speed { get; protected internal set; }

        public MovableObject()
        {
            this.Position = new Position();
            this.Speed = new Position();
        }

        protected internal void Move(GameTime gameTime)
        {
            this.CutSpeed();

            this.Position += this.Speed;
        }

        private void CutSpeed()
        {
            if (this.Speed > Const.MaxMovableObjectSpeed)
            {
                this.Speed = Const.MaxMovableObjectSpeed;
            }

            if (this.Speed.X < Const.MinMovableObjectSpeed.X)
            {
                this.Speed.X = Const.MinMovableObjectSpeed.X;
            }

            if (this.Speed.Y < Const.MinMovableObjectSpeed.Y)
            {
                this.Speed.Y = Const.MinMovableObjectSpeed.Y;
            }
        }
    }
}