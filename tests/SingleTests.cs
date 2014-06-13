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

    public class SingleTests
    {
        [Fact]
        public void NullNaNReturnsOriginalWhenNonNaN()
        {
            Assert.Equal(1.23f, 1.23f.NullNaN());
        }

        [Fact]
        public void NullNaNReturnsNullWhenNaN()
        {
            Assert.Equal(null, float.NaN.NullNaN());
        }

        [Fact]
        public void ToIncreasing()
        {
            var expectations = new[]
            { 
                -12.500f, 
                -09.778f, 
                -07.056f, 
                -04.333f, 
                -01.611f, 
                +01.111f, 
                +03.833f, 
                +06.556f, 
                +09.278f, 
                +12.000f,
            };

            var ns = from n in (-12.5f).To(12, 10)
                     select (float) Math.Round(n, 3);
            Assert.Equal(expectations, ns.ToArray());
        }

        [Fact]
        public void ToEdgeCase()
        {
            var expectations = new[]
            { 
                1.000f, 
                2.333f, 
                3.667f, 
                5.000f,
            };

            var ns = from n in 1f.To(5f, 4)
                     select (float) Math.Round(n, 3);
            Assert.Equal(expectations, ns.ToArray());
        }

        [Fact]
        public void ToDecreasing()
        {
            var expectations = new[]
            { 
                +12.000f,
                +09.278f,
                +06.556f,
                +03.833f,
                +01.111f,
                -01.611f, 
                -04.333f, 
                -07.056f, 
                -09.778f, 
                -12.500f,
            };

            var ns = from n in 12f.To(-12.5f, 10)
                     select (float) Math.Round(n, 3);
            Assert.Equal(expectations, ns.ToArray());
        }

        [Fact]
        public void ToFailsWithNegativeCount()
        {
            const int count = -1;
            var e = Assert.Throws<ArgumentOutOfRangeException>(() => 0f.To(1, count));
            Assert.Equal("count", e.ParamName);
            Assert.Equal(count, e.ActualValue);
        }

        [Fact]
        public void ToWithZeroCountReturnsEmptySequence()
        {
            Assert.False(0f.To(1, 0).GetEnumerator().MoveNext());
        }
    }
}