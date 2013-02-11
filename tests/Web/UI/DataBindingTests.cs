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

namespace Mannex.Tests.Web.UI
{
    #region Imports

    using System;
    using Mannex.Web.UI;
    using Xunit;

    #endregion

    public class HtmlStringTests
    {
        [Fact]
        public void DataBindFailsWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() => DataBindingExtensions.DataBind(null, "?"));
            Assert.Equal("obj", e.ParamName);
            e = Assert.Throws<ArgumentNullException>(() => DataBindingExtensions.DataBind<object>(null, "?"));
            Assert.Equal("obj", e.ParamName);
        }

        [Fact]
        public void DataBindWithNullExpressionYieldThis()
        {
            var obj = new object();
            Assert.Same(obj, obj.DataBind(null));
            Assert.Same(obj, obj.DataBind<object>(null));
        }

        [Fact]
        public void DataBindWithEmptyExpressionYieldThis()
        {
            var obj = new object();
            Assert.Same(obj, obj.DataBind(string.Empty));
            Assert.Same(obj, obj.DataBind<object>(string.Empty));
        }

        [Fact]
        public void DataBindReturnsValueExpressionEvaluation()
        {
            Assert.Equal(3, "foo".DataBind("Length"));
            Assert.Equal(3, "foo".DataBind<int>("Length"));
        }
    }
}