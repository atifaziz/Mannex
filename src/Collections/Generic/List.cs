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

        public static T TryPop<T>(this IList<T> list)
        {
            return list.TryPop(default(T));
        }

        /// <summary>
        /// Treats list like a stack, popping (removing and returning)
        /// the last value on the list. If the list is empty, then
        /// <paramref name="emptyValue"/> is returned instead.
        /// </summary>

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
        
        public static T TryShift<T>(this IList<T> list)
        {
            if (list == null) throw new ArgumentNullException("list");
            return list.TryShift(default(T));
        }

        /// <summary>
        /// Removes and returns the first value of the list, returning
        /// <paramref name="emptyValue"/> if the list is empty.
        /// </summary>

        public static T TryShift<T>(this IList<T> list, T emptyValue)
        {
            if (list == null) throw new ArgumentNullException("list");
            return list.Count > 0 ? list.Shift() : emptyValue;
        }

        //
        // Queue semantics
        //

        /// <summary>
        /// Treats list like a queue, appending <see cref="value"/>.
        /// </summary>

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

        public static T Dequeue<T>(this IList<T> list)
        {
            return list.Shift();
        }

        /// <summary>
        /// Treats list like a queue, removing and returning the
        /// first value or the default value for <typeparamref name="T"/>
        /// if the list is empty.
        /// </summary>

        public static T TryDequeue<T>(this IList<T> list)
        {
            return list.TryShift();
        }

        /// <summary>
        /// Treats list like a queue, removing and returning the
        /// first value or <paramref name="emptyValue"/> if the 
        /// list is empty.
        /// </summary>
        
        public static T TryDequeue<T>(this IList<T> list, T emptyValue)
        {
            return list.TryShift(emptyValue);
        }
    }
}