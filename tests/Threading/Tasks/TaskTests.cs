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

namespace Mannex.Tests.Threading.Tasks
{
    #region Improts

    using System;
    using System.Threading.Tasks;
    using Mannex.Threading.Tasks;
    using Xunit;

    #endregion

    public class TaskTests
    {
        [Fact]
        public void ApmizeNop()
        {
            var task = new TaskCompletionSource<object>().Task;
            var result = task.Apmize(null, null);
            Assert.Same(task, result);
        }

        [Fact]
        public void ApmizeWithCallbackOnly()
        {
            var tcs = new TaskCompletionSource<object>();
            var task = tcs.Task;
            IAsyncResult actualResult = null;
            var result = task.Apmize(ar => actualResult = ar, null);
            tcs.SetResult(null);
            Assert.Same(task, result);
            Assert.NotNull(actualResult);
            Assert.Same(result, actualResult);
        }

        [Fact]
        public void ApmizeWithStateObnly()
        {
            var task = new TaskCompletionSource<object>().Task;
            var state = new object();
            var result = task.Apmize(null, state);
            Assert.NotSame(task, result);
            Assert.Same(state, result.AsyncState);
        }

        [Fact]
        public void ApmizeWithCallbackAndState()
        {
            var tcs = new TaskCompletionSource<object>();
            var task = tcs.Task;
            IAsyncResult actualResult = null;
            var state = new object();
            var isCompletedDuringCallback = (bool?)null;
            var resultCell = new Task<object>[1];
            var result = resultCell[0] = task.Apmize(ar =>
            {
                actualResult = ar;
                isCompletedDuringCallback = resultCell[0].IsCompleted;
            }, state);
            tcs.SetResult(null);
            Assert.NotSame(task, result);
            Assert.Same(state, result.AsyncState);
            Assert.NotNull(actualResult);
            Assert.Same(result, actualResult);
            Assert.NotNull(isCompletedDuringCallback);
            Assert.True((bool)isCompletedDuringCallback);
        }
    }
}