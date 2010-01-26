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

    #endregion

    /// <summary>
    /// Extension methods for types implementing <see cref="IComparable{T}"/>.
    /// </summary>

    static partial class IComparerExtensions
    {
        /// <summary>
        /// Creates an <see cref="IComparer{T}"/> implementation
        /// that inverts the results of comparing values through
        /// another comparer such that the sort order is inverted.
        /// </summary>
        
        public static IComparer<T> Invert<T>(this IComparer<T> comparer)
        {
            if (comparer == null) throw new ArgumentNullException("comparer");
            var ic = comparer as InvertingComparer<T>;
            return ic == null ? new InvertingComparer<T>(comparer) : ic.Inner;
        }

        private class InvertingComparer<T> : IComparer<T>
        {
            internal readonly IComparer<T> Inner;

            public InvertingComparer(IComparer<T> inner)
            {
                Debug.Assert(inner != null);
                Inner = inner;
            }

            public int Compare(T x, T y)
            {
                return -Inner.Compare(x, y);
            }
        }
    }
}