using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Algorithms.Library;
using SoonRemoveStuff;
using Scope = SoonRemoveStuff.Scope;

namespace XonixGame.Entities
{
    public class HeadFlyweight
    {
        protected HeadFlyweight()
        {
            this.CommandsActionBinder = new Dictionary<Commands, Action<Head>>
            {
                {Commands.MoveUp, (head) =>
                {
                    head.Position.Y -= head.Speed.Y;
                }},
                {Commands.MoveDown, (head) =>
                {
                    head.Position.Y += head.Speed.Y;
                }},
                {Commands.MoveLeft, (head) =>
                {
                    head.Position.X -= head.Speed.X;
                }},
                {Commands.MoveRight, (head) =>
                {
                    head.Position.X += head.Speed.X;
                }},
                {Commands.Wait, (head) => { }},
            };
        }

        public static HeadFlyweight Instance => SingletonCreator<HeadFlyweight>.CreatorInstance;

        public IDictionary<Commands, Action<Head>> CommandsActionBinder { get; }

        private sealed class SingletonCreator<S>
            where S : class
        {
            public static S CreatorInstance { get; } = (S)typeof(S).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic,
                                                                                    null,
                                                                                    new Type[0],
                                                                                    new ParameterModifier[0]).Invoke(null);
        }
    }
}
