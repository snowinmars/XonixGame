using SoonRemoveStuff;

namespace XonixGame.Entities
{
    public class Player : AbstractItem/*, IMoveable, SoonRemoveStuff.IDrawable, IUpdatable*/
    {
        public Player(Head head)
        {
            this.Head = head;
        }

        public Head Head { get; set; }

        public Position Position { get; set; }
    }
}