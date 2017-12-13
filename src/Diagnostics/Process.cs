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
    using System.IO;
    using System.Threading;
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

        /// <summary>
        /// Begins asynchronous read operations on the re-directed <see cref="Process.StandardOutput"/> 
        /// and <see cref="Process.StandardError"/> of the application. 
        /// Each line on the standard output is written to a <see cref="TextWriter"/>.
        /// </summary>
        /// <returns>
        /// Returns an action that can be used to wait on outputs to drain.
        /// </returns>

        public static Func<TimeSpan?, bool> BeginReadLine(this Process process, TextWriter output)
        {
            return BeginReadLine(process, output, null);
        }

        /// <summary>
        /// Begins asynchronous read operations on the re-directed <see cref="Process.StandardOutput"/> 
        /// and <see cref="Process.StandardError"/> of the application.
        /// Each line on either is written to a respective <see cref="TextWriter"/>.
        /// </summary>
        /// <returns>
        /// Returns an action that can be used to wait on outputs to drain.
        /// </returns>

        public static Func<TimeSpan?, bool> BeginReadLine(this Process process, TextWriter output, TextWriter error)
        {
            if (process == null) throw new ArgumentNullException("process");

            return BeginReadLineImpl(process,
                (output ?? TextWriter.Null).WriteLine,
                (error ?? TextWriter.Null).WriteLine);
        }

        /// <summary>
        /// Begins asynchronous read operations on the re-directed <see cref="Process.StandardOutput"/> 
        /// and <see cref="Process.StandardError"/> of the application. Each line on the standard output
        /// is sent to a callback.
        /// </summary>
        /// <returns>
        /// Returns an action that can be used to wait on outputs to drain.
        /// </returns>
        
        public static Func<TimeSpan?, bool> BeginReadLine(this Process process, Action<string> output)
        {
            return BeginReadLine(process, output, null);
        }

        /// <summary>
        /// Begins asynchronous read operations on the re-directed <see cref="Process.StandardOutput"/> 
        /// and <see cref="Process.StandardError"/> of the application. Each line on either is
        /// sent to a respective callback.
        /// </summary>
        /// <returns>
        /// Returns an action that can be used to wait on outputs to drain.
        /// </returns>
        
        public static Func<TimeSpan?, bool> BeginReadLine(this Process process, Action<string> output, Action<string> error)
        {
            if (process == null) throw new ArgumentNullException("process");

            return BeginReadLineImpl(process,
                output ?? (TextWriter.Null.WriteLine),
                error ?? (TextWriter.Null.WriteLine));
        }

        private static Func<TimeSpan?, bool> BeginReadLineImpl(Process process, Action<string> output, Action<string> error)
        {
            var outeof = new ManualResetEvent(false);
            process.OutputDataReceived += OnDataReceived(output, () => outeof.Set());
            process.BeginOutputReadLine();

            var erreof = new ManualResetEvent(false);
            process.ErrorDataReceived += OnDataReceived(error, () => erreof.Set());
            process.BeginErrorReadLine();

            return timeout => WaitHandle.WaitAll(new[] { outeof, erreof }, timeout.ToTimeout());
        }

        private static DataReceivedEventHandler OnDataReceived(
            Action<string> line, Action eof)
        {
            return (sender, e) =>
            {
                if (e.Data != null)
                    line(e.Data);
                else
                    eof();
            };
        }
    }
}
