namespace GroupDocs.Viewer.Cli.Common.Parameters
{
    /// <summary>
    /// Command-line parameter interface.
    /// </summary>
    public interface IParameter
    {
        /// <summary>
        /// Full parameter name (eg. 'help').
        /// </summary>
        string ParameterName { get; }

        /// <summary>
        /// Short parameter name (eg. 'h').
        /// </summary>
        string ShortParameterName { get; }

        /// <summary>
        /// Description of parameter (eg. 'Display help').
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Parameter validation flag (parse + serialization of value status).
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// Parse arguments and validate values.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        /// <returns>Valid or not</returns>
        bool ParseAndValidate(string[] args);

        /// <summary>
        /// Get last validation message.
        /// </summary>
        /// <returns></returns>
        string GetValidationMessage();

        /// <summary>
        /// Get help text.
        /// </summary>
        /// <returns></returns>
        string GetHelpText();
    }
}
