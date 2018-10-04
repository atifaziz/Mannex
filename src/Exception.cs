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
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.ExceptionServices;
    using System.Runtime.InteropServices;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="Exception"/>.
    /// </summary>

    static partial class ExceptionExtensions
    {
        static readonly Func<Exception, Exception> PrepForRemoting;

        static ExceptionExtensions()
        {
            var method = typeof(Exception).GetMethod("PrepForRemoting", 
                             BindingFlags.Instance | BindingFlags.NonPublic, 
                             /* binder */ null, Type.EmptyTypes, null);

            PrepForRemoting = method != null
                            ? (Func<Exception, Exception>) Delegate.CreateDelegate(typeof(Func<Exception, Exception>), method)
                            : (_ => null);
        }

        /// <summary>
        /// Preserve stack trace for re-throwing. 
        /// </summary>
        /// <remarks>
        /// Credit: <a href="http://msdn.microsoft.com/en-us/devlabs/ee794896.aspx">Reactive Extensions for .NET (Rx)</a>
        /// and <a href="http://piers7.blogspot.com/2010/09/rethrowing-exceptions-without-losing.html">Cup(Of T): Rethrowing Exceptions Without Losing Original Stack Trace</a>
        /// </remarks>

        public static Exception PrepareForRethrow(this Exception e)
        {
            if (e == null) throw new ArgumentNullException(nameof(e));

            return PrepForRemoting(e) ?? throw new PlatformNotSupportedException();
        }

        /// <summary>
        /// Re-throw an exception while preserving the stack trace.
        /// </summary>
        /// <remarks>
        /// Credit: <a href="http://msdn.microsoft.com/en-us/devlabs/ee794896.aspx">Reactive Extensions for .NET (Rx)</a>
        /// and <a href="http://piers7.blogspot.com/2010/09/rethrowing-exceptions-without-losing.html">Cup(Of T): Rethrowing Exceptions Without Losing Original Stack Trace</a>
        /// </remarks>

        [DebuggerStepThrough]
        public static void Rethrow(this Exception e)
        {
            if (e == null) throw new ArgumentNullException(nameof(e));

            var ee = PrepForRemoting(e);
            if (ee == null)
            {
#if NET40
                throw new PlatformNotSupportedException();
#else
                ExceptionDispatchInfo.Capture(e).Throw();
#endif
            }
            else
            {
                throw ee;
            }
        }

        /// <summary>
        /// Gets a sequence containing the <see cref="Exception"/> object
        /// and its complete chain of nested exceptions via 
        /// <see cref="Exception.InnerException"/>.
        /// </summary>
        /// <remarks>
        /// This method uses deferred and streaming execution semantics.
        /// </remarks>

        [DebuggerStepThrough]
        public static IEnumerable<Exception> InnerExceptions(this Exception e)
        {
            if (e == null) throw new ArgumentNullException(nameof(e));
            return YieldInnerExceptions(e);
        }

        static IEnumerable<Exception> YieldInnerExceptions(Exception e)
        {
            for (; e != null; e = e.InnerException)
                yield return e;
        }

        /// <summary>
        /// Determines whether the <c>HRESULT</c> for the 
        /// <see cref="Exception"/> represents a file or device sharing 
        /// violation error.
        /// </summary>

        public static bool IsSharingViolation(this Exception exception)
        {
            if (exception == null) throw new ArgumentNullException(nameof(exception));
            // http://support.microsoft.com/kb/316609
            return /* ERROR_SHARING_VIOLATION */ 0x80070020 == (uint) Marshal.GetHRForException(exception);
        }
    }
}
