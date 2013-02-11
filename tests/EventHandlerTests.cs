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
        public event EventHandler<EventArgs> Event;

        [Fact]
        public void FireFailsWithNullSender()
        {
            Assert.Throws<ArgumentNullException>(() => 
                Event.Fire(null, EventArgs.Empty));
        }

        [Fact]
        public void FireFailsWithNullArgs()
        {
            Assert.Throws<ArgumentNullException>(() =>
                Event.Fire(new object(), null));
        }

        [Fact]
        public void FireFires()
        {
            var fired = false;
            var args = new EventArgs();
            Event += ((sender, aargs) =>
            {
                Assert.Same(this, sender);
                Assert.Same(args, aargs);
                fired = true;
            });
            Assert.True(Event.Fire(this, args));
            Assert.True(fired);
        }

        [Fact]
        public void FireReturnsFalseWhenNotFired()
        {
            Assert.False(Event.Fire(this, EventArgs.Empty));
        }
    }
}