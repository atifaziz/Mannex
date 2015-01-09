#region License, Terms and Author(s)
//
// Mannex - Extension methods for .NET
// Copyright (c) 2009 Atif Aziz. All rights reserved.
//
//  Author(s):
//
//      Atif Aziz, http://www.raboof.com
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

namespace Mannex.Collections.Generic
{
    #region Imports

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="Dictionary{TKey,TValue}"/>.
    /// </summary>

    static partial class DictionaryExtensions
    {
        /// <summary>
        /// Finds the value for a key, returning the default value for 
        /// <typeparamref name="TKey"/> if the key is not present.
        /// </summary>

        [DebuggerStepThrough]
        public static TValue Find<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
        {
            return Find(dict, key, default(TValue));
        }

        /// <summary>
        /// Finds the value for a key, returning a given default value for 
        /// <typeparamref name="TKey"/> if the key is not present.
        /// </summary>

        [DebuggerStepThrough]
        public static TValue Find<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue @default)
        {
            if (dict == null) throw new ArgumentNullException("dict");
            TValue value;
            return dict.TryGetValue(key, out value) ? value : @default;
        }

        /// <summary>
        /// Gets the value associated with the specified key. If the key is 
        /// not present then a function is called back with the key to 
        /// determine the exception to be thrown.
        /// </summary>
        /// <remarks>
        /// If the function called when the key is not found returns a null 
        /// reference then a <see cref="KeyNotFoundException"/> is thrown.
        /// The same happens if a null reference is supplied for the 
        /// function.
        /// </remarks>

        [DebuggerStepThrough]
        public static TValue GetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TKey, Exception> errorSelector)
        {
            if (dictionary == null) throw new ArgumentNullException("dictionary");

            TValue value;
            if (!dictionary.TryGetValue(key, out value))
                throw (errorSelector != null ? errorSelector(key) : null) ?? new KeyNotFoundException();

            return value;
        }

        #if NET45

        /// <summary>
        /// Returns a <see cref="ReadOnlyDictionary{TKey,TValue}"/> that 
        /// wraps this dictionary, rendering it effectively read-only.
        /// </summary>

        [DebuggerStepThrough]
        public static ReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            if (dictionary == null) throw new ArgumentNullException("dictionary");
            return new ReadOnlyDictionary<TKey, TValue>(dictionary);
        }

        #else

        /// <summary>
        /// Returns an <see cref="IDictionary{TKey,TValue}"/> that wraps this
        /// dictionary and disallows any modifications.
        /// </summary>

        [DebuggerStepThrough]
        public static IDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            if (dictionary == null) throw new ArgumentNullException("dictionary");
            return new ReadOnlyDictionary<TKey, TValue>(dictionary);
        }

        // Adapted from http://stackoverflow.com/a/1269311/6682

        [Serializable]
        [DebuggerDisplayAttribute("Count = {Count}")]
        partial class ReadOnlyDictionary<TKey, TValue> : IDictionary<TKey, TValue>
        {
            readonly IDictionary<TKey, TValue> _inner;

            public ReadOnlyDictionary() : this(new Dictionary<TKey, TValue>()) {}
            public ReadOnlyDictionary(IDictionary<TKey, TValue> inner) { _inner = inner; }

            public int Count { get { return _inner.Count; } }
            public bool IsReadOnly { get { return true; } }

            public bool ContainsKey(TKey key) { return _inner.ContainsKey(key); }
            public ICollection<TKey> Keys { get { return _inner.Keys; } }
            public bool TryGetValue(TKey key, out TValue value) { return _inner.TryGetValue(key, out value); }
            public ICollection<TValue> Values { get { return _inner.Values; } }
            public TValue this[TKey key] { get { return _inner[key]; } }
            TValue IDictionary<TKey, TValue>.this[TKey key] { get { return this[key]; }
                                                              set { throw ReadOnlyException(); } }
            public bool Contains(KeyValuePair<TKey, TValue> item) { return _inner.Contains(item); }
            public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) { _inner.CopyTo(array, arrayIndex); }
            public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() { return _inner.GetEnumerator(); }
            IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

            // Forbidden members...

            void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item) { throw ReadOnlyException(); }
            void ICollection<KeyValuePair<TKey, TValue>>.Clear() { throw ReadOnlyException(); }
            bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item) { throw ReadOnlyException(); }
            void IDictionary<TKey, TValue>.Add(TKey key, TValue value) { throw ReadOnlyException(); }
            bool IDictionary<TKey, TValue>.Remove(TKey key) { throw ReadOnlyException(); }

            static Exception ReadOnlyException() { return new NotSupportedException("Dictionary is read-only."); }
        }

        #endif
    }
}