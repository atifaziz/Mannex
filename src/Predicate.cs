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
    using System.Diagnostics;

    #endregion

    static partial class PredicateExtensions
    {
        /// <summary>
        /// Combines two <see cref="Predicate{T}"/> to form a logical AND.
        /// </summary>

        [DebuggerStepThrough]
        public static Predicate<T> And<T>(this Predicate<T> lhs, Predicate<T> rhs)
        {
            if (lhs == null) throw new ArgumentNullException("lhs");
            if (rhs == null) throw new ArgumentNullException("rhs");
            return obj => lhs(obj) && rhs(obj);
        }

        /// <summary>
        /// Combines two <see cref="Predicate{T}"/> to form a logical OR.
        /// </summary>

        [DebuggerStepThrough]
        public static Predicate<T> Or<T>(this Predicate<T> lhs, Predicate<T> rhs)
        {
            if (lhs == null) throw new ArgumentNullException("lhs");
            if (rhs == null) throw new ArgumentNullException("rhs");
            return obj => lhs(obj) || rhs(obj);
        }
    }
}