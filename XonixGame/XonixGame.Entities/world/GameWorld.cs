using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SandS.Algorithm.Library.PositionNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using SandS.Algorithm.CommonNamespace;
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
                this.CutPlayerPosition();
                this.FinalizePlayerLine();
            }

            this.HandlePosition();
        }

        private void FinalizePlayerLine()
        {
            int insideArea = 0;
            int outsideArea = 0;

            for (int i = 0; i < Config.AreaAccuracyCalculation; i++)
            {
                Position randomDot = GetRandomDot();

                if (IsOutside(randomDot))
                {
                    ++outsideArea;
                }
                else
                {
                    ++insideArea;
                }
            }
        }

        private bool IsOutside(Position randomDot)
        {
            return GetintersectionCount(randomDot) % 2 == 0;
        }

        private Position GetRandomDot()
        {
            int x = CommonValues.Random.Next(this.Rectangle.Left, this.Rectangle.Right);
            int y = CommonValues.Random.Next(this.Rectangle.Top, this.Rectangle.Bottom);
            return new Position(x, y);
        }

        private int GetintersectionCount(Position dot)
        {
            int count = 0;

            for (int i = 1, j = 0; i < this.PlayerPositions.Count; i++, j++)
            {
                bool isDotBetweenYPositions = (this.PlayerPositions[i].Y <= dot.Y && dot.Y < this.PlayerPositions[j].Y) ||
                                                (this.PlayerPositions[j].Y <= dot.Y && dot.Y < this.PlayerPositions[i].Y);

                if (!isDotBetweenYPositions)
                {
                    continue;
                }

                Position deltaPosition = new Position((this.PlayerPositions[j].X - this.PlayerPositions[i].X),
                                                        this.PlayerPositions[j].Y - this.PlayerPositions[i].Y);

                int interpolationCoefficient = (dot.Y - this.PlayerPositions[i].Y) / deltaPosition.Y;

                int interpolatedX = deltaPosition.X * interpolationCoefficient + this.PlayerPositions[i].X;

                if (isDotBetweenYPositions &&
                    (dot.X > interpolatedX))
                {
                    ++count;
                }
            }

            return count;
        }

        private void CutPlayerPosition()
        {
            this.Player.Position.X = this.Player.Position.X.CantBeLess(0);
            this.Player.Position.X = this.Player.Position.X.CantBeMore(Config.WorldSize.X - Config.PlayerSize.X);
            this.Player.Position.Y = this.Player.Position.Y.CantBeLess(0);
            this.Player.Position.Y = this.Player.Position.Y.CantBeMore(Config.WorldSize.Y - Config.PlayerSize.Y);
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