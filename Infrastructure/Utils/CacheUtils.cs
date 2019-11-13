/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using System.Threading;
using System.Threading.Tasks;
using Framework.Infrastructure.Utils;

namespace Microsoft.Extensions.Caching.Distributed
{
    public static class CacheUtils
    {
        public static void Set<T>(this IDistributedCache distributedCache, string key, T value, DistributedCacheEntryOptions options)
        {
            distributedCache.Set(key, value.ToByteArray(), options);
        }

        public static T Get<T>(this IDistributedCache distributedCache, string key)
            where T : class
        {
            var result = distributedCache.Get(key);
            return result.FromByteArray<T>();
        }

        public static async Task SetAsync<T>(this IDistributedCache distributedCache, string key, T value, DistributedCacheEntryOptions options, CancellationToken token = default(CancellationToken))
        {
            await distributedCache.SetAsync(key, value.ToByteArray(), options, token);
        }

        public static async Task<T> GetAsync<T>(this IDistributedCache distributedCache, string key, CancellationToken token = default(CancellationToken))
            where T : class
        {
            var result = await distributedCache.GetAsync(key, token);
            return result.FromByteArray<T>();
        }
    }
}
