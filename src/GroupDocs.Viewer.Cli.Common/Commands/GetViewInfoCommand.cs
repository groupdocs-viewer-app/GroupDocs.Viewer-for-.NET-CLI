using GroupDocs.Viewer.Cli.Common.Commands.Interfaces;
using GroupDocs.Viewer.Cli.Common.Enums;
using GroupDocs.Viewer.Cli.Common.Logging;
using GroupDocs.Viewer.Cli.Common.Parameters;
using GroupDocs.Viewer.Cli.Common.Parameters.Implementations;
using GroupDocs.Viewer.Cli.Common.Utils;
using GroupDocs.Viewer.Cli.Utils;
using GroupDocs.Viewer.Options;
using System.Collections.Generic;

namespace GroupDocs.Viewer.Cli.Common.Commands
{
    /// <summary>
    /// Get info for file.
    /// </summary>
    internal class GetViewInfoCommand : ICommand
    {
        /// <summary>
        /// Source file name for render.
        /// </summary>
        private string SourceFileName { get; set; }

        /// <summary>
        /// Get info command type.
        /// </summary>
        public CommandType CommandType => CommandType.GetViewInfo;

        /// <summary>
        /// Render output format (html, html with embedded resources, PNG, JPG, PDF).
        /// </summary>
        private OutputFormat OutputFormat { get; set; } = OutputFormat.Html;

        /// <summary>
        /// License path.
        /// </summary>
        private string LicensePath { get; set; }

        /// <summary>
        /// Execute method of command.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        public void Execute(string[] args = null)
        {
            // If license path set - set license for product.
            if (!string.IsNullOrEmpty(LicensePath))
            {
                License license = new License();
                license.SetLicense(LicensePath);
                Reporter.Output.WriteLine("License set.");
            }

            using Viewer viewer = CommandContext.IsVerbose()
                ? new Viewer(SourceFileName, new ViewerSettings(new CliLogger()))
                : new Viewer(SourceFileName);

            ViewInfoOptions viewOptions = null;
            string viewInfoDescription = string.Empty;

            if (OutputFormat == OutputFormat.Html ||
                OutputFormat == OutputFormat.HtmlExt)
            {
                viewOptions = ViewInfoOptions.ForHtmlView();
                viewInfoDescription = "HTML view";
            }
            else if (OutputFormat == OutputFormat.Jpg)
            {
                viewOptions = ViewInfoOptions.ForJpgView(false);
                viewInfoDescription = "JPEG view";
            }
            else if (OutputFormat == OutputFormat.Png)
            {
                viewOptions = ViewInfoOptions.ForPngView(false);
                viewInfoDescription = "PNG view";
            }
            else if (OutputFormat == OutputFormat.Pdf)
            {
                // There is no specific view info options for PDF
                viewOptions = ViewInfoOptions.ForHtmlView();
                viewInfoDescription = "PDF view";
            }

            // Get info for file.
            Results.ViewInfo info = viewer.GetViewInfo(viewOptions);

            // Write file info to console.
            Reporter.Output.WriteLine("File info for " + viewInfoDescription + ":");
            Reporter.Output.WriteLine("File type: " + info.FileType);
            Reporter.Output.WriteLine("Pages count: " + info.Pages.Count);
        }

        /// <summary>
        /// Parse command-line.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        /// <returns>Returns parse result arguments.    </returns>
        public CommandLineParseResult Parse(string[] args)
        {
            // Register parameters in container.
            ParameterParsersContainer parameterParsersContainer = CreateParametersContainer();

            // Parse parameter result.
            CommandLineParseResult parameterParsersValidationResult = parameterParsersContainer.ValidateAllParametersAndCheckIsValid(args);

            // Continue only parameters valid.
            if (!parameterParsersValidationResult.Success)
            {
                return parameterParsersValidationResult;
            }

            OutputFormat = parameterParsersContainer.GetByParameterType<OutputFormatParameter, OutputFormat>().ResultValue;

            // Check parameters count.
            if (args.Length < 1)
                return CommandLineParseResult.Failure($"{MainCommands.GetViewInfoCommand} command should have at least 1 arguments");

            // Parameters syntax and count are validated - returning success result.
            SourceFileName = args[1];
            LicensePath = parameterParsersContainer.GetByParameterType<LicensePathParameter, string>().ResultValue;

            return CommandLineParseResult.Successful();
        }

        private ParameterParsersContainer CreateParametersContainer()
        {
            ParameterParsersContainer parameterParsersContainer = new ParameterParsersContainer();
            parameterParsersContainer.RegisterParameter(new LicensePathParameter());
            parameterParsersContainer.RegisterParameter(new OutputFormatParameter());
            parameterParsersContainer.RegisterParameter(new VerboseParameter());
            return parameterParsersContainer;
        }

        /// <summary>
        /// Display help for get-view-info command.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        public void DisplayHelp(string[] args)
        {
            DisplayHelpHelper.DisplayHelp(args, CreateParametersContainer());
        }
    }
}

