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

namespace Mannex.Tests.Reflection
{
    #region Imports

    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
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
        public void IsDefinedReturnsFalseForSubclassWhenAttributeInheritanceIsNotRequested()
        {
            Assert.False(typeof(Subtest).IsDefined<DescriptionAttribute>(false));
        }

        [Fact]
        public void GetCustomAttributesFailsWithNullThis()
        {
            Assert.Throws<ArgumentNullException>(() =>
                ICustomAttributeProviderExtensions.GetCustomAttributes<object>(null, true));
        }

        [Fact]
        public void GetCustomAttributesReturnsRequestedAttributeWhenAttributesApplied()
        {
            ICustomAttributeProvider cap = typeof(Test);
            var attributes = cap.GetCustomAttributes<TestAttribute>(true);
            Assert.NotNull(attributes);
            Assert.Equal(new[] { 12, 34 }, attributes.Select(a => a.Value).OrderBy(x => x).ToArray());
        }

        [Fact]
        public void GetCustomAttributesReturnsEmptyWhenAttributesAbsent()
        {
            ICustomAttributeProvider cap = typeof(Test);
            var attributes = cap.GetCustomAttributes<CategoryAttribute>(true);
            Assert.NotNull(attributes);
            Assert.Equal(0, attributes.Length);
        }

        [Fact]
        public void GetCustomAttributesReturnsEmptyForSubclassWhenAttributeInheritanceIsNotRequested()
        {
            ICustomAttributeProvider cap = typeof(Subtest);
            var attributes = cap.GetCustomAttributes<TestAttribute>(false);
            Assert.NotNull(attributes);
            Assert.Equal(0, attributes.Length);
        }

        [Fact]
        public void GetCustomAttributeFailsWithNullThis()
        {
            Assert.Throws<ArgumentNullException>(() =>
                ICustomAttributeProviderExtensions.GetCustomAttribute<object>(null, true));
        }

        [Fact]
        public void GetCustomAttributeFailsForDuplicateAttribtueApplication()
        {
            ICustomAttributeProvider cap = typeof(Test);
            Assert.Throws<AmbiguousMatchException>(() =>
                cap.GetCustomAttribute<TestAttribute>(true));
        }

        [Fact]
        public void GetCustomAttributeReturnsRequestedAttributeWhenAttributesApplied()
        {
            ICustomAttributeProvider cap = typeof(Test);
            var attribute = cap.GetCustomAttribute<DescriptionAttribute>(true);
            Assert.NotNull(attribute);
            Assert.Equal("foo", attribute.Description);
        }

        [Fact]
        public void GetCustomAttributeReturnsEmptyWhenAttributesAbsent()
        {
            ICustomAttributeProvider cap = typeof(Test);
            Assert.Null(cap.GetCustomAttribute<CategoryAttribute>(true));
        }

        [Fact]
        public void GetCustomAttributeReturnsEmptyForSubclassWhenAttributeInheritanceIsNotRequested()
        {
            ICustomAttributeProvider cap = typeof(Subtest);
            Assert.Null(cap.GetCustomAttribute<DescriptionAttribute>(true));
        }

        [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
        class TestAttribute : Attribute
        {
            public int Value { get; set; }

            public TestAttribute(int value)
            {
                Value = value;
            }
        }

        [Description("foo"), Test(12), Test(34)]
        class Test {}

        class Subtest { }
    }
}