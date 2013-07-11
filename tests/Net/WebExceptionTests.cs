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
    using System.Linq;
    using System.Net;
    using System.Runtime.Serialization;
    using Mannex.Net;
    using Xunit;

    #endregion

    public class WebExceptionTests
    {
        [Fact]
        public void TryGetWebResponseWithNullThis()
        {
            Assert.Equal("exception", Assert.Throws<ArgumentNullException>(() => WebExceptionExtensions.TryGetWebResponse<WebResponse>(null)).ParamName);
        }

        [Fact]
        public void TryGetWebResponseReturnsWebResponseWhenProtocolError()
        {
            var response = new WebResponseStub();
            var e = new WebException(null, null, WebExceptionStatus.ProtocolError, response);
            Assert.Same(response, e.TryGetWebResponse<WebResponseStub>());
        }

        [Fact]
        public void TryGetWebResponseReturnsNullWhenNonProtocolError()
        {
            foreach (var status in from WebExceptionStatus status in Enum.GetValues(typeof(WebExceptionStatus))
                                   where status != WebExceptionStatus.ProtocolError
                                   select status)
            {
                var e = new WebException(null, null, status, new WebResponseStub());
                Assert.True(null == e.TryGetWebResponse<WebResponse>(), status.ToString());
            }
        }

        [Fact]
        public void TryGetWebResponseReturnsWebResponseWhenTypeCompatible()
        {
            var response = new OtherWebResponseStub();
            var e = new WebException(null, null, WebExceptionStatus.ProtocolError, response);
            Assert.Null(e.TryGetWebResponse<WebResponseStub>());
        }

        [Fact]
        public void TryGetWebResponseReturnsNullWhenWebResponseTypeIsIncompatible()
        {
            var response = new OtherWebResponseStub();
            var e = new WebException(null, null, WebExceptionStatus.ProtocolError, response);
            Assert.Null(e.TryGetWebResponse<HttpWebResponse>());
        }

        public class WebResponseStub : WebResponse {}
        public class OtherWebResponseStub : WebResponse {}
    }
}