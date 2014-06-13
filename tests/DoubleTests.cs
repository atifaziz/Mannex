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
    using System.Linq;
    using Xunit;

    #endregion

    public class DoubleTests
    {
        [Fact]
        public void NullNaNReturnsOriginalWhenNonNaN()
        {
            Assert.Equal(1.23, (1.23).NullNaN());
        }

        [Fact]
        public void NullNaNReturnsNullWhenNaN()
        {
            Assert.Equal(null, double.NaN.NullNaN());
        }

        [Fact]
        public void ToIncreasing()
        {
            var expectations = new[]
            { 
                -12.500, 
                -09.778, 
                -07.056, 
                -04.333, 
                -01.611, 
                +01.111, 
                +03.833, 
                +06.556, 
                +09.278, 
                +12.000,
            };

            var ns = from n in (-12.5).To(12, 10)
                     select Math.Round(n, 3);
            Assert.Equal(expectations, ns.ToArray());
        }

        [Fact]
        public void ToEdgeCase()
        {
            var expectations = new[]
            { 
                1.000, 
                2.333, 
                3.667, 
                5.000,
            };

            var ns = from n in (1.0).To(5.0, 4)
                     select Math.Round(n, 3);
            Assert.Equal(expectations, ns.ToArray());
        }

        [Fact]
        public void ToDecreasing()
        {
            var expectations = new[]
            { 
                +12.000,
                +09.278,
                +06.556,
                +03.833,
                +01.111,
                -01.611, 
                -04.333, 
                -07.056, 
                -09.778, 
                -12.500,
            };

            var ns = from n in (12.0).To(-12.5, 10)
                     select Math.Round(n, 3);
            Assert.Equal(expectations, ns.ToArray());
        }

        [Fact]
        public void ToFailsWithNegativeCount()
        {
            const int count = -1;
            var e = Assert.Throws<ArgumentOutOfRangeException>(() => 0.0.To(1, count));
            Assert.Equal("count", e.ParamName);
            Assert.Equal(count, e.ActualValue);
        }

        [Fact]
        public void ToWithZeroCountReturnsEmptySequence()
        {
            Assert.False(0.0.To(1, 0).GetEnumerator().MoveNext());
        }
    }
}