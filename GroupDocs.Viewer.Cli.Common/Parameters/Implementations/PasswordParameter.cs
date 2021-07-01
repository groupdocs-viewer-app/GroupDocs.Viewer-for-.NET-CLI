namespace GroupDocs.Viewer.Cli.Common.Parameters
{
    /// <summary>
    /// Password for password protected document open.
    /// </summary>
    public class PasswordParameter : StringParameter
    {
        public override string ParameterName => "password";

        public override string ShortParameterName => "pwd";

        public override string Description => "Password to open password-protected file.";

        public override void FillValidValues()
        {
            // Accepts any values
        }

        public override string GetHelpText()
        {
            return "If file password protected you can specify password with this option";
        }

        public override ParameterParseResult<string> Parse(string[] args)
        {
            return ParameterParseResult<string>.CreateSuccessResult(this, GetStringValue(args));
        }
    }
}
