using GroupDocs.Viewer.Cli.Common.Commands.Interfaces;
using GroupDocs.Viewer.Cli.Common.Enums;
using GroupDocs.Viewer.Cli.Common.Logging;
using GroupDocs.Viewer.Cli.Common.Parameters;
using GroupDocs.Viewer.Cli.Common.Parameters.Implementations;
using GroupDocs.Viewer.Cli.Common.Utils;
using GroupDocs.Viewer.Cli.Utils;
using GroupDocs.Viewer.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GroupDocs.Viewer.Cli.Common.Commands
{
    /// <summary>
    /// View (render) document to HTML/PDF/PNG/JPG command implementation.
    /// </summary>
    internal class ViewCommand : ICommand
    {
        /// <summary>
        /// Source file name for view (render).
        /// </summary>
        private string SourceFileName { get; set; }

        /// <summary>
        /// Destination file name.
        /// </summary>
        private string DestinationFileName { get; set; } = "output_{0}.html";

        /// <summary>
        /// Page numbers.
        /// </summary>
        private int[] PagesNumbers { get; set; }

        /// <summary>
        /// File type for document load.
        /// </summary>
        private FileType FileType { get; set; } = FileType.Unknown;

        /// <summary>
        /// Output format to view (render) - By default we view document to HTML.
        /// </summary>
        private OutputFormat OutputFormat { get; set; } = OutputFormat.Html;

        /// <summary>
        /// License path.
        /// </summary>
        private string LicensePath { get; set; }

        /// <summary>
        /// Encoding for source document.
        /// </summary>
        private Encoding Encoding { get; set; }

        /// <summary>
        /// Password for password protected documents.
        /// </summary>
        private string Password { get; set; }

        /// <summary>
        /// Resource file format.
        /// </summary>
        private string ResourceFileFormat { get; set; }

        /// <summary>
        /// Resource URL format.
        /// </summary>
        private string ResourceUrlFormat { get; set; }

        /// <summary>
        /// Verbose logging enabled or not.
        /// </summary>
        private bool Verbose { get; set; }

        /// <summary>
        /// Indicate that List of attachments requested.
        /// </summary>
        private bool GetAttachmentsListRequested { get; set; }

        /// <summary>
        /// Max height for output image.
        /// </summary>
        int MaxHeight { get; set; }

        /// <summary>
        /// Max width for output image.
        /// </summary>
        int MaxWidth { get; set; }

        /// <summary>
        /// Output width for output image.
        /// </summary>
        int OutputWidth { get; set; }

        /// <summary>
        /// Output height for output image.
        /// </summary>
        int OutputHeight { get; set; }

        /// <summary>
        /// Attachments Ids List
        /// </summary>
        string[] AttachmentsIds { get; set; }

        /// <summary>
        /// Parameter parsers container.
        /// </summary>
        ParameterParsersContainer ParameterParsersContainer { get; set; }

        /// <summary>
        /// Viewer command type
        /// </summary>
        public CommandType CommandType { get => CommandType.View; }

        /// <summary>
        /// Execute command implementation.
        /// </summary>
        /// <param name="args">Arguments.</param>
        public void Execute(string[] args = null)
        {
            // If license path set - set license.
            if (!string.IsNullOrEmpty(LicensePath))
            {
                License license = new License();
                license.SetLicense(LicensePath);
                Reporter.Output.WriteLine("License set.");
            }

            if (!GetAttachmentsListRequested && AttachmentsIds.Length == 0)
            {
                // Display converting pages info.
                if (PagesNumbers == null || PagesNumbers.Length == 0)
                {
                    Reporter.Output.WriteLine("Converting all pages.");
                }
                else
                {
                    Reporter.Output.WriteLine($"Converting {string.Join(",", PagesNumbers)} pages");
                }
            }

            LoadOptions loadOptions = new LoadOptions()
            {
                Password = Password,
                FileType = FileType,
                Encoding = Encoding
            };

            // Initializing viewer with load options.
            using Viewer viewer = CommandContext.IsVerbose()
                ? new Viewer(SourceFileName, loadOptions, new ViewerSettings(new CliLogger()))
                : new Viewer(SourceFileName, loadOptions);

            // If list of attachments requested - show attachments list on screen.
            if (GetAttachmentsListRequested)
            {
                ParameterParsersContainer.GetByParameterType<AttachmentsListParameter, bool>().Parameter.Execute(viewer);

                return;
            }

            if (AttachmentsIds.Length > 0)
            {
                ParameterParsersContainer.GetByParameterType<SaveAttachmentsParameter, string[]>().Parameter.Execute(viewer);

                return;
            }

            // Set additional parameters for options.

            if (OutputFormat == OutputFormat.Html)
            {
                HtmlViewOptions options = HtmlViewOptions.
                    ForEmbeddedResources(DestinationFileName);

                options.ImageHeight = OutputHeight;
                options.ImageWidth = OutputWidth;
                options.ImageMaxHeight = MaxHeight;
                options.ImageMaxWidth = MaxWidth;

                View(viewer, options);
            }
            else if (OutputFormat == OutputFormat.HtmlExt)
            {
                HtmlViewOptions options = HtmlViewOptions.
                    ForExternalResources(DestinationFileName, ResourceFileFormat, ResourceUrlFormat);

                options.ImageHeight = OutputHeight;
                options.ImageWidth = OutputWidth;
                options.ImageMaxHeight = MaxHeight;
                options.ImageMaxWidth = MaxWidth;

                View(viewer, options);
            }
            else if (OutputFormat == OutputFormat.Jpg)
            {
                JpgViewOptions jpgViewOptions = new JpgViewOptions(DestinationFileName);

                jpgViewOptions.Width = OutputWidth;
                jpgViewOptions.Height = OutputHeight;
                jpgViewOptions.MaxWidth = MaxWidth;
                jpgViewOptions.MaxHeight = MaxHeight;

                View(viewer, jpgViewOptions);
            }
            else if (OutputFormat == OutputFormat.Png)
            {
                PngViewOptions pngViewOptions = new PngViewOptions(DestinationFileName);

                pngViewOptions.Width = OutputWidth;
                pngViewOptions.Height = OutputHeight;
                pngViewOptions.MaxWidth = MaxWidth;
                pngViewOptions.MaxHeight = MaxHeight;

                View(viewer, pngViewOptions);
            }
            else if (OutputFormat == OutputFormat.Pdf)
            {
                PdfViewOptions pdfViewOptions = new PdfViewOptions(DestinationFileName);
                pdfViewOptions.ImageHeight = OutputHeight;
                pdfViewOptions.ImageWidth = OutputWidth;
                pdfViewOptions.ImageMaxHeight = MaxHeight;
                pdfViewOptions.ImageMaxWidth = MaxWidth;

                View(viewer, pdfViewOptions);
            }
        }

        /// <summary>
        /// Convert document to selected format.
        /// </summary>
        /// <param name="viewer">Viewer object.</param>
        /// <param name="options">Options for view.</param>
        private void View(Viewer viewer, ViewOptions options)
        {
            // Calling different methods depending on pages numbers are supplied or not.
            if (PagesNumbers == null || PagesNumbers.Length == 0)
            {
                viewer.View(options);
            }
            else
            {
                viewer.View(options, PagesNumbers);
            }
        }

        /// <summary>
        /// Parse command-line arguments.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        /// <returns>Parse result.</returns>
        public CommandLineParseResult Parse(string[] args)
        {
            string[] thisCommandArguments = args.Skip(1).ToArray();

            // expect: view type, source, destination
            if (thisCommandArguments.Length < 1)
            {
                return CommandLineParseResult.Failure(
                    $"{MainCommands.ViewCommand} command should have at least 1 argument");
            }

            // Take source file name and creating parameters container.
            SourceFileName = thisCommandArguments[0];
            ParameterParsersContainer = CreateParameterContainer();

            CommandLineParseResult parameterParsersValidationResult = ParameterParsersContainer.ValidateAllParametersAndCheckIsValid(args);

            if (!parameterParsersValidationResult.Success)
            {
                return parameterParsersValidationResult;
            }

            // Take parsed and validated parameters values.
            SetParametersValues(ParameterParsersContainer);

            CommandContext.SetVerbose(Verbose);

            string fileType = ParameterParsersContainer.GetByParameterType<FileTypeParameter, string>().ResultValue;

            if (!string.IsNullOrEmpty(fileType))
            {
                FileType = FileType.FromExtension(fileType);
            }

            return CommandLineParseResult.Successful();
        }

        /// <summary>
        /// Set parsed parameters values.
        /// </summary>
        /// <param name="parameterParsersContainer">Parameter parses container.</param>
        private void SetParametersValues(ParameterParsersContainer parameterParsersContainer)
        {
            Encoding = parameterParsersContainer.GetByParameterType<EncodingParameter, Encoding>().ResultValue;
            LicensePath = parameterParsersContainer.GetByParameterType<LicensePathParameter, string>().ResultValue;
            Password = parameterParsersContainer.GetByParameterType<PasswordParameter, string>().ResultValue;
            PagesNumbers = parameterParsersContainer.GetByParameterType<PagesParameter, int[]>().ResultValue;
            MaxHeight = parameterParsersContainer.GetByParameterType<IntParameter, int>().ResultValue;
            MaxWidth = parameterParsersContainer.GetByParameterType<MaxWidthParameter, int>().ResultValue;
            OutputWidth = parameterParsersContainer.GetByParameterType<WidthParameter, int>().ResultValue;
            OutputHeight = parameterParsersContainer.GetByParameterType<HeightParameter, int>().ResultValue;
            ResourceFileFormat = parameterParsersContainer.GetByParameterType<ResourceFileFormatParameter, string>().ResultValue;
            ResourceUrlFormat = parameterParsersContainer.GetByParameterType<ResourceUrlFormatParameter, string>().ResultValue;
            Verbose = parameterParsersContainer.GetByParameterType<VerboseParameter, bool>().ResultValue;
            OutputFormat = parameterParsersContainer.GetByParameterType<OutputFormatParameter, OutputFormat>().ResultValue;
            DestinationFileName = parameterParsersContainer.GetByParameterType<OutputPathParameter, string>().ResultValue;
            GetAttachmentsListRequested = parameterParsersContainer.GetByParameterType<AttachmentsListParameter, bool>().ResultValue;
            AttachmentsIds = parameterParsersContainer.GetByParameterType<SaveAttachmentsParameter, string[]>().ResultValue;
        }

        /// <summary>
        /// Create and fill parameters container for register, parse and validation.
        /// </summary>
        /// <returns>Parameter parsers container.</returns>
        private static ParameterParsersContainer CreateParameterContainer()
        {
            ParameterParsersContainer parameterParsersContainer = new ParameterParsersContainer();
            parameterParsersContainer.RegisterParameter(new EncodingParameter());
            parameterParsersContainer.RegisterParameter(new LicensePathParameter());
            parameterParsersContainer.RegisterParameter(new PagesParameter());
            parameterParsersContainer.RegisterParameter(new FileTypeParameter());
            parameterParsersContainer.RegisterParameter(new MaxWidthParameter());
            parameterParsersContainer.RegisterParameter(new MaxHeightParameter());
            parameterParsersContainer.RegisterParameter(new HeightParameter());
            parameterParsersContainer.RegisterParameter(new WidthParameter());
            parameterParsersContainer.RegisterParameter(new ResourceFileFormatParameter());
            parameterParsersContainer.RegisterParameter(new ResourceUrlFormatParameter());
            parameterParsersContainer.RegisterParameter(new PasswordParameter());
            parameterParsersContainer.RegisterParameter(new VerboseParameter());
            parameterParsersContainer.RegisterParameter(new OutputFormatParameter());
            parameterParsersContainer.RegisterParameter(new OutputPathParameter());
            parameterParsersContainer.RegisterParameter(new AttachmentsListParameter());
            parameterParsersContainer.RegisterParameter(new SaveAttachmentsParameter());

            return parameterParsersContainer;
        }

        /// <summary>
        /// Display help for view command.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        public void DisplayHelp(string[] args)
        {
            DisplayHelpHelper.DisplayHelp(args, CreateParameterContainer());
        }
    }
}

