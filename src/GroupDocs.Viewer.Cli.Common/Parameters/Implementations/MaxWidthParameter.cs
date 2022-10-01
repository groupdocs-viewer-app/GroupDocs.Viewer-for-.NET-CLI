using System.Text;

namespace GroupDocs.Viewer.Cli.Common.Parameters
{
    /// <summary>
    /// Max width for output image parameter.
    /// </summary>
    public class MaxWidthParameter : IntParameter
    {
        public override string ParameterName => "max-width";

        public override string ShortParameterName => null;

        public override string Description => "Max width for output image.";

        /// <summary>
        /// Check parsed parameter value - we filter negative values.
        /// </summary>
        /// <param name="parsedValue">Parsed value.</param>
        /// <returns>Validated parameter parse result.</returns>
        public override ParameterParseResult<int> ValidateParameterParsedValue(int parsedValue)
        {
            if (parsedValue < 0)
            {
                return ParameterParseResult<int>.CreateFailedResult(this, "MaxWidth should be >=0! ");
            }

            return ParameterParseResult<int>.CreateSuccessResult(this, parsedValue);
        }

        public override string GetHelpText()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Specify max width limit for output document, height will be calculated proportionally");
            sb.AppendLine("If you set Width/Height parameters, MaxWidth/MaxHeight parameters will be ignored.");
            return sb.ToString();
        }
    }
}
