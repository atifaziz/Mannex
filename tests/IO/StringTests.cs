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

namespace Mannex.Tests.IO
{
    #region Imports

    using System;
    using System.IO;
    using System.Linq;
    using Mannex.IO;
    using Xunit;
    using StringExtensions = Mannex.IO.StringExtensions;

    #endregion

    public class StringTests
    {
        [Fact]
        public void ToFileNameSafeFailsWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() => StringExtensions.ToFileNameSafe(null));
            Assert.Equal("str", e.ParamName);
            
            e = Assert.Throws<ArgumentNullException>(() => StringExtensions.ToFileNameSafe(null, string.Empty));
            Assert.Equal("str", e.ParamName);
        }

        [Fact]
        public void ToFileNameSafeFailsWithEmptyThis()
        {
            var e = Assert.Throws<ArgumentException>(() => string.Empty.ToFileNameSafe());
            Assert.Equal("str", e.ParamName);

            e = Assert.Throws<ArgumentException>(() => string.Empty.ToFileNameSafe(string.Empty));
            Assert.Equal("str", e.ParamName);
        }

        [Fact]
        public void ToFileNameSafeReplacesInvalidFileNameChars()
        {
            Assert.Equal("foo_bar", "foo*bar".ToFileNameSafe());
            Assert.Equal("foo_bar", "foo*bar".ToFileNameSafe(null));
        }

        [Fact]
        public void ToFileNameSafeUsesUnderscoreAsDefaultReplacement()
        {
            Assert.Equal("foo-bar", "foo*bar".ToFileNameSafe("-"));
        }

        [Fact]
        public void ToFileNameSafeFailsWithReplacementCarryingInvalidFileNameChars()
        {
            foreach (var e in Path.GetInvalidFileNameChars()
                                  .Select(ch => Assert.Throws<ArgumentException>(()
                                             => "foo".ToFileNameSafe(ch.ToString()))))
            {
                Assert.Equal("replacement", e.ParamName);
            }
        }

        [Fact]
        public void ToFileNameSafeReplacesAllInvalidFileNameChars()
        {
            var chars = Path.GetInvalidFileNameChars();
            Assert.Equal(new string('_', chars.Length), new string(chars).ToFileNameSafe());
        }

        [Fact]
        public void ToPathNameSafeFailsWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() => StringExtensions.ToPathNameSafe(null));
            Assert.Equal("str", e.ParamName);

            e = Assert.Throws<ArgumentNullException>(() => StringExtensions.ToPathNameSafe(null, string.Empty));
            Assert.Equal("str", e.ParamName);
        }

        [Fact]
        public void ToPathNameSafeFailsWithEmptyThis()
        {
            var e = Assert.Throws<ArgumentException>(() => string.Empty.ToPathNameSafe());
            Assert.Equal("str", e.ParamName);

            e = Assert.Throws<ArgumentException>(() => string.Empty.ToPathNameSafe(string.Empty));
            Assert.Equal("str", e.ParamName);
        }

        [Fact]
        public void ToPathNameSafeReplacesInvalidPathNameChars()
        {
            Assert.Equal("foo_bar", "foo|bar".ToPathNameSafe());
            Assert.Equal("foo_bar", "foo|bar".ToPathNameSafe(null));
        }

        [Fact]
        public void ToPathNameSafeUsesUnderscoreAsDefaultReplacement()
        {
            Assert.Equal("foo-bar", "foo|bar".ToPathNameSafe("-"));
        }

        [Fact]
        public void ToPathNameSafeFailsWithReplacementCarryingInvalidPathNameChars()
        {
            foreach (var e in Path.GetInvalidPathChars()
                                  .Select(ch => Assert.Throws<ArgumentException>(()
                                             => "foo".ToPathNameSafe(ch.ToString()))))
            {
                Assert.Equal("replacement", e.ParamName);
            }
        }

        [Fact]
        public void ToPathNameSafeReplacesAllInvalidPathNameChars()
        {
            var chars = Path.GetInvalidPathChars();
            Assert.Equal(new string('_', chars.Length), new string(chars).ToPathNameSafe());
        }

        [Fact]
        public void ReadWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() => StringExtensions.Read(null));
            Assert.Equal("str", e.ParamName);
        }

        [Fact]
        public void ReadReturnsReaderReturingContent()
        {
            var reader = "foobar".Read();
            Assert.NotNull(reader);
            Assert.Equal("foobar", reader.ReadToEnd());
        }
    }
}