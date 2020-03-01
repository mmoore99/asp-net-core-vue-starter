using System;
using Microsoft.Extensions.Caching.Memory;

namespace Fbits.VueMpaTemplate.Helpers.Extensions
{
    public static class MemoryCacheExtensions
    {
        public static T AddOrGetExisting<T>(this MemoryCache cache, string key, Func<T> valueFactory, DateTime expirationAt) where T : new()
        {
            if (cache.TryGetValue(key, out T cacheEntry)) return cacheEntry;

            var newCacheObject = valueFactory();
            cache.Set(key, newCacheObject, expirationAt);
            return newCacheObject;
        }
    }
}