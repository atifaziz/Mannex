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

namespace Mannex.Tests.Collections.Generic
{
    #region Imports

    using System;
    using System.Linq;
    using Mannex.Collections.Generic;
    using Xunit;

    #endregion

    public class IEnumeratorTests
    {
        [Fact]
        public void ReadWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() =>
                IEnumeratorExtensions.Read<object>(null));
            Assert.Equal("enumerator", e.ParamName);
        }

        [Fact]
        public void Read()
        {
            var e = Enumerable.Range(1, 3).GetEnumerator();
            Assert.Equal(1, e.Read());
            Assert.Equal(2, e.Read());
            Assert.Equal(3, e.Read());
        }

        [Fact]
        public void ReadFailsWithExhaustedEnumerator()
        {
            Assert.Throws<InvalidOperationException>(() =>
                Enumerable.Empty<int>().GetEnumerator().Read());
        }

        [Fact]
        public void TryReadWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() =>
                IEnumeratorExtensions.TryRead<object>(null));
            Assert.Equal("enumerator", e.ParamName);
        }

        [Fact]
        public void TryRead()
        {
            const int eos = -1;
            var e = Enumerable.Range(1, 3).GetEnumerator();
            Assert.Equal(1, e.TryRead(eos));
            Assert.Equal(2, e.TryRead(eos));
            Assert.Equal(3, e.TryRead(eos));
            Assert.Equal(eos, e.TryRead(eos));
        }

        [Fact]
        public void TryReadWithExhaustedEnumerator()
        {
            const int eos = -1;
            var e = Enumerable.Empty<int>().GetEnumerator();
            Assert.Equal(eos, e.TryRead(eos));
            Assert.Equal(eos, e.TryRead(eos)); // should be harmless
        }
    }
}