using System.Reflection;
using GroupDocs.Viewer.Cli;

try
{
    var options = CliOptions.Parse(args);

    if (options.ShowHelp)
    {
        CliOptions.PrintHelp();
        return 0;
    }

    if (options.ShowVersion)
    {
        PrintVersion();
        return 0;
    }

    PrintBanner(options);

    var app = ServerBootstrap.Build(options);

    if (!options.NoOpen)
    {
        app.Lifetime.ApplicationStarted.Register(() =>
            BrowserLauncher.Open($"http://{options.Bind}:{options.Port}/"));
    }

    await app.RunAsync();
    return 0;
}
catch (ArgumentException ex)
{
    Console.Error.WriteLine($"Error: {ex.Message}");
    Console.Error.WriteLine("Run 'groupdocs-viewer --help' for usage.");
    return 1;
}

static void PrintVersion()
{
    var version = typeof(CliOptions).Assembly
        .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion
        ?? typeof(CliOptions).Assembly.GetName().Version?.ToString()
        ?? "unknown";
    Console.WriteLine($"groupdocs-viewer {version}");
}

static void PrintBanner(CliOptions options)
{
    var version = typeof(CliOptions).Assembly
        .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion
        ?? typeof(CliOptions).Assembly.GetName().Version?.ToString()
        ?? "unknown";

    Console.WriteLine($$"""

       ____                    ____
      / ___|_ __ ___  _   _ _ |  _ \  ___   ___ ___
     | |  _| '__/ _ \| | | | '| | | |/ _ \ / __/ __|
     | |_| | | | (_) | |_| | ,| |_| | (_) | (__\__ \
      \____|_|  \___/ \__,_|_|| ____/ \___/ \___|___/
                              |_|
      V I E W E R  {{version}}

    """);

    if (options.IsFileMode)
        Console.WriteLine($"  Opening file: {Path.Combine(options.StoragePath, options.FileName!)}");
    else
        Console.WriteLine($"  Serving files from: {options.StoragePath}");

    Console.WriteLine($"  Listening on: http://{options.Bind}:{options.Port}/");
    Console.WriteLine();
    Console.WriteLine("  Press Ctrl+C to stop the server.");
    Console.WriteLine();
}
