using GroupDocs.Viewer.Cli.Common.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GroupDocs.Viewer.Cli.Common.Parameters
{
    /// <summary>
    /// Parameter parsers container - register your parameter instance here.
    /// </summary>
    public class ParameterParsersContainer
    {
        private readonly IList<IParameter> _parameters = new List<IParameter>();

        /// <summary>
        /// Register parameter for parse and validation.
        /// </summary>
        /// <typeparam name="T">Parameter output value type.</typeparam>
        /// <param name="parameter">parameter instance.</param>
        public void RegisterParameter<T>(Parameter<T> parameter)
        {
            _parameters.Add(parameter);
        }

        public IList<IParameter> GetList()
        {
            return _parameters;
        }

        /// <summary>
        /// Get parameter result by parameter type and parameter parse result type.
        /// </summary>
        /// <typeparam name="TParam">Parameter type.</typeparam>
        /// <typeparam name="T">Parameter output type.</typeparam>
        /// <returns>Typed parameter parse result.</returns>
        public ParameterParseResult<T> GetByParameterType<TParam, T>()
        {
            IParameter parameter = _parameters.FirstOrDefault(x => x is TParam);

            if (parameter == null)
            {
                throw new ParameterNotRegisteredException($"Error! Parameter with type name {typeof(TParam).Name} is not registered!");
            }

            Parameter<T> typedParameter = (parameter as Parameter<T>);

            return typedParameter.LastValidationResult;
        }

        /// <summary>
        /// Get parameters with invalid values.
        /// </summary>
        /// <returns>List of invalid (failed validation or parse) parameters.</returns>
        public List<IParameter> GetInvalidParameters()
        {
            return _parameters.Where(x => !x.IsValid).ToList();
        }

        /// <summary>
        /// Validate all parameters and get common validation result.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        /// <returns>Command-line parse result.</returns>
        public CommandLineParseResult ValidateAllParametersAndCheckIsValid(string[] args)
        {
            bool allValid = true;

            foreach (IParameter parameter in _parameters)
            {
                if (!parameter.ParseAndValidate(args))
                {
                    allValid = false;
                }
            }

            if (!allValid)
            {
                IList<IParameter> invalidParameters = GetInvalidParameters();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Following parameters invalid:");

                foreach (IParameter parameter in invalidParameters)
                {
                    string parameterName = string.IsNullOrEmpty(parameter.ShortParameterName) ?
                        parameter.ParameterName : $"{parameter.ParameterName}({parameter.ShortParameterName})";

                    sb.AppendLine($"{parameterName}: {parameter.GetValidationMessage()}");
                }

                string message = sb.ToString();

                return CommandLineParseResult.Failure(message);
            }

            return CommandLineParseResult.Successful();
        }
    }
}
