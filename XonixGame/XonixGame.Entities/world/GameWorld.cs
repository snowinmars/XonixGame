﻿using Microsoft.Xna.Framework;
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
        public GameWorld(Player player) : base()
        {
            this.Player = player;
            this.playerPositions = new List<Position>(128);
            this.PreviousPosition = this.Player.Position;
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

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
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
                //if (this.PreviousPosition - this.Player.Position > Config.PositionEpsilon)
                Position p = new Position();
                p.X = this.PreviousPosition.X - this.Player.Position.X;
                p.Y = this.PreviousPosition.Y - this.Player.Position.Y;

                if (p.X > Config.PositionEpsilon.X ||
                    p.Y > Config.PositionEpsilon.Y)
                {
                    this.playerPositions.Add(this.Player.Position);
                    this.PreviousPosition = this.Player.Position;
                }
            }
        }

        private bool CheckIsBorderCollise()
        {
            return !this.Rectangle.Contains(this.Player.Rectangle);
        }

        public override void LoadContent(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            this.Player.Texture = TextureStorage.Instance.Get(TextureType.Default);
        }

        public override void Initialize()
        {
        }
    }
}