using System;
using System.Collections.Generic;
using System.Reflection;
using Algorithms.Library;
using Microsoft.Xna.Framework;
using SoonRemoveStuff;

namespace XonixGame.Entities
{
    public class HeadFlyweight
    {
        #region Singleton

        protected HeadFlyweight()
        {
            var wait = new Position(0,0);
            var up = new Position(0, -1);
            var down = new Position(0, 1);
            var left = new Position(-1, 0);
            var right = new Position(1, 0);


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
            public static S CreatorInstance { get; } = (S)typeof(S).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic,
                                                                                    null,
                                                                                    new Type[0],
                                                                                    new ParameterModifier[0]).Invoke(null);
        }

        #endregion Singleton

        public IDictionary<Commands, Position> CommandDirectionBinder { get; set; }
    }
}