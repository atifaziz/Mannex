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

    #endregion

    /// <summary>
    /// Extension methods for <see cref="string"/>.
    /// </summary>

    static partial class StringExtensions
    {
        /// <summary>
        /// Splits a string into a key and a value part using a specified 
        /// character to separate the two.
        /// </summary>
        /// <remarks>
        /// The key or value of the resulting pair is never <c>null</c>.
        /// </remarks>

        public static KeyValuePair<string, string> SplitPair(this string str, char separator)
        {
            if (str == null) throw new ArgumentNullException("str");
            
            return SplitPair(str, str.IndexOf(separator), 1);
        }

        /// <summary>
        /// Splits a string into a key and a value part using any of a 
        /// specified set of characters to separate the two.
        /// </summary>
        /// <remarks>
        /// The key or value of the resulting pair is never <c>null</c>.
        /// </remarks>

        public static KeyValuePair<string, string> SplitPair(this string str, params char[] separators)
        {
            if (str == null) throw new ArgumentNullException("str");

            return separators == null || separators.Length == 0 
                       ? new KeyValuePair<string, string>(str, string.Empty) 
                       : SplitPair(str, str.IndexOfAny(separators), 1);
        }

        /// <summary>
        /// Splits a string into a key and a value part by removing a
        /// portion of the string.
        /// </summary>
        /// <remarks>
        /// The key or value of the resulting pair is never <c>null</c>.
        /// </remarks>

        public static KeyValuePair<string, string> SplitPair(this string str, int index, int count)
        {
            if (str == null) throw new ArgumentNullException("str");
            if (count <= 0) throw new ArgumentOutOfRangeException("count", count, null);

            return new KeyValuePair<string, string>(
                /* key   */ index < 0 ? str : str.Substring(0, index),
                /* value */ index < 0 || index + 1 >= str.Length
                ? string.Empty
                : str.Substring(index + count));
        }

        /// <summary>
        /// Removes all leading and trailing occurrences of a set of 
        /// characters specified in an array from the key and value.
        /// </summary>
        /// <remarks>
        /// The key or value of the resulting pair is never <c>null</c>.
        /// </remarks>

        public static KeyValuePair<string, string> Trim(this KeyValuePair<string, string> pair, params char[] chars)
        {
            return new KeyValuePair<string, string>(
                (pair.Key ?? string.Empty).Trim(chars),
                (pair.Value ?? string.Empty).Trim(chars));
        }
    }
}