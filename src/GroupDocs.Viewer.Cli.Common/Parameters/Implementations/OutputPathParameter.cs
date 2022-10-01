using GroupDocs.Viewer.Cli.Common.Enums;

namespace GroupDocs.Viewer.Cli.Common.Parameters.Implementations
{
    /// <summary>
    /// Output path parameter.
    /// </summary>
    public class OutputPathParameter : StringParameter
    {
        public override string ParameterName => "output";

        public override string ShortParameterName => "o";

        public override string Description => "Output file path when rendering to HTML, PNG, and JPG. Or output filename when rendering to PDF.";

        public override void FillValidValues() { }

        public override ParameterParseResult<string> Parse(string[] args)
        {
            ParameterParseResult<string> result = base.Parse(args);

            // Make default file name if it empty
            if (result.Success && (string.IsNullOrEmpty(result.ResultValue) || string.IsNullOrWhiteSpace(result.ResultValue)))
            {
                OutputFormatParameter outputFormatParameter = new OutputFormatParameter();

                // Make default file name depend on output format parameter (-f, --format)
                if (outputFormatParameter.ParseAndValidate(args))
                {
                    OutputFormat outputFormat = outputFormatParameter.LastValidationResult.ResultValue;
                    switch (outputFormat)
                    {
                        case OutputFormat.Pdf:
                            result = ParameterParseResult<string>.CreateSuccessResult(this, "output.pdf");
                            break;
                        case OutputFormat.Png:
                            result = ParameterParseResult<string>.CreateSuccessResult(this, "output_{0}.png");
                            break;
                        case OutputFormat.Jpg:
                            result = ParameterParseResult<string>.CreateSuccessResult(this, "output_{0}.jpg");
                            break;
                        default:
                            result = ParameterParseResult<string>.CreateSuccessResult(this, "output_{0}.html");
                            break;
                    }
                }
                else
                {
                    // If it not valid validation error for output format parameter will be catched at general validation.
                }
            }

            return result;
        }
    }
}
