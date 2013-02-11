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
    using Mannex.Web;
    using Xunit;

    #endregion

    public class HtmlStringTests
    {
        [Fact]
        public void HtmlEncodeFailsWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() => HtmlStringExtensions.HtmlEncode(null));
            Assert.Equal("str", e.ParamName);
        }

        [Fact]
        public void HtmlEncode()
        {
            Assert.Equal("&lt;foo&gt; &amp; &quot;bar&quot;", "<foo> & \"bar\"".HtmlEncode());
        }
    }
}