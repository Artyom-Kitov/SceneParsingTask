using System.Drawing;

namespace SceneParsingLib.Primitives
{
    public class Rectangle : IPrimitive
    {
        public int X1 { get; }
        public int Y1 { get; }
        public int X2 { get; }
        public int Y2 { get; }

        public Rectangle(int x1, int y1, int x2, int y2)
        {
            if (x1 > x2 || y1 > y2)
            {
                throw new ArgumentException("invalid rectangle coordinates");
            }
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
        }

        public void Draw(Graphics g, int xShift = 0, int yShift = 0)
        {
            int xFrom = X1 - xShift;
            int yFrom = Y1 - yShift;
            int width = X2 - X1;
            int height = Y2 - Y1;
            g.DrawRectangle(IPrimitive.DrawingPen, xFrom, yFrom, width, height);
        }
    }
}
