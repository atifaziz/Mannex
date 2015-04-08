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
    using System.Collections.Generic;
    using System.Linq;
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
        public void PushAddsToTheEnd()
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

        [Fact]
        public void UnshiftFailsWithNullThis()
        {
            Assert.Throws<ArgumentNullException>(() => ListExtensions.Unshift(null, new object()));
        }

        [Fact]
        public void UnshiftPrepends()
        {
            var list = new List<int>();
            list.Unshift(12);
            Assert.Equal(12, list[0]);
            list.Unshift(34);
            Assert.Equal(34, list[0]);
            Assert.Equal(12, list[1]);
        }

        [Fact]
        public void ShiftFailsWithNullThis()
        {
            Assert.Throws<ArgumentNullException>(() => ListExtensions.Shift<object>(null));
        }

        [Fact]
        public void ShiftFailsWithEmptyList()
        {
            Assert.Throws<InvalidOperationException>(() => new List<object>().Shift());
        }

        [Fact]
        public void ShiftReturnsAndRemovesFirstValue()
        {
            var list = new List<int>(new[] { 12, 34, 56 });
            Assert.Equal(3, list.Count);
            Assert.Equal(12, list.Shift());
            Assert.Equal(2, list.Count);
            Assert.Equal(34, list.Shift());
            Assert.Equal(1, list.Count);
            Assert.Equal(56, list.Shift());
            Assert.Equal(0, list.Count);
        }

        [Fact]
        public void TryShiftFailsWithNullThis()
        {
            Assert.Throws<ArgumentNullException>(() => ListExtensions.TryShift<object>(null));
        }

        [Fact]
        public void TryShiftReturnsDefaultForEmptyList()
        {
            var list = new List<int?>();
            Assert.Null(list.TryShift());
            Assert.Equal(-1, list.TryShift(-1));
        }

        [Fact]
        public void TryShiftReturnsAndRemovesFirstValue()
        {
            var list = new List<int>(new[] { 12, 34, 56 });
            Assert.Equal(3, list.Count);
            Assert.Equal(12, list.TryShift());
            Assert.Equal(2, list.Count);
            Assert.Equal(34, list.TryShift());
            Assert.Equal(1, list.Count);
            Assert.Equal(56, list.TryShift());
            Assert.Equal(0, list.Count);
        }

        [Fact]
        public void EnqueueFailsWithNullThis()
        {
            PushFailsWithNullThis();
        }

        [Fact]
        public void EnqueueAddsToTheEnd()
        {
            PushAddsToTheEnd();
        }

        [Fact]
        public void DequeueFailsWithNullThis()
        {
            ShiftFailsWithNullThis();
        }

        [Fact]
        public void DequeueFailsWithEmptyList()
        {
            ShiftFailsWithEmptyList();
        }

        [Fact]
        public void DequeueReturnsAndRemovesFirstValue()
        {
            ShiftReturnsAndRemovesFirstValue();
        }

        [Fact]
        public void TryDequeueFailsWithNullThis()
        {
            TryShiftFailsWithNullThis();
        }

        [Fact]
        public void TryDequeueReturnsDefaultForEmptyList()
        {
            TryShiftReturnsDefaultForEmptyList();
        }

        [Fact]
        public void TryDequeueReturnsAndRemovesFirstValue()
        {
            TryShiftReturnsAndRemovesFirstValue();
        }

        [Fact]
        public void SliceFailsWithNullThis()
        {
            Assert.Throws<ArgumentNullException>(() => ListExtensions.Slice<object>(null, 0));
            Assert.Throws<ArgumentNullException>(() => ListExtensions.Slice<object>(null, 0, 0));
        }

        [Fact]
        public void SliceReturnsIdentifiedPortion()
        {
            var nums = new[] { 12, 34, 56, 78 };
            Assert.Equal(nums, nums.Slice(0).ToArray());
            Assert.Equal(nums, nums.Slice(0, 4).ToArray());
            Assert.Equal(new[] { 12, 34, 56 }, nums.Slice(0, 3).ToArray());
            Assert.Equal(new[] { 34, 56 }, nums.Slice(1, 3).ToArray());
        }

        [Fact]
        public void SliceClipsEnd()
        {
            var nums = new[] { 12, 34, 56, 78 };
            Assert.Equal(nums, nums.Slice(0, 10).ToArray());
        }

        [Fact]
        public void SliceReturnsEmptySequenceWhenEndOccursBeforeStart()
        {
            var nums = new[] { 12, 34, 56, 78 };
            Assert.Empty(nums.Slice(10).ToArray());
            Assert.Empty(nums.Slice(3, 1).ToArray());
            Assert.Empty(nums.Slice(1, -10).ToArray());
        }

        [Fact]
        public void SliceReturnsPortionIdentifiedByOffsets()
        {
            var nums = new[] { 12, 34, 56, 78 };
            Assert.Equal(new[] { 78 }, nums.Slice(-1).ToArray());
            Assert.Equal(new[] { 56, 78 }, nums.Slice(-2).ToArray());
            Assert.Equal(new[] { 34, 56, 78 }, nums.Slice(-3).ToArray());
            Assert.Equal(new[] { 34, 56 }, nums.Slice(-3, -1).ToArray());
        }

        [Fact]
        public void BinarySearchFailsWithNullThis()
        {
            Assert.Throws<ArgumentNullException>(() => ListExtensions.BinarySearch(null, "foo"));
            Assert.Throws<ArgumentNullException>(() => ListExtensions.BinarySearch(null, 0, 10, "foo"));
            Assert.Throws<ArgumentNullException>(() => ListExtensions.BinarySearch(null, "foo", null));
            Assert.Throws<ArgumentNullException>(() => ListExtensions.BinarySearch(null, 0, 10, "foo", null));
        }
 
        [Fact]
        public void BinarySearchReturnsIndexBitwiseComplementFirstElementThatIsLargerThanValueWhenValueNotFound()
        {
            IList<string> items = new[] { "bravo", "delta" };
            Assert.Equal(-1, items.BinarySearch("alpha"));
            Assert.Equal(-2, items.BinarySearch("charlie"));
            Assert.Equal(-3, items.BinarySearch("echo"));
        }

        [Fact]
        public void BinarySearchReturnsIndexWhenValueFound()
        {
            IList<string> items = new[] { "alpha", "bravo", "charlie" };
            Assert.Equal(0, items.BinarySearch("alpha"));
            Assert.Equal(1, items.BinarySearch("bravo"));
            Assert.Equal(2, items.BinarySearch("charlie"));
       }
 
        [Fact]
        public void BinarySearchWithComparerReturnsIndexBitwiseComplementFirstElementThatIsLargerThanValueWhenValueNotFound()
        {
            IList<string> items = new[] { "BRAVO", "DELTA" };
            var comparer = StringComparer.InvariantCultureIgnoreCase;
            Assert.Equal(-1, items.BinarySearch("alpha", comparer));
            Assert.Equal(-2, items.BinarySearch("charlie", comparer));
            Assert.Equal(-3, items.BinarySearch("echo", comparer));
        }

        [Fact]
        public void BinarySearchWithComparerReturnsIndexWhenValueFound()
        {
            IList<string> items = new[] { "ALPHA", "BRAVO", "CHARLIE" };
            var comparer = StringComparer.InvariantCultureIgnoreCase;
            Assert.Equal(0, items.BinarySearch("alpha", comparer));
            Assert.Equal(1, items.BinarySearch("bravo", comparer));
            Assert.Equal(2, items.BinarySearch("charlie", comparer));
        }

        [Fact]
        public void AsReadOnlyWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() => ListExtensions.AsReadOnly<object>(null));
            Assert.Equal("list", e.ParamName);
        }

        [Fact]
        public void AsReadOnly()
        {
            var list = (IList<int>) new [] { 1, 2, 3 }.ToList();
            Assert.False(list.IsReadOnly);
            var roList = list.AsReadOnly();
            Assert.True(roList.IsReadOnly);
        }
 
        [Fact]
        public void AsReadOnlyOnReadOnlyListReturnsSameListReference()
        {
            var list = (IList<int>) new [] { 1, 2, 3 }.ToList();
            Assert.False(list.IsReadOnly);
            var roList = list.AsReadOnly();
            Assert.True(roList.IsReadOnly);
            Assert.Same(roList, roList.AsReadOnly());
        }

        [Fact]
        public void PopAtFailsWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() => ListExtensions.PopAt<object>(null, 0));
            Assert.Equal("list", e.ParamName);
        }

        [Fact]
        public void PopAtFailsWithIndexOutOfRange()
        {
            var e = Assert.Throws<ArgumentOutOfRangeException>(() => new List<object>().PopAt(1));
            Assert.Equal("index", e.ParamName);
        }

        [Fact]
        public void PopAt()
        {
            const int a = 123;
            const int b = 456;
            const int c = 789;
            var list = new[] { a, b, c }.ToList();
            Assert.Equal(b, list.PopAt(1));
            Assert.Equal(2, list.Count);
            Assert.Equal(a, list[0]);
            Assert.Equal(c, list[1]);
        }

        [Fact]
        public void TryPopAtFailsWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() => ListExtensions.TryPopAt<object>(null, 0));
            Assert.Equal("list", e.ParamName);
        }

        [Fact]
        public void TryPopAtWithoutEmptyValue()
        {
            Assert.Equal(0, new List<int>().TryPopAt(10));
        }

        [Fact]
        public void TryPopAtWithOutOfBoundsIndexAndUserSpecificEmpty()
        {
            Assert.Equal(-1, new List<int>().TryPopAt(10, -1));
        }

        [Fact]
        public void TryPopAt()
        {
            const int a = 123;
            const int b = 456;
            const int c = 789;
            var list = new[] { a, b, c }.ToList();
            Assert.Equal(b, list.TryPopAt(1));
            Assert.Equal(2, list.Count);
            Assert.Equal(a, list[0]);
            Assert.Equal(c, list[1]);
        }
    }
}