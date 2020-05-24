using System;
using FastCache.Abstractions;

namespace FastCache.Core
{
    internal sealed class Cache : ICache
    {
        public Cache()
        {
            
        }
        public void Insert(string key, object value)
        {
            throw new NotImplementedException();
        }

        public object Get(string key)
        {
            throw new NotImplementedException();
        }
    }
}
