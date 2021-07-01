using GroupDocs.Viewer.Cli.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GroupDocs.Viewer.Cli.Common.Parameters
{
    /// <summary>
    /// Document page numbers parameter.
    /// </summary>
    public class PagesParameter : Parameter<int[]>
    {
        public override string ParameterName => "pages";

        public override string ShortParameterName => "p";

        public override string Description => "Comma-separated page numbers to convert e.g '1,2,3'.";

        public override void FillValidValues() { }

        /// <summary>
        /// Validate command-line parameter value - it should list of digits, separated by ','.
        /// </summary>
        /// <param name="rawValue">raw (string) value from command-line.</param>
        /// <returns>Parameter passed result.</returns>
        public override ParameterParseResult<int[]> ValidateParameterCommandLineRawValue(string rawValue)
        {
            char[] validCharacters = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', ',' };

            foreach (char stringChar in rawValue)
            {
                if (!validCharacters.Any(u => u.Equals(stringChar)))
                {
                    return ParameterParseResult<int[]>.CreateFailedResult(this, "Only digits and ',' are allowed!");
                }
            }

            return ParameterParseResult<int[]>.CreateSuccessResult(this, null);
        }

        public override string GetHelpText()
        {
            return "Specify page numbers, separated by ','. For example: 1,2,3,4";
        }

        /// <summary>
        /// Parse parameter value - digits separated by ','.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        /// <returns>Parameter parse result.</returns>
        public override ParameterParseResult<int[]> Parse(string[] args)
        {
            string rawValue = this.GetStringValue(args);

            List<int> pagesNumbers = new List<int>();
            string[] pagesNumbersStrings = rawValue.Split(",", StringSplitOptions.RemoveEmptyEntries);

            foreach (string num in pagesNumbersStrings)
            {
                if (int.TryParse(num, out var result))
                {
                    pagesNumbers.Add(result);
                }
                else
                {
                    throw new CommandLineParseException($"Page numbers parsing error: {num} is not integer value.");
                }
            }

            return ParameterParseResult<int[]>.CreateSuccessResult(this, pagesNumbers.ToArray());
        }
    }
}
