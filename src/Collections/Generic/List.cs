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
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="List{T}"/>.
    /// </summary>

    static partial class ListExtensions
    {
        /// <summary>
        /// Returns the index of the last item in the list or -1 if the
        /// list is empty.
        /// </summary>

        [DebuggerStepThrough]
        public static int LastIndex<T>(this IList<T> list)
        {
            if (list == null) throw new ArgumentNullException("list");
            return list.Count - 1;
        }

        //
        // Stack semantics
        //

        /// <summary>
        /// Treats list like a stack, pushing <paramref name="value"/>
        /// on to the list; in other words adding it to the end of 
        /// the list.
        /// </summary>

        [DebuggerStepThrough]
        public static void Push<T>(this IList<T> list, T value)
        {
            if (list == null) throw new ArgumentNullException("list");
            list.Add(value);
        }

        /// <summary>
        /// Treats list like a stack, popping (removing and returning)
        /// the last value on the list.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if list is empty.
        /// </exception>

        [DebuggerStepThrough]
        public static T Pop<T>(this IList<T> list)
        {
            if (list == null) throw new ArgumentNullException("list");

            var value = list.Last();
            list.RemoveAt(list.LastIndex());
            return value;
        }


        /// <summary>
        /// Treats list like a stack, popping (removing and returning)
        /// the last value on the list. If the list is empty, then
        /// the default value for <typeparamref name="T"/> is returned.
        /// </summary>

        [DebuggerStepThrough]
        public static T TryPop<T>(this IList<T> list)
        {
            return list.TryPop(default(T));
        }

        /// <summary>
        /// Treats list like a stack, popping (removing and returning)
        /// the last value on the list. If the list is empty, then
        /// <paramref name="emptyValue"/> is returned instead.
        /// </summary>

        [DebuggerStepThrough]
        public static T TryPop<T>(this IList<T> list, T emptyValue)
        {
            if (list == null) throw new ArgumentNullException("list");
            return list.Count > 0 ? list.Pop() : emptyValue;
        }

        /// <summary>
        /// Treats list like a stack, peeking and return the first
        /// value on the list.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if list is empty.
        /// </exception>

        [DebuggerStepThrough]
        public static T Peek<T>(this IList<T> list)
        {
            if (list == null) throw new ArgumentNullException("list");
            return list.First();
        }

        /// <summary>
        /// Treats list like a stack, peeking and return the first
        /// value on the list. If the list is empty, then
        /// the default value for <typeparamref name="T"/> is returned.
        /// </summary>

        [DebuggerStepThrough]
        public static T TryPeek<T>(this IList<T> list)
        {
            return list.TryPeek(default(T));
        }

        /// <summary>
        /// Treats list like a stack, peeking and return the first
        /// value on the list. If the list is empty, then
        /// <paramref name="emptyValue"/> is returned instead.
        /// </summary>

        public static T TryPeek<T>(this IList<T> list, T emptyValue)
        {
            if (list == null) throw new ArgumentNullException("list");
            return list.Count > 0 ? list.Peek() : emptyValue;
        }

        //
        // ECMAScript Array.shift/unshift semantics
        //

        /// <summary>
        /// Adds <paramref name="value"/> to beginning of the list.
        /// </summary>

        [DebuggerStepThrough]
        public static void Unshift<T>(this IList<T> list, T value)
        {
            if (list == null) throw new ArgumentNullException("list");
            list.Insert(0, value);
        }

        /// <summary>
        /// Removes and returns the first value of the list.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if list is empty.
        /// </exception>

        [DebuggerStepThrough]
        public static T Shift<T>(this IList<T> list)
        {
            if (list == null) throw new ArgumentNullException("list");
            var value = list.First();
            list.RemoveAt(0);
            return value;
        }

        /// <summary>
        /// Removes and returns the first value of the list, returning
        /// the default value for <typeparamref name="T"/> if the
        /// list is empty.
        /// </summary>

        [DebuggerStepThrough]
        public static T TryShift<T>(this IList<T> list)
        {
            if (list == null) throw new ArgumentNullException("list");
            return list.TryShift(default(T));
        }

        /// <summary>
        /// Removes and returns the first value of the list, returning
        /// <paramref name="emptyValue"/> if the list is empty.
        /// </summary>

        [DebuggerStepThrough]
        public static T TryShift<T>(this IList<T> list, T emptyValue)
        {
            if (list == null) throw new ArgumentNullException("list");
            return list.Count > 0 ? list.Shift() : emptyValue;
        }

        //
        // Queue semantics
        //

        /// <summary>
        /// Treats list like a queue, appending <paramref name="value"/>.
        /// </summary>

        [DebuggerStepThrough]
        public static void Enqueue<T>(this IList<T> list, T value)
        {
            list.Push(value);
        }

        /// <summary>
        /// Treats list like a queue, removing and returning the
        /// first value.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if list is empty.
        /// </exception>

        [DebuggerStepThrough]
        public static T Dequeue<T>(this IList<T> list)
        {
            return list.Shift();
        }

        /// <summary>
        /// Treats list like a queue, removing and returning the
        /// first value or the default value for <typeparamref name="T"/>
        /// if the list is empty.
        /// </summary>

        [DebuggerStepThrough]
        public static T TryDequeue<T>(this IList<T> list)
        {
            return list.TryShift();
        }

        /// <summary>
        /// Treats list like a queue, removing and returning the
        /// first value or <paramref name="emptyValue"/> if the 
        /// list is empty.
        /// </summary>

        [DebuggerStepThrough]
        public static T TryDequeue<T>(this IList<T> list, T emptyValue)
        {
            return list.TryShift(emptyValue);
        }

        //
        // Slicing and dicing
        //

        /// <summary>
        /// Returns elements from the list, starting at the index
        /// specified by <paramref name="start"/> index.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If <paramref name="start"/> is negative, it is treated as 
        /// <see cref="List{T}.Count"/> + <paramref name="start"/>.</para>
        /// <para>
        /// This method uses defered semantics.</para>
        /// </remarks>

        [DebuggerStepThrough]
        public static IEnumerable<T> Slice<T>(this IList<T> list, int start)
        {
            if (list == null) throw new ArgumentNullException("list");
            return list.Slice(start, list.Count);
        }

        /// <summary>
        /// Returns elements from the specified portion of the list, 
        /// identified by <paramref name="start"/> index and 
        /// <paramref name="end"/> (exclusive) index.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method copies up to, but not including, the element indicated by 
        /// <paramref name="end"/>. If <paramref name="start"/> is negative, it 
        /// is treated as <see cref="List{T}.Count"/> + <paramref name="start"/>.
        /// If <paramref name="end"/> is negative, it is treated as 
        /// <see cref="List{T}.Count"/> + <paramref name="end"/>. 
        /// If <paramref name="end"/> occurs before <paramref name="start"/>, no 
        /// elements returned.</para>
        /// <para>
        /// This method uses defered semantics.</para>
        /// </remarks>

        [DebuggerStepThrough]
        public static IEnumerable<T> Slice<T>(this IList<T> list, int start, int end /* exclusive */)
        {
            if (list == null) throw new ArgumentNullException("list");
            return SliceImpl(list, start, end);
        }

        static IEnumerable<T> SliceImpl<T>(IList<T> list, int start, int end)
        {
            //
            // This method copies up to, but not including, the element 
            // indicated by end. If start is negative, it is 
            // treated as length + start where length is the count of items
            // in the list. If end is negative, it is treated as 
            // length + end where length is the count of items in the list. 
            // If end is omitted, extraction continues to the end of 
            // the list. If end occurs before start, no elements are 
            // copied to the new list.
            //

            for (var i = list.ClipIndex(start); i < list.ClipIndex(end); i++)
                yield return list[i];
        }

        static int ClipIndex<T>(this ICollection<T> collection, int index)
        {
            Debug.Assert(collection != null);
            return Math.Min(collection.Count, index < 0 ? Math.Max(0, collection.Count + index) : index);
        }

        /// <summary>
        /// Searches the entire sorted list for a specific element, using 
        /// the <see cref="IComparable{T}"/> implemented by each element 
        /// of the list and by the specified object. 
        /// </summary>

        public static int BinarySearch<T>(this IList<T> list, T value)
        {
            return list.BinarySearch(value, null);
        }

        /// <summary>
        /// Searches the entire sorted list for a value using the specified 
        /// <see cref="IComparer{T}"/> implementation. 
        /// </summary>

        public static int BinarySearch<T>(this IList<T> list, T value, IComparer<T> comparer)
        {
            if (list == null) throw new ArgumentNullException("list");
            return list.BinarySearch(0, list.Count, value, comparer);
        }

        /// <summary>
        /// Searches a range of elements the sorted list for a value, using 
        /// the <see cref="IComparable{T}"/> implemented by each element of 
        /// the list and by the specified value. 
        /// </summary>

        public static int BinarySearch<T>(this IList<T> list, int index, int length, T value)
        {
            return list.BinarySearch(index, length, value, null);
        }

        /// <summary>
        /// Searches a range of elements in the sorted list for a value, 
        /// using the specified <see cref="IComparer{T}"/> implementation. 
        /// </summary>

        public static int BinarySearch<T>(this IList<T> list, int index, int length, T value, IComparer<T> comparer)
        {
            if (list == null) throw new ArgumentNullException("list");
            
            comparer = comparer ?? Comparer<T>.Default;

            var first = index; 
            var last = (index + length) - 1; 
            
            while (first <= last)
            {
                var middle = first + ((last - first) / 2); 
                var comparison = comparer.Compare(list[middle], value);
                if (comparison == 0)
                    return middle; 
                if (comparison < 0)
                    first = middle + 1;
                else             
                    last = middle - 1; 
            } 
            
            return ~first;
        }

        /// <summary>
        /// Returns a read-only wrapper for the list otherwise returns the 
        /// list itself if its <see cref="ICollection{T}.IsReadOnly"/> is 
        /// <c>true</c>.
        /// </summary>

        [DebuggerStepThrough]
        public static IList<T> AsReadOnly<T>(this IList<T> list)
        {
            if (list == null) throw new ArgumentNullException("list");
            return list.IsReadOnly ? list : new ReadOnlyCollection<T>(list);
        }

        /// <summary>
        /// Removes and returns the item at a given index of the list.
        /// </summary>

        [DebuggerStepThrough]
        public static T PopAt<T>(this IList<T> list, int index)
        {
            if (list == null) throw new ArgumentNullException("list");
            var item = list[index];
            list.RemoveAt(index);
            return item;
        }

        /// <summary>
        /// Removes and returns the item at a given index of the list. If the
        /// list has fewer items then the default value of <typeparamref name="T"/>
        /// is returned instead.
        /// </summary>

        [DebuggerStepThrough]
        public static T TryPopAt<T>(this IList<T> list, int index)
        {
            return TryPopAt(list, index, default(T));
        }

        /// <summary>
        /// Removes and returns the item at a given index of the list. If the
        /// list has fewer items then a user-supplied value of
        /// <typeparamref name="T"/> is returned instead.
        /// </summary>

        [DebuggerStepThrough]
        public static T TryPopAt<T>(this IList<T> list, int index, T emptyValue)
        {
            if (list == null) throw new ArgumentNullException("list");
            return index < list.Count ? list.PopAt(index) : emptyValue;
        }
    }
}