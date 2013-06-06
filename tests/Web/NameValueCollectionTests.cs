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

namespace Mannex.Tests.Web
{
    #region Imports

    using System;
    using System.Collections.Specialized;
    using System.Web;
    using Mannex.Web;
    using Xunit;

    #endregion

    public class NameValueCollectionTests
    {
        [Fact]
        public void ToQueryStringFailsWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() => NameValueCollectionExtensions.ToQueryString(null));
            Assert.Equal("collection", e.ParamName);
        }

        [Fact]
        public void ToQueryStringOnEmptyCollectionReturnsEmptyString()
        {
            Assert.Equal(string.Empty, new NameValueCollection().ToQueryString());
        }

        [Fact]
        public void ToQueryStringOnCollectionWithOnePair()
        {
            var collection = new NameValueCollection
            {
                { "key", "value" },
            };
            Assert.Equal("?key=value", collection.ToQueryString());
        }

        [Fact]
        public void ToQueryStringOnCollectionWithManyPairs()
        {
            var collection = new NameValueCollection
            {
                { "ka", "v1" },
                { "kb", "v2" },
                { "kc", "v3" },
            };
            Assert.Equal("?ka=v1&kb=v2&kc=v3", collection.ToQueryString());
        }

        [Fact]
        public void ToQueryStringOnCollectionWithUnsortedMultiValueKeys()
        {
            var collection = new NameValueCollection
            {
                { "a", "a1" },
                { "c", "c1" },
                { "b", "b1" },
                { "a", "a2" },
                { "c", "c2" },
                { "b", "b2" },
                { "a", "a3" },
                { "c", "c3" },
                { "b", "b3" },
            };
            Assert.Equal("?a=a1&a=a2&a=a3&c=c1&c=c2&c=c3&b=b1&b=b2&b=b3", collection.ToQueryString());
        }

        [Fact]
        public void ToQueryStringOnCollectionWithMultiValueNullKey()
        {
            var collection = new NameValueCollection
            {
                { null, "v1" },
                { null, "v2" },
                { null, "v3" },
            };
            Assert.Equal("?v1&v2&v3", collection.ToQueryString());
        }

        [Fact]
        public void ToQueryStringOnCollectionWithNullKeyAndHavingEmptyValues()
        {
            var collection = new NameValueCollection
            {
                { null, string.Empty },
                { null, string.Empty },
                { null, string.Empty },
            };
            Assert.Equal(string.Empty, collection.ToQueryString());
        }

        [Fact]
        public void ToQueryStringOnCollectionWithKeyHavingEmptyValues()
        {
            var collection = new NameValueCollection
            {
                { "key", string.Empty },
                { "key", string.Empty },
                { "key", string.Empty },
            };
            Assert.Equal("?key=&key=&key=", collection.ToQueryString());
        }

        [Fact]
        public void ToQueryStringOnCollectionWithEmptyKeyHavingEmptyValues()
        {
            var collection = new NameValueCollection
            {
                { string.Empty, string.Empty },
                { string.Empty, string.Empty },
                { string.Empty, string.Empty },
            };
            Assert.Equal(string.Empty, collection.ToQueryString());
        }

        [Fact]
        public void ToQueryStringEncodesValues()
        {
            var collection = new NameValueCollection
            {
                { "msg", "hello world" },
            };
            Assert.Equal("?msg=hello%20world", collection.ToQueryString());
        }

        [Fact]
        public void ToW3FormEncodedFailsWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() => NameValueCollectionExtensions.ToW3FormEncoded(null));
            Assert.Equal("collection", e.ParamName);
        }

        [Fact]
        public void ToW3FormEncodedOnEmptyCollectionReturnsEmptyString()
        {
            Assert.Equal(string.Empty, new NameValueCollection().ToW3FormEncoded());
        }

        [Fact]
        public void ToW3FormEncodedOnCollectionWithOnePair()
        {
            var collection = new NameValueCollection
            {
                { "key", "value" },
            };
            Assert.Equal("key=value", collection.ToW3FormEncoded());
        }

        [Fact]
        public void ToW3FormEncodedOnCollectionWithManyPairs()
        {
            var collection = new NameValueCollection
            {
                { "ka", "v1" },
                { "kb", "v2" },
                { "kc", "v3" },
            };
            Assert.Equal("ka=v1&kb=v2&kc=v3", collection.ToW3FormEncoded());
        }

        [Fact]
        public void ToW3FormEncodedOnCollectionWithUnsortedMultiValueKeys()
        {
            var collection = new NameValueCollection
            {
                { "a", "a1" },
                { "c", "c1" },
                { "b", "b1" },
                { "a", "a2" },
                { "c", "c2" },
                { "b", "b2" },
                { "a", "a3" },
                { "c", "c3" },
                { "b", "b3" },
            };
            Assert.Equal("a=a1&a=a2&a=a3&c=c1&c=c2&c=c3&b=b1&b=b2&b=b3", collection.ToW3FormEncoded());
        }

        [Fact]
        public void ToW3FormEncodedOnCollectionWithMultiValueNullKey()
        {
            var collection = new NameValueCollection
            {
                { null, "v1" },
                { null, "v2" },
                { null, "v3" },
            };
            Assert.Equal("v1&v2&v3", collection.ToW3FormEncoded());
        }

        [Fact]
        public void ToW3FormEncodedOnCollectionWithNullKeyAndHavingEmptyValues()
        {
            var collection = new NameValueCollection
            {
                { null, string.Empty },
                { null, string.Empty },
                { null, string.Empty },
            };
            Assert.Equal(string.Empty, collection.ToW3FormEncoded());
        }

        [Fact]
        public void ToW3FormEncodedOnCollectionWithKeyHavingEmptyValues()
        {
            var collection = new NameValueCollection
            {
                { "key", string.Empty },
                { "key", string.Empty },
                { "key", string.Empty },
            };
            Assert.Equal("key=&key=&key=", collection.ToW3FormEncoded());
        }

        [Fact]
        public void ToW3FormEncodedOnCollectionWithEmptyKeyHavingEmptyValues()
        {
            var collection = new NameValueCollection
            {
                { string.Empty, string.Empty },
                { string.Empty, string.Empty },
                { string.Empty, string.Empty },
            };
            Assert.Equal(string.Empty, collection.ToW3FormEncoded());
        }

        [Fact]
        public void ToW3FormEncodedEncodesValues()
        {
            var collection = new NameValueCollection
            {
                { "msg", "hello world" },
            };
            Assert.Equal("msg=hello%20world", collection.ToW3FormEncoded());
        }

        static readonly string VeryLargeValue = new string('z', 64 * 1024);

        [Fact]
        public void ToW3FormEncodedFailsForLargeValues()
        {
            var collection = new NameValueCollection
            {
                { "foo", VeryLargeValue },
            };
            Assert.Throws<UriFormatException>(() => collection.ToW3FormEncoded());
        }

        [Fact]
        public void ToW3FormEncodedWithNullEncoderDefaultsToUriEscapeDataStringAndSoFailsForLargeValues()
        {
            var collection = new NameValueCollection
            {
                { "foo", VeryLargeValue },
            };
            Assert.Throws<UriFormatException>(() => collection.ToW3FormEncoded(null));
        }

        [Fact]
        public void ToW3FormEncodedWithCustomEncoder()
        {
            var collection = new NameValueCollection
            {
                { "msg", "hello world" },
            };
            string arg = null;
            Func<string, string> encoder = s => { arg = s; return Uri.EscapeDataString(s); };
            Assert.Equal("msg=hello%20world", collection.ToW3FormEncoded(encoder));
            Assert.Equal("hello world", arg);
        }
    }
}