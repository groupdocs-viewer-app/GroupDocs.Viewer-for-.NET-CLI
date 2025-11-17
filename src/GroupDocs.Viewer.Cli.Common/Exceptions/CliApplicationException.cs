using System;


namespace GroupDocs.Viewer.Cli.Common.Exceptions
{
    /// <summary>
    /// Base application exception.
    /// </summary>
    public class CliApplicationException : Exception
    {
        public CliApplicationException(string message) : base(message)
        {
        }

    }
}
