using System.Linq;

namespace GroupDocs.Viewer.Cli.Common.Parameters
{
    /// <summary>
    /// Resource URL format of external URL - for HTML with external resources rendering.
    /// </summary>
    public class ResourceUrlFormatParameter : StringParameter
    {
        public ResourceUrlFormatParameter() { }

        public override string Description => "Resource URL format for HTML with external resources";

        public override string ParameterName => "resource-url-format";

        public override string ShortParameterName => null;
        
        protected override string DefaultValue => "p_{0}_{1}";
        
        public override void FillValidValues() { }
        
        public override string GetHelpText()
        {
            return "Mask for external resource URL format, for example \"p_{0}_{1}\"";
        }
        
        public override ParameterParseResult<string> ValidateParameterCommandLineRawValue(string rawValue)
        {
            if (!string.IsNullOrEmpty(rawValue) &&
                !rawValue.Any(u => char.IsLetterOrDigit(u) || u.Equals('{') || u.Equals('}') || u.Equals('_')))
            {
                return ParameterParseResult<string>.CreateFailedResult(this, "Only letters and '{', '}' are allowed. ");
            }

            return ParameterParseResult<string>.CreateSuccessResult(this, rawValue);
        }

    }
}
