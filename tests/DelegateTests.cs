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

namespace Mannex.Tests
{
    using System;
    using System.Collections.Generic;
    using Xunit;

    public class DelegateTests
    {
        [Fact]
        public void InvokeAsEventHandlerWhileIgnoringErrorsFailsWithNonEventHandlers()
        {
            Action a = () => { };
            Action b = () => { };
            var c = Delegate.Combine(a, b);
            Assert.Throws<InvalidCastException>(() =>
                c.InvokeAsEventHandlerWhileIgnoringErrors(new object(), EventArgs.Empty));
        }

        [Fact]
        public void InvokeAsEventHandlerWhileIgnoringErrorsWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() =>
                DelegateExtensions.InvokeAsEventHandlerWhileIgnoringErrors(null, new object(), EventArgs.Empty));
            Assert.Equal("del", e.ParamName);
        }

        [Fact]
        public void InvokeAsEventHandlerWhileIgnoringErrors()
        {
            var calls = new List<EventHandler<EventArgs>>();
            EventHandler<EventArgs> all = null;
            var a = AccumulateWithInvocationNotice(ref all, calls.Add, delegate { });
            var b = AccumulateWithInvocationNotice(ref all, calls.Add, delegate { throw new Exception(); });
            var c = AccumulateWithInvocationNotice(ref all, calls.Add, delegate { });
            all.InvokeAsEventHandlerWhileIgnoringErrors(null, EventArgs.Empty);
            Assert.Equal(new[] { a, b, c }, calls);
        }

        static EventHandler<T> AccumulateWithInvocationNotice<T>(
            ref EventHandler<T> accumulator,
            Action<EventHandler<T>> onCall, EventHandler<T> handler)
        {
            accumulator += (sender, args) =>
            {
                onCall(handler);
                handler(sender, args);
            };
            return handler;
        }
    }
}