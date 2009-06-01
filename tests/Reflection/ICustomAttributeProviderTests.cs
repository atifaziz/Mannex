﻿#region License, Terms and Author(s)
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

namespace Mannex.Tests.Reflection
{
    #region Imports

    using System;
    using System.ComponentModel;
    using Mannex.Reflection;
    using Xunit;

    #endregion

    public class ICustomAttributeProviderTests
    {
        [Fact]
        public void IsDefinedFailsWithNullThis()
        {
            Assert.Throws<ArgumentNullException>(() => 
                ICustomAttributeProviderExtensions.IsDefined<object>(null, true));
        }

        [Fact]
        public void IsDefinedReturnsTrueWhenAttributeIsApplied()
        {
            Assert.True(typeof(Test).IsDefined<DescriptionAttribute>(true));
        }
        
        [Fact]
        public void IsDefinedReturnsTrueWhenAttributeIsAbsent()
        {
            Assert.False(typeof(Test).IsDefined<CategoryAttribute>(true));
        }

        [Fact]
        public void IsDefinedReturnsFalseForSubclasWhenAttributeInheritanceIsNotRequested()
        {
            Assert.False(typeof(Subtest).IsDefined<DescriptionAttribute>(false));
        }

        [Description]
        class Test {}

        class Subtest { }
    }
}