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

namespace Mannex.Tests.Collections.Specialized
{
    #region Improts

    using System;
    using System.Collections.Specialized;
    using Mannex.Collections.Specialized;
    using Xunit;

    #endregion

    public class NameValueCollectionTests
    {
        [Fact]
        public void FilterFailsWithNullThisAndPredicate()
        {
            Assert.Throws<ArgumentNullException>(() =>
                NameValueCollectionExtensions.Filter(null, delegate { return false; }));
        }

        [Fact]
        public void FilterFailsWithNullPredicate()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new NameValueCollection().Filter((Func<string, bool>) null));
        }

        [Fact]
        public void FilterReturnsSelectionBasedOnPredicate()
        {
            var collection = new NameValueCollection
            {
                { "foo", "FOO" },
                { "bar", "BAR" },
                { "baz", "BAZ1" },
                { "baz", "BAZ2" },
            };

            var result = collection.Filter(k => k.StartsWith("b", StringComparison.Ordinal));
            Assert.Equal(2, result.Count);
            Assert.Equal("bar", result.GetKey(0));
            Assert.Equal("BAR", result[0]);
            Assert.Equal("baz", result.GetKey(1));
            Assert.Equal(new[]{ "BAZ1", "BAZ2" }, result.GetValues(1));
        }

        [Fact]
        public void FilterFailsWithNullThisAndKeySelector()
        {
            Assert.Throws<ArgumentNullException>(() =>
                NameValueCollectionExtensions.Filter(null, delegate { return null; }));
        }

        [Fact]
        public void FilterFailsWithNullKeySelector()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new NameValueCollection().Filter((Func<string, string>) null));
        }

        [Fact]
        public void FilterReturnsSelectionBasedOnKeySelector()
        {
            var collection = new NameValueCollection
            {
                { "foo", "FOO" },
                { "bar", "BAR" },
                { "baz", "BAZ1" },
                { "baz", "BAZ2" },
            };

            var result = collection.Filter(k => k.StartsWith("b", StringComparison.Ordinal) ? k.ToUpperInvariant() : null);
            Assert.Equal(2, result.Count);
            Assert.Equal("BAR", result.GetKey(0));
            Assert.Equal("BAR", result[0]);
            Assert.Equal("BAZ", result.GetKey(1));
            Assert.Equal(new[] { "BAZ1", "BAZ2" }, result.GetValues(1));
        }

        [Fact]
        public void FilterByPrefixFailsWithNullThis()
        {
            Assert.Throws<ArgumentNullException>(() =>
                NameValueCollectionExtensions.FilterByPrefix(null, "prefix"));
        }

        [Fact]
        public void FilterByPrefixReturnsItemsWithKeysMatchingPrefix()
        {
            var collection = new NameValueCollection
            {
                { "a:foo", "FOO" },
                { "b:",    "B" },
                { "b:bar", "BAR" },
                { "b:baz", "BAZ1" },
                { "b:baz", "BAZ2" },
            };

            var result = collection.FilterByPrefix("b:");
            Assert.Equal(2, result.Count);
            Assert.Equal("bar", result.GetKey(0));
            Assert.Equal("BAR", result[0]);
            Assert.Equal("baz", result.GetKey(1));
            Assert.Equal(new[] { "BAZ1", "BAZ2" }, result.GetValues(1));
        }

        [Fact]
        public void FilterByPrefixReturnsExactCopyWithNullOrEmptyPrefix()
        {
            var collection = new NameValueCollection
            {
                { "a:foo", "FOO" },
                { "b:bar", "BAR" },
                { "b:baz", "BAZ1" },
                { "b:baz", "BAZ2" },
            };

            foreach (var prefix in new[] { null, string.Empty })
            {
                var result = collection.FilterByPrefix(prefix);
                Assert.NotSame(collection, result);
                Assert.Equal(3, result.Count);
                Assert.Equal("a:foo", result.GetKey(0));
                Assert.Equal("FOO", result[0]);
                Assert.Equal("b:bar", result.GetKey(1));
                Assert.Equal("BAR", result[1]);
                Assert.Equal("b:baz", result.GetKey(2));
                Assert.Equal(new[] { "BAZ1", "BAZ2" }, result.GetValues(2));
            }
        }
    }
}