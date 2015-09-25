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

#if NET4

namespace Mannex.Threading.Tasks
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Extension methods for <see cref="CancellationToken"/>.
    /// </summary>

    static partial class CancellationTokenExtensions
    {
        /// <summary>
        /// Creates a task the completes when the cancellation token enters
        /// the cancelled state.
        /// </summary>

        public static Task AsTask(this CancellationToken cancellationToken)
        {
            return cancellationToken.AsTask(0);
        }

        /// <summary>
        /// Creates a task the completes when the cancellation token enters
        /// the cancelled state. An additional parameter specifies the result
        /// to return for the task.
        /// </summary>

        public static Task<T> AsTask<T>(this CancellationToken cancellationToken, T result)
        {
            #if NET45
            if (cancellationToken.IsCancellationRequested)
                return Task.FromResult(result);
            #endif

            var tcs = new TaskCompletionSource<T>();

            #if !NET45
            if (cancellationToken.IsCancellationRequested)
            {
                tcs.SetResult(result);
                return tcs.Task;
            }
            #endif

            var registration = new CancellationTokenRegistration[1];
            registration[0] = cancellationToken.Register(() =>
            {
                tcs.TrySetResult(result);
                registration[0].Dispose();
            });
            return tcs.Task;
        }
    }
}

#endif
