using GroupDocs.Viewer.Cli.Common.Parameters;
using GroupDocs.Viewer.Cli.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GroupDocs.Viewer.Cli.Common.Utils
{
    public static class DisplayHelpHelper
    {
        public static void DisplayHelp(string[] args, ParameterParsersContainer parameterParsersContainer)
        {
            IList<IParameter> parametersList = parameterParsersContainer.GetList();

            //TODO: display only basic parameters (not the command-specific one)
            foreach (var param in parametersList)
            {
                IParameter parameter = (Activator.CreateInstance(param.GetType()) as IParameter);

                if (string.IsNullOrEmpty(parameter.ShortParameterName))
                {
                    Reporter.Output.WriteLine(
                        $"  --{parameter.ParameterName}".PadRight(35) + $"{parameter.Description}");
                }
                else
                {
                    Reporter.Output.WriteLine(
                        $"  --{parameter.ParameterName}, -{parameter.ShortParameterName}".PadRight(35) + $"{parameter.Description}");
                }
            }

            foreach (string arg in args)
            {
                IParameter parameter = parametersList
                    .FirstOrDefault(x =>
                        ("--" + x.ParameterName).Equals(arg, StringComparison.InvariantCultureIgnoreCase) ||
                        ("-" + x.ShortParameterName).Equals(arg, StringComparison.InvariantCultureIgnoreCase));

                if (parameter != null)
                {
                    DisplayHelpForParameter(parameter);
                }
            }
        }

        private static void DisplayHelpForParameter(IParameter parameter)
        {
            Reporter.Output.WriteLine();
            Reporter.Output.WriteLine($"Help for parameter  --{parameter.ParameterName}, -{parameter.ShortParameterName} {parameter.Description}");
            Reporter.Output.WriteLine(parameter.GetHelpText());
        }
    }
}

