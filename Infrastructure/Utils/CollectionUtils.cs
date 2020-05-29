/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System;
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

        public static List<string> ToTrimmedUpperCase(this List<string> source)
        {
            if (source == null || !source.Any())
                return source;
            for (int i = 0; i < source.Count; ++i)
            {
                source[i] = source[i].Trim().ToUpper();
            }

            return source;
        }

        public static List<string> Trim(this List<string> source)
        {
            if (source == null || !source.Any())
                return source;
            for (int i = 0; i < source.Count; ++i)
            {
                source[i] = source[i].Trim();
            }

            return source;
        }

        public static List<string> ToUpperCase(this List<string> source)
        {
            if (source == null || !source.Any())
                return source;
            for (int i = 0; i < source.Count; ++i)
            {
                source[i] = source[i].ToUpper();
            }

            return source;
        }

        public static List<string> OnlyInSource(this IEnumerable<string> source, IEnumerable<string> theOtherSource)
        {
            if (source == null || theOtherSource == null)
                return new List<string>();

            var intersects = source.Intersect(theOtherSource);

            return source.Where(p => !intersects.Any(p2 => p2 == p)).ToList();
        }

        public static List<TResult> SelectToList<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            if (source == null)
                return new List<TResult>();

            return source.Select<TSource, TResult>(selector).ToList();
        }

        public static List<TResult> SelectToList<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, int, TResult> selector)
        {
            if (source == null)
                return new List<TResult>();

            return source.Select<TSource, TResult>(selector).ToList();
        }

        public static List<TResult> SelectToDistinctList<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            if (source == null)
                return new List<TResult>();

            return source.Select<TSource, TResult>(selector).Distinct().ToList();
        }

        public static List<TResult> SelectToDistinctList<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, int, TResult> selector)
        {
            if (source == null)
                return new List<TResult>();

            return source.Select<TSource, TResult>(selector).Distinct().ToList();
        }
    }
}
