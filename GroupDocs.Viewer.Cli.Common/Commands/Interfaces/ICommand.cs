using GroupDocs.Viewer.Cli.Common.Enums;
using GroupDocs.Viewer.Cli.Common.Parameters;

namespace GroupDocs.Viewer.Cli.Common.Commands.Interfaces
{
    /// <summary>
    /// Viewer command interface.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Command type.
        /// </summary>
        CommandType CommandType { get; }

        /// <summary>
        /// Execute command
        /// </summary>
        /// <param name="args">Arguments.</param>
        void Execute(string[] args = null);

        /// <summary>
        /// Parse command-line arguments and return parse result.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        /// <returns>Parse result.</returns>
        CommandLineParseResult Parse(string[] args);

        /// <summary>
        /// Display help for command.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        void DisplayHelp(string[] args);
    }
}
