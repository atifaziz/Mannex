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
    using System.Collections.Generic;
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
            {
                throw (errorSelector != null ? errorSelector(key) : null) 
                   ?? new ArgumentException(KeyNotFoundSystemErrorMessage);
            }

            return value;
        }

        static string _frameworkKeyNotFoundErrorMessage;

        static string KeyNotFoundSystemErrorMessage
        {
            get { return _frameworkKeyNotFoundErrorMessage ?? (_frameworkKeyNotFoundErrorMessage = GetFrameworkKeyNotFoundErrorMessage().ToString()); }
        }

        static string GetFrameworkKeyNotFoundErrorMessage()
        {
            try
            {
                var result = new Dictionary<object, string>()[new object()];
                Debug.Fail("A bad assumption has been made. Code should have never reached here!");
                // ReSharper disable HeuristicUnreachableCode
                return result;
                // ReSharper restore HeuristicUnreachableCode
            }
            catch (ArgumentException e)
            {
                return e.Message;
            }
        } 
    }
}