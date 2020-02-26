using System.Drawing;

namespace Project.VisualObjects
{
    public abstract class ImageObject : VisualObject
    {
        private Image _Image;

        protected ImageObject(Point Position, Point Direction, Size Size, Image Image) : base(Position, Direction, Size)
        {
            _Image = Image;
        }

        public override void Draw(Graphics g)
        {
            g.DrawImage(_Image, Rect);
        }
    }
}
