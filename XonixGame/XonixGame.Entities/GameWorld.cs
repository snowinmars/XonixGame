using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SoonRemoveStuff;
using System.Collections.Generic;
using XonixGame.Configuration;

namespace XonixGame.Entities
{
    public class GameWorld : World, SoonRemoveStuff.IDrawable, IUpdatable
    {
        public GameWorld(Player player)
        {
            this.Player = player;
            this.playerPositions = new List<Position>(128);
            this.PreviousPosition = this.Player.Head.Position;
        }

        public override Rectangle Rectangle
        {
            get
            {
                return new Rectangle(0, 0, 200, 200);
            }
        }

        private IList<Position> playerPositions { get; }

        public Player Player { get; private set; }

        public override void Draw(SpriteBatch spriteBatch)
        {
            this.Player.Draw(spriteBatch);
        }

        private Position PreviousPosition { get; set; }

        public override void Update(GameTime gameTime)
        {
            this.Player.Update(gameTime);

            bool isborderCollise = this.CheckIsBorderCollise();

            if (isborderCollise)
            {
                throw new ArgumentOutOfRangeException();
            }
            else
            {
                if (this.PreviousPosition - this.Player.Head.Position > Config.PositionEpsilon)
                {
                    this.playerPositions.Add(this.Player.Head.Position);
                    this.PreviousPosition = this.Player.Head.Position;
                }
            }
        }

        private bool CheckIsBorderCollise()
        {
            return !this.Rectangle.Contains(this.Player.Head.Rectangle);
        }
    }
}