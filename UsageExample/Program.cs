using SceneParsingLib;
using System.Drawing.Imaging;
using System.Reflection;

string resourceName = "UsageExample.TestScene.txt";
using Stream stream = Assembly.GetCallingAssembly().GetManifestResourceStream(resourceName)
    ?? throw new InvalidOperationException($"resource {resourceName} not found");
using StreamReader reader = new(stream);

Console.WriteLine("Parsing test scene...");
Scene scene = SceneParser.Parse(reader);

string outPath = "result.bmp";
scene.ToBitmap().Save(outPath, ImageFormat.Bmp);
Console.WriteLine($"Saved as {Path.GetFullPath(outPath)}");
