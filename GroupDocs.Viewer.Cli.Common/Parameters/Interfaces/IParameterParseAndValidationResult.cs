namespace GroupDocs.Viewer.Cli.Common.Parameters.Interfaces
{
    /// <summary>
    /// Parameter parse and validation result.
    /// </summary>
    public interface IParameterParseAndValidationResult
    {
        /// <summary>
        /// Result success or not.
        /// </summary>
        bool Success { get; set; }

        /// <summary>
        /// Validation and parse message.
        /// </summary>
        string ValidationMessage { get; set; }
    }
}
