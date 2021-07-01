using System.Diagnostics;
using System.Reflection;

namespace GroupDocs.Viewer.Cli
{
    public class Product
    {
        public static readonly string Version = GetProductVersion();

        private static string GetProductVersion()
        {
            Assembly viewerAssembly = Assembly.GetAssembly(typeof(GroupDocs.Viewer.License));
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(viewerAssembly.Location);
            
            return fileVersionInfo.FileVersion;
        }
    }
}