using SandS.Algorithm.Library.EnumsNamespace;
using SandS.Algorithm.Library.PositionNamespace;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace XonixGame.Entities
{
    public class HeadFlyweight
    {
        #region Singleton

        protected HeadFlyweight()
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

        public static HeadFlyweight Instance => SingletonCreator<HeadFlyweight>.CreatorInstance;

        private sealed class SingletonCreator<S>
            where S : class
        {
            #region Public Properties

            public static S CreatorInstance { get; } = (S)typeof(S).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic,
                                                                                    null,
                                                                                    new Type[0],
                                                                                    new ParameterModifier[0]).Invoke(null);

            #endregion Public Properties
        }

        #endregion Singleton

        #region Public Properties

        public IDictionary<Commands, Position> CommandDirectionBinder { get; set; }

        #endregion Public Properties
    }
}