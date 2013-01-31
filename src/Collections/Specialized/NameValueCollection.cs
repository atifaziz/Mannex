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

namespace Mannex.Collections.Specialized
{
    #region Imports

    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using Generic;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="NameValueCollection"/>.
    /// </summary>

    static partial class NameValueCollectionExtensions
    {
        /// <summary>
        /// Gets the values associated with the specified key from the 
        /// <see cref="NameValueCollection"/> combined into one 
        /// comma-separated list and then applies a function to convert it
        /// into a value of the return type. If the key is not found then
        /// the result is the default value of the return type.
        /// </summary>

        public static T TryGetValue<T>(this NameValueCollection collection, string key, Func<string, T> selector)
        {
            return collection.TryGetValue(key, default(T), selector);
        }

        /// <summary>
        /// Gets the values associated with the specified key from the 
        /// <see cref="NameValueCollection"/> combined into one 
        /// comma-separated list and then applies a function to convert it
        /// into a value of the return type. An additional parameter 
        /// specifies the default value to return instead.
        /// </summary>

        public static T TryGetValue<T>(this NameValueCollection collection, string key, T defaultValue, Func<string, T> selector)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            var value = collection[key];
            return value == null ? defaultValue : selector(value);
        }

        /// <summary>
        /// Create a <see cref="NameValueCollection"/> from a sequence of
        /// <see cref="KeyValuePair{String,String}"/>.
        /// </summary>

        public static NameValueCollection ToNameValueCollection(
            this IEnumerable<KeyValuePair<string, string>> source)
        {
            if (source == null) throw new ArgumentNullException("source");

            var collection = CreateCollection(source as ICollection<KeyValuePair<string, string>>);
            collection.Add(source);
            return collection;
        }

        /// <summary>
        /// Create a <see cref="NameValueCollection"/> from an <see cref="IEnumerable{T}"/>
        /// given a function to select the name and value of each <typeparamref name="T"/>
        /// in the source sequence.
        /// </summary>

        public static NameValueCollection ToNameValueCollection<T>(
            this IEnumerable<T> source,
            Func<T, string> nameSelector,
            Func<T, IEnumerable<string>> valuesSelector)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (nameSelector == null) throw new ArgumentNullException("nameSelector");
            if (valuesSelector == null) throw new ArgumentNullException("valuesSelector");

