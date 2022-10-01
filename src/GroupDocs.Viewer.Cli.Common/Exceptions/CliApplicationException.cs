using System;
using System.Runtime.Serialization;

namespace GroupDocs.Viewer.Cli.Common.Exceptions
{
    /// <summary>
    /// Base application exception.
    /// </summary>
    [Serializable]
    public class CliApplicationException : Exception
    {
        public CliApplicationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandLineParseException"/> class with a specified error message.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The streaming context.</param>
        protected CliApplicationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
