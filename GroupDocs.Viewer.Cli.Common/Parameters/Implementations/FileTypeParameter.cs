using System.Text;

namespace GroupDocs.Viewer.Cli.Common.Parameters
{
    /// <summary>
    /// Parameter for document source file type.
    /// </summary>
    public class FileTypeParameter : Parameter<string>
    {
        public override string ParameterName => "file-type";
        
        public override string ShortParameterName => null;

        public override string Description => "Source document file type e.g. 'DOCX'.";
        
        public override void FillValidValues()
        {
            foreach (FileType fileType in FileType.GetSupportedFileTypes())
            {
                ValidValues.Add(fileType.Extension.Replace(".", ""));
            }
        }

        public override string GetHelpText()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("With this option you can specify which format to use for load document, possible values are:");
            sb.AppendLine();
            foreach (FileType fileType in FileType.GetSupportedFileTypes())
            {
                sb.AppendLine($"{fileType.Extension.Replace(".","")} - {fileType.FileFormat}");
            }

            return sb.ToString();
        }

        public override ParameterParseResult<string> Parse(string[] args)
        {
            return ParameterParseResult<string>.CreateSuccessResult(this, GetStringValue(args));
        }
    }
}
