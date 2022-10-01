namespace GroupDocs.Viewer.Cli.Common.Parameters
{
    /// <summary>
    /// Parameter to set output image width in pixels.
    /// </summary>
    public class WidthParameter : IntParameter
    {
        public override string ParameterName => "width";

        public override string ShortParameterName => null;

        public override string Description => "Output image width.";

        public override string GetHelpText()
        {
            return "Set output image width in pixels";
        }

        /// <summary>
        /// Check parsed parameter value - we filter negative values.
        /// </summary>
        /// <param name="parsedValue">Parsed value.</param>
        /// <returns>Validated parameter parse result.</returns>
        public override ParameterParseResult<int> ValidateParameterParsedValue(int parsedValue)
        {
            if (parsedValue < 0)
                return ParameterParseResult<int>.CreateFailedResult(this, "MaxWidth should be >= 0! ");

            return ParameterParseResult<int>.CreateSuccessResult(this, parsedValue);
        }
    }
}
