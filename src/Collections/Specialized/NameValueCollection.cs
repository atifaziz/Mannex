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

        public static T Filter<T>(this T collection, Func<string, bool> predicate)
            where T : NameValueCollection, new()
        {
            if (collection == null) throw new ArgumentNullException("collection");
            if (predicate == null) throw new ArgumentNullException("predicate");
            return collection.Filter(key => predicate(key) ? key : null);
        }

        public static T Filter<T>(this T collection, Func<string, string> keySelector)
            where T : NameValueCollection, new()
        {
            if (collection == null) throw new ArgumentNullException("collection");
            if (keySelector == null) throw new ArgumentNullException("keySelector");

            var selection =
                from i in Enumerable.Range(0, collection.Count)
                let key = keySelector(collection.GetKey(i))
                where key != null
                from value in collection.GetValues(i)
                select key.AsKeyTo(value);

            var result = new T();
            result.Add(selection);
            return result;
        }

        public static T FilterByPrefix<T>(this T collection, string prefix)
            where T : NameValueCollection, new()
        {
            if (collection == null) throw new ArgumentNullException("collection");
            return string.IsNullOrEmpty(prefix)
                 ? new T { collection }
                 : collection.Filter(key => key.Length > prefix.Length && key.StartsWith(prefix, StringComparison.OrdinalIgnoreCase) ? key.Substring(prefix.Length) : null);
        }
    }
}
