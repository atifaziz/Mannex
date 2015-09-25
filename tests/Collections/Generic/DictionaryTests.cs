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
    using Mannex.Collections.Generic;
    using Xunit;

    #endregion

    public class DictionaryTests
    {
        [Fact]
        public void FindFailsWithNullThis()
        {
            Assert.Throws<ArgumentNullException>(() => 
                DictionaryExtensions.Find<object, object>(null, null));
        }

        [Fact]
        public void FindReturnsDefaultWhenKeyKeyNotPresent()
        {
            Assert.Equal(0, new Dictionary<int, int>().Find(42));
        }

        [Fact]
        public void FindReturnsSpecificDefaultWhenKeyKeyNotPresent()
        {
            Assert.Equal(-42, new Dictionary<int, int>().Find(42, -42));
        }

        [Fact]
        public void FindReturnsValueOfPresentKey()
        {
            var dict = new Dictionary<int, string> { { 42, "fourty two" } };
            Assert.Equal("fourty two", dict.Find(42));
        }

        [Fact]
        public void GetFailsWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() => DictionaryExtensions.GetValue<object, object>(null, "foo", delegate { return null; }));
            Assert.Equal(e.ParamName, "dictionary");
        }

        [Fact]
        public void GetWithNullErrorSelector()
        {
            var dict = new Dictionary<string, object>();
            Assert.Throws<KeyNotFoundException>(() => dict.GetValue("foo", null));
        }

        [Fact]
        public void GetWithErrorSelectorReturningNullException()
        {
            var dict = new Dictionary<string, object>();
            Assert.Throws<KeyNotFoundException>(() => dict.GetValue("foo", _ => null));
        }

        [Fact]
        public void GetWithErrorSelectorReturningApplicationException()
        {
            var dict = new Dictionary<string, object>();
            var e = Assert.Throws<ApplicationException>(() => dict.GetValue("foo", key => new ApplicationException("`" + key + "` not found.")));
            Assert.Equal("`foo` not found.", e.Message);
        }

        [Fact]
        public void GetWithExistingKeyAndErrorSelector()
        {
            var dict = new Dictionary<string, int> { { "foo", 42 } };
            Assert.Equal(42, dict.GetValue("foo", null));
        }

        [Fact]
        public void PopFailsWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() =>
                DictionaryExtensions.Pop<object, object>(null, new object()));
            Assert.Equal(e.ParamName, "dictionary");
        }

        [Fact]
        public void PopFailsWhenKeyNotFound()
        {
            var map = new Dictionary<object, object>();
            Assert.Throws<KeyNotFoundException>(() => map.Pop(new object()));
        }

        [Fact]
        public void Pop()
        {
            var k = new object();
            var v = new object();
            var map = new Dictionary<object, object> { { k, v } };
            Assert.Equal(v, map.Pop(k));
            Assert.Equal(0, map.Count);
        }
    }
}