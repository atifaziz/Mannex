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

namespace Mannex.Diagnostics
{
    #region Imports

    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using Threading;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="Process"/>.
    /// </summary>

    static partial class ProcessExtensions
    {
        /// <summary>
        /// Attempts to kill the process identified by the <see cref="Process"/>
        /// object and returns <c>null</c> on success otherwise the error
        /// that occurred in the attempt.
        /// </summary>

        public static Exception TryKill(this Process process)
        {
            if (process == null) throw new ArgumentNullException("process");

            try
            {
                process.Kill();
                return null;
            }
            catch (InvalidOperationException e)
            {
                // Occurs when:
                // - process has already exited. 
                // - no process is associated with this Process object.
                return e;
            }
            catch (Win32Exception e)
            {
                // Occurs when:
                // - associated process could not be terminated. 
                // - process is terminating.
                // - associated process is a Win16 executable.
                return e;
            }
        }

        /// <summary>
        /// Instructs the <see cref="Process"/> component to wait the specified 
        /// amount of time for the associated process to exit. If the specified
        /// time-out period is <c>null</c> then the wait is indefinite.
        /// </summary>

        public static bool WaitForExit(this Process process, TimeSpan? timeout)
        {
            if (process == null) throw new ArgumentNullException("process");
            return process.WaitForExit(timeout.ToTimeout());
        }
    }
}
