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
    using System.Text;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="Array"/> sub-types.
    /// </summary>

    static partial class ArrayExtensions
    {
        /// <summary>
        /// Formats bytes in hexadecimal format, appending to the 
        /// supplied <see cref="StringBuilder"/>.
        /// </summary>
        
        public static string ToHex(this byte[] buffer)
        {
            return ToHex(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// Formats bytes in hexadecimal format, appending to the 
        /// supplied <see cref="StringBuilder"/>.
        /// </summary>
        
        public static string ToHex(this byte[] buffer, int index, int count)
        {
            return ToHex(buffer, index, count, null).ToString();
        }

        /// <summary>
        /// Formats bytes in hexadecimal format, appending to the 
        /// supplied <see cref="StringBuilder"/>.
        /// </summary>
        
        public static StringBuilder ToHex(this byte[] buffer, int index, int count, StringBuilder sb)
        {
            if (buffer == null) throw new ArgumentNullException("buffer");
            if (index < 0 || index > buffer.Length) throw new ArgumentOutOfRangeException("index");
            if (index + count > buffer.Length) throw new ArgumentOutOfRangeException("count");
            
            if (sb == null)
                sb = new StringBuilder(count * 2);

            for (var i = index; i < index + count; i++)
            {
                const string hexdigits = "0123456789abcdef";
                var b = buffer[i];
                sb.Append(hexdigits[b/16]);
                sb.Append(hexdigits[b%16]);
            }

            return sb;
        }

        /// <summary>
        /// Rotates the elements of the array (in-place) such that all 
        /// elements are shifted left by one position, with the exception of 
        /// the first element which assumes the last position in the array.
        /// </summary>
        
        public static void Rotate<T>(this T[] array)
        {
            if (array == null) throw new ArgumentNullException("array");
            if (array.Length == 0) return;
            var first = array[0];
            Array.Copy(array, 1, array, 0, array.Length - 1);
            array[array.Length - 1] = first;
        }
    }
}
