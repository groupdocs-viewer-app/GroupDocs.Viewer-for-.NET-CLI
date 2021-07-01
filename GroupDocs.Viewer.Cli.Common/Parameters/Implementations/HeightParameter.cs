namespace GroupDocs.Viewer.Cli.Common.Parameters
{
    /// <summary>
    /// Parameter to set output image height in pixels.
    /// </summary>
    public class HeightParameter : IntParameter
    {
        public override string ParameterName => "height";
        
        public override string ShortParameterName => null;

        public override string Description => "Output image height.";

        /// <summary>
        /// Check parsed parameter value - we filter negative values.
        /// </summary>
        /// <param name="parsedValue">Parsed value.</param>
        /// <returns>Validated parameter parse result.</returns>
        public override ParameterParseResult<int> ValidateParameterParsedValue(int parsedValue)
        {
            if (parsedValue < 0)
            {
                return ParameterParseResult<int>.CreateFailedResult(this, "Width should be >=0! ");
            }

            return ParameterParseResult<int>.CreateSuccessResult(this, parsedValue);
        }

        public override string GetHelpText()
        {
            return "Specify height for output image in pixels";
        }
    }
}
