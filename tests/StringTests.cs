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
    #region Improts

    using System;
    using Xunit;

    #endregion

    public class StringTests
    {
        [Fact]
        public void MaskEmptyReturnsMaskWithEmptyThis()
        {
            Assert.Equal("foo", string.Empty.MaskEmpty("foo"));
        }

        [Fact]
        public void MaskEmptyReturnsMaskWithNullThis()
        {
            Assert.Equal("foo", StringExtensions.MaskEmpty(null, "foo"));
        }

        [Fact]
        public void MaskEmptyReturnsThisWithNonEmptyThis()
        {
            Assert.Equal("foo", "foo".MaskEmpty("bar"));
        }

        [Fact]
        public void SliceFailsWithNullThis()
        {
            Assert.Throws<ArgumentNullException>(() => StringExtensions.Slice(null, 0));
            Assert.Throws<ArgumentNullException>(() => StringExtensions.Slice(null, 0, null));
            Assert.Throws<ArgumentNullException>(() => StringExtensions.Slice(null, 0, 0));
        }

        [Fact]
        public void SliceReturnsSection()
        {
            const string str = "quick brown fox";
            Assert.Equal("brown", str.Slice(6, 11));
            Assert.Equal(str, str.Slice(0));
            Assert.Equal(str, str.Slice(0, str.Length + 5));
            Assert.Equal(string.Empty, str.Slice(str.Length + 5));
            Assert.Equal("fox", str.Slice(-3));
            Assert.Equal("brown", str.Slice(6, -4));
            Assert.Equal("brown", str.Slice(-9, -4));
            Assert.Equal(string.Empty, str.Slice(1, 0));
        }

        [Fact]
        public void EmbedFailsWithNullThis()
        {
            Assert.Throws<ArgumentNullException>(() => StringExtensions.Embed(null, string.Empty));
        }

        [Fact]
        public void EmbedFailsWithNullTarget()
        {
            Assert.Throws<ArgumentNullException>(() => "foo".Embed(null));
        }

        [Fact]
        public void EmbedFormatsThisIntoTaget()
        {
            Assert.Equal("hello world!", "world".Embed("hello {0}!"));
        }

        [Fact]
        public void WrapFailsWithNullThis()
        {
            Assert.Throws<ArgumentNullException>(() => StringExtensions.Wrap(null, string.Empty, string.Empty));
        }

        [Fact]
        public void WrapWraps()
        {
            Assert.Equal("(foo)", "foo".Wrap("(", ")"));
        }

        [Fact]
        public void WrapIgnoresNullComponent()
        {
            Assert.Equal("-foo", "foo".Wrap("-", null));
            Assert.Equal("foo-", "foo".Wrap(null, "-"));
            Assert.Equal("foo", "foo".Wrap(null, null));
        }
    }
}