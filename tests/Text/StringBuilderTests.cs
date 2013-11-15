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

namespace Mannex.Tests.Text
{
    #region Imports

    using System;
    using System.Text;
    using Mannex.Text;
    using Xunit;

    #endregion

    public class StringBuilderTests
    {
        [Fact]
        public void AppendNullableCharFailsWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() => StringBuilderExtensions.Append(null, 'a'));
            Assert.Equal("sb", e.ParamName);
        }

        [Fact]
        public void AppendNullableCharDoesntAppendWithNullChar()
        {
            Assert.Equal(0, new StringBuilder().Append((char?) null).Length);
        }

        [Fact]
        public void AppendNullableCharDoesntAppendWithChar()
        {
            Assert.Equal('a', new StringBuilder().Append((char?) 'a')[0]);
        }
    }
}