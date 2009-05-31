namespace Mannex.Collections.Generic
{
    #region Imports

    using System;
    using System.Collections.Generic;

    #endregion

    static partial class DictionaryExtensions
    {
        public static TValue Find<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
        {
            return Find(dict, key, default(TValue));
        }

        public static TValue Find<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue @default)
        {
            if (dict == null) throw new ArgumentNullException("dict");
            TValue value;
            return dict.TryGetValue(key, out value) ? value : @default;
        }
    }
}