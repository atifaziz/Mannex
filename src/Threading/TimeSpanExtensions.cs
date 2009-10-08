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

namespace Mannex.Threading
{
    #region Imports

    using System;
    using System.Threading;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="TimeSpan"/>.
    /// </summary>

    static partial class TimeSpanExtensions
    {
        /// <summary>
        /// Converts <see cref="TimeSpan"/> to milliseconds as expected by
        /// most of the <see cref="System.Threading"/> API.
        /// </summary>

        public static int ToTimeout(this TimeSpan timeout)
        {
            return (int) timeout.TotalMilliseconds;
        }

        /// <summary>
        /// Converts <see cref="TimeSpan"/> to milliseconds as expected by
        /// most of the <see cref="System.Threading"/> API. If the the 
        /// <see cref="TimeSpan"/> value is <c>null</c> then the result is 
        /// same as <see cref="Timeout.Infinite"/>.
        /// </summary>

        public static int ToTimeout(this TimeSpan? timeout)
        {
            return timeout == null 
                 ? Timeout.Infinite 
                 : timeout.Value.ToTimeout();
        }
    }
}
