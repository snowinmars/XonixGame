using SandS.Algorithm.Library.EnumsNamespace;
using SandS.Algorithm.Library.PositionNamespace;
using System.Collections.Generic;

namespace XonixGame.Entities
{
    public class PlayerFlyweight
    {
        #region Singleton

        protected PlayerFlyweight()
        {
            Position wait = new Position(0, 0);
            Position up = new Position(0, -1);
            Position down = new Position(0, 1);
            Position left = new Position(-1, 0);
            Position right = new Position(1, 0);

            this.CommandDirectionBinder = new Dictionary<Commands, Position>
            {
                {Commands.Wait, wait},
                {Commands.MoveDown, down },
                {Commands.MoveUp, up },
                {Commands.MoveLeft, left },
                {Commands.MoveRight, right},
            };
        }

        public static PlayerFlyweight Instance { get; private set; } = new PlayerFlyweight();

        #endregion Singleton

        public IDictionary<Commands, Position> CommandDirectionBinder { get; private set; }
    }
}