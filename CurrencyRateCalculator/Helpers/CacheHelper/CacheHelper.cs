using System;
using Microsoft.Extensions.Caching.Memory;

namespace ExampleApi.Helpers
{
    public class CacheHelper : ICacheHelper
    {
        private readonly IMemoryCache _cache;

        public CacheHelper(IMemoryCache cache)
        {
            _cache = cache;
        }
        
        public bool TryGetValue<T>(string key, out T cacheEntry)
        {
            return _cache.TryGetValue(key, out cacheEntry);
        }

        public void InsertEntry<T>(string key, T data, TimeSpan expiration)
        {
            T cacheEntry;
            if (!_cache.TryGetValue(key, out cacheEntry))
            {
                _cache.Set(key, data, expiration);
            }
        }
    }
}