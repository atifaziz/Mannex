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

namespace Mannex.Tests
{
    #region Imports

    using System;
    using Xunit;

    #endregion

    public class ICloneableTests
    {
        [Fact]
        public void CloneObjectFailsWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() => ICloneableExtensions.CloneObject<Array>(null));
            Assert.Equal("source", e.ParamName);
        }

        [Fact]
        public void CloneObjectReturnsCopy()
        {
            var array = new[] { new object() };
            var clone = array.CloneObject();
            Assert.NotSame(array, clone);
            Assert.Equal(array[0], clone[0]);
        }
    }
}
