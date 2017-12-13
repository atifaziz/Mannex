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
    #region Imports

    using System;
    using System.Threading;
    using Mannex.Threading;
    using Xunit;

    #endregion

    public class TimeSpanTests
    {
        [Fact]
        public void ToTimeoutReturnsRoundedMilliseconds()
        {
            Assert.Equal(1234, TimeSpan.FromSeconds(1.2344).ToTimeout());
            Assert.Equal(1235, TimeSpan.FromSeconds(1.2345).ToTimeout());
        }

        [Fact]
        public void ToTimeoutOnNullReturnsInfiniteTimeout()
        {
            Assert.Equal(Timeout.Infinite, ((TimeSpan?) null).ToTimeout());
        }

        [Fact]
        public void ToTimeoutOnNonNullReturnsRoundedMilliseconds()
        {
            Assert.Equal(1234, ((TimeSpan?) TimeSpan.FromSeconds(1.2344)).ToTimeout());
            Assert.Equal(1235, ((TimeSpan?) TimeSpan.FromSeconds(1.2345)).ToTimeout());
        }
    }
}