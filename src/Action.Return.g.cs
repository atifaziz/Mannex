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

    partial class ActionExtensions
    {
        
        /// <summary>
        /// Creates a <see cref="Func{T1,T2,TResult}"/> from the 
        /// <see cref="Action{T1,T2}"/> which when invoked will 
        /// call the <see cref="Action{T1,T2}"/> and then return 
        /// the given result of type <typeparamref name="TResult"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>

        [DebuggerStepThrough]
        public static Func<T1, T2, TResult> Return<T1, T2, TResult>(this Action<T1, T2> action, TResult result)
        {
            if (action == null) throw new ArgumentNullException("action");
            return (arg1, arg2) => { action(arg1, arg2); return result; };
        }
        
        /// <summary>
        /// Creates a <see cref="Func{T1,T2,T3,TResult}"/> from the 
        /// <see cref="Action{T1,T2,T3}"/> which when invoked will 
        /// call the <see cref="Action{T1,T2,T3}"/> and then return 
        /// the given result of type <typeparamref name="TResult"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>

        [DebuggerStepThrough]
        public static Func<T1, T2, T3, TResult> Return<T1, T2, T3, TResult>(this Action<T1, T2, T3> action, TResult result)
        {
            if (action == null) throw new ArgumentNullException("action");
            return (arg1, arg2, arg3) => { action(arg1, arg2, arg3); return result; };
        }
        
        /// <summary>
        /// Creates a <see cref="Func{T1,T2,T3,T4,TResult}"/> from the 
        /// <see cref="Action{T1,T2,T3,T4}"/> which when invoked will 
        /// call the <see cref="Action{T1,T2,T3,T4}"/> and then return 
        /// the given result of type <typeparamref name="TResult"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <typeparam name="T4">The type of the fourth argument.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>

        [DebuggerStepThrough]
        public static Func<T1, T2, T3, T4, TResult> Return<T1, T2, T3, T4, TResult>(this Action<T1, T2, T3, T4> action, TResult result)
        {
            if (action == null) throw new ArgumentNullException("action");
            return (arg1, arg2, arg3, arg4) => { action(arg1, arg2, arg3, arg4); return result; };
        }
        
        /// <summary>
        /// Creates a <see cref="Func{T1,T2,T3,T4,T5,TResult}"/> from the 
        /// <see cref="Action{T1,T2,T3,T4,T5}"/> which when invoked will 
        /// call the <see cref="Action{T1,T2,T3,T4,T5}"/> and then return 
        /// the given result of type <typeparamref name="TResult"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <typeparam name="T4">The type of the fourth argument.</typeparam>
        /// <typeparam name="T5">The type of the fifth argument.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>

        [DebuggerStepThrough]
        public static Func<T1, T2, T3, T4, T5, TResult> Return<T1, T2, T3, T4, T5, TResult>(this Action<T1, T2, T3, T4, T5> action, TResult result)
        {
            if (action == null) throw new ArgumentNullException("action");
            return (arg1, arg2, arg3, arg4, arg5) => { action(arg1, arg2, arg3, arg4, arg5); return result; };
        }
        
        /// <summary>
        /// Creates a <see cref="Func{T1,T2,T3,T4,T5,T6,TResult}"/> from the 
        /// <see cref="Action{T1,T2,T3,T4,T5,T6}"/> which when invoked will 
        /// call the <see cref="Action{T1,T2,T3,T4,T5,T6}"/> and then return 
        /// the given result of type <typeparamref name="TResult"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <typeparam name="T4">The type of the fourth argument.</typeparam>
        /// <typeparam name="T5">The type of the fifth argument.</typeparam>
        /// <typeparam name="T6">The type of the sixth argument.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>

        [DebuggerStepThrough]
        public static Func<T1, T2, T3, T4, T5, T6, TResult> Return<T1, T2, T3, T4, T5, T6, TResult>(this Action<T1, T2, T3, T4, T5, T6> action, TResult result)
        {
            if (action == null) throw new ArgumentNullException("action");
            return (arg1, arg2, arg3, arg4, arg5, arg6) => { action(arg1, arg2, arg3, arg4, arg5, arg6); return result; };
        }
        
        /// <summary>
        /// Creates a <see cref="Func{T1,T2,T3,T4,T5,T6,T7,TResult}"/> from the 
        /// <see cref="Action{T1,T2,T3,T4,T5,T6,T7}"/> which when invoked will 
        /// call the <see cref="Action{T1,T2,T3,T4,T5,T6,T7}"/> and then return 
        /// the given result of type <typeparamref name="TResult"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <typeparam name="T4">The type of the fourth argument.</typeparam>
        /// <typeparam name="T5">The type of the fifth argument.</typeparam>
        /// <typeparam name="T6">The type of the sixth argument.</typeparam>
        /// <typeparam name="T7">The type of the seventh argument.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>

        [DebuggerStepThrough]
        public static Func<T1, T2, T3, T4, T5, T6, T7, TResult> Return<T1, T2, T3, T4, T5, T6, T7, TResult>(this Action<T1, T2, T3, T4, T5, T6, T7> action, TResult result)
        {
            if (action == null) throw new ArgumentNullException("action");
            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7) => { action(arg1, arg2, arg3, arg4, arg5, arg6, arg7); return result; };
        }
        
        /// <summary>
        /// Creates a <see cref="Func{T1,T2,T3,T4,T5,T6,T7,T8,TResult}"/> from the 
        /// <see cref="Action{T1,T2,T3,T4,T5,T6,T7,T8}"/> which when invoked will 
        /// call the <see cref="Action{T1,T2,T3,T4,T5,T6,T7,T8}"/> and then return 
        /// the given result of type <typeparamref name="TResult"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <typeparam name="T4">The type of the fourth argument.</typeparam>
        /// <typeparam name="T5">The type of the fifth argument.</typeparam>
        /// <typeparam name="T6">The type of the sixth argument.</typeparam>
        /// <typeparam name="T7">The type of the seventh argument.</typeparam>
        /// <typeparam name="T8">The type of the eight argument.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>

        [DebuggerStepThrough]
        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> Return<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this Action<T1, T2, T3, T4, T5, T6, T7, T8> action, TResult result)
        {
            if (action == null) throw new ArgumentNullException("action");
            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => { action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8); return result; };
        }
        
        /// <summary>
        /// Creates a <see cref="Func{T1,T2,T3,T4,T5,T6,T7,T8,T9,TResult}"/> from the 
        /// <see cref="Action{T1,T2,T3,T4,T5,T6,T7,T8,T9}"/> which when invoked will 
        /// call the <see cref="Action{T1,T2,T3,T4,T5,T6,T7,T8,T9}"/> and then return 
        /// the given result of type <typeparamref name="TResult"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <typeparam name="T4">The type of the fourth argument.</typeparam>
        /// <typeparam name="T5">The type of the fifth argument.</typeparam>
        /// <typeparam name="T6">The type of the sixth argument.</typeparam>
        /// <typeparam name="T7">The type of the seventh argument.</typeparam>
        /// <typeparam name="T8">The type of the eight argument.</typeparam>
        /// <typeparam name="T9">The type of the nineth argument.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>

        [DebuggerStepThrough]
        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> Return<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action, TResult result)
        {
            if (action == null) throw new ArgumentNullException("action");
            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => { action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9); return result; };
        }
        
        /// <summary>
        /// Creates a <see cref="Func{T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,TResult}"/> from the 
        /// <see cref="Action{T1,T2,T3,T4,T5,T6,T7,T8,T9,T10}"/> which when invoked will 
        /// call the <see cref="Action{T1,T2,T3,T4,T5,T6,T7,T8,T9,T10}"/> and then return 
        /// the given result of type <typeparamref name="TResult"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <typeparam name="T4">The type of the fourth argument.</typeparam>
        /// <typeparam name="T5">The type of the fifth argument.</typeparam>
        /// <typeparam name="T6">The type of the sixth argument.</typeparam>
        /// <typeparam name="T7">The type of the seventh argument.</typeparam>
        /// <typeparam name="T8">The type of the eight argument.</typeparam>
        /// <typeparam name="T9">The type of the nineth argument.</typeparam>
        /// <typeparam name="T10">The type of the tenth argument.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>

        [DebuggerStepThrough]
        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> Return<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action, TResult result)
        {
            if (action == null) throw new ArgumentNullException("action");
            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => { action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10); return result; };
        }
        
        /// <summary>
        /// Creates a <see cref="Func{T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,TResult}"/> from the 
        /// <see cref="Action{T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11}"/> which when invoked will 
        /// call the <see cref="Action{T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11}"/> and then return 
        /// the given result of type <typeparamref name="TResult"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <typeparam name="T4">The type of the fourth argument.</typeparam>
        /// <typeparam name="T5">The type of the fifth argument.</typeparam>
        /// <typeparam name="T6">The type of the sixth argument.</typeparam>
        /// <typeparam name="T7">The type of the seventh argument.</typeparam>
        /// <typeparam name="T8">The type of the eight argument.</typeparam>
        /// <typeparam name="T9">The type of the nineth argument.</typeparam>
        /// <typeparam name="T10">The type of the tenth argument.</typeparam>
        /// <typeparam name="T11">The type of the eleventh argument.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>

        [DebuggerStepThrough]
        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> Return<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action, TResult result)
        {
            if (action == null) throw new ArgumentNullException("action");
            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => { action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11); return result; };
        }
        
        /// <summary>
        /// Creates a <see cref="Func{T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,TResult}"/> from the 
        /// <see cref="Action{T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12}"/> which when invoked will 
        /// call the <see cref="Action{T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12}"/> and then return 
        /// the given result of type <typeparamref name="TResult"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <typeparam name="T4">The type of the fourth argument.</typeparam>
        /// <typeparam name="T5">The type of the fifth argument.</typeparam>
        /// <typeparam name="T6">The type of the sixth argument.</typeparam>
        /// <typeparam name="T7">The type of the seventh argument.</typeparam>
        /// <typeparam name="T8">The type of the eight argument.</typeparam>
        /// <typeparam name="T9">The type of the nineth argument.</typeparam>
        /// <typeparam name="T10">The type of the tenth argument.</typeparam>
        /// <typeparam name="T11">The type of the eleventh argument.</typeparam>
        /// <typeparam name="T12">The type of the twelfth argument.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>

        [DebuggerStepThrough]
        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> Return<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action, TResult result)
        {
            if (action == null) throw new ArgumentNullException("action");
            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => { action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12); return result; };
        }
        
        /// <summary>
        /// Creates a <see cref="Func{T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,TResult}"/> from the 
        /// <see cref="Action{T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13}"/> which when invoked will 
        /// call the <see cref="Action{T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13}"/> and then return 
        /// the given result of type <typeparamref name="TResult"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <typeparam name="T4">The type of the fourth argument.</typeparam>
        /// <typeparam name="T5">The type of the fifth argument.</typeparam>
        /// <typeparam name="T6">The type of the sixth argument.</typeparam>
        /// <typeparam name="T7">The type of the seventh argument.</typeparam>
        /// <typeparam name="T8">The type of the eight argument.</typeparam>
        /// <typeparam name="T9">The type of the nineth argument.</typeparam>
        /// <typeparam name="T10">The type of the tenth argument.</typeparam>
        /// <typeparam name="T11">The type of the eleventh argument.</typeparam>
        /// <typeparam name="T12">The type of the twelfth argument.</typeparam>
        /// <typeparam name="T13">The type of the thirteenth argument.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>

        [DebuggerStepThrough]
        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> Return<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action, TResult result)
        {
            if (action == null) throw new ArgumentNullException("action");
            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => { action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13); return result; };
        }
        
        /// <summary>
        /// Creates a <see cref="Func{T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,TResult}"/> from the 
        /// <see cref="Action{T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14}"/> which when invoked will 
        /// call the <see cref="Action{T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14}"/> and then return 
        /// the given result of type <typeparamref name="TResult"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <typeparam name="T4">The type of the fourth argument.</typeparam>
        /// <typeparam name="T5">The type of the fifth argument.</typeparam>
        /// <typeparam name="T6">The type of the sixth argument.</typeparam>
        /// <typeparam name="T7">The type of the seventh argument.</typeparam>
        /// <typeparam name="T8">The type of the eight argument.</typeparam>
        /// <typeparam name="T9">The type of the nineth argument.</typeparam>
        /// <typeparam name="T10">The type of the tenth argument.</typeparam>
        /// <typeparam name="T11">The type of the eleventh argument.</typeparam>
        /// <typeparam name="T12">The type of the twelfth argument.</typeparam>
        /// <typeparam name="T13">The type of the thirteenth argument.</typeparam>
        /// <typeparam name="T14">The type of the fourteenth argument.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>

        [DebuggerStepThrough]
        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> Return<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action, TResult result)
        {
            if (action == null) throw new ArgumentNullException("action");
            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => { action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14); return result; };
        }
        
        /// <summary>
        /// Creates a <see cref="Func{T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,TResult}"/> from the 
        /// <see cref="Action{T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15}"/> which when invoked will 
        /// call the <see cref="Action{T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15}"/> and then return 
        /// the given result of type <typeparamref name="TResult"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <typeparam name="T4">The type of the fourth argument.</typeparam>
        /// <typeparam name="T5">The type of the fifth argument.</typeparam>
        /// <typeparam name="T6">The type of the sixth argument.</typeparam>
        /// <typeparam name="T7">The type of the seventh argument.</typeparam>
        /// <typeparam name="T8">The type of the eight argument.</typeparam>
        /// <typeparam name="T9">The type of the nineth argument.</typeparam>
        /// <typeparam name="T10">The type of the tenth argument.</typeparam>
        /// <typeparam name="T11">The type of the eleventh argument.</typeparam>
        /// <typeparam name="T12">The type of the twelfth argument.</typeparam>
        /// <typeparam name="T13">The type of the thirteenth argument.</typeparam>
        /// <typeparam name="T14">The type of the fourteenth argument.</typeparam>
        /// <typeparam name="T15">The type of the fifteenth argument.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>

        [DebuggerStepThrough]
        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> Return<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action, TResult result)
        {
            if (action == null) throw new ArgumentNullException("action");
            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => { action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15); return result; };
        }
        
        /// <summary>
        /// Creates a <see cref="Func{T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,TResult}"/> from the 
        /// <see cref="Action{T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16}"/> which when invoked will 
        /// call the <see cref="Action{T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16}"/> and then return 
        /// the given result of type <typeparamref name="TResult"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <typeparam name="T4">The type of the fourth argument.</typeparam>
        /// <typeparam name="T5">The type of the fifth argument.</typeparam>
        /// <typeparam name="T6">The type of the sixth argument.</typeparam>
        /// <typeparam name="T7">The type of the seventh argument.</typeparam>
        /// <typeparam name="T8">The type of the eight argument.</typeparam>
        /// <typeparam name="T9">The type of the nineth argument.</typeparam>
        /// <typeparam name="T10">The type of the tenth argument.</typeparam>
        /// <typeparam name="T11">The type of the eleventh argument.</typeparam>
        /// <typeparam name="T12">The type of the twelfth argument.</typeparam>
        /// <typeparam name="T13">The type of the thirteenth argument.</typeparam>
        /// <typeparam name="T14">The type of the fourteenth argument.</typeparam>
        /// <typeparam name="T15">The type of the fifteenth argument.</typeparam>
        /// <typeparam name="T16">The type of the sixteenth argument.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>

        [DebuggerStepThrough]
        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> Return<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> action, TResult result)
        {
            if (action == null) throw new ArgumentNullException("action");
            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) => { action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16); return result; };
        }
    }
}
