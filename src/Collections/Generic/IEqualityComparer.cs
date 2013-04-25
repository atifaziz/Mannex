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
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Extension methods for types implementing <see cref="IEqualityComparer{T}"/>.
    /// </summary>

    static partial class IEqualityComparerExtensions
    {
        /// <summary>
        /// Returns a delegate to <see cref="IEqualityComparer{T}.Equals(T,T)"/>.
        /// </summary>
        
        public static Func<T, T, bool> EqualsFunc<T>(this IEqualityComparer<T> comparer)
        {
            if (comparer == null) throw new ArgumentNullException("comparer");
            return comparer.Equals;
        }

        /// <summary>
        /// Returns a delegate to <see cref="IEqualityComparer{T}.GetHashCode(T)"/>.
        /// </summary>

        public static Func<T, int> GetHashCodeFunc<T>(this IEqualityComparer<T> comparer)
        {
            if (comparer == null) throw new ArgumentNullException("comparer");
            return comparer.GetHashCode;
        }
    }
}