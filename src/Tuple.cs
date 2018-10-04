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

    #endregion

    /// <summary>
    /// Extension methods for <see cref="Tuple"/>.
    /// </summary>

    static partial class TupleExtensions
    {
        /// <summary>
        /// Returns a <see cref="IEnumerable{T}"/> of <see cref="object"/>
        /// whose elements are the items of a given <see cref="Tuple{T}" />.
        /// </summary>
        /// <typeparam name="T">The type of the tuple's only component.</typeparam>

        public static IEnumerable<T> AsEnumerable<T>(this Tuple<T> tuple)
        {
            if (tuple == null) throw new ArgumentNullException(nameof(tuple));
            return AsEnumerableImpl(tuple);
        }

        static IEnumerable<T> AsEnumerableImpl<T>(this Tuple<T> tuple)
        {
            yield return tuple.Item1;
        }

        /// <summary>
        /// Folds items of a <see cref="Tuple{T}" /> into a single element
        /// of <typeparamref name="TResult"/>.
        /// </summary>
        /// <typeparam name="T">The type of the tuple's only component.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        ///
        public static TResult Fold<T, TResult>(this Tuple<T> tuple, Func<T, TResult> selector)
        {
            if (tuple == null) throw new ArgumentNullException(nameof(tuple));
            if (selector == null) throw new ArgumentNullException(nameof(selector));
            return selector(tuple.Item1);
        }

    }
}
