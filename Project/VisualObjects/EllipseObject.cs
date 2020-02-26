using System.Drawing;

namespace Project.VisualObjects
{
    public class EllipseObject : VisualObject
    {
        public EllipseObject(Point Position, Point Direction, Size Size) : base(Position, Direction, Size)
        {

        }

        public override void Draw(Graphics g)
        {
            g.DrawEllipse(Pens.White, Rect);
        }
    }
}
