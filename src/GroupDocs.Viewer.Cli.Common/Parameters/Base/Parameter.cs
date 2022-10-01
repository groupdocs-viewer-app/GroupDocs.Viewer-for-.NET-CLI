using GroupDocs.Viewer.Cli.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GroupDocs.Viewer.Cli.Common.Parameters
{
    /// <summary>
    /// Parameter base class.
    /// </summary>
    /// <typeparam name="T">Output type for parsed parameter output.</typeparam>
    public abstract class Parameter<T> : IParameter
    {
        /// <summary>
        /// Valid values list. No validation if empty.
        /// </summary>
        protected List<string> ValidValues { get; set; } = new List<string>();

        /// <summary>
        /// Parameter name - eg. help.
        /// </summary>
        public abstract string ParameterName { get; }

        /// <summary>
        /// Short parameter name - eg. h.
        /// </summary>
        public abstract string ShortParameterName { get; }

        /// <summary>
        /// Parameter help description - eg. Display help.
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// Fill valid values list of parameter, if it not filled - parameter allow any values. 
        /// </summary>
        public abstract void FillValidValues();

        /// <summary>
        /// Parse commandline arguments implementation.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        /// <returns>Typed parameter parse result.</returns>
        public abstract ParameterParseResult<T> Parse(string[] args);

        /// <summary>
        /// Default parameter value.
        /// </summary>
        protected virtual T DefaultValue { get; }

        /// <summary>
        /// Last validation parameter result.
        /// </summary>
        public ParameterParseResult<T> LastValidationResult { get; private set; }

        #region Constructors
        protected Parameter() { Init(); }
        #endregion

        #region Public
        /// <summary>
        /// Get help text.
        /// </summary>
        /// <returns>Help text string.</returns>
        public virtual string GetHelpText()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Valid values:");

            foreach (string validValues in ValidValues)
            {
                sb.AppendLine(validValues);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Validate command-line parameter raw(string) value.
        /// </summary>
        /// <param name="rawValue"></param>
        /// <returns>Parameter parse result.</returns>
        public virtual ParameterParseResult<T> ValidateParameterCommandLineRawValue(string rawValue)
        {
            // If valid list is not empty - filter invalid values.
            if (ValidValues.Count > 0 && !string.IsNullOrWhiteSpace(rawValue) && !CheckValueIsValid(rawValue))
            {
                return ParameterParseResult<T>.CreateFailedResult(this,
                    $"value {rawValue} is invalid, valid values: \n {GetValidValuesListString()} ");

            }

            return ParameterParseResult<T>.CreateSuccessResult(this, default(T));
        }

        /// <summary>
        /// Validate parameter parsed typized value.
        /// </summary>
        /// <param name="parsedValue">Parsed value.</param>
        /// <returns>Typized validation result.</returns>
        public virtual ParameterParseResult<T> ValidateParameterParsedValue(T parsedValue)
        {
            // By default - there is not validation.
            return ParameterParseResult<T>.CreateSuccessResult(this, parsedValue);
        }

        /// <summary>
        /// Parse and validate.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        /// <returns>Validation passed or not.</returns>
        public bool ParseAndValidate(string[] args)
        {
            string rawValue = GetStringValue(args);

            // Check raw (string) value of parameter valid
            LastValidationResult = ValidateParameterCommandLineRawValue(rawValue);

            if (!LastValidationResult.Success)
            {
                return false;
            }

            // Parse
            LastValidationResult = Parse(args);

            if (LastValidationResult.Success)
            {
                // Validate parsed parameter value.
                LastValidationResult = ValidateParameterParsedValue(LastValidationResult.ResultValue);
            }

            // Set default value if it not set and parse is success
            if (DefaultValue != null && LastValidationResult.Success && LastValidationResult.ResultValue == null)
            {
                LastValidationResult.ResultValue = DefaultValue;
            }

            return LastValidationResult.Success;
        }

        /// <summary>
        /// Display help text.
        /// </summary>
        public void DisplayHelp()
        {
            Reporter.Output.WriteLine(Description);
            Reporter.Output.WriteLine(GetHelpText());
        }

        /// <summary>
        /// Parameter is valid or not.
        /// </summary>
        public bool IsValid
        {
            get
            {
                if (LastValidationResult == null)
                {
                    return false;
                }

                return LastValidationResult.Success;

            }
        }

        /// <summary>
        /// Get validation message.
        /// </summary>
        /// <returns>Validation message string.</returns>
        public string GetValidationMessage()
        {
            if (LastValidationResult != null)
            {
                return LastValidationResult.ValidationMessage;
            }

            return string.Empty;
        }
        
        #endregion

        #region Private

        /// <summary>
        /// Get valid values list string for help text display.
        /// </summary>
        /// <returns>Valid values list string.</returns>
        private string GetValidValuesListString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string validValue in ValidValues)
            {
                sb.AppendLine(validValue);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Parameter init method.
        /// </summary>
        private void Init()
        {
            FillValidValues();
        }

        /// <summary>
        /// Get parameter string value.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        /// <returns>Parameter string value.</returns>
        protected string GetStringValue(string[] args)
        {
            string value = string.Empty;

            for (int i = 0; i < args.Length - 1; i++)
            {
                if (args[i].Equals("-" + ShortParameterName,
                    StringComparison.InvariantCultureIgnoreCase) ||
                    args[i].Equals("--" + ParameterName, StringComparison.InvariantCultureIgnoreCase))
                {
                    value = args[i + 1];
                }
            }

            return value;
        }

        /// <summary>
        /// Check parameter exist in command-line.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        /// <returns>true/false.</returns>
        protected bool ThisParameterExistInCommandLine(string[] args)
        {

            bool result = args.Any(x =>
                x.Equals("-" + ShortParameterName, StringComparison.InvariantCultureIgnoreCase) ||
                x.Equals("--" + ParameterName, StringComparison.InvariantCultureIgnoreCase));

            return result;
        }

        protected virtual bool CheckValueIsValid(string value)
        {
            return ValidValues.Contains(value);
        }

        public virtual void Execute(Viewer viewer)
        {
            // Override if need.
        }
        #endregion
    }
}
