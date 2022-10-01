using System;
using System.Runtime.Serialization;

namespace GroupDocs.Viewer.Cli.Common.Exceptions
{
    /// <summary>
    /// Parameter not registered exception.
    /// </summary>
    [Serializable]
    public class ParameterNotRegisteredException : CliApplicationException
    {
        /// <summary>
        /// Parameter not registered exception.
        /// </summary>
        /// <param name="message"></param>
        public ParameterNotRegisteredException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterNotRegisteredException"/> class with a specified error message.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The streaming context.</param>
        protected ParameterNotRegisteredException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
