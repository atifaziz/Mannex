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
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Extension methods for <see cref="WaitHandle"/>.
    /// </summary>

    static partial class WaitHandleExtensions
    {
        /// <summary>
        /// Asynchronously and indefinitely waits for a
        /// <see cref="WaitHandle"/> to become signaled.
        /// </summary>

        public static Task<bool> WaitOneAsync(this WaitHandle handle)
        {
            return WaitOneAsync(handle, null, CancellationToken.None);
        }

        /// <summary>
        /// Asynchronously waits for a <see cref="WaitHandle"/> to become
        /// signaled. An additional parameter specifies a time-out for the
        /// wait to be satisfied.
        /// </summary>

        public static Task<bool> WaitOneAsync(this WaitHandle handle, TimeSpan? timeout)
        {
            return WaitOneAsync(handle, timeout, CancellationToken.None);
        }

        /// <summary>
        /// Asynchronously and indefinitely waits for a
        /// <see cref="WaitHandle"/> to become signaled. An additional
        /// parameter specifies a <see cref="CancellationToken"/> to be used
        /// for cancelling the wait.
        /// </summary>

        public static Task<bool> WaitOneAsync(this WaitHandle handle, CancellationToken cancellationToken)
        {
            return WaitOneAsync(handle, null, cancellationToken);
        }

        /// <summary>
        /// Asynchronously waits for a <see cref="WaitHandle"/> to become
        /// signaled. Additional parameters specify a time-out for the wait as
        /// well as a <see cref="CancellationToken"/> to be used for cancelling
        /// the wait.
        /// </summary>

        public static Task<bool> WaitOneAsync(this WaitHandle handle, TimeSpan? timeout, CancellationToken cancellationToken)
        {
            if (handle == null) throw new ArgumentNullException("handle");

            cancellationToken.ThrowIfCancellationRequested();

            var tcs = new TaskCompletionSource<bool>();

            var rwhref = new[] { default(RegisteredWaitHandle) };
            var rwh = rwhref[0] = ThreadPool.RegisterWaitForSingleObject(handle,
                (_, timedOut) =>
                {
                    if (tcs.TrySetResult(!timedOut))
                        rwhref[0].Unregister(null);
                },
                null, timeout.ToTimeout(), executeOnlyOnce: true);

            try
            {
                if (cancellationToken.CanBeCanceled)
                {
                    var ctrref = new[] { default(CancellationTokenRegistration) };
                    ctrref[0] = cancellationToken.Register(() =>
                    {
                        if (tcs.TrySetCanceled())
                            rwhref[0].Unregister(null);
                        ctrref[0].Dispose();
                    });
                }

                rwh = null; // safe to relinquish ownership
                return tcs.Task;
            }
            finally
            {
                if (rwh != null)
                    rwh.Unregister(null);
            }
        }
    }
}
