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

namespace Mannex.Threading.Tasks
{
    #region Imports

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="TaskFactory"/>.
    /// </summary>

    static partial class TaskFactoryExtensions
    {
        /// <summary>
        /// Creates and starts a new <see cref="Task" /> that iterates
        /// through a sequence of tasks where each task is run as a
        /// continuation of its predecessor.
        /// </summary>

        public static Task StartNew(
            this TaskFactory taskFactory, IEnumerable<Task> job)
        {
            return StartNew(taskFactory, job, CancellationToken.None);
        }

        /// <summary>
        /// Creates and starts a new <see cref="Task" /> that iterates
        /// through a sequence of tasks where each task is run as a
        /// continuation of its predecessor.
        /// </summary>

        public static Task StartNew(
            this TaskFactory taskFactory, IEnumerable<Task> job,
            CancellationToken cancellationToken)
        {
            return StartNew(taskFactory, job, cancellationToken, TaskCreationOptions.None, null);
        }

        /// <summary>
        /// Creates and starts a new <see cref="Task" /> that iterates
        /// through a sequence of tasks where each task is run as a
        /// continuation of its predecessor.
        /// </summary>

        public static Task StartNew(
            this TaskFactory taskFactory, IEnumerable<Task> job,
            TaskCreationOptions creationOptions)
        {
            return StartNew(taskFactory, job, CancellationToken.None, creationOptions, null);
        }

        /// <summary>
        /// Creates and starts a new <see cref="Task" /> that iterates
        /// through a sequence of tasks where each task is run as a
        /// continuation of its predecessor.
        /// </summary>

        public static Task StartNew(
            this TaskFactory taskFactory, IEnumerable<Task> job,
            CancellationToken cancellationToken, TaskCreationOptions creationOptions,
            TaskScheduler scheduler)
        {
            return StartNew(taskFactory, job, null, cancellationToken, creationOptions, scheduler);
        }

        /// <summary>
        /// Creates and starts a new <see cref="Task" /> that iterates
        /// through a sequence of tasks where each task is run as a
        /// continuation of its predecessor.
        /// </summary>

        public static Task StartNew(
            this TaskFactory taskFactory, IEnumerable<Task> job, object state)
        {
            return StartNew(taskFactory, job, state, CancellationToken.None);
        }

        /// <summary>
        /// Creates and starts a new <see cref="Task" /> that iterates
        /// through a sequence of tasks where each task is run as a
        /// continuation of its predecessor.
        /// </summary>

        public static Task StartNew(
            this TaskFactory taskFactory, IEnumerable<Task> job, object state,
            CancellationToken cancellationToken)
        {
            return StartNew(taskFactory, job, state, cancellationToken, TaskCreationOptions.None, null);
        }

        /// <summary>
        /// Creates and starts a new <see cref="Task" /> that iterates
        /// through a sequence of tasks where each task is run as a
        /// continuation of its predecessor.
        /// </summary>

        public static Task StartNew(
            this TaskFactory taskFactory, IEnumerable<Task> job, object state,
            TaskCreationOptions creationOptions)
        {
            return StartNew(taskFactory, job, state, CancellationToken.None, creationOptions, null);
        }

        /// <summary>
        /// Creates and starts a new <see cref="Task" /> that iterates
        /// through a sequence of tasks where each task is run as a
        /// continuation of its predecessor.
        /// </summary>

        public static Task StartNew(
            this TaskFactory taskFactory, IEnumerable<Task> job, object state,
            CancellationToken cancellationToken, TaskCreationOptions creationOptions,
            TaskScheduler scheduler)
        {
            if (taskFactory == null) throw new ArgumentNullException(nameof(taskFactory));
            if (job == null) throw new ArgumentNullException(nameof(job));

            var tcs = new TaskCompletionSource<object>(state, creationOptions);

            if (cancellationToken.IsCancellationRequested)
            {
                tcs.SetCanceled();
            }
            else
            {
                IEnumerator<Task> task;

                try
                {
                    task = job.GetEnumerator();
                }
                catch (Exception e)
                {
                    tcs.SetException(e);
                    return tcs.Task;
                }

                Quantum();

                void Quantum() // ReSharper disable AccessToModifiedClosure
                {
                    while (true)
                    {
                        if (cancellationToken.IsCancellationRequested)
                            tcs.SetCanceled();

                        bool done;
                        try
                        {
                            done = !task.MoveNext();
                        }
                        catch (Exception e)
                        {
                            try { task.Dispose(); } // ReSharper disable once EmptyGeneralCatchClause
                            catch { }
                            tcs.SetException(e);
                            return;
                        }

                        if (done)
                        {
                            tcs.SetResult(null);
                            break;
                        }

                        if (!task.Current.IsCompleted)
                        {
                            if (scheduler != null)
                                task.Current.ContinueWith(s => Quantum(), scheduler);
                            else
                                task.Current.ContinueWith(s => Quantum());
                            break;
                        }
                    }
                }
                // ReSharper restore AccessToModifiedClosure
            }

            return tcs.Task;
        }
    }
}
