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

        [Fact]
        public void SubstringsFailsWithNullThis()
        {
            var e1 = Assert.Throws<ArgumentNullException>(() => StringExtensions.Substrings(null, 0, 0));
            Assert.Equal("str", e1.ParamName);
            var e2 = Assert.Throws<ArgumentNullException>(() => StringExtensions.Substrings<object>(null, 0, 0, delegate { return null; }));
            Assert.Equal("str", e2.ParamName);
        }

        [Fact]
        public void SubstringsFailsWithNullFunction()
        {
            var e = Assert.Throws<ArgumentNullException>(() => string.Empty.Substrings<object>(0, 0, null));
            Assert.Equal("resultor", e.ParamName);
        }

        [Fact]
        public void Substrings()
        {
            Assert.Equal(new[] { "left", "mid", "right" }, "leftmidright".Substrings(4, 3));
            const string foo = "foo";
            const string bar = "bar";
            const string foobar = foo + bar;
            const string _ = "";
            Assert.Equal(new[] { _, foo, bar }, foobar.Substrings(0, foo.Length));
            Assert.Equal(new[] { foo, bar, _ }, foobar.Substrings(foo.Length, bar.Length));
            Assert.Equal(new[] { _, _, foobar }, foobar.Substrings(0, 0));
            Assert.Equal(new[] { foobar, _, _ }, foobar.Substrings(foobar.Length, 0));
            Assert.Equal(new[] { _, _, _ }, _.Substrings(0, 0));
        }

        [Fact]
        public void IsTruthyWithNullReturnsFalse()
        {
            Assert.False(StringExtensions.IsTruthy(null));
        }

        [Fact]
        public void IsTruthy()
        {
            var tests =
                from t in new[]
                {
                    new { Input = "true",   Expected = true  },
                    new { Input = "yes",    Expected = true  },
                    new { Input = "1",      Expected = true  },
                    new { Input = "on",     Expected = true  },
                    new { Input = "false",  Expected = false },
                    new { Input = "foo",    Expected = false },
                    new { Input = "bar",    Expected = false },
                    new { Input = "-1",     Expected = false },
                }
                from ws in new[]
                {
                    new { Before = string.Empty, After = string.Empty },
                    new { Before = "\r\n\t\x20", After = "\x20\t\r\n" },
                    new { Before = string.Empty, After = "\x20\t\r\n" },
                    new { Before = "\r\n\t\x20", After = string.Empty },
                }
                select new 
                { 
                    Input = ws.Before + t.Input + ws.After, 
                    t.Expected 
                };

            foreach (var test in tests)
                Assert.Equal(test.Expected, test.Input.IsTruthy());
        }

        [Fact]
        public void HasPrefixFailsWithNullThis()
        {
             var e = Assert.Throws<ArgumentNullException>(() => 
                        StringExtensions.HasPrefix(null, string.Empty, StringComparison.Ordinal));
            Assert.Equal("str", e.ParamName);
        }

        [Fact]
        public void HasPrefix()
        {
            Assert.True("foobar".HasPrefix("foo", StringComparison.Ordinal));
            Assert.True("foobar".HasPrefix("FOO", StringComparison.OrdinalIgnoreCase));
            Assert.False("foobar".HasPrefix(null, StringComparison.Ordinal));
            Assert.False("foobar".HasPrefix(string.Empty, StringComparison.Ordinal));
            Assert.False("foo".HasPrefix("foo", StringComparison.Ordinal));
        }

        [Fact]
        public void HasSuffixFailsWithNullThis()
        {
             var e = Assert.Throws<ArgumentNullException>(() => 
                        StringExtensions.HasSuffix(null, string.Empty, StringComparison.Ordinal));
            Assert.Equal("str", e.ParamName);
        }

        [Fact]
        public void HasSuffix()
        {
            Assert.True("foobar".HasSuffix("bar", StringComparison.Ordinal));
            Assert.True("foobar".HasSuffix("BAR", StringComparison.OrdinalIgnoreCase));
            Assert.False("foobar".HasSuffix(null, StringComparison.Ordinal));
            Assert.False("foobar".HasSuffix(string.Empty, StringComparison.Ordinal));
            Assert.False("bar".HasSuffix("bar", StringComparison.Ordinal));
        }
 
        [Fact]
        public void CharAtFailsWithNullThis()
        {
             var e = Assert.Throws<ArgumentNullException>(() => StringExtensions.TryCharAt(null, 0));
            Assert.Equal("str", e.ParamName);
        }

        [Fact]
        public void CharAt()
        {
            const string word = "word";
            Assert.Equal('w', word.TryCharAt(0));
            Assert.Equal('o', word.TryCharAt(1));
            Assert.Equal('r', word.TryCharAt(2));
            Assert.Equal('d', word.TryCharAt(3));
            Assert.Equal('d', word.TryCharAt(-1));
            Assert.Equal('r', word.TryCharAt(-2));
            Assert.Equal('o', word.TryCharAt(-3));
            Assert.Equal('w', word.TryCharAt(-4));
            Assert.Equal(null, word.TryCharAt(-(word.Length + 10)));
            Assert.Equal(null, word.TryCharAt(word.Length + 10));
            Assert.Equal(null, string.Empty.TryCharAt(0));
            Assert.Equal(null, string.Empty.TryCharAt(1));
            Assert.Equal(null, string.Empty.TryCharAt(-1));
        }
    
        [Fact]
        public void TrimCommonLeadingSpaceFailsWithNullThis()
        {
             var e = Assert.Throws<ArgumentNullException>(() => StringExtensions.TrimCommonLeadingSpace(null));
            Assert.Equal("str", e.ParamName);
        }
 
        [Fact]
        public void TrimCommonLeadingSpace()
        {
            Assert.Equal(
                string.Join(Environment.NewLine, new[] {
                    string.Empty,
                    "SELECT",
                    "    *",
                    "FROM",
                    "    foo",
                    "WHERE bar IS NOT NULL",
                    "ORDER BY id",
                    string.Empty }), @"  
                    SELECT
                        *
                    FROM
                        foo
                    WHERE bar IS NOT NULL
                    ORDER BY id
                    ".TrimCommonLeadingSpace());
        }

        [Fact]
        public void PartitionFailsWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() => StringExtensions.Partition(null, 42));
            Assert.Equal("str", e.ParamName);
        }

        [Fact]
        public void PartitionOnEmptyStringYieldsEmptySequence()
        {
            using (var e = string.Empty.Partition(42).GetEnumerator())
                Assert.False(e.MoveNext());
        }

        [Fact]
        public void PartitionEvenly()
        {
            const string foo = "foo";
            const string bar = "bar";
            const string baz = "baz";
            Assert.Equal(new[] { foo, bar, baz }, string.Concat(foo, bar, baz).Partition(3).ToArray());
        }

        [Fact]
        public void PartitionIrregularTail()
        {
            Assert.Equal(new[] { "hello ", "there ", "foobar", "!" }, "hello there foobar!".Partition(6).ToArray());
        }

        [Fact]
        public void PartitionIrregularSingleton()
        {
            const string foobar = "foobar";
            Assert.Equal(new[] { foobar }, foobar.Partition(foobar.Length * 2).ToArray());
        }

        [Fact]
        public void SplitTwoUsingStringSeparator()
        {
            const string str = "one|sep|two|sep|three|sep|four|sep|five";
            Assert.Equal(new[] { "one", "two|sep|three|sep|four|sep|five" }, 
                str.Split("|SEP|", StringComparison.OrdinalIgnoreCase,  
                          (a, b) => new[] { a, b }));
        }

        [Fact]
        public void SplitThreeUsingStringSeparator()
        {
            const string str = "one|sep|two|sep|three|sep|four|sep|five";
            Assert.Equal(new[] { "one", "two", "three|sep|four|sep|five" }, 
                str.Split("|SEP|", StringComparison.OrdinalIgnoreCase, 
                          (a, b, c) => new[] { a, b, c }));
        }

        [Fact]
        public void SplitFourUsingStringSeparator()
        {
            const string str = "one|sep|two|sep|three|sep|four|sep|five";
            Assert.Equal(new[] { "one", "two", "three", "four|sep|five" }, 
                str.Split("|SEP|", StringComparison.OrdinalIgnoreCase, 
                          (a, b, c, d) => new[] { a, b, c, d }));
        }

        [Fact]
        public void ReplaceFailsWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() => StringExtensions.Replace(null, "foo", "bar", StringComparison.Ordinal));
            Assert.Equal("str", e.ParamName);
        }

        [Fact]
        public void ReplaceFailsWithNullOldValue()
        {
            var e = Assert.Throws<ArgumentNullException>(() => "foo".Replace(null, "bar", StringComparison.Ordinal));
            Assert.Equal("oldValue", e.ParamName);
        }

        [Fact]
        public void ReplaceFailsWithZeroLengthOldValue()
        {
            var e = Assert.Throws<ArgumentException>(() => "foo".Replace(string.Empty, "bar", StringComparison.Ordinal));
            Assert.Equal("oldValue", e.ParamName);
        }

        [Fact]
        public void Replace()
        {
            Assert.Equal("foo BAR foo BAR foo BAR foo", "foo bar foo bar foo bar foo".Replace("bar", "BAR", StringComparison.Ordinal));
        }

        [Fact]
        public void ReplaceCaseInsensitive()
        {
            Assert.Equal("Foo * Foo * Foo * Foo", "Foo Bar Foo Bar Foo Bar Foo".Replace("bar", "*", StringComparison.OrdinalIgnoreCase));
        }

        [Fact]
        public void ReplaceWithoutOldValueReturnsOriginal()
        {
            var input = string.Concat(Enumerable.Repeat("foo bar", 3));
            Assert.Same(input, input.Replace("-", string.Empty, StringComparison.Ordinal));
        }

        [Fact]
        public void RepeatFailsWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() => StringExtensions.Repeat(null, 0));
            Assert.Equal("str", e.ParamName);
        }

        [Fact]
        public void RepeatFailsWithNegativeCount()
        {
            const int count = -1;
            var e = Assert.Throws<ArgumentOutOfRangeException>(() => "foo".Repeat(count));
            Assert.Equal("count", e.ParamName);
            Assert.Equal(count, e.ActualValue);
        }

        [Fact]
        public void RepeatZeroCountReturnsEmptyString()
        {
            Assert.Equal(0, "foo".Repeat(0).Length);
        }

        [Fact]
        public void Repeat()
        {
            const string foo = "foo";
            Assert.Equal(foo, foo.Repeat(1));
            Assert.Equal(foo + foo, foo.Repeat(2));
            Assert.Equal(foo + foo + foo, foo.Repeat(3));
            Assert.Equal(foo + foo + foo + foo + foo + foo + foo + foo + foo + foo, 
                         foo.Repeat(10));
            Assert.Equal(new string('-', 42), "-".Repeat(42));
        }
    }
}