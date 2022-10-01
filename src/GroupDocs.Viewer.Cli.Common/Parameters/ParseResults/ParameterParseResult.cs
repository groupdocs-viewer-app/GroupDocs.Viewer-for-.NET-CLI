using GroupDocs.Viewer.Cli.Common.Parameters.Interfaces;

namespace GroupDocs.Viewer.Cli.Common.Parameters
{
    /// <summary>
    /// Parse result for parameter.
    /// </summary>
    /// <typeparam name="T">Type of parameter output value.</typeparam>
    public class ParameterParseResult<T> : IParameterParseAndValidationResult
    {
        /// <summary>
        /// Parse and validation result success or not.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Validation message.
        /// </summary>
        public string ValidationMessage { get; set; }

        /// <summary>
        /// Result value of parameter.
        /// </summary>
        public T ResultValue { get; set; }

        /// <summary>
        /// Current parameter object.
        /// </summary>
        public Parameter<T> Parameter { get; protected set; }

        /// <summary>
        /// Create failed parse result for parameter.
        /// </summary>
        /// <param name="parameter">Parameter object.</param>
        /// <param name="message">Failure message.</param>
        /// <returns>Parameter parse result object.</returns>
        public static ParameterParseResult<T> CreateFailedResult(Parameter<T> parameter, string message)
        {
            const string failedTemplateString = "Failed parse for parameter {0}({1}): {2}";

            return new ParameterParseResult<T>()
            {
                Success = false,
                Parameter = parameter,
                ValidationMessage = string.Format(failedTemplateString, parameter.ParameterName, parameter.ShortParameterName, message)
            };
        }

        /// <summary>
        /// Create success result.
        /// </summary>
        /// <param name="parameter">Parameter object.</param>
        /// <param name="value">Parameter value.</param>
        /// <returns>Parameter parse result object.</returns>
        public static ParameterParseResult<T> CreateSuccessResult(Parameter<T> parameter, T value)
        {
            return new ParameterParseResult<T>()
            {
                Success = true,
                ResultValue = value,
                Parameter = parameter
            };
        }
    }
}
