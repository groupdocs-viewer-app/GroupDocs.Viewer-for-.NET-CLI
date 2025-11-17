using System;


namespace GroupDocs.Viewer.Cli.Common.Exceptions
{
    /// <summary>
    /// Command line parse exception.
    /// </summary>
    public class CommandLineParseException : CliApplicationException
    {
        /// <summary>
        /// Command line parse exception.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public CommandLineParseException(string message) : base(message)
        {
        }

    }
}
