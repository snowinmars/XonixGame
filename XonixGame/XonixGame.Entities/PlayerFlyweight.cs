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
            PositionVector wait = new PositionVector(0, 0);
            PositionVector up = new PositionVector(0, -Config.DefaultHeadSpeed.Y);
            PositionVector down = new PositionVector(0, Config.DefaultHeadSpeed.Y);
            PositionVector left = new PositionVector(-Config.DefaultHeadSpeed.X, 0);
            PositionVector right = new PositionVector(Config.DefaultHeadSpeed.X, 0);

            this.CommandDirectionBinder = new Dictionary<Commands, PositionVector>
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

        public IDictionary<Commands, PositionVector> CommandDirectionBinder { get; private set; }
    }
}