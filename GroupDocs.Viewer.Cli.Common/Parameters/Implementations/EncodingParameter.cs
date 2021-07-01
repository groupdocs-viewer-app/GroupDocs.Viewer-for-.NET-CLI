using GroupDocs.Viewer.Cli.Utils;
using System;
using System.Text;

namespace GroupDocs.Viewer.Cli.Common.Parameters
{
    /// <summary>
    /// Parameter for set document encoding.
    /// </summary>
    public class EncodingParameter : Parameter<Encoding>
    {
        public override string ParameterName => "encoding";

        public override string ShortParameterName => "enc";

        public override string Description => "Source document encoding e.g 'UTF-8'";

        public override void FillValidValues()
        {
            EncodingInfo[] encodingsInfoList = Encoding.GetEncodings();

            foreach (EncodingInfo encInfo in encodingsInfoList)
            {
                ValidValues.Add(encInfo.Name);
            }
        }

        public override ParameterParseResult<Encoding> Parse(string[] args)
        {
            string rawValue = GetStringValue(args);
            Encoding encoding = null;

            if (!string.IsNullOrEmpty(rawValue))
            {
                try
                {
                    // All values should be validated by pre filled encodings info list.
                    encoding = Encoding.GetEncoding(rawValue);
                }
                catch (Exception ex)
                {
                    Reporter.Error.WriteLine(ex.Message);
                    return new ParameterParseResult<Encoding>() { Success = false, ValidationMessage = ex.Message };
                }

            }

            return new ParameterParseResult<Encoding>() { Success = true, ResultValue = encoding };
        }
    }
}
