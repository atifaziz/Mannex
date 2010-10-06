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

    /// <summary>
    /// Extension methods for <see cref="Func{T}"/> and family.
    /// </summary>

    static partial class FuncExtensions
    {
        // ReSharper disable RedundantLambdaSignatureParentheses
    
        /// <summary>
        /// Partially applies the first argument of
        /// <see cref="Func{T1,T2,TResult}"/>.
        /// </summary>

        public static Func<T2, TResult> Apply<T1, T2, TResult>(
            this Func<T1, T2, TResult> func,
            T1 arg1)
        {
            if (func == null) throw new ArgumentNullException("func");
            return (arg2) => func(arg1, arg2);
        }

        /// <summary>
        /// Partially applies the first argument of
        /// <see cref="Func{T1,T2,T3,TResult}"/>.
        /// </summary>

        public static Func<T2, T3, TResult> Apply<T1, T2, T3, TResult>(
            this Func<T1, T2, T3, TResult> func,
            T1 arg1)
        {
            if (func == null) throw new ArgumentNullException("func");
            return (arg2, arg3) => func(arg1, arg2, arg3);
        }

        /// <summary>
        /// Partially applies the first 2 arguments of
        /// <see cref="Func{T1,T2,T3,TResult}"/>.
        /// </summary>

        public static Func<T3, TResult> Apply<T1, T2, T3, TResult>(
            this Func<T1, T2, T3, TResult> func,
            T1 arg1, T2 arg2)
        {
            if (func == null) throw new ArgumentNullException("func");
            return (arg3) => func(arg1, arg2, arg3);
        }

        /// <summary>
        /// Partially applies the first argument of
        /// <see cref="Func{T1,T2,T3,T4,TResult}"/>.
        /// </summary>

        public static Func<T2, T3, T4, TResult> Apply<T1, T2, T3, T4, TResult>(
            this Func<T1, T2, T3, T4, TResult> func,
            T1 arg1)
        {
            if (func == null) throw new ArgumentNullException("func");
            return (arg2, arg3, arg4) => func(arg1, arg2, arg3, arg4);
        }

        /// <summary>
        /// Partially applies the first 2 arguments of
        /// <see cref="Func{T1,T2,T3,T4,TResult}"/>.
        /// </summary>

        public static Func<T3, T4, TResult> Apply<T1, T2, T3, T4, TResult>(
            this Func<T1, T2, T3, T4, TResult> func,
            T1 arg1, T2 arg2)
        {
            if (func == null) throw new ArgumentNullException("func");
            return (arg3, arg4) => func(arg1, arg2, arg3, arg4);
        }

        /// <summary>
        /// Partially applies the first 3 arguments of
        /// <see cref="Func{T1,T2,T3,T4,TResult}"/>.
        /// </summary>

        public static Func<T4, TResult> Apply<T1, T2, T3, T4, TResult>(
            this Func<T1, T2, T3, T4, TResult> func,
            T1 arg1, T2 arg2, T3 arg3)
        {
            if (func == null) throw new ArgumentNullException("func");
            return (arg4) => func(arg1, arg2, arg3, arg4);
        }
    }
}
