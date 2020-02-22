﻿using System.Drawing;

namespace Project
{
    class VisualObject
    {
        protected Point _Position;
        protected Point _Direction;
        protected Size _Size;

        public VisualObject(Point Position, Point Direction, Size Size)
        {
            _Position = Position;
            _Direction = Direction;
            _Size = Size;
        }

        public virtual void Draw(Graphics g)
        {

            using (Image image = Image.FromFile("meteor.png"))
            {
                g.DrawImage(image, _Position.X, _Position.Y, _Size.Width, _Size.Width);
            }

            //g.DrawEllipse(Pens.White,
            //    _Position.X, _Position.Y,
            //    _Size.Width, _Size.Width);
        }

        public virtual void Update()
        {
            _Position = new Point(
                _Position.X + _Direction.X,
                _Position.Y + _Direction.Y);

            if (_Position.X < 0)
                _Direction = new Point(-_Direction.X, _Direction.Y);
            if (_Position.Y < 0)
                _Direction = new Point(_Direction.X, -_Direction.Y);

            if (_Position.X > Game.Width)
                _Direction = new Point(-_Direction.X, _Direction.Y);
            if (_Position.Y > Game.Height)
                _Direction = new Point(_Direction.X, -_Direction.Y);
        }
    }
}