            var collection = CreateCollection(source as ICollection<T>);
            collection.Add(source, nameSelector, valuesSelector);
            return collection;
        }

        /// <summary>
        /// Adds items from an <see cref="IEnumerable{T}"/>,
        /// given a function to select the name and value of each <typeparamref name="T"/>
        /// in the source sequence.
        /// </summary>

        public static void Add<T>(
            this NameValueCollection collection,
            IEnumerable<T> source,
            Func<T, string> nameSelector,
            Func<T, IEnumerable<string>> valuesSelector)
        {
            if (collection == null) throw new ArgumentNullException("collection");
            if (source == null) throw new ArgumentNullException("source");
            if (nameSelector == null) throw new ArgumentNullException("nameSelector");
            if (valuesSelector == null) throw new ArgumentNullException("valuesSelector");

            var items = from item in source
                        from value in valuesSelector(item)
                        select nameSelector(item).AsKeyTo(value);

            collection.Add(items);
        }

        /// <summary>
        /// Adds items from a sequence of 
        /// <see cref="KeyValuePair{String,String}"/>.
        /// </summary>

        public static void Add(
            this NameValueCollection collection,
            IEnumerable<KeyValuePair<string, string>> source)
        {
            if (collection == null) throw new ArgumentNullException("collection");
            if (source == null) throw new ArgumentNullException("source");

            foreach (var item in source)
                collection.Add(item.Key, item.Value);
        }

        private static NameValueCollection CreateCollection<T>(ICollection<T> collection)
        {
            return collection != null 
                 ? new NameValueCollection(collection.Count) 
                 : new NameValueCollection();
        }

        /// <summary>
        /// Returns a new collection with only those entries where keys
        /// match a given predicate.
        /// </summary>

        public static T Filter<T>(this T collection, Func<string, bool> predicate)
            where T : NameValueCollection, new()
        {
            if (collection == null) throw new ArgumentNullException("collection");
            if (predicate == null) throw new ArgumentNullException("predicate");
            return collection.Filter(predicate, key => key);
        }

        /// <summary>
        /// Returns a new collection with only those entries where keys
        /// match a given predicate. An additional function provides 
        /// the keys projected in the new collection.
        /// </summary>

        public static T Filter<T>(this T collection, Func<string, bool> predicate, Func<string, string> keySelector)
            where T : NameValueCollection, new()
        {
            if (collection == null) throw new ArgumentNullException("collection");
            if (predicate == null) throw new ArgumentNullException("predicate");
            if (keySelector == null) throw new ArgumentNullException("keySelector");

            var selection =
                from i in Enumerable.Range(0, collection.Count)
                where predicate(collection.GetKey(i))
                from value in collection.GetValues(i)
                select keySelector(collection.GetKey(i)).AsKeyTo(value);

            var result = new T();
            result.Add(selection);
            return result;
        }

        /// <summary>
        /// Returns a new collection where the keys are prefixed by a given
        /// string. The keys in new collection are without the prefix.
        /// </summary>

        public static T FilterByPrefix<T>(this T collection, string prefix)
            where T : NameValueCollection, new()
        {
            if (collection == null) throw new ArgumentNullException("collection");
            return string.IsNullOrEmpty(prefix)
                 ? new T { collection }
                 : collection.Filter(key => key != null && key.StartsWith(prefix, StringComparison.OrdinalIgnoreCase), 
                                     key => key.Length == prefix.Length ? null : key.Substring(prefix.Length));
        }

        /// <summary>
        /// Updates this collection with another where values of existing
        /// keys are replaced but those of new ones added.
        /// </summary>

        public static void Update(this NameValueCollection collection, NameValueCollection source)
        {
            if (collection == null) throw new ArgumentNullException("collection");
            if (source == null) throw new ArgumentNullException("source");

            for (var i = 0; i < source.Count; i++)
            {
                var key = source.GetKey(i);
                collection.Remove(key);

                var values = source.GetValues(i);
                if (values != null)
                {
                    foreach (var value in values)
                        collection.Add(key, value);
                }
                else
                {
                    collection.Add(key, null);
                }
            }
        }

        /// <summary>
        /// Returns an <see cref="IEnumerable{T}"/> object that enumerates
        /// the entries of this collection as pairs of key-values.
        /// </summary>
        /// <remarks>
        /// This method uses deferred execution.
        /// </remarks>

        public static IEnumerable<KeyValuePair<string, string[]>> AsEnumerable(this NameValueCollection collection)
        {
            if (collection == null) throw new ArgumentNullException("collection");
            return collection.AsEnumerable((c, k, i) => k.AsKeyTo(c.GetValues(i)));
        }

        /// <summary>
        /// Returns an <see cref="IEnumerable{T}"/> object that enumerates
        /// the entries of this collection. An additional parameter
        /// determines how to project each entry given its containing 
        /// collection, key and index.
        /// </summary>
        /// <remarks>
        /// This method uses deferred execution.
        /// </remarks>

        public static IEnumerable<TResult> AsEnumerable<T, TResult>(this T collection, Func<T, string, int, TResult> selector) 
            where T : NameValueCollection
        {
            if (collection == null) throw new ArgumentNullException("collection");
            if (selector == null) throw new ArgumentNullException("selector");
            
            return from i in Enumerable.Range(0, collection.Count)
                   select collection.GetKey(i).AsKeyTo(i) into e
                   select selector(collection, e.Key, e.Value);
        }

        /// <summary>
        /// Determines whether a key exists in the <see cref="NameObjectCollectionBase.Keys"/>
        /// or not. The check is made without regard to case of the key.
        /// </summary>
    
        public static bool ContainsKey(this NameValueCollection collection, string name)
        {
            if (collection == null) throw new ArgumentNullException("collection");
            return collection.ContainsKey(name, null);
        }

        /// <summary>
        /// Determines whether a key exists in the <see cref="NameObjectCollectionBase.Keys"/>
        /// or not. An additional parameter sepcifies a <see cref="StringComparer"/>
        /// to use to compare keys (where <c>null</c> is allowed and defaults 
        /// to same as <see cref="StringComparer.OrdinalIgnoreCase"/>).
        /// </summary>

        public static bool ContainsKey(this NameValueCollection collection, string name, StringComparer comparer)
        {
            if (collection == null) throw new ArgumentNullException("collection");
            var keys = from string key in collection.Keys select key;
            return keys.Contains(name, comparer ?? StringComparer.OrdinalIgnoreCase);
        }
    }
}
