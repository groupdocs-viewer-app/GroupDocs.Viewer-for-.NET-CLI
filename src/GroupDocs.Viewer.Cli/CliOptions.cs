using GroupDocs.Viewer.UI.Core;

namespace GroupDocs.Viewer.Cli;

public class CliOptions
{
    public string StoragePath { get; private set; } = Directory.GetCurrentDirectory();
    public bool IsFileMode { get; private set; }
    public string? FileName { get; private set; }
    public int Port { get; private set; } = 8080;
    public string Bind { get; private set; } = "localhost";
    public bool NoOpen { get; private set; }
    public string? LicensePath { get; private set; }
    public ViewerType ViewerType { get; private set; } = ViewerType.HtmlWithEmbeddedResources;
    public bool Verbose { get; private set; }
    public bool ShowVersion { get; private set; }
    public bool ShowHelp { get; private set; }

    public static CliOptions Parse(string[] args)
    {
        var options = new CliOptions();
        var i = 0;

        while (i < args.Length)
        {
            var arg = args[i];

            switch (arg)
            {
                case "--help" or "-h" or "-?" or "/?":
                    options.ShowHelp = true;
                    return options;

                case "--version":
                    options.ShowVersion = true;
                    return options;

                case "--port" or "-p":
                    options.Port = ParseIntArg(args, ref i, arg);
                    break;

                case "--bind" or "-b":
                    options.Bind = ParseStringArg(args, ref i, arg);
                    break;

                case "--no-open":
                    options.NoOpen = true;
                    break;

                case "--license-path" or "-l":
                    options.LicensePath = ParseStringArg(args, ref i, arg);
                    break;

                case "--viewer-type" or "-t":
                    options.ViewerType = ParseViewerType(ParseStringArg(args, ref i, arg));
                    break;

                case "--verbose" or "-v":
                    options.Verbose = true;
                    break;

                default:
                    if (arg.StartsWith('-'))
                        throw new ArgumentException($"Unknown option: {arg}");

                    // Positional argument: path
                    ResolvePath(options, arg);
                    break;
            }

            i++;
        }

        // Resolve license from environment if not set via CLI
        options.LicensePath ??= ResolveLicensePath();

        return options;
    }

    private static void ResolvePath(CliOptions options, string path)
    {
        var fullPath = Path.GetFullPath(path);

        if (Directory.Exists(fullPath))
        {
            options.StoragePath = fullPath;
            options.IsFileMode = false;
        }
        else if (File.Exists(fullPath))
        {
            options.StoragePath = Path.GetDirectoryName(fullPath)!;
            options.FileName = Path.GetFileName(fullPath);
            options.IsFileMode = true;
        }
        else
        {
            throw new ArgumentException($"Path not found: {path}");
        }
    }

    private static string? ResolveLicensePath()
    {
        var envPath = Environment.GetEnvironmentVariable("GROUPDOCS_LIC_PATH");
        if (!string.IsNullOrEmpty(envPath) && File.Exists(envPath))
            return envPath;

        var candidates = new[] { "GroupDocs.Viewer.lic", "GroupDocs.Viewer.Product.Family.lic" };
        foreach (var candidate in candidates)
        {
            var fullPath = Path.GetFullPath(candidate);
            if (File.Exists(fullPath))
                return fullPath;
        }

        return null;
    }

    private static ViewerType ParseViewerType(string value)
    {
        return value.ToLowerInvariant() switch
        {
            "html" or "htmlwithembeddedresources" => ViewerType.HtmlWithEmbeddedResources,
            "htmlext" or "htmlwithexternalresources" => ViewerType.HtmlWithExternalResources,
            "png" => ViewerType.Png,
            "jpg" => ViewerType.Jpg,
            _ => throw new ArgumentException(
                $"Invalid viewer type: {value}. Valid values: Html, HtmlExt, Png, Jpg")
        };
    }

    private static string ParseStringArg(string[] args, ref int i, string name)
    {
        if (i + 1 >= args.Length)
            throw new ArgumentException($"Option {name} requires a value.");
        return args[++i];
    }

    private static int ParseIntArg(string[] args, ref int i, string name)
    {
        var str = ParseStringArg(args, ref i, name);
        if (!int.TryParse(str, out var value))
            throw new ArgumentException($"Option {name} requires an integer value, got: {str}");
        return value;
    }

    public static void PrintHelp()
    {
        Console.WriteLine(
            """
            Usage: groupdocs-viewer [path] [options]

            Arguments:
              path                     File or directory to serve (default: current directory)

            Options:
              -p, --port <port>        Port to listen on (default: 8080)
              -b, --bind <address>     Bind address (default: localhost)
                  --no-open            Don't auto-open browser
              -l, --license-path <path> Path to GroupDocs license file
              -t, --viewer-type <type> Viewer type: Html, HtmlExt, Png, Jpg (default: Html)
              -v, --verbose            Enable verbose logging
                  --version            Show version info
              -h, --help               Show this help
            """);
    }
}
