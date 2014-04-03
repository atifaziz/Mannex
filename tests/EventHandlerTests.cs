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
    #region Imports

    using System;
    using Xunit;

    #endregion

    public class EventHandlerTests
    {
        public event EventHandler<EventArgs> EventT;
        public event EventHandler Event;

        [Fact]
        public void FireFailsWithNullSender()
        {
            Assert.Throws<ArgumentNullException>(() => 
                EventT.Fire(null, EventArgs.Empty));
        }

        [Fact]
        public void FireFailsWithNullArgs()
        {
            Assert.Throws<ArgumentNullException>(() =>
                EventT.Fire(new object(), null));
        }

        [Fact]
        public void FireFires()
        {
            var fired = false;
            var args = new EventArgs();
            EventT += ((sender, aargs) =>
            {
                Assert.Same(this, sender);
                Assert.Same(args, aargs);
                fired = true;
            });
            Assert.True(EventT.Fire(this, args));
            Assert.True(fired);
        }

        [Fact]
        public void FireReturnsFalseWhenNotFired()
        {
            Assert.False(EventT.Fire(this, EventArgs.Empty));
        }

        [Fact]
        public void OnceFailsWithNullHandler()
        {
            var e = Assert.Throws<ArgumentNullException>(() =>
                EventHandlerExtensions.Once(null, delegate { }, delegate { }));
            Assert.Equal("handler", e.ParamName);
        }

        [Fact]
        public void OnceFailsWithNullAddHandler()
        {
            var e = Assert.Throws<ArgumentNullException>(() =>
                new EventHandler(delegate { }).Once(null, delegate { }));
            Assert.Equal("addHandler", e.ParamName);
        }

        [Fact]
        public void OnceFailsWithNullRemoveHandler()
        {
            var e = Assert.Throws<ArgumentNullException>(() =>
                new EventHandler(delegate { }).Once(delegate { }, null));
            Assert.Equal("removeHandler", e.ParamName);
        }

        [Fact]
        public void OnceTFailsWithNullHandler()
        {
            var e = Assert.Throws<ArgumentNullException>(() =>
                EventHandlerExtensions.Once<EventArgs>(null, delegate { }, delegate { }));
            Assert.Equal("handler", e.ParamName);
        }

        [Fact]
        public void OnceTFailsWithNullAddHandler()
        {
            var e = Assert.Throws<ArgumentNullException>(() =>
                new EventHandler<EventArgs>(delegate { }).Once(null, delegate { }));
            Assert.Equal("addHandler", e.ParamName);
        }

        [Fact]
        public void OnceTFailsWithNullRemoveHandler()
        {
            var e = Assert.Throws<ArgumentNullException>(() =>
                new EventHandler<EventArgs>(delegate { }).Once(delegate { }, null));
            Assert.Equal("removeHandler", e.ParamName);
        }

        [Fact]
        public void OnceT()
        {
            var counter = 0;
            EventHandler<EventArgs> handler = delegate { counter++; };
            handler.Once(h => EventT += h, h => EventT -= h);
            EventT.Fire(this, EventArgs.Empty);
            Assert.Equal(1, counter);
            EventT.Fire(this, EventArgs.Empty);
            Assert.Equal(1, counter);
        }

        [Fact]
        public void Once()
        {
            var counter = 0;
            EventHandler handler = delegate { counter++; };
            handler.Once(h => Event += h, h => Event -= h);
            Event.Fire(this, EventArgs.Empty);
            Assert.Equal(1, counter);
            Event.Fire(this, EventArgs.Empty);
            Assert.Equal(1, counter);
        }

        [Fact]
        public void OnceWhenHandlerFails()
        {
            var counter = 0;
            EventHandler handler = delegate
            { 
                if (++counter == 1) throw new ApplicationException();
            };
            handler.Once(h => Event += h, h => Event -= h);
            try
            {
                Event.Fire(this, EventArgs.Empty);
            }
            catch (ApplicationException) { }
            Assert.Equal(1, counter);
            Event.Fire(this, EventArgs.Empty);
            Assert.Equal(1, counter);
        }

        [Fact]
        public void OnceTWhenHandlerFails()
        {
            var counter = 0;
            EventHandler<EventArgs> handler = delegate
            {
                if (++counter == 1) throw new ApplicationException();
            };
            handler.Once(h => EventT += h, h => EventT -= h);
            try
            {
                EventT.Fire(this, EventArgs.Empty);
            }
            catch (ApplicationException) { }
            Assert.Equal(1, counter);
            EventT.Fire(this, EventArgs.Empty);
            Assert.Equal(1, counter);
        }
    }
}