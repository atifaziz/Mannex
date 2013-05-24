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
    /// Extension methods for <see cref="Action{T}"/> and family.
    /// </summary>

    static partial class ActionExtensions
    {
        /// <summary>
        /// Creates a <see cref="Func{T,TResult}"/> from the 
        /// <see cref="Action"/> which when invoked will 
        /// call the <see cref="Action"/> and then return 
        /// the given result of type <typeparamref name="TResult"/>.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>

        [DebuggerStepThrough]
        public static Func<TResult> Return<TResult>(this Action action, TResult result)
        {
            if (action == null) throw new ArgumentNullException("action");
            return () => { action(); return result; };
        }

        /// <summary>
        /// Creates a <see cref="Func{T,TResult}"/> from the 
        /// <see cref="Action{T}"/> which when invoked will 
        /// call the <see cref="Action{T}"/> and then return 
        /// the given result of type <typeparamref name="TResult"/>.
        /// </summary>
        /// <typeparam name="T">The type of the first argument.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>

        [DebuggerStepThrough]
        public static Func<T, TResult> Return<T, TResult>(this Action<T> action, TResult result)
        {
            if (action == null) throw new ArgumentNullException("action");
            return arg => { action(arg); return result; };
        }
    }
}