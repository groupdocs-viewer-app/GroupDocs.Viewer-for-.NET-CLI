namespace GroupDocs.Viewer.Cli.Logging
{
    /// <summary>
    /// Simple console logger
    /// </summary>
    public class Logger : ILogger
    {
        private static readonly ILogger NullLogger = new Logger(console: null);
        private static object _lock = new object();

        private readonly AnsiConsole _console;

        static Logger()
        {
            Reset();
        }

        private Logger(AnsiConsole console)
        {
            _console = console;
        }

        public static Logger Output { get; private set; }
        public static Logger Error { get; private set; }

        public static void Reset()
        {
            lock (_lock)
            {
                Output = new Logger(AnsiConsole.GetOutput());
                Error = new Logger(AnsiConsole.GetError());
            }
        }

        public void WriteLine(string message)
        {
            lock (_lock)
            {
                _console?.WriteLine(message);
            }
        }

        public void WriteLine()
        {
            lock (_lock)
            {
                _console?.Writer?.WriteLine();
            }
        }

        public void Write(string message)
        {
            lock (_lock)
            {
                _console?.Write(message);
            }
        }
    }
}