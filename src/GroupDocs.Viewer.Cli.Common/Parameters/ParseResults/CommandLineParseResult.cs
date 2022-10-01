namespace GroupDocs.Viewer.Cli.Common.Parameters
{
    /// <summary>
    /// Command-line parse result.
    /// </summary>
    public class CommandLineParseResult
    {
        /// <summary>
        /// Parse success or not.
        /// </summary>
        public bool Success { get; private set; }

        /// <summary>
        /// Failure message.
        /// </summary>
        public string Message { get; private set; }

        private CommandLineParseResult() { }

        public static CommandLineParseResult Successful() => 
            new CommandLineParseResult { Success = true };

        public static CommandLineParseResult Failure(string message) => 
            new CommandLineParseResult
            {
                Success = false, 
                Message = message
            };
    }
}
