using System.Drawing;

namespace SceneParsingLib.Primitives
{
    public class VerticalLine(int x, int y1, int y2) : IPrimitive
    {
        public int X { get; } = x;
        public int Y1 { get; } = y1;
        public int Y2 { get; } = y2;

        public void Draw(Graphics g, int xShift = 0, int yShift = 0)
        {
            g.DrawLine(IPrimitive.DrawingPen, X - xShift, Y1 - yShift, X - xShift, Y2 - yShift);
        }
    }
}
