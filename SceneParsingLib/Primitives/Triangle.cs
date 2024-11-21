using System.Drawing;

namespace SceneParsingLib.Primitives
{
    public class Triangle(int x1, int y1, int x2, int y2, int x3, int y3) : IPrimitive
    {
        public int X1 { get; } = x1;
        public int Y1 { get; } = y1;
        public int X2 { get; } = x2;
        public int Y2 { get; } = y2;
        public int X3 { get; } = x3;
        public int Y3 { get; } = y3;

        public void Draw(Graphics g, int xShift = 0, int yShift = 0)
        {
            g.DrawLine(IPrimitive.DrawingPen, X1, Y1, X2, Y2);
            g.DrawLine(IPrimitive.DrawingPen, X2, Y2, X3, Y3);
            g.DrawLine(IPrimitive.DrawingPen, X3, Y3, X1, Y1);
        }
    }
}
