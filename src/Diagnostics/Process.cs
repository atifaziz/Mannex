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
    #if NET4
    using System.Threading.Tasks;
    #endif
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

        static Func<TimeSpan?, bool> BeginReadLineImpl(Process process, Action<string> output, Action<string> error)
        {
            var done = new ManualResetEvent(false);
            var pending = 2;
            var onEof = new Action(() => { if (Interlocked.Decrement(ref pending) == 0) done.Set(); });

            process.OutputDataReceived += OnDataReceived(output, onEof);
            process.BeginOutputReadLine();

            process.ErrorDataReceived += OnDataReceived(error, onEof);
            process.BeginErrorReadLine();

            return timeout => done.WaitOne(timeout.ToTimeout());
        }

        static DataReceivedEventHandler OnDataReceived(
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

        #if NET4

        /// <summary>
        /// Creates <see cref="Task"/> that completes when the process exits
        /// with an exit code of zero and throws an <see cref="Exception"/>
        /// otherwise.
        /// </summary>

        public static Task AsTask(this Process process)
        {
            return AsTask(process, p => new Exception(string.Format("Process exited with the non-zero code {0}.", p.ExitCode)));
        }

        /// <summary>
        /// Creates <see cref="Task"/> that completes when the process exits
        /// with an exit code of zero and throws an <see cref="Exception"/>
        /// otherwise. An additional parameter enables a function to
        /// customize the <see cref="Exception"/> object thrown.
        /// </summary>

        public static Task AsTask(this Process process, Func<Process, Exception> errorSelector)
        {
            return process.AsTask(true, p => p,
                                  p => p.ExitCode != 0 ? errorSelector(p) : null,
                                  p => (object) null);
        }

        /// <summary>
        /// Creates <see cref="Task"/> that completes when the process exits.
        /// Additional parameters specify how to project the results from
        /// the execution of the process as a result or error for the task.
        /// </summary>
        /// <remarks>
        /// If <see cref="errorSelector"/> return <c>null</c> then the task
        /// is considered to have succeeded and <see cref="resultSelector"/>
        /// determines its result. If <see cref="errorSelector"/> returns
        /// an instance of <see cref="Exception"/> then the task is
        /// considered to have failed with that exception.
        /// </remarks>

        public static Task<TResult> AsTask<T, TResult>(this Process process, bool dispose,
            Func<Process, T> selector,
            Func<T, Exception> errorSelector,
            Func<T, TResult> resultSelector)
        {
            if (process == null) throw new ArgumentNullException("process");
            if (selector == null) throw new ArgumentNullException("selector");
            if (errorSelector == null) throw new ArgumentNullException("errorSelector");
            if (resultSelector == null) throw new ArgumentNullException("resultSelector");

            TaskCompletionSource<TResult> tcs;

            if (process.HasExited)
            {
                var temp = selector(process);
                var e = errorSelector(temp);
                if (e != null)
                {
                    tcs = new TaskCompletionSource<TResult>();
                    tcs.SetException(e);
                    return tcs.Task;
                }
                var result = resultSelector(temp);
                if (dispose)
                    process.Dispose();
                #if NET45
                return Task.FromResult(result);
                #else
                    tcs = new TaskCompletionSource<TResult>();
                    tcs.SetResult(result);
                    return tcs.Task;
                #endif
            }

            tcs = new TaskCompletionSource<TResult>();
            process.EnableRaisingEvents = true;
            process.Exited += delegate
            {
                var temp = selector(process);
                var e = errorSelector(temp);
                if (e != null)
                {
                    if (dispose)
                        process.Dispose();
                    tcs.SetException(e);
                }
                else
                {
                    var result = resultSelector(temp);
                    if (dispose)
                        process.Dispose();
                    tcs.TrySetResult(result);
                }
            };
            return tcs.Task;
        }

        #endif // NET4
    }
}