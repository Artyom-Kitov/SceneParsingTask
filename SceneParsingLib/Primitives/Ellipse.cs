using System.Drawing;

namespace SceneParsingLib.Primitives
{
    public class Ellipse : IPrimitive
    {
        public int X { get; }
        public int Y { get; }
        public int Width { get; }
        public int Height { get; }

        public Ellipse(int x, int y, int width, int height)
        {
            if (width <= 0 || height <= 0)
            {
                throw new ArgumentException("invalid ellipse radius parameters");
            }
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public void Draw(Graphics g, int xShift = 0, int yShift = 0)
        {
            g.DrawEllipse(IPrimitive.DrawingPen, X - xShift, Y - yShift, Width, Height);
        }
    }
}
