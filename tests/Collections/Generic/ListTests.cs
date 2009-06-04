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
    #region Improts

    using System;
    using System.Collections.Generic;
    using Mannex.Collections.Generic;
    using Xunit;

    #endregion

    public class ListTests
    {
        [Fact]
        public void LastIndexFailsWithNullThis()
        {
            Assert.Throws<ArgumentNullException>(() => ListExtensions.LastIndex<object>(null));
        }

        [Fact]
        public void LastIndexReturnsIndexOfLastItem()
        {
            Assert.Equal(0, new[] { "foo" }.LastIndex());
            Assert.Equal(1, new[] { "foo", "bar" }.LastIndex());
            Assert.Equal(2, new[] { "foo", "bar", "baz" }.LastIndex());
        }

        [Fact]
        public void LastIndexReturnsMinusOneForEmptyList()
        {
            Assert.Equal(-1, new object[0].LastIndex());
        }

        [Fact]
        public void PushFailsWithNullThis()
        {
            Assert.Throws<ArgumentNullException>(() => ListExtensions.Push(null, new object()));
        }

        [Fact]
        public void PushAddsToThenEnd()
        {
            var list = new List<int>();
            list.Push(12);
            Assert.Equal(12, list[0]);
            list.Push(34);
            Assert.Equal(34, list[1]);
        }

        [Fact]
        public void PopFailsWithNullThis()
        {
            Assert.Throws<ArgumentNullException>(() => ListExtensions.Pop<object>(null));
        }

        [Fact]
        public void PopFailsWithEmptyList()
        {
            Assert.Throws<InvalidOperationException>(() => new List<object>().Pop());
        }

        [Fact]
        public void PopReturnsAndRemovesLastValue()
        {
            var list = new List<int>(new[]{ 12, 34, 56 });
            Assert.Equal(3, list.Count);
            Assert.Equal(56, list.Pop());
            Assert.Equal(2, list.Count);
            Assert.Equal(34, list.Pop());
            Assert.Equal(1, list.Count);
            Assert.Equal(12, list.Pop());
            Assert.Equal(0, list.Count);
        }

        [Fact]
        public void TryPopFailsWithNullThis()
        {
            Assert.Throws<ArgumentNullException>(() => ListExtensions.TryPop<object>(null));
        }

        [Fact]
        public void TryPopReturnsDefaultForEmptyList()
        {
            var list = new List<int?>();
            Assert.Null(list.TryPop());
            Assert.Equal(-1, list.TryPop(-1));
        }

        [Fact]
        public void TryPopReturnsAndRemovesLastValue()
        {
            var list = new List<int>(new[] { 12, 34, 56 });
            Assert.Equal(3, list.Count);
            Assert.Equal(56, list.TryPop());
            Assert.Equal(2, list.Count);
            Assert.Equal(34, list.TryPop());
            Assert.Equal(1, list.Count);
            Assert.Equal(12, list.TryPop());
            Assert.Equal(0, list.Count);
        }

        [Fact]
        public void PeekFailsWithNullThis()
        {
            Assert.Throws<ArgumentNullException>(() => ListExtensions.Peek<object>(null));
        }

        [Fact]
        public void PeekFailsWithEmptyList()
        {
            Assert.Throws<InvalidOperationException>(() => new List<object>().Peek());
        }

        [Fact]
        public void PeekReturnsFirstValue()
        {
            Assert.Equal(12, new[] { 12, 34, 56 }.Peek());
        }

        [Fact]
        public void TryPeekFailsWithNullThis()
        {
            Assert.Throws<ArgumentNullException>(() => ListExtensions.TryPeek<object>(null));
        }

        [Fact]
        public void TryPeekReturnsDefaultForEmptyList()
        {
            var list = new int?[0];
            Assert.Null(list.TryPeek());
            Assert.Equal(-1, list.TryPeek(-1));
        }

        [Fact]
        public void TryPeekReturnsFirstValue()
        {
            Assert.Equal(12, new[] { 12, 34, 56 }.TryPeek());
        }
    }
}