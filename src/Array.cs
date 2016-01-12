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
    using System.Linq;
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
        /// Converts a one-dimensional array into a two-dimensional array of
        /// user-specified width.
        /// </summary>

        public static T[,] ToArray2D<T>(this T[] source, int columns)
        {
            return ToArray2DImpl(source, columns, default(T), fill: false);
        }

        /// <summary>
        /// Converts a one-dimensional array into a two-dimensional array of
        /// user-specified width. An additional parameter specifies the default
        /// value to use to fill the resulting array when source array has too
        /// few elements.
        /// </summary>

        public static T[,] ToArray2D<T>(this T[] source, int columns, T defaultValue)
        {
            return ToArray2DImpl(source, columns, defaultValue, fill: true);
        }

        static T[,] ToArray2DImpl<T>(T[] source, int columns, T defaultValue, bool fill)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (columns < 0) throw new ArgumentOutOfRangeException("source", columns, null);
            if (columns == 0) return new T[0, 0];

            var rows = source.Length / columns + (source.Length % columns == 0 ? 0 : 1);
            var target = new T[rows, columns];

            for (var i = 0; i < source.Length; i++)
                target[i / columns, i % columns] = source[i];

            if (fill && source.Length < target.Length
                     && !EqualityComparer<T>.Default.Equals(defaultValue, default(T)))
            {
                for (var i = source.Length; i < target.Length; i++)
                    target[i / columns, i % columns] = defaultValue;
            }
            return target;
        }

        /// <summary>
        /// Enumerates the elements at a specific row of a two-dimensional
        /// array.
        /// </summary>
        /// <remarks>
        /// This method uses deferred execution.
        /// </remarks>

        public static IEnumerable<T> Row<T>(this T[,] array, int index)
        {
            if (array == null) throw new ArgumentNullException("array");
            var zero = array.GetLowerBound(0);
            if (index < zero || index >= array.GetLength(0) - zero) throw new ArgumentOutOfRangeException("index", index, null);
            return RowImpl(array, index);
        }

        static IEnumerable<T> RowImpl<T>(T[,] array, int y)
        {
            return from x in Enumerable.Range(array.GetLowerBound(1), array.GetLength(1))
                   select array[y, x];
        }

        /// <summary>
        /// Enumerates the elements at a specific columns of a two-dimensional
        /// array.
        /// </summary>
        /// <remarks>
        /// This method uses deferred execution.
        /// </remarks>

        public static IEnumerable<T> Column<T>(this T[,] array, int index)
        {
            if (array == null) throw new ArgumentNullException("array");
            var zero = array.GetLowerBound(1);
            if (index < zero || index >= array.GetLength(1) - zero) throw new ArgumentOutOfRangeException("index", index, null);
            return ColumnImpl(array, index);
        }

        static IEnumerable<T> ColumnImpl<T>(T[,] array, int x)
        {
            return from y in Enumerable.Range(array.GetLowerBound(0), array.GetLength(0))
                   select array[y, x];
        }
    }
}
