using System.Linq;

namespace GroupDocs.Viewer.Cli.Common.Parameters
{
    /// <summary>
    /// Resource file format for external resource file - for HTML with external resources rendering.
    /// </summary>
    public class ResourceFileFormatParameter : StringParameter
    {
        public ResourceFileFormatParameter() { }

        public override string ParameterName => "resource-filepath-template";

        public override string ShortParameterName => null;

        public override string Description => "Resource filepath template when converting to HTML with external resources e.g. 'p_{0}_{1}'";

        protected override string DefaultValue => "p_{0}_{1}"; 

        public override void FillValidValues() { }

        public override string GetHelpText()
        {
            return "Mask for external resource file path format, for example \"p_{0}_{1}\"";
        }

        /// <summary>
        /// Validate parameter command-line raw(string) value - only letters,digits and '{', '}' are allowed. 
        /// </summary>
        /// <param name="rawValue"></param>
        /// <returns></returns>
        public override ParameterParseResult<string> ValidateParameterCommandLineRawValue(string rawValue)
        {
            if (!string.IsNullOrEmpty(rawValue) && 
                !rawValue.Any(u => char.IsLetterOrDigit(u) || u.Equals('{') || u.Equals('}') || u.Equals('_')))
            {
                return ParameterParseResult<string>.CreateFailedResult(this, "Only letters,digits and '{', '}' are allowed. ");
            }

            return ParameterParseResult<string>.CreateSuccessResult(this, rawValue);
        }
    }
}
