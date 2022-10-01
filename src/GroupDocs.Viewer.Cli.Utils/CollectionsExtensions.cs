using System.Collections.Generic;
using System.Linq;

namespace GroupDocs.Viewer.Cli.Utils
{
    public static class CollectionsExtensions
    {
        public static IEnumerable<T> OrEmptyIfNull<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null
                ? Enumerable.Empty<T>()
                : enumerable;
        }
    }
}