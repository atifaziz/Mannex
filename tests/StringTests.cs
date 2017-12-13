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

        [Fact]
        public void QuoteFailsWithNullThis()
        {
            Assert.Throws<ArgumentNullException>(() => StringExtensions.Quote(null, string.Empty, string.Empty));
        }

        [Fact]
        public void QuoteQuotes()
        {
            Assert.Equal("'foo'", "foo".Quote("'", null));
        }

        [Fact]
        public void QuoteEscapesEmbeddedQuotes()
        {
            Assert.Equal(@"'foo \'bar\' baz'", "foo 'bar' baz".Quote("'", @"\'"));
        }

        [Fact]
        public void QuoteRemovesQuotesWithNullEscape()
        {
            Assert.Equal(@"'foo bar baz'", "foo 'bar' baz".Quote("'", null));
        }

        [Fact] 
        public void SplitFailsWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() => StringExtensions.Split(null, ',', (a, b) => a + b));
            Assert.Equal("str", e.ParamName);
            e = Assert.Throws<ArgumentNullException>(() => StringExtensions.Split(null, ',', (a, b, c) => a + b + c));
            Assert.Equal("str", e.ParamName);
            e = Assert.Throws<ArgumentNullException>(() => StringExtensions.Split(null, ',', (a, b, c, d) => a + b + c + d));
            Assert.Equal("str", e.ParamName);
            
            var separators = new char[0];
            e = Assert.Throws<ArgumentNullException>(() => StringExtensions.Split(null, separators, (a, b) => a + b));
            Assert.Equal("str", e.ParamName);
            e = Assert.Throws<ArgumentNullException>(() => StringExtensions.Split(null, separators, (a, b, c) => a + b + c));
            Assert.Equal("str", e.ParamName);
            e = Assert.Throws<ArgumentNullException>(() => StringExtensions.Split(null, separators, (a, b, c, d) => a + b + c + d));
            Assert.Equal("str", e.ParamName);
        }

        [Fact]
        public void SplitFailsWithNullResultFunc()
        {
            var e = Assert.Throws<ArgumentNullException>(() => string.Empty.Split(',', (Func<string, string, string>) null));
            Assert.Equal("resultFunc", e.ParamName);
            e = Assert.Throws<ArgumentNullException>(() => string.Empty.Split(',', (Func<string, string, string, string>)null));
            Assert.Equal("resultFunc", e.ParamName);
            e = Assert.Throws<ArgumentNullException>(() => string.Empty.Split(',', (Func<string, string, string, string, string>)null));
            Assert.Equal("resultFunc", e.ParamName);

            var separators = new char[0];
            e = Assert.Throws<ArgumentNullException>(() => string.Empty.Split(separators, (Func<string, string, string>)null));
            Assert.Equal("resultFunc", e.ParamName);
            e = Assert.Throws<ArgumentNullException>(() => string.Empty.Split(separators, (Func<string, string, string, string>)null));
            Assert.Equal("resultFunc", e.ParamName);
            e = Assert.Throws<ArgumentNullException>(() => string.Empty.Split(separators, (Func<string, string, string, string, string>)null));
            Assert.Equal("resultFunc", e.ParamName);
        }

        [Fact]
        public void SplitTwoUsingSingleSeparator()
        {
            Assert.Equal(new[] { "one", "two,three,four,five" }, 
                "one,two,three,four,five".Split(',', (a, b) => new[] { a, b }));
        }

        [Fact]
        public void SplitThreeUsingSingleSeparator()
        {
            Assert.Equal(new[] { "one", "two", "three,four,five" }, 
                "one,two,three,four,five".Split(',', (a, b, c) => new[] { a, b, c }));
        }

        [Fact]
        public void SplitFourUsingSingleSeparator()
        {
            Assert.Equal(new[] { "one", "two", "three", "four,five" }, 
                "one,two,three,four,five".Split(',', (a, b, c, d) => new[] { a, b, c, d }));
        }

        [Fact]
        public void SplitTwoUsingMultipleSeparators()
        {
            Assert.Equal(new[] { "one", "two;three|four,five" }, 
                "one,two;three|four,five".Split(new[] { ',', ';' }, (a, b) => new[] { a, b }));
        }

        [Fact]
        public void SplitThreeUsingMultipleSeparators()
        {
            Assert.Equal(new[] { "one", "two", "three|four,five" }, 
                "one,two;three|four,five".Split(new[] { ',', ';' }, (a, b, c) => new[] { a, b, c }));
        }

        [Fact]
        public void SplitFourUsingMultipleSeparators()
        {
            Assert.Equal(new[] { "one", "two", "three|four", "five" }, 
                "one,two;three|four,five".Split(new[] { ',', ';' }, (a, b, c, d) => new[] { a, b, c, d }));
        }
 
        [Fact]
        public void SplitIntoLinesFailsWithNullThis()
        {
            Assert.Throws<ArgumentNullException>(() => StringExtensions.SplitIntoLines(null));
        }

        [Fact]
        public void SplitIntoLines()
        {
            using (var e = "line 1\rline 2\nline 3\r\nline 4".SplitIntoLines().GetEnumerator())
            {
                Assert.NotNull(e);
                Assert.True(e.MoveNext()); Assert.Equal("line 1", e.Current);
                Assert.True(e.MoveNext()); Assert.Equal("line 2", e.Current);
                Assert.True(e.MoveNext()); Assert.Equal("line 3", e.Current);
                Assert.True(e.MoveNext()); Assert.Equal("line 4", e.Current);
                Assert.False(e.MoveNext());
            }
        }
    
        [Fact]
        public void NormalizeWhiteSpaceFailsWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() => 
                        StringExtensions.NormalizeWhiteSpace(null));
            Assert.Equal("str", e.ParamName);
        }

        [Fact]
        public void NormalizeWhiteSpace()
        {
            Assert.Equal("foo bar", " \t foo \r\n bar \t ".NormalizeWhiteSpace());
        }
    }
}