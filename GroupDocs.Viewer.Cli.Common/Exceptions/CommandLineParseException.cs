using System;
using System.Runtime.Serialization;

namespace GroupDocs.Viewer.Cli.Common.Exceptions
{
    /// <summary>
    /// Command line parse exception.
    /// </summary>
    [Serializable]
    public class CommandLineParseException : CliApplicationException
    {
        /// <summary>
        /// Command line parse exception.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public CommandLineParseException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandLineParseException"/> class with a specified error message.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The streaming context.</param>
        protected CommandLineParseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
