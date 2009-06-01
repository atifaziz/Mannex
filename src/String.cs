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

namespace Mannex
{
    #region Imports

    using System;
    using System.Diagnostics;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="string"/>.
    /// </summary>

    static partial class StringExtensions
    {
        /// <summary>
        /// Masks an empty string with a given mask such that the result
        /// is never an empty string. If the input string is null or
        /// empty then it is masked, otherwise the original is returned.
        /// </summary>
        /// <remarks>
        /// Use this method to guarantee that you never get an empty
        /// string. Bear in mind, however, that if the mask itself is an 
        /// empty string then this method could yield an empty string!
        /// </remarks>

        [DebuggerStepThrough]
        public static string MaskEmpty(this string str, string mask)
        {
            return !string.IsNullOrEmpty(str) ? str : mask;
        }

        /// <summary>
        /// Returns a section of a string from a give starting point on.
        /// </summary>
        /// <remarks>
        /// If <paramref name="start"/> is negative, it is  treated as 
        /// <c>length</c> + <paramref name="start" /> where <c>length</c> 
        /// is the length of the string. If <paramref name="start"/>
        /// is greater or equal to the length of the string then 
        /// no characters are copied to the new string.
        /// </remarks>

        [DebuggerStepThrough]
        public static string Slice(this string str, int start)
        {
            return Slice(str, start, null);
        }

        /// <summary>
        /// Returns a section of a string.
        /// </summary>
        /// <remarks>
        /// This method copies up to, but not including, the element
        /// indicated by <paramref name="end"/>. If <paramref name="start"/> 
        /// is negative, it is  treated as <c>length</c> + <paramref name="start" /> 
        /// where <c>length</c> is the length of the string. If 
        /// <paramref name="end"/> is negative, it is treated as <c>length</c> + 
        /// <paramref name="end"/> where <c>length</c> is the length of the
        /// string. If <paramref name="end"/> occurs before <paramref name="start"/>, 
        /// no characters are copied to the new string.
        /// </remarks>

        [DebuggerStepThrough]
        public static string Slice(this string str, int start, int? end)
        {
            if (str == null) throw new ArgumentNullException("str");
            return SliceImpl(str, start, end ?? str.Length);
        }

        private static string SliceImpl(this string str, int start, int end)
        {
            if (str == null) throw new ArgumentNullException("str");
            var length = str.Length;

            if (start < 0)
            {
                start = length + start;
                if (start < 0)
                    start = 0;
            }
            else
            {
                if (start > length)
                    start = length;
            }

            if (end < 0)
            {
                end = length + end;
                if (end < 0)
                    end = 0;
            }
            else
            {
                if (end > length)
                    end = length;
            }

            var sliceLength = end - start;

            return sliceLength > 0 ?
                str.Substring(start, sliceLength) : string.Empty;
        }
    }
}