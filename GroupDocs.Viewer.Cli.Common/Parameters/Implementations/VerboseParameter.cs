namespace GroupDocs.Viewer.Cli.Common.Parameters.Implementations
{
    /// <summary>
    /// Enable detailed (verbose) logging.
    /// </summary>
    public class VerboseParameter : Parameter<bool>
    {
        public override string ParameterName => "verbose";

        public override string ShortParameterName => "v";

        public override string Description => "Enable detailed logging to console.";

        public override void FillValidValues() { }

        public override ParameterParseResult<bool> Parse(string[] args)
        {
            return ParameterParseResult<bool>.CreateSuccessResult(this, ThisParameterExistInCommandLine(args));
        }
    }
}
