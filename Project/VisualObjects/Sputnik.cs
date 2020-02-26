using System;
using System.Drawing;

namespace Project
{
    class Sputnik : VisualObject
    {
        private int _Angle;
        public Sputnik(Point Position, Point Direction, int Size, int Angle)
            : base(Position, Direction, new Size(Size, Size / 3 * 2))
        {
            _Angle = Angle;
        }

        public override void Draw(Graphics g)
        {
            g.TranslateTransform(_Position.X,_Position.Y);
            g.RotateTransform(_Angle);
            g.FillRectangle(Brushes.White, 0, 0, _Size.Width, _Size.Height);
            g.FillRectangle(Brushes.White, 0 + _Size.Width / 3, 0 + 15, _Size.Width / 3 * 2, _Size.Height / 3 * 2);
            g.FillRectangle(Brushes.White, 0 + _Size.Width / 3, 0 - 11, _Size.Width / 3 * 2, _Size.Height / 3 * 2);
            g.ResetTransform();
        }

        public override void Update()
        {
            _Position = new Point(
                _Position.X + _Direction.X,
                _Position.Y + _Direction.Y);

            if (_Position.X < 0)
            {
                _Direction = new Point(-_Direction.X, _Direction.Y);
                NewAngle();
            }
            if (_Position.Y < 0)
            {
                _Direction = new Point(_Direction.X, -_Direction.Y);
                NewAngle();
            }
        
            if (_Position.X > Game.Width)
            {
                _Direction = new Point(-_Direction.X, _Direction.Y);
                NewAngle();
            }

            if (_Position.Y > Game.Height)
            {
                _Direction = new Point(_Direction.X, -_Direction.Y);
                NewAngle();
            }
            
        }

        private void NewAngle()
        {
            Random r = new Random();
            _Angle = r.Next(30);
        }
    }
}
