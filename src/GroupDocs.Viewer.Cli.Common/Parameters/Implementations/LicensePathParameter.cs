using GroupDocs.Viewer.Cli.Utils;
using System;
using System.IO;

namespace GroupDocs.Viewer.Cli.Common.Parameters
{
    /// <summary>
    /// Set license path.
    /// </summary>
    public class LicensePathParameter : StringParameter
    {
        public override string ParameterName => "license-path";

        public override string ShortParameterName => null;

        public override string Description => "Path to license file.";

        public override void FillValidValues() { }

        /// <summary>
        /// Validate supplied file path - if not empty (from command-line or ENV) check file exist.
        /// </summary>
        /// <param name="rawValue">raw string value from command-line.</param>
        /// <returns>Parsed parameter result with value.</returns>
        public override ParameterParseResult<string> ValidateParameterCommandLineRawValue(string rawValue)
        {
            // Check that license in exist in file path.
            rawValue = CheckLicensePathInEnvIfCurrentValueIsEmpty(rawValue);

            // If it not empty - check file exist
            if (!string.IsNullOrEmpty(rawValue))
            {

                try
                {
                    System.IO.Path.GetFullPath(rawValue);
                    FileAttributes attr = File.GetAttributes(rawValue);

                    if (attr.HasFlag(FileAttributes.Directory))
                    {
                        return ParameterParseResult<string>.CreateFailedResult(this, $"Path {rawValue} is invalid - should be full path to file.");
                    }

                    return ParameterParseResult<string>.CreateSuccessResult(this, rawValue);
                }
                catch (Exception ex)
                {
                    Reporter.Error.WriteLine(ex.Message);
                    return ParameterParseResult<string>.CreateFailedResult(this, $"Path {rawValue} is invalid - {ex.Message}");
                }
            }

            return ParameterParseResult<string>.CreateSuccessResult(this, rawValue);
        }

        public override ParameterParseResult<string> ValidateParameterParsedValue(string parsedValue)
        {
            parsedValue = CheckLicensePathInEnvIfCurrentValueIsEmpty(parsedValue, false);

            if (!string.IsNullOrEmpty(parsedValue))
            {
                return ParameterParseResult<string>.CreateSuccessResult(this, parsedValue);
            }

            return base.ValidateParameterParsedValue(parsedValue);
        }

        public override string GetHelpText()
        {
            return "Set path to license file, for example: C:\\Licenses\\GroupDocs.Viewer.lic";
        }

        private string TryGetLicensePathFromEnv()
        {
            return CommandContext.GetLicensePath();
        }

        /// <summary>
        /// Check license path in ENV if empty - if set, get value from ENV.
        /// </summary>
        /// <param name="rawValue">Current raw value (string) from command-line.</param>
        /// <param name="display">Display message "Found license in ENV" or not.</param>
        /// <returns></returns>
        private string CheckLicensePathInEnvIfCurrentValueIsEmpty(string rawValue, bool display = true)
        {
            if (string.IsNullOrEmpty(rawValue))
            {
                rawValue = TryGetLicensePathFromEnv();

                if (!string.IsNullOrEmpty(rawValue) && display)
                {
                    Reporter.Output.WriteLine("Found license in ENV - trying to set it from ENV");
                }
            }

            return rawValue;
        }
    }
}
