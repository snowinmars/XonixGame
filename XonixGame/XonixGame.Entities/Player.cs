using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SandS.Algorithm.Library.EnumsNamespace;
using SandS.Algorithm.Library.OtherNamespace;
using System;
using System.Collections.Generic;
using XonixGame.Constants;
using XonixGame.ContentStorage;

namespace XonixGame.Entities
{
    public class Player : MovableObject
    {
        private readonly KeyboardInputHelper keyboardInputHelper;

        private string TextureName { get; set; }
            public Matrix WorldMatrix { get; set; }
        
        public Player()
        {
            this.keyboardInputHelper = new KeyboardInputHelper();

            this.Position.SetZero();
            this.Speed.SetZero();

            this.commands = new List<Commands>(256);

            this.TextureName = Const.TexturePlayerName;

            WorldMatrix = Matrix.CreateWorld(new Vector3(0f, 0f, 0f),
                                                new Vector3(0, 0, -1),
                                                Vector3.Up);
        }

        public void Update(GameTime gameTime)
        {
            this.keyboardInputHelper.Update(gameTime);

            this.HandleInput();

            this.Move(gameTime);
        }

        private void HandleInput()
        {
            IEnumerable<Commands> commands = this.ReadCommands();
            foreach (Commands command in commands)
            {
                this.Apply(command);
            }
        }

        private void Apply(Commands command)
        {
            switch (command)
            {
                case Commands.Wait:
                    this.Speed.SetZero();
                    break;

                case Commands.MoveUp:
                    this.WorldMatrix *= Matrix.CreateTranslation(0, -Const.SpeedStep.Y, 0);
                    break;

                case Commands.MoveDown:
                    this.WorldMatrix *= Matrix.CreateTranslation(0, Const.SpeedStep.Y, 0);
                    break;

                case Commands.MoveLeft:
                    this.WorldMatrix *= Matrix.CreateTranslation(-Const.SpeedStep.X, 0, 0);
                    break;

                case Commands.MoveRight:
                    this.WorldMatrix *= Matrix.CreateTranslation(Const.SpeedStep.X,0, 0);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(command), command, null);
            }
        }

        private IList<Commands> commands { get; }

        private IEnumerable<Commands> ReadCommands()
        {
            this.commands.Clear();

            Commands command;

            foreach (var key in Storage.AllKeyboardKeys)
            {
                if (this.keyboardInputHelper.IsKeyDown(key) &&
                    Storage.KeysCommandBinding.TryGetValue(key, out command))
                {
                    this.commands.Add(command);
                }
            }

            return this.commands;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Storage.Textures[this.TextureName], this.Position.ToVector2());
        }
    }
}