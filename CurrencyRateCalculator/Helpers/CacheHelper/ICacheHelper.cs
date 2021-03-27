using System;

namespace ExampleApi.Helpers
{
    public interface ICacheHelper
    {
        bool TryGetValue<T>(string key, out T cacheEntry);
        void InsertEntry<T>(string key, T data, TimeSpan expiration);
    }
}