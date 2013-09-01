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

namespace Mannex.Tests.Net.Mime
{
    #region Imports

    using System;
    using System.Net.Mime;
    using System.Text;
    using Mannex.Net.Mime;
    using Xunit;

    #endregion

    public class ContentTypeTests
    {
        [Fact]
        public void EncodingFromCharSetWithNullThis()
        {
            Assert.Equal("contentType", Assert.Throws<ArgumentNullException>(() => ContentTypeExtensions.EncodingFromCharSet(null)).ParamName);
            Assert.Equal("contentType", Assert.Throws<ArgumentNullException>(() => ContentTypeExtensions.EncodingFromCharSet(null, (Encoding)null)).ParamName);
            Assert.Equal("contentType", Assert.Throws<ArgumentNullException>(() => ContentTypeExtensions.EncodingFromCharSet(null, (Func<string, Encoding>)null)).ParamName);
            Assert.Equal("contentType", Assert.Throws<ArgumentNullException>(() => ContentTypeExtensions.EncodingFromCharSet(null, null, null)).ParamName);
        }
        
        [Fact]
        public void EncodingFromCharSet()
        {
            Assert.Equal(Encoding.UTF8, new ContentType("text/plain; charset=UTF-8").EncodingFromCharSet());
        }

        [Fact]
        public void EncodingFromCharSetWhenCharSetSpecMissing()
        {
            Assert.Null(new ContentType("text/plain").EncodingFromCharSet());
        }

        [Fact]
        public void EncodingFromCharSetWithDefaultEncodingWhenCharSetSpecMissing()
        {
            Assert.Equal(Encoding.ASCII, new ContentType("text/plain").EncodingFromCharSet(Encoding.ASCII));
        }
        
        [Fact]
        public void EncodingFromCharSetWithEncodingSelector()
        {
            var called = false;
            new ContentType("text/plain; charset=UTF-8").EncodingFromCharSet(_ =>
            {
                called = true;
                return null;
            });
            Assert.True(called);
        }
    }
}