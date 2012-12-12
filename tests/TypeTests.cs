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
    #region Improts

    using System;
    using System.Net;
    using Xunit;

    #endregion

    public class TypeTests
    {
        [Fact]
        public void IsConstructionOfGenericTypeDefinitionWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() => TypeExtensions.IsConstructionOfGenericTypeDefinition(null, typeof(Nullable<>)));
            Assert.Equal("type", e.ParamName);
        }

        [Fact]
        public void IsConstructionOfGenericTypeDefinitionWithNullGenericTypeDefinition()
        {
            var e = Assert.Throws<ArgumentNullException>(() => TypeExtensions.IsConstructionOfGenericTypeDefinition(typeof(Nullable<int>), null));
            Assert.Equal("genericTypeDefinition", e.ParamName);
        }

        [Fact]
        public void IsConstructionOfGenericTypeDefinitionWithNonGenericTypeDefinition()
        {
            var e = Assert.Throws<ArgumentException>(() => TypeExtensions.IsConstructionOfGenericTypeDefinition(typeof(Nullable<int>), typeof(int)));
            Assert.Equal("genericTypeDefinition", e.ParamName);
        }

        [Fact]
        public void IsConstructionOfGenericTypeDefinitionReturnsTrueWhenTypeIsConstructionOfGenericTypeDefinition()
        {
            Assert.True(typeof(Nullable<int>).IsConstructionOfGenericTypeDefinition(typeof(Nullable<>)));
            
        }

        [Fact]
        public void IsConstructionOfGenericTypeDefinitionReturnsFalseWhenTypeIsNotConstructionOfGenericTypeDefinition()
        {
            Assert.False(typeof(int).IsConstructionOfGenericTypeDefinition(typeof(Nullable<>)));
        }

        [Fact]
        public void IsConstructionOfNullableWithNullThis()
        {
            Assert.Throws<ArgumentNullException>(() => TypeExtensions.IsConstructionOfNullable(null));
        }

        [Fact]
        public void IsConstructionOfNullableReturnsTrueWhenTypeIsNullable()
        {
            Assert.True(typeof(Nullable<int>).IsConstructionOfNullable());
            
        }

        [Fact]
        public void IsConstructionOfNullableReturnsFalseWhenTypeIsNotNullable()
        {
            Assert.False(typeof(int).IsConstructionOfNullable());
        }
    }
}