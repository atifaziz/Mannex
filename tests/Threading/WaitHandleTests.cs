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

namespace Mannex.Tests.Threading
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Mannex.Threading;
    using Xunit;

    public class WaitHandleTests
    {
        [Fact]
        public void WaitOneAsyncThrowsWithNullThis()
        {
            foreach (var e in WaitOneAsyncWithNullThis())
                Assert.Equal("handle", e.Result.ParamName);
        }

        static IEnumerable<Task<ArgumentNullException>> WaitOneAsyncWithNullThis()
        {
            yield return Assert.ThrowsAsync<ArgumentNullException>(() => WaitHandleExtensions.WaitOneAsync(null));
            yield return Assert.ThrowsAsync<ArgumentNullException>(() => WaitHandleExtensions.WaitOneAsync(null, CancellationToken.None));
            yield return Assert.ThrowsAsync<ArgumentNullException>(() => WaitHandleExtensions.WaitOneAsync(null, TimeSpan.Zero));
            yield return Assert.ThrowsAsync<ArgumentNullException>(() => WaitHandleExtensions.WaitOneAsync(null, TimeSpan.Zero, CancellationToken.None));
        }

        [Fact]
        public async void WaitOneAsyncTimesOut()
        {
            Assert.False(await CreateUnsignaledEvent().WaitOneAsync(TimeSpan.FromSeconds(0.5)));
        }

        [Fact]
        public async void WaitOneAsync()
        {
            var mre = CreateUnsignaledEvent();
            var task = mre.WaitOneAsync();
            mre.Set();
            Assert.True(await task);
        }

        [Fact]
        public async void WaitOneAsyncWithAlreadySignaledWaitHandle()
        {
            Assert.True(await CreateSignaledEvent().WaitOneAsync());
        }

        [Fact]
        public async void WaitOneAsyncWithPreCancellation()
        {
            var cancellationToken = new CancellationToken(canceled: true);
            await Assert.ThrowsAsync<OperationCanceledException>(() =>
                CreateSignaledEvent().WaitOneAsync(cancellationToken));
        }

        [Fact]
        public async void WaitOneAsyncWithPostCancellation()
        {
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(0.5));
            await Assert.ThrowsAsync<TaskCanceledException>(() =>
                CreateUnsignaledEvent().WaitOneAsync(cts.Token));
        }

        static ManualResetEvent CreateSignaledEvent()
        {
            return new ManualResetEvent(initialState: true);
        }

        static ManualResetEvent CreateUnsignaledEvent()
        {
            return new ManualResetEvent(initialState: false);
        }
    }
}