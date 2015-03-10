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

    public class DateTimeTests
    {
        [Fact]
        public void GetQuarter()
        {
            Assert.Equal(1, new DateTime(2014,  1, 15).GetQuarter());
            Assert.Equal(1, new DateTime(2014,  2, 15).GetQuarter());
            Assert.Equal(1, new DateTime(2014,  3, 15).GetQuarter());
            Assert.Equal(2, new DateTime(2014,  4, 15).GetQuarter());
            Assert.Equal(2, new DateTime(2014,  5, 15).GetQuarter());
            Assert.Equal(2, new DateTime(2014,  6, 15).GetQuarter());
            Assert.Equal(3, new DateTime(2014,  7, 15).GetQuarter());
            Assert.Equal(3, new DateTime(2014,  8, 15).GetQuarter());
            Assert.Equal(3, new DateTime(2014,  9, 15).GetQuarter());
            Assert.Equal(4, new DateTime(2014, 10, 15).GetQuarter());
            Assert.Equal(4, new DateTime(2014, 11, 15).GetQuarter());
            Assert.Equal(4, new DateTime(2014, 12, 15).GetQuarter());
        }

        [Fact]
        public void FirstDayOfMonth()
        {
            var input = new DateTime(2014, 6, 15, 1, 2, 3, 4, DateTimeKind.Local);
            Assert.Equal(new DateTime(2014, 6, 1, 1, 2, 3, 4, DateTimeKind.Local), input.FirstDayOfMonth());
        }

        [Fact]
        public void FirstDayOfQuarter()
        {
            var input = new DateTime(2014, 6, 15, 1, 2, 3, 4, DateTimeKind.Local);
            Assert.Equal(new DateTime(2014, 4, 1, 1, 2, 3, 4, DateTimeKind.Local), input.FirstDayOfQuarter());
        }

        [Fact]
        public void FirstDayOfYear()
        {
            var input = new DateTime(2014, 6, 15, 1, 2, 3, 4, DateTimeKind.Local);
            Assert.Equal(new DateTime(2014, 1, 1, 1, 2, 3, 4, DateTimeKind.Local), input.FirstDayOfYear());
        }

        [Fact]
        public void WithTimeFrom()
        {
            const int y = 2014;
            const int mo = 6;
            const int d = 15;
            const int h = 12;
            const int mi = 34;
            const int s = 56;
            const int ms = 789;
            Assert.Equal(new DateTime(y, mo, d, h, mi, s, ms),
                new DateTime(y, mo, d, 1, 2, 3, 456).WithTimeFrom(new DateTime(2013, 5, 14, h, mi, s, ms)));
        }

        [Fact]
        public void WithTimeFromFailsWithMismatchingKind()
        {
            Assert.Throws<ArgumentException>(() => DateTime.Now.WithTimeFrom(DateTime.UtcNow));
        }
    }
}