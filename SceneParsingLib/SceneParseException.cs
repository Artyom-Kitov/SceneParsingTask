namespace SceneParsingLib
{
    public class SceneParseException(string message, int line) : Exception($"Line {line}: {message}")
    {
    }
}
