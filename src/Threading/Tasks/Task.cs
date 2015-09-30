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
    #region Imports

    using System;
    using System.Threading;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="Task"/>.
    /// </summary>

    static partial class TaskExtensions
    {
        /// <summary>
        /// Returns a <see cref="Task{T}"/> that can be used as the
        /// <see cref="IAsyncResult"/> return value from the method
        /// that begin the operation of an API following the
        /// <a href="http://msdn.microsoft.com/en-us/library/ms228963.aspx">Asynchronous Programming Model</a>.
        /// If an <see cref="AsyncCallback"/> is supplied, it is invoked
        /// when the supplied task concludes (fails, cancels or completes
        /// successfully).
        /// </summary>

        public static Task<T> Apmize<T>(this Task<T> task, AsyncCallback callback, object state)
        {
            return Apmize(task, callback, state, null);
        }

        /// <summary>
        /// Returns a <see cref="Task{T}"/> that can be used as the
        /// <see cref="IAsyncResult"/> return value from the method
        /// that begin the operation of an API following the
        /// <a href="http://msdn.microsoft.com/en-us/library/ms228963.aspx">Asynchronous Programming Model</a>.
        /// If an <see cref="AsyncCallback"/> is supplied, it is invoked
        /// when the supplied task concludes (fails, cancels or completes
        /// successfully).
        /// </summary>

        public static Task<T> Apmize<T>(this Task<T> task, AsyncCallback callback, object state, TaskScheduler scheduler)
        {
            var result = task;

            TaskCompletionSource<T> tcs = null;
            if (task.AsyncState != state)
            {
                tcs = new TaskCompletionSource<T>(state);
                result = tcs.Task;
            }

            Task t = task;
            if (tcs != null)
            {
                t = t.ContinueWith(delegate { tcs.TryConcludeFrom(task); },
                                   CancellationToken.None,
                                   TaskContinuationOptions.ExecuteSynchronously,
                                   TaskScheduler.Default);
            }
            if (callback != null)
            {
                // ReSharper disable RedundantAssignment
                t = t.ContinueWith(delegate { callback(result); }, // ReSharper restore RedundantAssignment
                                   CancellationToken.None,
                                   TaskContinuationOptions.None,
                                   scheduler ?? TaskScheduler.Default);
            }

            return result;
        }

        /// <summary>
        /// Returns a <see cref="Task{T}"/> that can be used as the
        /// <see cref="IAsyncResult"/> return value from the method
        /// that begin the operation of an API following the
        /// <a href="http://msdn.microsoft.com/en-us/library/ms228963.aspx">Asynchronous Programming Model</a>.
        /// If an <see cref="AsyncCallback"/> is supplied, it is invoked
        /// when the supplied task concludes (fails, cancels or completes
        /// successfully).
        /// </summary>

        public static Task Apmize(this Task task, AsyncCallback callback, object state, TaskScheduler scheduler)
        {
            var result = task;

            TaskCompletionSource<object> tcs = null;
            if (task.AsyncState != state)
            {
                tcs = new TaskCompletionSource<object>(state);
                result = tcs.Task;
            }

            var t = task;
            if (tcs != null)
            {
                t = t.ContinueWith(delegate { tcs.TryConcludeFrom(task, delegate { return null; }); },
                                   CancellationToken.None,
                                   TaskContinuationOptions.ExecuteSynchronously,
                                   TaskScheduler.Default);
            }
            if (callback != null)
            {
                // ReSharper disable RedundantAssignment
                t = t.ContinueWith(delegate { callback(result); }, // ReSharper restore RedundantAssignment
                                   CancellationToken.None,
                                   TaskContinuationOptions.None,
                                   scheduler ?? TaskScheduler.Default);
            }

            return result;
        }
    }
}

#endif // NET4

#if NET45

namespace Mannex.Threading.Tasks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    static partial class TaskExtensions
    {
        /// <summary>
        /// Specifies whether an awaiter used to wait this
        /// <see cref="Task"/> continues on the captured context.
        /// </summary>

        public static ConfiguredTaskAwaitable<T> ContinueOnCapturedContext<T>(this Task<T> task, bool value)
        {
            return task.ConfigureAwait(value);
        }

        /// <summary>
        /// Creates a task that will complete when all of the task in the
        /// sequence complete, whether they succeed, fail or cancel. An
        /// additional
        /// </summary>
        /// <remarks>
        /// This method differs from <see cref="Task.WhenAll(IEnumerable{Task})"/>
        /// in that it completes irrespective of whether the tasks succeeded,
        /// failed or cancelled.
        /// </remarks>

        public static Task<T[]> WhenAllCompleted<T>(this IEnumerable<T> tasks)
            where T : Task
        {
            return tasks.WhenAllCompleted(CancellationToken.None);
        }

        /// <summary>
        /// Creates a task that will complete when all of the task in the
        /// sequence complete, whether they succeed, fail or cancel. An
        /// additional parameter specifies a <see cref="CancellationToken"/>
        /// the can be used to cancel the returned task.
        /// </summary>
        /// <remarks>
        /// This method differs from <see cref="Task.WhenAll(IEnumerable{Task})"/>
        /// in that it completes irrespective of whether the tasks succeeded,
        /// failed or cancelled.
        /// </remarks>

        public static async Task<T[]> WhenAllCompleted<T>(this IEnumerable<T> tasks,
            CancellationToken cancellationToken)
            where T : Task
        {
            if (tasks == null) throw new ArgumentNullException("tasks");
            var result = tasks.ToArray();
            var pending = result.ToList();
            while (pending.Count > 0)
            {
                if (cancellationToken.CanBeCanceled)
                    cancellationToken.ThrowIfCancellationRequested();
                var task = await Task.WhenAny(pending).ConfigureAwait(false);
                pending.Remove((T) task);
            }
            return result;
        }
    }
}

#endif // NET45
