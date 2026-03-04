using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GroupDocs.Viewer.Cli;

public static class BrowserLauncher
{
    public static void Open(string url)
    {
        try
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("open", url);
            }
            else
            {
                Process.Start("xdg-open", url);
            }
        }
        catch
        {
            // Silently ignore — browser open is best-effort
        }
    }
}
