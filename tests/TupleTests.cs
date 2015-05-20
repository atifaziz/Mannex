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
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    #endregion

    public class TupleTests
    {
        [Fact]
        public void AsEnumerableWithNullThis()
        {
            // ReSharper disable InvokeAsExtensionMethod
            
            AssertArgumentNullExceptionForNullThis(() => TupleExtensions.AsEnumerable((Tuple<object>)null));
            AssertArgumentNullExceptionForNullThis(() => TupleExtensions.AsEnumerable((Tuple<object, object>)null));
            AssertArgumentNullExceptionForNullThis(() => TupleExtensions.AsEnumerable((Tuple<object, object, object>)null));
            AssertArgumentNullExceptionForNullThis(() => TupleExtensions.AsEnumerable((Tuple<object, object, object, object>)null));
            AssertArgumentNullExceptionForNullThis(() => TupleExtensions.AsEnumerable((Tuple<object, object, object, object, object>)null));
            AssertArgumentNullExceptionForNullThis(() => TupleExtensions.AsEnumerable((Tuple<object, object, object, object, object, object>)null));
            AssertArgumentNullExceptionForNullThis(() => TupleExtensions.AsEnumerable((Tuple<object, object, object, object, object, object, object>)null));

            // ReSharper restore InvokeAsExtensionMethod
        }

        static void AssertEqual(IEnumerable<object> actual, params object[] expected)
        {
            Assert.NotNull(actual);
            Assert.NotNull(expected);
            Assert.True(actual.SequenceEqual(expected));
        }

        [Fact]
        public void AsEnumerableWithTuple1()
        {
            AssertEqual(Tuple.Create((object) 1).AsEnumerable(), 1);
        }

        [Fact]
        public void AsEnumerableWithTuple2()
        {
            AssertEqual(Tuple.Create(1, 2).AsEnumerable(), 1, 2);
        }

        [Fact]
        public void AsEnumerableWithTuple3()
        {
            AssertEqual(Tuple.Create(1, 2, 3).AsEnumerable(), 1, 2, 3);
        }

        [Fact]
        public void AsEnumerableWithTuple4()
        {
            AssertEqual(Tuple.Create(1, 2, 3, 4).AsEnumerable(), 1, 2, 3, 4);
        }

        [Fact]
        public void AsEnumerableWithTuple5()
        {
            AssertEqual(Tuple.Create(1, 2, 3, 4, 5).AsEnumerable(), 1, 2, 3, 4, 5);
        }

        [Fact]
        public void AsEnumerableWithTuple6()
        {
            AssertEqual(Tuple.Create(1, 2, 3, 4, 5, 6).AsEnumerable(), 1, 2, 3, 4, 5, 6);
        }

        [Fact]
        public void AsEnumerableWithTuple7()
        {
            AssertEqual(Tuple.Create(1, 2, 3, 4, 5, 6, 7).AsEnumerable(), 1, 2, 3, 4, 5, 6, 7);
        }

        static void AssertArgumentNullExceptionForNullThis(Action testCode)
        {
            var e = Assert.Throws<ArgumentNullException>(testCode);
            Assert.Equal("tuple", e.ParamName);
        }
    }
}