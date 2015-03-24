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
    using System.Collections.Generic;
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

        /// <summary>
        /// Updates this array with results of applying a function to 
        /// elements paired from this and the source array.
        /// </summary>
        /// <remarks>
        /// The array is not updated if the application of the function
        /// fails on any pair of elements. The operation is thus atomic.
        /// </remarks>

        public static void Update<TTarget, TSource>(this TTarget[] target, IEnumerable<TSource> source, Func<TTarget, TSource, TTarget> function)
        {
            if (function == null) throw new ArgumentNullException("function");
            target.Update(source, (l, r, i) => function(l, r));
        }

        /// <summary>
        /// Updates this array with results of applying a function to 
        /// elements paired from this and the source array. An additional
        /// parameter to supplied function provides the index of the 
        /// elements.
        /// </summary>
        /// <remarks>
        /// The array is not updated if the application of the function
        /// fails on any pair of elements. The operation is thus atomic.
        /// </remarks>

        public static void Update<TTarget, TSource>(this TTarget[] target, IEnumerable<TSource> source, Func<TTarget, TSource, int, TTarget> function)
        {
            if (target == null) throw new ArgumentNullException("target");
            if (function == null) throw new ArgumentNullException("function");
            if (source == null) throw new ArgumentNullException("source");

            var results = new TTarget[target.Length];

            using (var e = source.GetEnumerator())
            for (var i = 0; i < target.Length; i++)
            {
                results[i] = e.MoveNext()
                           ? function(target[i], e.Current, i)
                           : target[i];
            }

            Array.Copy(results, 0, target, 0, results.Length);
        }
    
        /// <summary>
        /// Updates this array with results of applying a function to 
        /// elements paired from this and the source array.
        /// </summary>
        /// <remarks>
        /// The array is not updated if the application of the function
        /// fails on any pair of elements. The operation is thus atomic.
        /// </remarks>

        public static TTarget[] Updating<TTarget, TSource>(this TTarget[] target, IEnumerable<TSource> source, Func<TTarget, TSource, TTarget> function)
        {
            target.Update(source, function);
            return target;
        }

        /// <summary>
        /// Updates this array with results of applying a function to 
        /// elements paired from this and the source array. An additional
        /// parameter to supplied function provides the index of the 
        /// elements.
        /// </summary>
        /// <remarks>
        /// The array is not updated if the application of the function
        /// fails on any pair of elements. The operation is thus atomic.
        /// </remarks>

        public static TTarget[] Updating<TTarget, TSource>(this TTarget[] target, Func<TTarget, TSource, int, TTarget> function, IEnumerable<TSource> source)
        {
            target.Update(source, function);
            return target;
        }

        /// <summary>
        /// Removes an item from the array if it is found.
        /// </summary>
        /// <returns>
        /// Returns a new array with the item removed. If the item does not
        /// exist then the original array is returned.
        /// </returns>

        public static T[] Remove<T>(this T[] array, T item)
        {
            return Remove(array, item, null);
        }

        /// <summary>
        /// Removes an item from the array if it is found. An additional
        /// parameter specifies the comparer to use to identify the item to
        /// remove.
        /// </summary>
        /// <returns>
        /// Returns a new array with the item removed. If the item does not
        /// exist then the original array is returned.
        /// </returns>

        public static T[] Remove<T>(this T[] array, T item, IEqualityComparer<T> comparer)
        {
            if (array == null) throw new ArgumentNullException("array");

            var index = comparer == null
                      ? Array.IndexOf(array, item)
                      : Array.FindIndex(array, other => comparer.Equals(other, item));
            if (index < 0)
                return array;
            var result = new T[array.Length - 1];
            if (index > 0) Array.Copy(array, 0, result, 0, index);
            Array.Copy(array, index + 1, result, index, array.Length - (index + 1));
            return result;
        }
    }
}
