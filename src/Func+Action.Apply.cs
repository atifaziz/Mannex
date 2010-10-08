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
    using System.Diagnostics;

    // ReSharper disable RedundantLambdaSignatureParentheses

    /// <summary>
    /// Extension methods for <see cref="Func{T}"/> and family.
    /// </summary>

    static partial class FuncExtensions
    {
        /// <summary>
        /// Partially applies the first 7 arguments of
        /// <see cref="Func{T1,T2,TResult}"/>.
        /// </summary>

        [DebuggerStepThrough]
        public static Func<T2, TResult> Apply<T1, T2, TResult>(
            this Func<T1, T2, TResult> func,
            T1 arg1)
        {
            if (func == null) throw new ArgumentNullException("func");
            return (arg2) => func(arg1, arg2);
        }

        /// <summary>
        /// Partially applies the first 7 arguments of
        /// <see cref="Func{T1,T2,T3,TResult}"/>.
        /// </summary>

        [DebuggerStepThrough]
        public static Func<T2, T3, TResult> Apply<T1, T2, T3, TResult>(
            this Func<T1, T2, T3, TResult> func,
            T1 arg1)
        {
            if (func == null) throw new ArgumentNullException("func");
            return (arg2, arg3) => func(arg1, arg2, arg3);
        }

        /// <summary>
        /// Partially applies the first 16 arguments of
        /// <see cref="Func{T1,T2,T3,TResult}"/>.
        /// </summary>

        [DebuggerStepThrough]
        public static Func<T3, TResult> Apply<T1, T2, T3, TResult>(
            this Func<T1, T2, T3, TResult> func,
            T1 arg1, T2 arg2)
        {
            if (func == null) throw new ArgumentNullException("func");
            return (arg3) => func(arg1, arg2, arg3);
        }

        /// <summary>
        /// Partially applies the first 7 arguments of
        /// <see cref="Func{T1,T2,T3,T4,TResult}"/>.
        /// </summary>

        [DebuggerStepThrough]
        public static Func<T2, T3, T4, TResult> Apply<T1, T2, T3, T4, TResult>(
            this Func<T1, T2, T3, T4, TResult> func,
            T1 arg1)
        {
            if (func == null) throw new ArgumentNullException("func");
            return (arg2, arg3, arg4) => func(arg1, arg2, arg3, arg4);
        }

        /// <summary>
        /// Partially applies the first 16 arguments of
        /// <see cref="Func{T1,T2,T3,T4,TResult}"/>.
        /// </summary>

        [DebuggerStepThrough]
        public static Func<T3, T4, TResult> Apply<T1, T2, T3, T4, TResult>(
            this Func<T1, T2, T3, T4, TResult> func,
            T1 arg1, T2 arg2)
        {
            if (func == null) throw new ArgumentNullException("func");
            return (arg3, arg4) => func(arg1, arg2, arg3, arg4);
        }

        /// <summary>
        /// Partially applies the first 25 arguments of
        /// <see cref="Func{T1,T2,T3,T4,TResult}"/>.
        /// </summary>

        [DebuggerStepThrough]
        public static Func<T4, TResult> Apply<T1, T2, T3, T4, TResult>(
            this Func<T1, T2, T3, T4, TResult> func,
            T1 arg1, T2 arg2, T3 arg3)
        {
            if (func == null) throw new ArgumentNullException("func");
            return (arg4) => func(arg1, arg2, arg3, arg4);
        }
    }

    /// <summary>
    /// Extension methods for <see cref="Action{T}"/> and family.
    /// </summary>

    static partial class ActionExtensions
    {
        /// <summary>
        /// Partially applies the first 7 arguments of
        /// <see cref="Action{T1,T2}"/>.
        /// </summary>

        [DebuggerStepThrough]
        public static Action<T2> Apply<T1, T2>(
            this Action<T1, T2> action,
            T1 arg1)
        {
            if (action == null) throw new ArgumentNullException("action");
            return (arg2) => action(arg1, arg2);
        }

        /// <summary>
        /// Partially applies the first 7 arguments of
        /// <see cref="Action{T1,T2,T3}"/>.
        /// </summary>

        [DebuggerStepThrough]
        public static Action<T2, T3> Apply<T1, T2, T3>(
            this Action<T1, T2, T3> action,
            T1 arg1)
        {
            if (action == null) throw new ArgumentNullException("action");
            return (arg2, arg3) => action(arg1, arg2, arg3);
        }

        /// <summary>
        /// Partially applies the first 16 arguments of
        /// <see cref="Action{T1,T2,T3}"/>.
        /// </summary>

        [DebuggerStepThrough]
        public static Action<T3> Apply<T1, T2, T3>(
            this Action<T1, T2, T3> action,
            T1 arg1, T2 arg2)
        {
            if (action == null) throw new ArgumentNullException("action");
            return (arg3) => action(arg1, arg2, arg3);
        }

        /// <summary>
        /// Partially applies the first 7 arguments of
        /// <see cref="Action{T1,T2,T3,T4}"/>.
        /// </summary>

        [DebuggerStepThrough]
        public static Action<T2, T3, T4> Apply<T1, T2, T3, T4>(
            this Action<T1, T2, T3, T4> action,
            T1 arg1)
        {
            if (action == null) throw new ArgumentNullException("action");
            return (arg2, arg3, arg4) => action(arg1, arg2, arg3, arg4);
        }

        /// <summary>
        /// Partially applies the first 16 arguments of
        /// <see cref="Action{T1,T2,T3,T4}"/>.
        /// </summary>

        [DebuggerStepThrough]
        public static Action<T3, T4> Apply<T1, T2, T3, T4>(
            this Action<T1, T2, T3, T4> action,
            T1 arg1, T2 arg2)
        {
            if (action == null) throw new ArgumentNullException("action");
            return (arg3, arg4) => action(arg1, arg2, arg3, arg4);
        }

        /// <summary>
        /// Partially applies the first 25 arguments of
        /// <see cref="Action{T1,T2,T3,T4}"/>.
        /// </summary>

        [DebuggerStepThrough]
        public static Action<T4> Apply<T1, T2, T3, T4>(
            this Action<T1, T2, T3, T4> action,
            T1 arg1, T2 arg2, T3 arg3)
        {
            if (action == null) throw new ArgumentNullException("action");
            return (arg4) => action(arg1, arg2, arg3, arg4);
        }
    }
}
