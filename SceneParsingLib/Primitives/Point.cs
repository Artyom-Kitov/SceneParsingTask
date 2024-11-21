using System.Drawing;

namespace SceneParsingLib.Primitives
{
    public class Point(int x, int y) : IPrimitive
    {
        public int X { get; } = x;
        public int Y { get; } = y;

        public void Draw(Graphics g, int xShift = 0, int yShift = 0)
        {
            g.FillRectangle(IPrimitive.DrawingPen.Brush, X - xShift, Y - yShift, 1, 1);
        }
    }
}
