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

namespace Mannex.Tests.Web.Script.Serialization
{
    #region Imports

    using System;
    using Mannex.Web.Script.Serialization;
    using Xunit;

    #endregion

    public class ObjectTests
    {
        [Fact]
        public void ToJsonStringWithNullThisReturnJsonNull()
        {
            Assert.Equal("null", ((object) null).ToJsonString());
        }

        [Fact]
        public void ToJsonStringFormatsJsonString()
        {
            Assert.Equal("\"foo bar\"", "foo bar".ToJsonString());
            Assert.Equal("42", 42.ToJsonString());
            Assert.Equal("true", true.ToJsonString());
            Assert.Equal("false", false.ToJsonString());
            Assert.Equal("[12,34,56]", new[] { 12, 34, 56 }.ToJsonString());
            Assert.Equal("{\"x\":12,\"y\":34}", new { x = 12, y = 34 }.ToJsonString());
        }

        [Fact]
        public void ToJsonStringEscapes()
        {
            Assert.Equal("\"\\\"foo bar\\\"\"", "\"foo bar\"".ToJsonString());
        }
    }
}