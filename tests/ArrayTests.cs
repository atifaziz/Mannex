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
    using System.Text;
    using Xunit;

    #endregion

    public class ArrayTests
    {
        [Fact]
        public void ToHexFailsWithNullThis()
        {
            Assert.Throws<ArgumentNullException>(() => 
                ArrayExtensions.ToHex(null, 0, 0, new StringBuilder()));
        }

        [Fact]
        public void ToHexWithNullStringBuilderSuppliesOne()
        {
            Assert.NotNull(new byte[0].ToHex(0, 0, null));
        }

        [Fact]
        public void ToHexFailsWithInvalidRanges()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => 
                new byte[] { 1, 2, 3 }.ToHex(-1, 0, new StringBuilder()));
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                new byte[] { 1, 2, 3 }.ToHex(4, 0, new StringBuilder()));
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                new byte[] { 1, 2, 3 }.ToHex(1, 3, new StringBuilder()));
        }

        [Fact]
        public void ToHex()
        {
            var result = Enumerable.Range(0, 256)
                                   .Select(n => (byte) n)
                                   .ToArray()
                                   .ToHex();

            Assert.Equal("000102030405060708090a0b0c0d0e0f"
                        +"101112131415161718191a1b1c1d1e1f"
                        +"202122232425262728292a2b2c2d2e2f"
                        +"303132333435363738393a3b3c3d3e3f"
                        +"404142434445464748494a4b4c4d4e4f"
                        +"505152535455565758595a5b5c5d5e5f"
                        +"606162636465666768696a6b6c6d6e6f"
                        +"707172737475767778797a7b7c7d7e7f"
                        +"808182838485868788898a8b8c8d8e8f"
                        +"909192939495969798999a9b9c9d9e9f"
                        +"a0a1a2a3a4a5a6a7a8a9aaabacadaeaf"
                        +"b0b1b2b3b4b5b6b7b8b9babbbcbdbebf"
                        +"c0c1c2c3c4c5c6c7c8c9cacbcccdcecf"
                        +"d0d1d2d3d4d5d6d7d8d9dadbdcdddedf"
                        +"e0e1e2e3e4e5e6e7e8e9eaebecedeeef"
                        +"f0f1f2f3f4f5f6f7f8f9fafbfcfdfeff", 
                        result);
        }

        [Fact]
        public void ToHexOnSubrange()
        {
            var result = Enumerable.Range(0, 256)
                                   .Select(n => (byte)n)
                                   .ToArray()
                                   .ToHex(10 * 16, 16);

            Assert.Equal("a0a1a2a3a4a5a6a7a8a9aaabacadaeaf", result);
        }

        [Fact]
        public void RotateFailsWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() =>
                        ArrayExtensions.Rotate<object>(null));
            Assert.Equal("array", e.ParamName);
        }

        [Fact]
        public void RotateEmptyArray()
        {
            var array = new object[0];
            array.Rotate();
            // no assertions; point is to test it does not fail
        }

        [Fact]
        public void Rotate()
        {
            var array = new [] { 1, 2, 3 };
            array.Rotate();
            Assert.Equal(new[] { 2, 3, 1 }, array);
        }
 
        [Fact]
        public void UpdateFailsWithNullThis()
        {
            ArgumentNullException e1, e2;

            e1 = Assert.Throws<ArgumentNullException>(() => 
                     ArrayExtensions.Update<object, object>(null, new object[0], (t, s, i) => -1));
            Assert.Equal("target", e1.ParamName);

            e2 = Assert.Throws<ArgumentNullException>(() => 
                     ArrayExtensions.Update<object, object>(null, new object[0], (t, s, i) => -1));
            Assert.Equal("target", e2.ParamName);
        }
 
        [Fact]
        public void UpdateFailsWithNullFunction()
        {
            ArgumentNullException e1, e2;

            e1 = Assert.Throws<ArgumentNullException>(() => 
                     new object[0].Update(new object[0], (Func<object, object, int, object>) null));
            Assert.Equal("function", e1.ParamName);

            e2 = Assert.Throws<ArgumentNullException>(() => 
                     new object[0].Update(new object[0], (Func<object, object, object>) null));
            Assert.Equal("function", e2.ParamName);
        }

        [Fact]
        public void UpdateFailsWithNullSource()
        {
            ArgumentNullException e1, e2;

            e1 = Assert.Throws<ArgumentNullException>(() => 
                     new object[0].Update((object[]) null, (t, s, i) => null));
            Assert.Equal("source", e1.ParamName);

            e2 = Assert.Throws<ArgumentNullException>(() => 
                     new object[0].Update((object[]) null, (t, s) => null));
            Assert.Equal("source", e2.ParamName);
        }
 
        [Fact]
        public void Update()
        {
            var xs = new[] { 4, 5, 6 };
            var ys = new[] { 7, 8, 9 };
            xs.Update(ys, (x, y, i) => (i + 1) * 100 + x * 10 + y);
            Assert.Equal(new[] { 147, 258, 369 }, xs);
        }

        [Fact]
        public void UpdateShort()
        {
            var xs = new[] { 4, 5,   };
            var ys = new[] { 7, 8, 9 };
            xs.Update(ys, (x, y, i) => (i + 1) * 100 + x * 10 + y);
            Assert.Equal(new[] { 147, 258 }, xs);
        }

        [Fact]
        public void UpdateLong()
        {
            var xs = new[] { 4, 5, 6 };
            var ys = new[] { 7, 8,   };
            xs.Update(ys, (x, y, i) => (i + 1) * 100 + x * 10 + y);
            Assert.Equal(new[] { 147, 258, 6 }, xs);
        }
    }
}