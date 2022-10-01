namespace GroupDocs.Viewer.Cli.Utils
{
    /// <summary>
    /// Reporter to command-line interface.
    /// </summary>
    public interface IReporter
    {
        /// <summary>
        /// Write line to output.
        /// </summary>
        /// <param name="message">Message.</param>
        void WriteLine(string message);

        /// <summary>
        /// Write line to output.
        /// </summary>
        void WriteLine();

        /// <summary>
        /// Write to output.
        /// </summary>
        void Write(string message);
    }
}