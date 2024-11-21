using SceneParsingLib.Primitives;
using System.Drawing;

namespace SceneParsingLib
{
    public class Scene
    {
        private readonly int _xFrom;
        private readonly int _yFrom;
        private readonly int _xTo;
        private readonly int _yTo;

        private IEnumerable<IPrimitive> _primitives;

        public Scene(int xFrom, int yFrom, int xTo, int yTo, IEnumerable<IPrimitive> primitives)
        {
            if (xFrom >= xTo || yFrom >= yTo)
            {
                throw new ArgumentException("invalid bounding box");
            }
            _xFrom = xFrom;
            _yFrom = yFrom;
            _xTo = xTo;
            _yTo = yTo;
            _primitives = primitives;
        }

        /// <summary>
        /// Converts the scene to a <c>Bitmap</c> image. Width and height of the resulting image are 
        /// equal to width and height of the scene's bounding box from (xFrom, yFrom) to (xTo, yTo).
        /// The image itself represents the view of the scene in the given bounding box.
        /// </summary>
        /// <returns><c>Bitmap</c> object</returns>
        public Bitmap ToBitmap()
        {
            int width = _xTo - _xFrom;
            int height = _yTo - _yFrom;

            var bitmap = new Bitmap(width, height);
            using (var g = Graphics.FromImage(bitmap))
            {
                foreach (var primitive in _primitives)
                {
                    primitive.Draw(g, _xFrom, _yFrom);
                }
            }
            return bitmap;
        }
    }
}
