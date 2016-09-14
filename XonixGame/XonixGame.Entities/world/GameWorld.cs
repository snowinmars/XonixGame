using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SandS.Algorithm.Library.PositionNamespace;
using System;
using System.Collections.Generic;
using SandS.Algorithm.Extensions.IComparableExtensionNamespace;
using XonixGame.Configuration;
using XonixGame.ContentMemoryStorageNamespace;

namespace XonixGame.Entities
{
    public class GameWorld : World
    {
        public GameWorld(Player player, Position size) : base()
        {
            this.Player = player;
            this.PlayerPositions = new List<Position>(128);
            this.PreviousPosition = this.Player.Position;

            this.Rectangle = new Rectangle(Point.Zero, size.ToPoint());
        }

        public override Rectangle Rectangle { get; }
       
        private IList<Position> PlayerPositions { get; }

        public Player Player { get; private set; }

        public Texture2D Texture { get; private set; }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, Vector2.Zero);
            this.Player.Draw(spriteBatch);

            foreach (var playerPosition in this.PlayerPositions)
            {
                spriteBatch.Draw(TextureStorage.Instance.Get(TextureType.Default), playerPosition.ToVector2());
            }
        }

        private Position PreviousPosition { get; set; }

        public override void Update(GameTime gameTime)
        {
            this.Player.Update(gameTime);

            bool isborderCollise = this.CheckBorderCollise();

            if (isborderCollise)
            {
                this.Player.Position.X = this.Player.Position.X.CantBeLess(0);
                this.Player.Position.X = this.Player.Position.X.CantBeMore(Config.WorldSize.X - Config.PlayerSize.X);
                this.Player.Position.Y = this.Player.Position.Y.CantBeLess(0);
                this.Player.Position.Y = this.Player.Position.Y.CantBeMore(Config.WorldSize.Y - Config.PlayerSize.Y);
            }

            this.HandlePosition();
        }

        
        private void HandlePosition()
        {
            if (this.PreviousPosition - this.Player.Position > Config.PositionEpsilon)
            {
                this.PlayerPositions.Add(this.Player.Position);
                this.PreviousPosition = this.Player.Position;
            }
        }

        private bool CheckBorderCollise()
        {
            bool contains = this.Rectangle.Top < this.Player.Rectangle.Top &&
                            this.Rectangle.Right > this.Player.Rectangle.Right &&
                            this.Rectangle.Bottom > this.Player.Rectangle.Bottom &&
                            this.Rectangle.Left < this.Player.Rectangle.Left;

            return !contains;
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