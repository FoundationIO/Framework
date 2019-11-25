/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System.Collections.Generic;
using System.Linq;

namespace Framework.Infrastructure.Utils
{
    public static class CollectionUtils
    {
        public static bool IsNotNullOrEmpty<TSource>(this IEnumerable<TSource> source)
        {
            if (!((source == null) || (!source.Any())))
                return true;
            return false;
        }

        public static bool IsNullOrEmpty<TSource>(this IEnumerable<TSource> source)
        {
            return !IsNotNullOrEmpty(source);
        }

        public static bool IsAllTrue(this IEnumerable<bool> source)
        {
            if (source == null || !source.Any())
                return false;

            foreach (var item in source)
            {
                if (!item)
                    return false;
            }

            return true;
        }
    }
}
