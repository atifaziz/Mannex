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
    using System.Diagnostics.Eventing.Reader;
    using Xunit;

    #endregion

    public class IComparableTests
    {
        [Fact]
        public void MinMaxFailsWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() => IComparableExtensions.MinMax(null, new Version(), new Version()));
            Assert.Equal("value", e.ParamName);
        }

        [Fact]
        public void MinMax()
        {
            Assert.Equal(42, 042.MinMax(10, 50));
            Assert.Equal(10, 000.MinMax(10, 50));
            Assert.Equal(50, 100.MinMax(10, 50));
        }

        [Fact]
        public void MinMaxNullable()
        {
            Assert.Equal(42,   ((int?) 042).MinMax(10, 50));
            Assert.Equal(10,   ((int?) 000).MinMax(10, 50));
            Assert.Equal(50,   ((int?) 100).MinMax(10, 50));
            Assert.Equal(null, ((int?) null).MinMax(10, 50));
        }

        [Fact]
        public void IsBetweenFailsWithNullReferenceForThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() => IComparableExtensions.IsBetween(null, new Version(), new Version()));
            Assert.Equal("value", e.ParamName);
        }

        [Theory]
        [InlineData(true , -5,   0, 5)]
        [InlineData(false, -5,  10, 5)]
        [InlineData(true , -5,  -5, 5)]
        [InlineData(true , -5,   5, 5)]
        [InlineData(false, -5, -10, 5)]
        public void IsBetween(bool expected, int lower, int test, int upper)
        {
            Assert.Equal(expected, test.IsBetween(lower, upper));
        }

        [Theory]
        [InlineData(true , -5,    0, 5)]
        [InlineData(false, -5,   10, 5)]
        [InlineData(true , -5,   -5, 5)]
        [InlineData(true , -5,    5, 5)]
        [InlineData(false, -5,  -10, 5)]
        [InlineData(null , -5, null, 5)]
        public void IsBetweenNullable(bool? expected, int lower, int? test, int upper)
        {
            Assert.Equal(expected, test.IsBetween(lower, upper));
        }

        [Fact]
        public void IsInBetweenFailsWithNullReferenceForThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() => IComparableExtensions.IsInBetween(null, new Version(), new Version()));
            Assert.Equal("value", e.ParamName);
        }

        [Theory]
        [InlineData(true , -5,   0, 5)]
        [InlineData(false, -5,  10, 5)]
        [InlineData(false, -5,  -5, 5)]
        [InlineData(false, -5,   5, 5)]
        [InlineData(false, -5, -10, 5)]
        public void IsInBetween(bool expected, int lower, int test, int upper)
        {
            Assert.Equal(expected, test.IsInBetween(lower, upper));
        }

        [Theory]
        [InlineData(true , -5,    0, 5)]
        [InlineData(false, -5,   10, 5)]
        [InlineData(false, -5,   -5, 5)]
        [InlineData(false, -5,    5, 5)]
        [InlineData(false, -5,  -10, 5)]
        [InlineData(null , -5, null, 5)]
        public void IsInBetweenNullable(bool? expected, int lower, int? test, int upper)
        {
            Assert.Equal(expected, test.IsInBetween(lower, upper));
        }
    }
}
