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

    /// <summary>
    /// Extension methods for <see cref="Func{T}"/> and family.
    /// </summary>

    static partial class FuncExtensions
    {
        /// <summary>
        /// Creates a <see cref="Predicate{T}"/> from a <see cref="Func{T,TResult}"/>.
        /// </summary>

        [DebuggerStepThrough]
        public static Converter<TInput, TOutput> ToConverter<TInput, TOutput>(this Func<TInput, TOutput> converter)
        {
            if (converter == null) throw new ArgumentNullException(nameof(converter));
            return input => converter(input);
        }

        /// <summary>
        /// Creates a <see cref="Predicate{T}"/> from a <see cref="Func{T,TResult}"/>
        /// that returns a Boolean.
        /// </summary>

        [DebuggerStepThrough]
        public static Predicate<T> ToPredicate<T>(this Func<T, bool> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            return arg => predicate(arg);
        }
    }
}