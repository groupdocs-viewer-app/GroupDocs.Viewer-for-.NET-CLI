using System.Diagnostics;
using System.Reflection;

namespace GroupDocs.Viewer.Cli
{
    public class Product
    {
        public static readonly string GroupDocsViewerVersion = GetGroupDocsViewerProductVersion();

        private static string GetGroupDocsViewerProductVersion()
        {
            Assembly viewerAssembly = Assembly.GetAssembly(typeof(GroupDocs.Viewer.License));
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(viewerAssembly?.Location ?? string.Empty);

            return fileVersionInfo.FileVersion;
        }

        public static readonly string CLIVersion = GetCliProductVersion();

        private static string GetCliProductVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version?.ToString();
        }
    }
}