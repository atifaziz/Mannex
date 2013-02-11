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

namespace Mannex.Tests.Text.RegularExpressions
{
    #region Imports

    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Xunit;
    using Mannex.Text.RegularExpressions;

    #endregion

    public class StringTests
    {
        [Fact]
        public void IsMatchFailsWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() => StringExtensions.IsMatch(null, string.Empty));
            Assert.Equal("str", e.ParamName);
        }

        [Fact]
        public void IsMatchFailsWithNullPattern()
        {
            var e = Assert.Throws<ArgumentNullException>(() => string.Empty.IsMatch(null));
            Assert.Equal("pattern", e.ParamName);
        }

        [Fact]
        public void IsMatchReturnsSuccessfulMatchObjectWhenPatternFound()
        {
            var match = "(123,456)".Match(@"[0-9]+");
            Assert.NotNull(match);
            Assert.True(match.Success);
        }

        [Fact]
        public void IsMatchReturnsUnsuccessfulMatchObjectWhenPatternNotFound()
        {
            var match = "(123,456)".Match(@"[a-z]+");
            Assert.NotNull(match);
            Assert.False(match.Success);
        }

        [Fact]
        public void IsMatchRespectsOptions()
        {
            Assert.False("(abc,def)".Match(@"[A-Z]+").Success);
            Assert.True("(abc,def)".Match(@"[A-Z]+", RegexOptions.IgnoreCase).Success);
        }

        [Fact]
        public void MatchFailsWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() => StringExtensions.Match(null, string.Empty));
            Assert.Equal("str", e.ParamName);
        }

        [Fact]
        public void MatchFailsWithNullPattern()
        {
            var e = Assert.Throws<ArgumentNullException>(() => string.Empty.Match(null));
            Assert.Equal("pattern", e.ParamName);
        }

        [Fact]
        public void MatchFailsWithNullSelector()
        {
            var e = Assert.Throws<ArgumentNullException>(() => string.Empty.Match<object>(".", null));
            Assert.Equal("selector", e.ParamName);
        }

        [Fact]
        public void MatchReturnsMatchingTextWhenPatternFound()
        {
            var match = "(123,456)".Match(@"[0-9]+");
            Assert.NotNull(match);
            Assert.True(match.Success);
            Assert.Equal("123", match.Value);
        }

        [Fact]
        public void MatchReturnsUnsuccessfulMatchObjectWhenPatternNotFound()
        {
            var match = "(123,456)".Match(@"[a-z]+");
            Assert.NotNull(match);
            Assert.False(match.Success);
        }

        [Fact]
        public void MatchRespectsOptions()
        {
            Assert.False("(abc,def)".Match(@"[A-Z]+").Success);
            Assert.True("(abc,def)".Match(@"[A-Z]+", RegexOptions.IgnoreCase).Success);
        }

        [Fact]
        public void MatchesFailsWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() => StringExtensions.Matches(null, string.Empty));
            Assert.Equal("str", e.ParamName);
        }

        [Fact]
        public void MatchesFailsWithNullPattern()
        {
            var e = Assert.Throws<ArgumentNullException>(() => string.Empty.Matches(null));
            Assert.Equal("pattern", e.ParamName);
        }

        [Fact]
        public void MatchesFailsWithNullSelector()
        {
            var e = Assert.Throws<ArgumentNullException>(() => string.Empty.Matches<object>(".", null));
            Assert.Equal("selector", e.ParamName);
        }

        [Fact]
        public void MatchesReturnsAllMatchingTextWherePatternOccurs()
        {
            var e = "(123,456)".Matches(@"[0-9]+").GetEnumerator();
            Assert.True(e.MoveNext());
            Assert.Equal("123", e.Current.Value);
            Assert.True(e.MoveNext());
            Assert.Equal("456", e.Current.Value);
            Assert.False(e.MoveNext());
        }

        [Fact]
        public void MatchesReturnsEmptySequenceWhenPatternNotFound()
        {
            var e = "(123,456)".Matches(@"[a-z]+").GetEnumerator();
            Assert.False(e.MoveNext());
        }

        [Fact]
        public void MatchesRespectsOptions()
        {
            Assert.False("(abc,def)".Matches(@"[A-Z]+").Any());
            Assert.True("(abc,def)".Matches(@"[A-Z]+", RegexOptions.IgnoreCase).Any());
        }
    }
}