﻿using Microsoft.Xna.Framework;
using System;
using System.CodeDom;

namespace SoonRemoveStuff
{
    public class Position : IComparable, IComparable<Position>, ICloneable
    {
        #region Public Constructors

        public Position() : this(0, 0)
        {
        }

        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public Position(Point p) : this(p.X, p.Y)
        {
        }

        public Position(Vector2 v) : this((int)v.X, (int)v.Y)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public int X { get; set; }
        public int Y { get; set; }

        #endregion Public Properties

        #region Convert

        public Point ToPoint()
        {
            return new Point(this.X, this.Y);
        }

        public Vector2 ToVector2()
        {
            return new Vector2(this.X, this.Y);
        }

        #endregion Convert

        #region Clone

        public Position Clone()
        {
            return new Position(this.X, this.Y);
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }

        #endregion Clone

        #region Equals

        public static bool operator !=(Position lhs, Position rhs)
            => !(lhs == rhs);

        public static bool operator ==(Position lhs, Position rhs)
        {
            return lhs != null && lhs.CompareTo(rhs) == 0;
        }

        public int CompareTo(object obj)
        {
            Position pos = obj as Position;

            return this.CompareTo(pos);
        }

        public int CompareTo(Position pos)
        {
            if ((object)pos == null)
            {
                return -1;
            }

            return this.X.CompareTo(pos.X) + this.Y.CompareTo(pos.Y);
        }

        public override bool Equals(object obj)
        {
            Position pos = obj as Position;

            return this.Equals(pos);
        }

        public bool Equals(Position pos)
        {
            if ((object)pos == null)
            {
                return false;
            }

            return this.CompareTo(pos) == 0;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (this.X * 397) ^ this.Y;
            }
        }

        #endregion Equals

        public static Position operator +(Position lhs, Position rhs)
        {
            return new Position(lhs.X + rhs.X, lhs.Y + rhs.Y);
        }

        public static Position operator -(Position lhs, Position rhs)
        {
            return new Position(lhs.X - rhs.X, lhs.Y - rhs.Y);
        }

        public static bool operator > (Position lhs, Position rhs)
            => Math.Abs(lhs.X) > Math.Abs(rhs.X) &&
                    Math.Abs(lhs.Y) > Math.Abs(rhs.Y);

        public static bool operator <(Position lhs, Position rhs)
            => Math.Abs(lhs.X) < Math.Abs(rhs.X) &&
                    Math.Abs(lhs.Y) < Math.Abs(rhs.Y);

        public void SetZero()
        {
            this.X = 0;
            this.Y = 0;
        }
    }
}