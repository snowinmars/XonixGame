using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Poly2Tri;
using System.Collections.Generic;
using XonixGame.Configuration;

namespace XonixGame.Entities
{
    public class GameWorld : World
    {


        public GameWorld(Player player) : base()
        {
            this.Player = player;

            this.BoundaryPolygon = this.InitBoundaries();
            this.FieldPolygon = this.InitField();
        }

        private PolygonWrapper InitField()
        {
            int x = Config.LeftUpperCorner.X;
            int y = Config.LeftUpperCorner.Y;

            IList<PolygonPoint> field = new List<PolygonPoint>
            {
                new PolygonPoint(-x, -y),
                new PolygonPoint(-x, y),
                new PolygonPoint(x,y),
                new PolygonPoint(x,-y),
            };

            Polygon fieldPolygon = new Polygon(field);

            return new PolygonWrapper(fieldPolygon, null);
        }

        private PolygonWrapper InitBoundaries()
        {
            int x = Config.LeftUpperCorner.X;
            int y = Config.LeftUpperCorner.Y;

            IList<PolygonPoint> boundary = new List<PolygonPoint>
            {
                new PolygonPoint(-x, -y),
                new PolygonPoint(-x, y),
                new PolygonPoint(x,y),
                new PolygonPoint(x,-y),
            };

            const double offset = 0.1;

            IList<PolygonPoint> boundaryHole = new List<PolygonPoint>
            {
                new PolygonPoint(-x + offset, -y + offset),
                new PolygonPoint(-x + offset, y - offset),
                new PolygonPoint(x - offset, y - offset),
                new PolygonPoint(x - offset, -y + offset),
            };

            Polygon boundaryPolygon = new Polygon(boundary);
            Polygon boundaryHolePolygon = new Polygon(boundaryHole);

            return new PolygonWrapper(boundaryPolygon, new[] { boundaryHolePolygon });
        }

        private PolygonWrapper BoundaryPolygon { get; set; }
        private PolygonWrapper FieldPolygon { get; set; }

        public Player Player { get; private set; }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
                this.FieldPolygon.Draw(spriteBatch);
                this.BoundaryPolygon.Draw(spriteBatch);
                this.Player.Draw(spriteBatch);
        }

        public override void Update()
        {
            this.Player.Update();
            this.FieldPolygon.Update(this.Player.Position);
            this.BoundaryPolygon.Update(this.Player.Position);
        }

        

        public override void LoadContent(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            this.Player.LoadContent(graphicsDevice);
            this.FieldPolygon.LoadContent(graphicsDevice);
            this.BoundaryPolygon.LoadContent(graphicsDevice);

            graphicsDevice.RasterizerState = RasterizerState.CullNone;
        }

        public override void Initialize()
        {
        }
    }
}