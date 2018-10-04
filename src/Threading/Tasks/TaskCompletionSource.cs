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
    using System.Diagnostics;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="TaskCompletionSource{TResult}"/>.
    /// </summary>

    static partial class TaskCompletionSourceExtensions
    {
        /// <summary>
        /// Attempts to conclude <see cref="TaskCompletionSource{TResult}"/>
        /// as being canceled, faulted or having completed successfully
        /// based on the corresponding status of the given 
        /// <see cref="Task{T}"/>.
        /// </summary>

        public static bool TryConcludeFrom<T>(this TaskCompletionSource<T> source, Task<T> task)
        {
            return source.TryConcludeFrom(task, t => t.Result);
        }

        /// <summary>
        /// Attempts to conclude <see cref="TaskCompletionSource{TResult}"/>
        /// as being canceled, faulted or having completed successfully
        /// based on the corresponding status of the given 
        /// <see cref="Task{T}"/>.
        /// </summary>

        public static bool TryConcludeFrom<T, TTask>(this TaskCompletionSource<T> source, TTask task, Func<TTask, T> resultSelector)
            where TTask : Task
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (task == null) throw new ArgumentNullException(nameof(task));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            if (task.IsCanceled)
            {
                source.TrySetCanceled();
            }
            else if (task.IsFaulted)
            {
                var aggregate = task.Exception;
                Debug.Assert(aggregate != null);
                source.TrySetException(aggregate.InnerExceptions);
            }
            else if (TaskStatus.RanToCompletion == task.Status)
            {
                source.TrySetResult(resultSelector(task));
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}
