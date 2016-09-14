using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SandS.Algorithm.Library.PositionNamespace;
using System;
using System.Collections.Generic;
using XonixGame.Configuration;
using XonixGame.ContentMemoryStorageNamespace;

namespace XonixGame.Entities
{
    public class GameWorld : World
    {
        public GameWorld(Player player, Position size) : base()
        {
            this.Player = player;
            this.playerPositions = new List<Position>(128);
            this.PreviousPosition = this.Player.Position;

            this.Rectangle = new Rectangle(Point.Zero, size.ToPoint());
        }

        public override Rectangle Rectangle { get; }
       
        private IList<Position> playerPositions { get; }

        public Player Player { get; private set; }

        public Texture2D Texture { get; private set; }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, Vector2.Zero);
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

            this.HandlePosition();
        }

        private void HandlePosition()
        {
            if (this.PreviousPosition - this.Player.Position > Config.PositionEpsilon)
            {
                this.playerPositions.Add(this.Player.Position);
                this.PreviousPosition = this.Player.Position;
            }
        }

        private bool CheckIsBorderCollise()
        {
            return !this.Rectangle.Contains(this.Player.Rectangle);
        }

        public override void LoadContent(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            this.Texture = TextureStorage.Instance.Get(TextureType.World);
            this.Player.Texture = TextureStorage.Instance.Get(TextureType.Player);
        }

        public override void Initialize()
        {
        }
    }
}