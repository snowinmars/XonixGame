using SandS.Algorithm.Library.PositionNamespace;

namespace XonixGame.Constants
{
    public static class Const
    {
        static Const()
        {
            Const.MaxMovableObjectSpeed = new Position(1, 1);
            Const.MinMovableObjectSpeed = new Position(-1, -1);

            Const.SpeedStep = new Position(1, 1);

            Const.TexturePlayerName = "Player";
        }

        public static string TexturePlayerName { get; }
        public static Position MaxMovableObjectSpeed { get; }
        public static Position SpeedStep { get; }
        public static Position MinMovableObjectSpeed { get; }
    }
}