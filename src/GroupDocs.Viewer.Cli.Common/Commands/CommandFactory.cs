using GroupDocs.Viewer.Cli.Common.Commands.Interfaces;
using GroupDocs.Viewer.Cli.Common.Enums;

namespace GroupDocs.Viewer.Cli.Common.Commands
{
    /// <summary>
    /// Command factory - get command by CommandType enum
    /// </summary>
    public static class CommandFactory
    {
        /// <summary>
        /// Create command
        /// </summary>
        /// <param name="commandType">Command Type</param>
        /// <returns>Requested command by ICommand interface</returns>
        public static ICommand Create(CommandType commandType)
        {
            if (commandType == CommandType.View)
            {
                return new ViewCommand();
            }
            else if (commandType == CommandType.GetViewInfo)
            {
                return new GetViewInfoCommand();
            }

            return null;
        }
    }
}
