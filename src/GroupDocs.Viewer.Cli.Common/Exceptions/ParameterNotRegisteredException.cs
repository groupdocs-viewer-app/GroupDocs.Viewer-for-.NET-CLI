using System;


namespace GroupDocs.Viewer.Cli.Common.Exceptions
{
    /// <summary>
    /// Parameter not registered exception.
    /// </summary>
    public class ParameterNotRegisteredException : CliApplicationException
    {
        /// <summary>
        /// Parameter not registered exception.
        /// </summary>
        /// <param name="message"></param>
        public ParameterNotRegisteredException(string message) : base(message) { }

    }
}
