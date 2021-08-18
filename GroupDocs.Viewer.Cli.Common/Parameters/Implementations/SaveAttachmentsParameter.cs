using GroupDocs.Viewer.Cli.Common.Exceptions;
using GroupDocs.Viewer.Cli.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GroupDocs.Viewer.Cli.Common.Parameters
{
    /// <summary>
    /// Save attachments parameter.
    /// </summary>
    public class SaveAttachmentsParameter : Parameter<string[]>
    {
        public override string ParameterName => "save-attachments";

        public override string ShortParameterName => null;

        public override string Description => "Save attachments on disk if selected file support it.";

        public override void FillValidValues() { }

        private string[] AttachmentsIds { get; set; }

        /// <summary>
        /// Validate command-line parameter value - it should list of digits, separated by ','.
        /// </summary>
        /// <param name="rawValue">raw (string) value from command-line.</param>
        /// <returns>Parameter passed result.</returns>
        public override ParameterParseResult<string[]> ValidateParameterCommandLineRawValue(string rawValue)
        {
            char quoter = '\'';       // quotation mark
            string delimiter = " ,"; // either space or comma
            string regex = string.Format("((?<field>[^\\r\\n{1}{0}]*)|[{1}](?<field>([^{1}]|[{1}][{1}])*)[{1}])([{0}]|(?<rowbreak>\\r\\n|\\n|$))", delimiter, quoter);

            List<string> values = new List<string>();

            Regex re = new Regex(regex);
            foreach (Match m in re.Matches(rawValue))
            {
                string field = m.Result("${field}").Replace("\"\"", "\"").Trim();
                if (field != string.Empty)
                {
                    values.Add(field);
                }
            }

            return ParameterParseResult<string[]>.CreateSuccessResult(this, values.ToArray());
        }

        public override string GetHelpText()
        {
            return "Specify attachments Ids in quotes '', separated by ','. For example: 'file1.txt','ThirdFolderWithItems/sample-inside-folder.txt'";
        }

        public override void Execute(Viewer viewer)
        {
            Reporter.Output.WriteLine("Saving attachments...");

            IList<Results.Attachment> attachmentsList = viewer.GetAttachments();

            if (viewer.GetFileInfo().FileType == FileType.SevenZip)
            {
                Reporter.Error.WriteLine("Attachments saving is not supported for 7-Zip format");

                return;
            }

            // Get all attachments list and check.
            foreach (string attachmentId in AttachmentsIds)
            {
                if (!attachmentsList.Any(x => x.Id == attachmentId))
                {
                    Reporter.Error.WriteLine($"Error attachment with ID '{attachmentId}' is not found.");
                    return;
                }
            }

            // Attachments validated - getting and saving.
            foreach (string attachmentId in AttachmentsIds)
            {
                Results.Attachment attachment = attachmentsList.FirstOrDefault(x => x.Id == attachmentId);

                Reporter.Output.WriteLine($"Saving attachment with ID '{attachmentId}'.");

                using (System.IO.FileStream stream =
                    new System.IO.FileStream(attachment.FileName, System.IO.FileMode.OpenOrCreate))
                    viewer.SaveAttachment(attachment, stream);
            }
        }

        /// <summary>
        /// Parse parameter value - digits separated by ','.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        /// <returns>Parameter parse result.</returns>
        public override ParameterParseResult<string[]> Parse(string[] args)
        {
            char quoter = '\'';       // quotation mark
            string delimiter = " ,"; // either space or comma
            string regex = string.Format("((?<field>[^\\r\\n{1}{0}]*)|[{1}](?<field>([^{1}]|[{1}][{1}])*)[{1}])([{0}]|(?<rowbreak>\\r\\n|\\n|$))", delimiter, quoter);
            string rawValue = GetStringValue(args);

            List<string> values = new List<string>();

            Regex re = new Regex(regex);
            foreach (Match m in re.Matches(rawValue))
            {
                string field = m.Result("${field}").Replace("\"\"", "\"").Trim();

                if (field != string.Empty)
                {
                    values.Add(field);
                }
            }

            AttachmentsIds = values.ToArray();

            if (ThisParameterExistInCommandLine(args) && AttachmentsIds.Length == 0)
            {
                Reporter.Error.WriteLine("Attachments Ids are not supplied, please supply attachments id separated by, in quotes: \'test.txt\', 'test2.txt\'");
            }

            return ParameterParseResult<string[]>.CreateSuccessResult(this, values.ToArray());
        }
    }
}
