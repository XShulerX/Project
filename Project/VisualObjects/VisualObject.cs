using System;
using System.Drawing;

namespace Project.VisualObjects
{
    public abstract class VisualObject
    {
        protected Point _Position;
        protected Point _Direction;
        protected Size _Size;

        public Point Position => _Position;

        public Rectangle Rect => new Rectangle(_Position, _Size);

        protected VisualObject(Point Position, Point Direction, Size Size)
        {
            if (Position.X < 0 || Position.Y < 0 || Size.Width < 0 || Size.Height < 0 || Math.Abs(Direction.X) > 20 || Math.Abs(Direction.Y) > 20)
            {
                throw new GameObjectException("Попытка создать объект с неправильными характеристиками");
            }
            
            _Position = Position;
            _Direction = Direction;
            _Size = Size;
        }

        public abstract void Draw(Graphics g);

        public abstract void Update();
    }
}
