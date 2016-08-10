using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SandS.Algorithm.Library.MenuNamespace;
using SandS.Algorithm.Library.PositionNamespace;
using SoonRemoveStuff;

namespace XonixGame.Entities
{
    public class MenuWorld : World
    {
        public MenuWorld() : base()
        {
            this.Menu = new Menu<MenuNode<MenuNodeBody>, MenuNodeBody>(new Position(0, 0));

            this.SetMenuStructure();
        }

        private void SetMenuStructure()
        {
            MenuNode<MenuNodeBody> head = new MenuNode<MenuNodeBody>(new MenuNodeBody(MenuNodeType.Head,
                                                                    "HEAD",
                                                                    new Drawable(),
                                                                    this.Menu.Position,
                                                                    0));

            head.Body.ClickableItem.MouseClick += (s, e) => this.Menu.DrawingNode = head;

            MenuNode<MenuNodeBody> start = new MenuNode<MenuNodeBody>(new MenuNodeBody(MenuNodeType.Node,
                                                                    "Start",
                                                                    new Drawable(),
                                                                    this.Menu.Position,
                                                                    1));

            start.Body.ClickableItem.MouseClick += (s, e) => this.Menu.DrawingNode = start;

            MenuNode<MenuNodeBody> settings = new MenuNode<MenuNodeBody>(new MenuNodeBody(MenuNodeType.Node,
                                                                    "Settings",
                                                                    new Drawable(),
                                                                    this.Menu.Position,
                                                                    2));

            settings.Body.ClickableItem.MouseClick += (s, e) => this.Menu.DrawingNode = settings;

            MenuNode<MenuNodeBody> exit = new MenuNode<MenuNodeBody>(new MenuNodeBody(MenuNodeType.Node,
                                                                    "Exit",
                                                                    new Drawable(),
                                                                    this.Menu.Position,
                                                                    3));

            exit.Body.ClickableItem.MouseClick += (s, e) => this.Menu.DrawingNode = exit;

            MenuNode<MenuNodeBody> audio = new MenuNode<MenuNodeBody>(new MenuNodeBody(MenuNodeType.Node,
                                                                    "Audio",
                                                                    new Drawable(),
                                                                    this.Menu.Position,
                                                                    1));

            audio.Body.ClickableItem.MouseClick += (s, e) => this.Menu.DrawingNode = audio;

            MenuNode<MenuNodeBody> video = new MenuNode<MenuNodeBody>(new MenuNodeBody(MenuNodeType.Node,
                                                                    "Video",
                                                                    new Drawable(),
                                                                    this.Menu.Position,
                                                                    2));

            video.Body.ClickableItem.MouseClick += (s, e) => this.Menu.DrawingNode = video;

            MenuNode<MenuNodeBody> settingsBack = new MenuNode<MenuNodeBody>(new MenuNodeBody(MenuNodeType.Node,
                                                                    "Back",
                                                                    new Drawable(),
                                                                    this.Menu.Position,
                                                                    3));

            settingsBack.Body.ClickableItem.MouseClick += (s, e) => this.Menu.DrawingNode = head;

            MenuNode<MenuNodeBody> audioBack = new MenuNode<MenuNodeBody>(new MenuNodeBody(MenuNodeType.Node,
                                                                    "Back",
                                                                    new Drawable(),
                                                                    this.Menu.Position,
                                                                    3));

            audioBack.Body.ClickableItem.MouseClick += (s, e) => this.Menu.DrawingNode = settings;

            MenuNode<MenuNodeBody> videoBack = new MenuNode<MenuNodeBody>(new MenuNodeBody(MenuNodeType.Node,
                                                                    "Back",
                                                                    new Drawable(),
                                                                    this.Menu.Position,
                                                                    3));

            videoBack.Body.ClickableItem.MouseClick += (s, e) => this.Menu.DrawingNode = settings;

            this.Menu.Connect(head, start);
            this.Menu.Connect(head, settings);
            this.Menu.Connect(head, exit);
            this.Menu.Connect(settings, audio);
            this.Menu.Connect(settings, video);
            this.Menu.Connect(settings, settingsBack);
            this.Menu.Connect(audio, audioBack);
            this.Menu.Connect(video, videoBack);

            this.Menu.AddNode(head);
        }

        private Menu<MenuNode<MenuNodeBody>,MenuNodeBody> Menu { get; }

        public override Rectangle Rectangle { get; }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            this.Menu.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            this.Menu.Update(gameTime);
        }

        public override void LoadContent(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            this.Menu.LoadContent(contentManager, graphicsDevice);
        }

        public override void Initialize()
        {
            this.Menu.Initialize();
        }
    }
}