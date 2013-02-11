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
    #region Imports

    using System;
    using System.Threading.Tasks;
    using Mannex.Threading.Tasks;
    using Xunit;

    #endregion

    public class TaskCompletionSourceTests
    {
        [Fact]
        public void TryConcludeFromFailsWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() =>
                        TaskCompletionSourceExtensions.TryConcludeFrom(null, new TaskCompletionSource<object>().Task));
            Assert.Equal("source", e.ParamName);
        }

        [Fact]
        public void TryConcludeFromFailsWithNullTask()
        {
            var tcs = new TaskCompletionSource<object>();
            var e = Assert.Throws<ArgumentNullException>(() => tcs.TryConcludeFrom(null));
            Assert.Equal("task", e.ParamName);
        }

        [Fact]
        public void TryConcludeFromSetsCompleted()
        {
            var subject = new
            {
                This = new TaskCompletionSource<object>(),
                That = new TaskCompletionSource<object>(),
            };
            var result = new object();
            subject.That.TrySetResult(result);
            Assert.True(subject.This.TryConcludeFrom(subject.That.Task));
            Assert.True(subject.This.Task.IsCompleted);
            Assert.Same(result, subject.This.Task.Result);
        }

        [Fact]
        public void TryConcludeFromSetsCanceled()
        {
            var subject = new
            {
                This = new TaskCompletionSource<object>(),
                That = new TaskCompletionSource<object>(),
            };
            subject.That.TrySetCanceled();
            Assert.True(subject.This.TryConcludeFrom(subject.That.Task));
            Assert.True(subject.This.Task.IsCanceled);
        }

        [Fact]
        public void TryConcludeFromSetsFailed()
        {
            var subject = new
            {
                This = new TaskCompletionSource<object>(),
                That = new TaskCompletionSource<object>(),
            };
            var e = new Exception();
            subject.That.TrySetException(e);
            Assert.True(subject.This.TryConcludeFrom(subject.That.Task));
            Assert.True(subject.This.Task.IsFaulted);                   // ReSharper disable PossibleNullReferenceException
            Assert.Same(e, subject.This.Task.Exception.InnerException); // ReSharper restore PossibleNullReferenceException
        }

        [Fact]
        public void TryConcludeFromWhenTaskPending()
        {
            var subject = new
            {
                This = new TaskCompletionSource<object>(),
                That = new TaskCompletionSource<object>(),
            };
            var status = subject.This.Task.Status;
            Assert.False(subject.This.TryConcludeFrom(subject.That.Task));
            Assert.Equal(status, subject.This.Task.Status);
        }
    }
}