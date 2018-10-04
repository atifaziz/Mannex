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
    using System;
    using System.Collections.Generic;

    partial class TupleExtensions
    {
        /// <summary>
        /// Returns a <see cref="IEnumerable{T}"/> of <see cref="object"/>
        /// whose elements are the items of a given
        /// <see cref="Tuple{T1,T2}" />.
        /// </summary>
        /// <typeparam name="T1">The type of the tuple's first component.</typeparam>
        /// <typeparam name="T2">The type of the tuple's second component.</typeparam>

        public static IEnumerable<object> AsEnumerable<T1, T2>(this Tuple<T1, T2> tuple)
        {
            if (tuple == null) throw new ArgumentNullException("tuple");

            return _(); IEnumerable<object> _()
            {
                yield return tuple.Item1;
                yield return tuple.Item2;
            }
        }

        /// <summary>
        /// Folds items of a <see cref="Tuple{T1,T2}" />
        /// into a single element of <typeparamref name="TResult"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the tuple's first component.</typeparam>
        /// <typeparam name="T2">The type of the tuple's second component.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>

        public static TResult Fold<T1, T2, TResult>(this Tuple<T1, T2> tuple, Func<T1, T2, TResult> selector)
        {
            if (tuple == null) throw new ArgumentNullException("tuple");
            if (selector == null) throw new ArgumentNullException("selector");
            return selector(tuple.Item1, tuple.Item2);
        }

        /// <summary>
        /// Returns a <see cref="IEnumerable{T}"/> of <see cref="object"/>
        /// whose elements are the items of a given
        /// <see cref="Tuple{T1,T2,T3}" />.
        /// </summary>
        /// <typeparam name="T1">The type of the tuple's first component.</typeparam>
        /// <typeparam name="T2">The type of the tuple's second component.</typeparam>
        /// <typeparam name="T3">The type of the tuple's third component.</typeparam>

        public static IEnumerable<object> AsEnumerable<T1, T2, T3>(this Tuple<T1, T2, T3> tuple)
        {
            if (tuple == null) throw new ArgumentNullException("tuple");

            return _(); IEnumerable<object> _()
            {
                yield return tuple.Item1;
                yield return tuple.Item2;
                yield return tuple.Item3;
            }
        }

        /// <summary>
        /// Folds items of a <see cref="Tuple{T1,T2,T3}" />
        /// into a single element of <typeparamref name="TResult"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the tuple's first component.</typeparam>
        /// <typeparam name="T2">The type of the tuple's second component.</typeparam>
        /// <typeparam name="T3">The type of the tuple's third component.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>

        public static TResult Fold<T1, T2, T3, TResult>(this Tuple<T1, T2, T3> tuple, Func<T1, T2, T3, TResult> selector)
        {
            if (tuple == null) throw new ArgumentNullException("tuple");
            if (selector == null) throw new ArgumentNullException("selector");
            return selector(tuple.Item1, tuple.Item2, tuple.Item3);
        }

        /// <summary>
        /// Returns a <see cref="IEnumerable{T}"/> of <see cref="object"/>
        /// whose elements are the items of a given
        /// <see cref="Tuple{T1,T2,T3,T4}" />.
        /// </summary>
        /// <typeparam name="T1">The type of the tuple's first component.</typeparam>
        /// <typeparam name="T2">The type of the tuple's second component.</typeparam>
        /// <typeparam name="T3">The type of the tuple's third component.</typeparam>
        /// <typeparam name="T4">The type of the tuple's fourth component.</typeparam>

        public static IEnumerable<object> AsEnumerable<T1, T2, T3, T4>(this Tuple<T1, T2, T3, T4> tuple)
        {
            if (tuple == null) throw new ArgumentNullException("tuple");

            return _(); IEnumerable<object> _()
            {
                yield return tuple.Item1;
                yield return tuple.Item2;
                yield return tuple.Item3;
                yield return tuple.Item4;
            }
        }

        /// <summary>
        /// Folds items of a <see cref="Tuple{T1,T2,T3,T4}" />
        /// into a single element of <typeparamref name="TResult"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the tuple's first component.</typeparam>
        /// <typeparam name="T2">The type of the tuple's second component.</typeparam>
        /// <typeparam name="T3">The type of the tuple's third component.</typeparam>
        /// <typeparam name="T4">The type of the tuple's fourth component.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>

        public static TResult Fold<T1, T2, T3, T4, TResult>(this Tuple<T1, T2, T3, T4> tuple, Func<T1, T2, T3, T4, TResult> selector)
        {
            if (tuple == null) throw new ArgumentNullException("tuple");
            if (selector == null) throw new ArgumentNullException("selector");
            return selector(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4);
        }

        /// <summary>
        /// Returns a <see cref="IEnumerable{T}"/> of <see cref="object"/>
        /// whose elements are the items of a given
        /// <see cref="Tuple{T1,T2,T3,T4,T5}" />.
        /// </summary>
        /// <typeparam name="T1">The type of the tuple's first component.</typeparam>
        /// <typeparam name="T2">The type of the tuple's second component.</typeparam>
        /// <typeparam name="T3">The type of the tuple's third component.</typeparam>
        /// <typeparam name="T4">The type of the tuple's fourth component.</typeparam>
        /// <typeparam name="T5">The type of the tuple's fifth component.</typeparam>

        public static IEnumerable<object> AsEnumerable<T1, T2, T3, T4, T5>(this Tuple<T1, T2, T3, T4, T5> tuple)
        {
            if (tuple == null) throw new ArgumentNullException("tuple");

            return _(); IEnumerable<object> _()
            {
                yield return tuple.Item1;
                yield return tuple.Item2;
                yield return tuple.Item3;
                yield return tuple.Item4;
                yield return tuple.Item5;
            }
        }

        /// <summary>
        /// Folds items of a <see cref="Tuple{T1,T2,T3,T4,T5}" />
        /// into a single element of <typeparamref name="TResult"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the tuple's first component.</typeparam>
        /// <typeparam name="T2">The type of the tuple's second component.</typeparam>
        /// <typeparam name="T3">The type of the tuple's third component.</typeparam>
        /// <typeparam name="T4">The type of the tuple's fourth component.</typeparam>
        /// <typeparam name="T5">The type of the tuple's fifth component.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>

        public static TResult Fold<T1, T2, T3, T4, T5, TResult>(this Tuple<T1, T2, T3, T4, T5> tuple, Func<T1, T2, T3, T4, T5, TResult> selector)
        {
            if (tuple == null) throw new ArgumentNullException("tuple");
            if (selector == null) throw new ArgumentNullException("selector");
            return selector(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5);
        }

        /// <summary>
        /// Returns a <see cref="IEnumerable{T}"/> of <see cref="object"/>
        /// whose elements are the items of a given
        /// <see cref="Tuple{T1,T2,T3,T4,T5,T6}" />.
        /// </summary>
        /// <typeparam name="T1">The type of the tuple's first component.</typeparam>
        /// <typeparam name="T2">The type of the tuple's second component.</typeparam>
        /// <typeparam name="T3">The type of the tuple's third component.</typeparam>
        /// <typeparam name="T4">The type of the tuple's fourth component.</typeparam>
        /// <typeparam name="T5">The type of the tuple's fifth component.</typeparam>
        /// <typeparam name="T6">The type of the tuple's sixth component.</typeparam>

        public static IEnumerable<object> AsEnumerable<T1, T2, T3, T4, T5, T6>(this Tuple<T1, T2, T3, T4, T5, T6> tuple)
        {
            if (tuple == null) throw new ArgumentNullException("tuple");

            return _(); IEnumerable<object> _()
            {
                yield return tuple.Item1;
                yield return tuple.Item2;
                yield return tuple.Item3;
                yield return tuple.Item4;
                yield return tuple.Item5;
                yield return tuple.Item6;
            }
        }

        /// <summary>
        /// Folds items of a <see cref="Tuple{T1,T2,T3,T4,T5,T6}" />
        /// into a single element of <typeparamref name="TResult"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the tuple's first component.</typeparam>
        /// <typeparam name="T2">The type of the tuple's second component.</typeparam>
        /// <typeparam name="T3">The type of the tuple's third component.</typeparam>
        /// <typeparam name="T4">The type of the tuple's fourth component.</typeparam>
        /// <typeparam name="T5">The type of the tuple's fifth component.</typeparam>
        /// <typeparam name="T6">The type of the tuple's sixth component.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>

        public static TResult Fold<T1, T2, T3, T4, T5, T6, TResult>(this Tuple<T1, T2, T3, T4, T5, T6> tuple, Func<T1, T2, T3, T4, T5, T6, TResult> selector)
        {
            if (tuple == null) throw new ArgumentNullException("tuple");
            if (selector == null) throw new ArgumentNullException("selector");
            return selector(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6);
        }

        /// <summary>
        /// Returns a <see cref="IEnumerable{T}"/> of <see cref="object"/>
        /// whose elements are the items of a given
        /// <see cref="Tuple{T1,T2,T3,T4,T5,T6,T7}" />.
        /// </summary>
        /// <typeparam name="T1">The type of the tuple's first component.</typeparam>
        /// <typeparam name="T2">The type of the tuple's second component.</typeparam>
        /// <typeparam name="T3">The type of the tuple's third component.</typeparam>
        /// <typeparam name="T4">The type of the tuple's fourth component.</typeparam>
        /// <typeparam name="T5">The type of the tuple's fifth component.</typeparam>
        /// <typeparam name="T6">The type of the tuple's sixth component.</typeparam>
        /// <typeparam name="T7">The type of the tuple's seventh component.</typeparam>

        public static IEnumerable<object> AsEnumerable<T1, T2, T3, T4, T5, T6, T7>(this Tuple<T1, T2, T3, T4, T5, T6, T7> tuple)
        {
            if (tuple == null) throw new ArgumentNullException("tuple");

            return _(); IEnumerable<object> _()
            {
                yield return tuple.Item1;
                yield return tuple.Item2;
                yield return tuple.Item3;
                yield return tuple.Item4;
                yield return tuple.Item5;
                yield return tuple.Item6;
                yield return tuple.Item7;
            }
        }

        /// <summary>
        /// Folds items of a <see cref="Tuple{T1,T2,T3,T4,T5,T6,T7}" />
        /// into a single element of <typeparamref name="TResult"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the tuple's first component.</typeparam>
        /// <typeparam name="T2">The type of the tuple's second component.</typeparam>
        /// <typeparam name="T3">The type of the tuple's third component.</typeparam>
        /// <typeparam name="T4">The type of the tuple's fourth component.</typeparam>
        /// <typeparam name="T5">The type of the tuple's fifth component.</typeparam>
        /// <typeparam name="T6">The type of the tuple's sixth component.</typeparam>
        /// <typeparam name="T7">The type of the tuple's seventh component.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>

        public static TResult Fold<T1, T2, T3, T4, T5, T6, T7, TResult>(this Tuple<T1, T2, T3, T4, T5, T6, T7> tuple, Func<T1, T2, T3, T4, T5, T6, T7, TResult> selector)
        {
            if (tuple == null) throw new ArgumentNullException("tuple");
            if (selector == null) throw new ArgumentNullException("selector");
            return selector(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6, tuple.Item7);
        }

    }
}
