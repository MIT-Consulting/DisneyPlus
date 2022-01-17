using System.Collections.Generic;
using System.Linq;

namespace DisneyPlus.Common
{
    public static class GenericExtensions
    {
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            foreach (var item in items)
                collection.Add(item);
        }

        public static bool IsAnyOf<T>(this T source, params T[] options) => options.Contains(source);
    } 
}
