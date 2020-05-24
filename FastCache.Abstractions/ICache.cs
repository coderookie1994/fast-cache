using System;
using System.Collections.Generic;
using System.Text;

namespace FastCache.Abstractions
{
    public interface ICache
    {
        void Insert(string key, object value);
        object Get(string key);
    }
}
