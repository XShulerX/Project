using Project.VisualObjects.Interfaces;
using System;
using System.Drawing;

namespace Project.VisualObjects
{
    class HealthPack : VisualObject, ICollision
    {
        public int bonusEnergy = 7;

        public HealthPack(Point Position, Point Direction, Size Size)
            : base(Position, Direction, Size)
        {
        }

        public bool CheckCollision(ICollision obj) => Rect.IntersectsWith(obj.Rect);

        public override void Draw(Graphics g)
        {
            g.TranslateTransform(_Position.X, _Position.Y);
            //g.FillEllipse(Brushes.White, 0, 0, 20, 20);
            g.FillRectangle(Brushes.Blue, 0, 8, 20, 5);
            g.FillRectangle(Brushes.Blue, 8, 0, 5, 20);
            g.DrawEllipse(Pens.Green, 0, 0, 20, 20);
            g.ResetTransform();
        }

        public override void Update()
        {
            _Position = new Point(_Position.X + _Direction.X, _Position.Y);
            if (_Position.X < 0)
            {
                _Position.X = Game.Width + _Size.Width;
                _Position.Y = new Random().Next(0,Game.Height);
            }
        }
    }
}
