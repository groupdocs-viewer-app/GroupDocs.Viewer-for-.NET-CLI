using GroupDocs.Viewer.Cli.Utils;
using System.Collections.Generic;
using System.Text;

namespace GroupDocs.Viewer.Cli.Common.Parameters
{
    /// <summary>
    /// Parameter for get attachment list.
    /// </summary>
    public class AttachmentsListParameter : Parameter<bool>
    {
        public override string ParameterName => "attachments-list";

        public override string ShortParameterName => null;

        public override string Description => "Get attachments list for selected file if it supported.";

        public override void FillValidValues() { }

        public override ParameterParseResult<bool> Parse(string[] args)
        {
            return ParameterParseResult<bool>.CreateSuccessResult(this, ThisParameterExistInCommandLine(args));
        }

        public override string GetHelpText()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Get list of attachments for selected file.");
            sb.AppendLine("For archives: save selected archive entry.");
            sb.AppendLine("For E-Mail messages: save attachment of selected message.");

            return sb.ToString();
        }

        public override void Execute(Viewer viewer)
        {
            Reporter.Output.WriteLine("Attachments list:");
            IList<Results.Attachment> attachmentsList = viewer.GetAttachments();

            foreach (Results.Attachment attachment in attachmentsList)
            {
                Reporter.Output.WriteLine(string.Format("Name: {0} ID: {1} Size: {2}", attachment.FileName, attachment.Id, attachment.Size));
            }
        }
    }
}
