using SceneParsingLib.Primitives;

namespace SceneParsingLib
{
    public class SceneParser
    {
        /// <summary>
        /// Parses <c>Scene</c> from reader
        /// </summary>
        /// <param name="reader"></param>
        /// <returns><c>Scene</c> object</returns>
        /// <exception cref="SceneParseException">
        /// Thrown if the format of scene is invalid
        /// </exception>
        public static Scene Parse(StreamReader reader)
        {
            ReadBoundingBox(reader, out int xFrom, out int yFrom, out int xTo, out int yTo);
            int lineNumber = 2;
            string? line;
            IList<IPrimitive> primitives = [];
            while ((line = reader.ReadLine()) != null)
            {
                var primitive = ReadPrimitive(line, lineNumber);
                primitives.Add(primitive);
                lineNumber++;
            }
            return new Scene(xFrom, yFrom, xTo, yTo, primitives);
        }

        private static void ReadBoundingBox(StreamReader reader, out int xFrom, out int yFrom, out int xTo, out int yTo)
        {
            string line = reader.ReadLine() ?? throw new SceneParseException("reached end of stream, bounding box expected", 1);
            string[] tokens = line.Split(' ');
            if (tokens.Length != 4)
            {
                throw new SceneParseException("invalid bounding box format", 1);
            }
            xFrom = ReadInt(tokens[0], 1);
            yFrom = ReadInt(tokens[1], 1);
            xTo = ReadInt(tokens[2], 1);
            yTo = ReadInt(tokens[3], 1);
            if (xFrom >= xTo || yFrom >= yTo)
            {
                throw new SceneParseException("invalid bounding box coordinates", 1);
            }
        }

        private static int ReadInt(string token, int lineNumber)
        {
            if (!int.TryParse(token, out int value))
            {
                throw new SceneParseException($"invalid integer: {token}", lineNumber);
            }
            return value;
        }

        private const string PointString = "point";
        private const string RectangleString = "rect";
        private const string HorizontalLineString = "hline";
        private const string VerticalLineString = "vline";
        private const string TriangleString = "triangle";
        private const string EllipseString = "ellipse";

        private static IPrimitive ReadPrimitive(string line, int lineNumber)
        {
            string[] tokens = line.Trim().Split(' ');
            if (tokens.Length == 0)
            {
                throw new SceneParseException("no primitive declaration found", lineNumber);
            }
            return tokens[0] switch
            {
                PointString => ReadPoint(tokens, lineNumber),
                RectangleString => ReadRectangle(tokens, lineNumber),
                HorizontalLineString => ReadHorizontalLine(tokens, lineNumber),
                VerticalLineString => ReadVerticalLine(tokens, lineNumber),
                TriangleString => ReadTriangle(tokens, lineNumber),
                EllipseString => ReadEllipse(tokens, lineNumber),
                _ => throw new SceneParseException($"unknown primitive type: {tokens[0]}", lineNumber)
            };
        }
        private static Point ReadPoint(string[] tokens, int lineNumber)
        {
            if (tokens.Length != 3)
            {
                throw new SceneParseException("invalid point format, expected 2 integers after declaration", lineNumber);
            }
            int x = ReadInt(tokens[1], lineNumber);
            int y = ReadInt(tokens[2], lineNumber);
            return new Point(x, y);
        }

        private static Rectangle ReadRectangle(string[] tokens, int lineNumber)
        {
            if (tokens.Length != 5)
            {
                throw new SceneParseException("invalid rectangle format, expected 4 integers after declaration", lineNumber);
            }
            int xFrom = ReadInt(tokens[1], lineNumber);
            int yFrom = ReadInt(tokens[2], lineNumber);
            int xTo = ReadInt(tokens[3], lineNumber);
            int yTo = ReadInt(tokens[4], lineNumber);
            if (xFrom > xTo || yFrom > yTo)
            {
                throw new SceneParseException("invalid rectangle coordinates", lineNumber);
            }
            return new Rectangle(xFrom, yFrom, xTo, yTo);
        }
        private static HorizontalLine ReadHorizontalLine(string[] tokens, int lineNumber)
        {
            if (tokens.Length != 4)
            {
                throw new SceneParseException("invalid horizontal line format, expected 3 integers after declaration", lineNumber);
            }
            int x1 = ReadInt(tokens[1], lineNumber);
            int x2 = ReadInt(tokens[2], lineNumber);
            int y = ReadInt(tokens[3], lineNumber);
            return new HorizontalLine(x1, x2, y);
        }

        private static VerticalLine ReadVerticalLine(string[] tokens, int lineNumber)
        {
            if (tokens.Length != 4)
            {
                throw new SceneParseException("invalid vertical line format, expected 3 integers after declaration", lineNumber);
            }
            int y1 = ReadInt(tokens[1], lineNumber);
            int y2 = ReadInt(tokens[2], lineNumber);
            int x = ReadInt(tokens[3], lineNumber);
            return new VerticalLine(x, y1, y2);
        }

        private static Triangle ReadTriangle(string[] tokens, int lineNumber)
        {
            if (tokens.Length != 7)
            {
                throw new SceneParseException("invalid triangle format, expected 6 integers after declaration", lineNumber);
            }
            int x1 = ReadInt(tokens[1], lineNumber);
            int y1 = ReadInt(tokens[2], lineNumber);
            int x2 = ReadInt(tokens[3], lineNumber);
            int y2 = ReadInt(tokens[4], lineNumber);
            int x3 = ReadInt(tokens[5], lineNumber);
            int y3 = ReadInt(tokens[6], lineNumber);
            return new Triangle(x1, y1, x2, y2, x3, y3);
        }

        private static Ellipse ReadEllipse(string[] tokens, int lineNumber)
        {
            if (tokens.Length != 5)
            {
                throw new SceneParseException("invalid ellipse format, expected 4 integers after declaration", lineNumber);
            }
            int x = ReadInt(tokens[1], lineNumber);
            int y = ReadInt(tokens[2], lineNumber);
            int width = ReadInt(tokens[3], lineNumber);
            int height = ReadInt(tokens[4], lineNumber);

            if (width <= 0 || height <= 0)
            {
                throw new SceneParseException("ellipse width and height should be positive", lineNumber);
            }
            return new Ellipse(x, y, width, height);
        }
    }
}
