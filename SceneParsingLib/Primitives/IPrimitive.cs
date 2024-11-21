using System.Drawing;

namespace SceneParsingLib.Primitives
{
    public interface IPrimitive
    {
        void Draw(Graphics g, int xShift = 0, int yShift = 0);

        public static readonly Pen DrawingPen = new(Color.White, 1);
    }
}
