namespace GroupDocs.Viewer.Cli.Common.Parameters
{
    /// <summary>
    /// Integer parameter template.
    /// </summary>
    public abstract class IntParameter : Parameter<int>
    {
        public override void FillValidValues()
        {
            // No checks for valid values here, but it can be overridden.
        }

        /// <summary>
        /// Parse command-line parameter implementation.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        /// <returns>Parameter parse result.</returns>
        public override ParameterParseResult<int> Parse(string[] args)
        {
            // get string value from command-line.
            string rawValue = GetStringValue(args);

            ParameterParseResult<int> result = null;

            // If it not empty - try parse and return parse result.
            if (!string.IsNullOrWhiteSpace(rawValue))
            {
                bool success = int.TryParse(rawValue, out int value);

                if (success)
                {
                    result = ParameterParseResult<int>.CreateSuccessResult(this, value);
                }
                else
                {
                    result = ParameterParseResult<int>.CreateFailedResult(this, $"value {rawValue} cannot be parsed as integer");
                }
            }
            else
            {
                result = ParameterParseResult<int>.CreateSuccessResult(this, 0);
            }

            return result;
        }
    }
}
