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
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Mannex.Collections.Generic;
    using Mannex.IO;
    using Xunit;

    #endregion

    public class TextReaderTests
    {
        [Fact]
        public void ReadLinesFailsWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() => TextReaderExtensions.ReadLines(null));
            Assert.Equal("reader", e.ParamName);
        }

        [Fact]
        public void ReadLinesReturnsAllLines()
        {
            Assert.Equal(new[] { "line1", "line2", "line3" }, 
                new StringReader("line1\nline2\nline3").ReadLines().ToArray());
        }

        [Fact]
        public void ConcatFailsWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() => TextReaderExtensions.Concat(null));
            Assert.Equal("first", e.ParamName);
        }

        [Fact]
        public void ConcatSequenceOverloadFailsWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() => TextReaderExtensions.Concat(null, (IEnumerable<TextReader>)null));
            Assert.Equal("first", e.ParamName);
        }

        [Fact]
        public void ConcatFailsWithNullOthers()
        {
            var e = Assert.Throws<ArgumentNullException>(() => TextReader.Null.Concat((TextReader[]) null));
            Assert.Equal("others", e.ParamName);
        }

        [Fact]
        public void ConcatSequenceOverloadFailsWithNullOthers()
        {
            var e = Assert.Throws<ArgumentNullException>(() => TextReader.Null.Concat((IEnumerable<TextReader>) null));
            Assert.Equal("others", e.ParamName);
        }

        [Fact]
        public void ConcatNone()
        {
            Assert.Equal(-1, TextReader.Null.Concat().Read());
        }

        [Fact]
        public void Concat()
        {
            var readers = 
                from s in new[] { "foo,", "bar,", "baz" } 
                select new StringReader(s);
            var result = TextReader.Null.Concat(readers).ReadToEnd();
            Assert.Equal("foo,bar,baz", result);
        }

        [Fact]
        public void ConcatWithNullReader()
        {
            var readers =
                from s in new[] { "foo", null, "bar" }
                select s != null ? new StringReader(s) : null;
            var result = TextReader.Null.Concat(readers).ReadToEnd();
            Assert.Equal("foobar", result);
        }
    }
}