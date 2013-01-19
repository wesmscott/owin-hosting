using System;
using System.Collections.Generic;
using System.Linq;

namespace Owin.Types
{
    public partial struct OwinOpaqueParameters
    {
        private readonly IDictionary<string, object> _dictionary;

        public OwinOpaqueParameters(IDictionary<string, object> dictionary)
        {
            _dictionary = dictionary;
        }

        public IDictionary<string, object> Dictionary
        {
            get { return _dictionary; }
        }

        public T Get<T>(string key)
        {
            object value;
            return _dictionary.TryGetValue(key, out value) ? (T)value : default(T);
        }

        public OwinOpaqueParameters Set(string key, object value)
        {
            _dictionary[key] = value;
            return this;
        }

    }
}
