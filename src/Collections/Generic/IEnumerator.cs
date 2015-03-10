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
    /// Extension methods for types implementing <see cref="IEnumerator{T}"/>.
    /// </summary>

    static partial class IEnumeratorExtensions
    {
        /// <summary>
        /// Creates an array containing the remaining items of an enumerator
        /// and then disposes the enumerator.
        /// </summary>

        public static T[] ToArray<T>(this IEnumerator<T> enumerator)
        {
            return enumerator.ToList().ToArray();
        }

        /// <summary>
        /// Creates a list containing the remaining items of an enumerator
        /// and then disposes the enumerator.
        /// </summary>

        public static List<T> ToList<T>(this IEnumerator<T> enumerator)
        {
            if (enumerator == null) throw new ArgumentNullException("enumerator");
            var list = new List<T>();
            using (enumerator)
            {
                while (enumerator.MoveNext())
                    list.Add(enumerator.Current);
            }
            return list;
        }

        /// <summary>
        /// Reads the next value from the enumerator otherwise throws
        /// <see cref="InvalidOperationException"/>.
        /// </summary>

        public static T Read<T>(this IEnumerator<T> enumerator)
        {
            if (enumerator == null) throw new ArgumentNullException("enumerator");
            if (!enumerator.MoveNext()) throw new InvalidOperationException();
            return enumerator.Current;
        }

        /// <summary>
        /// Attempts to read the next value from the enumerator. If the
        /// enumerator has no more values then returns the default value of
        /// <typeparamref name="T"/> instead.
        /// </summary>

        public static T TryRead<T>(this IEnumerator<T> enumerator)
        {
            return TryRead(enumerator, default(T));
        }

        /// <summary>
        /// Attempts to read the next value from the enumerator. If the
        /// enumerator has no more values then returns a given sentinel value
        /// instead.
        /// </summary>

        public static T TryRead<T>(this IEnumerator<T> enumerator, T sentinel)
        {
            if (enumerator == null) throw new ArgumentNullException("enumerator");
            return enumerator.MoveNext() ? enumerator.Current : sentinel;
        }
    }
}