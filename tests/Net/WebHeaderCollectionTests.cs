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

namespace Mannex.Tests.Net
{
    #region Imports

    using System;
    using System.Net;
    using System.Net.Mime;
    using Mannex.Net;
    using Xunit;

    #endregion

    public class WebHeaderCollectionTests
    {
        [Fact]
        public void MapWithNullThis()
        {
            Assert.Equal("headers", Assert.Throws<ArgumentNullException>(() => WebHeaderCollectionExtensions.Map(null, default(HttpResponseHeader), _ => (object)null)).ParamName);
        }

        [Fact]
        public void MapWithNullResultor()
        {
            var headers = new WebHeaderCollection();
            Assert.Equal("mapper", Assert.Throws<ArgumentNullException>(() => headers.Map<object>(default(HttpResponseHeader), null)).ParamName);
        }

        [Fact]
        public void MapWhenHeaderMissing()
        {
            var headers = new WebHeaderCollection();
            Assert.Equal(-1, headers.Map(HttpResponseHeader.ContentType, -1, delegate { throw new InvalidOperationException(); }));
        }

        [Fact]
        public void Map()
        {
            var headers = new WebHeaderCollection
            {
                { HttpResponseHeader.ContentType, "text/plain" }
            };
            var contentType = headers.Map(HttpResponseHeader.ContentType, null, h => new ContentType(h));
            Assert.NotNull(contentType);
            Assert.Equal("text/plain", contentType.MediaType);
        }
    }
}