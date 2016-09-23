using SandS.Algorithm.Library.EnumsNamespace;
using SandS.Algorithm.Library.PositionNamespace;
using System;
using System.Collections.Generic;
using System.Reflection;

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

        public static PlayerFlyweight Instance => SingletonCreator<PlayerFlyweight>.CreatorInstance;

        private sealed class SingletonCreator<S>
            where S : class
        {


            public static S CreatorInstance { get; } = (S)typeof(S).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic,
                                                                                    null,
                                                                                    new Type[0],
                                                                                    new ParameterModifier[0]).Invoke(null);


        }
        #endregion Singleton





        public IDictionary<Commands, Position> CommandDirectionBinder { get; set; }


    }
}