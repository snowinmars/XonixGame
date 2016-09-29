using SandS.Algorithm.Library.EnumsNamespace;
using SandS.Algorithm.Library.PositionNamespace;
using System.Collections.Generic;
using XonixGame.Configuration;

namespace XonixGame.Entities
{
    public class PlayerFlyweight
    {
        #region Singleton

        protected PlayerFlyweight()
        {
            Position wait = new Position(0, 0);
            Position up = new Position(0, -Config.DefaultHeadSpeed.Y);
            Position down = new Position(0, Config.DefaultHeadSpeed.Y);
            Position left = new Position(-Config.DefaultHeadSpeed.X, 0);
            Position right = new Position(Config.DefaultHeadSpeed.X, 0);

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