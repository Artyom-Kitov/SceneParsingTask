using System.Drawing;

namespace SceneParsingLib.Primitives
{
    public class HorizontalLine(int x1, int x2, int y) : IPrimitive
    {
        public int X1 { get; } = x1;
        public int X2 { get; } = x2;
        public int Y { get; } = y;

        public void Draw(Graphics g, int xShift = 0, int yShift = 0)
        {
            g.DrawLine(IPrimitive.DrawingPen, X1 - xShift, Y - yShift, X2 - xShift, Y - yShift);
        }
    }
}
