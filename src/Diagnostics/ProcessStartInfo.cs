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
    using System.Diagnostics;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="ProcessStartInfo"/>.
    /// </summary>

    static partial class ProcessStartInfoExtensions
    {
        /// <summary>
        /// Starts the process and waits for it to complete asynchronously.
        /// </summary>

        public static Task<Process> StartAsync(this ProcessStartInfo startInfo)
        {
            return StartAsync(startInfo, new CancellationToken());
        }

        /// <summary>
        /// Starts the process and waits for it to complete asynchronously.
        /// </summary>

        public static Task<Process> StartAsync(this ProcessStartInfo startInfo, CancellationToken cancellationToken)
        {
            return StartAsync(startInfo, null, null, cancellationToken);
        }

        /// <summary>
        /// Starts the process and waits for it to complete asynchronously.
        /// </summary>

        public static Task<T> StartAsync<T>(this ProcessStartInfo startInfo, Func<Process, string, T> selector)
        {
            return StartAsync(startInfo, CancellationToken.None, selector);
        }

        /// <summary>
        /// Starts the process and waits for it to complete asynchronously.
        /// </summary>

        public static Task<T> StartAsync<T>(this ProcessStartInfo startInfo, CancellationToken cancellationToken, Func<Process, string, T> selector)
        {
            return StartAsync(startInfo, false, cancellationToken, (p, stdout, _) => selector(p, stdout));
        }

        /// <summary>
        /// Starts the process and waits for it to complete asynchronously.
        /// </summary>

        public static Task<T> StartAsync<T>(this ProcessStartInfo startInfo, Func<Process, string, string, T> selector)
        {
            return StartAsync(startInfo, CancellationToken.None, selector);
        }

        /// <summary>
        /// Starts the process and waits for it to complete asynchronously.
        /// </summary>

        public static Task<T> StartAsync<T>(this ProcessStartInfo startInfo, CancellationToken cancellationToken, Func<Process, string, string, T> selector)
        {
            return StartAsync(startInfo, true, cancellationToken, selector);
        }

        static Task<T> StartAsync<T>(ProcessStartInfo startInfo, bool captureStandardError, CancellationToken cancellationToken, Func<Process, string, string, T> selector)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            var stdout = new StringWriter();
            var stderr = captureStandardError ? new StringWriter() : null;
            var task = StartAsync(startInfo, stdout, stderr, cancellationToken);
            return task.ContinueWith(t => selector(t.Result,
                                                   stdout.ToString(),
                                                   (stderr != null ? stderr.ToString() : null)),
                                     cancellationToken,
                                     TaskContinuationOptions.ExecuteSynchronously,
                                     TaskScheduler.Current);
        }

        /// <summary>
        /// Starts the process and waits for it to complete asynchronously.
        /// </summary>

        public static Task<Process> StartAsync(this ProcessStartInfo startInfo, TextWriter stdout)
        {
            return StartAsync(startInfo, stdout, CancellationToken.None);
        }

        /// <summary>
        /// Starts the process and waits for it to complete asynchronously.
        /// </summary>

        public static Task<Process> StartAsync(this ProcessStartInfo startInfo,
            TextWriter stdout, CancellationToken cancellationToken)
        {
            return startInfo.StartAsync(stdout, null, cancellationToken);
        }

        /// <summary>
        /// Starts the process and waits for it to complete asynchronously.
        /// </summary>

        public static Task<Process> StartAsync(this ProcessStartInfo startInfo,
            TextWriter stdout, TextWriter stderr)
        {
            return StartAsync(startInfo, stdout, stderr, CancellationToken.None);
        }

        /// <summary>
        /// Starts the process and waits for it to complete asynchronously.
        /// </summary>

        public static Task<Process> StartAsync(this ProcessStartInfo startInfo,
            TextWriter stdout, TextWriter stderr,
            CancellationToken cancellationToken)
        {
            if (startInfo == null) throw new ArgumentNullException("startInfo");

            cancellationToken.ThrowIfCancellationRequested();

            var tcs = new TaskCompletionSource<Process>();
            Process ownedProcess = null;
            try
            {
                var capturingOutput = stdout != null || stderr != null;
                if (capturingOutput)
                    startInfo.RedirectStandardOutput = startInfo.RedirectStandardError = true;

                var process = ownedProcess = Process.Start(startInfo);
                if (process == null)
                    throw new Exception("No process available for completion.");

                if (cancellationToken.CanBeCanceled)
                    cancellationToken.Register(() =>
                    {
                        if (capturingOutput)
                        {
                            process.CancelOutputRead();
                            process.CancelErrorRead();
                        }
                        process.TryKill();
                    });

                process.EnableRaisingEvents = true;

                var drain = capturingOutput
                          ? process.BeginReadLine(stdout, stderr)
                          : _ => true;

                process.Exited += delegate
                {
                    while (!drain(TimeSpan.FromSeconds(1)))
                    {
                        if (cancellationToken.IsCancellationRequested)
                        {
                            tcs.TrySetCanceled();
                            return;
                        }
                    }
                    tcs.TrySetResult(process);
                };

                ownedProcess = null;
            }
            finally
            {
                if (ownedProcess != null)
                    ownedProcess.Dispose();
            }

            return tcs.Task;
        }
    }
}
