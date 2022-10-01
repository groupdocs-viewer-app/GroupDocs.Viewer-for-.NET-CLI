namespace GroupDocs.Viewer.Cli.Common.Enums
{
    /// <summary>
    /// Viewer command type.
    /// </summary>
    public enum CommandType
    {
        /// <summary>
        /// View (render) document to selected output format (HTML,PDF,PNG,JPG).
        /// </summary>
        View,

        /// <summary>
        /// Get view info for source document (format, pages count).
        /// </summary>
        GetViewInfo,
    }
}
