using GroupDocs.Viewer.Cli.Utils;
using GroupDocs.Viewer.Logging;
using System;

namespace GroupDocs.Viewer.Cli.Common.Logging
{
    /// <summary>
    /// CLI logging implementation.
    /// </summary>
    public class CliLogger : ILogger
    {
        /// <summary>
        /// Log error message.
        /// </summary>
        /// <param name="message">Message text.</param>
        /// <param name="exception">Exception object.</param>
        public void Error(string message, Exception exception)
        {
            Reporter.Error.WriteLine($"[ERROR] Exception {exception.Message} - {message} ");
        }

        /// <summary>
        /// Log trace (info) message.
        /// </summary>
        /// <param name="message"></param>
        public void Trace(string message)
        {
            string infoPrefix = "[TRACE] ";
            Reporter.Verbose.WriteLine(infoPrefix + message);
        }

        /// <summary>
        /// Log warning message.
        /// </summary>
        /// <param name="message"></param>
        public void Warning(string message)
        {
            Reporter.Verbose.WriteLine("[WARNING] " + message);
        }
    }
}
