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
    using System.Globalization;
    using System.Linq;
    using System.Linq.Expressions;
    using Xunit;
    using Xunit.Extensions;

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
            var e = Assert.Throws<ArgumentNullException>(() => typeof(int?).IsConstructionOfGenericTypeDefinition(null));
            Assert.Equal("genericTypeDefinition", e.ParamName);
        }

        [Fact]
        public void IsConstructionOfGenericTypeDefinitionWithNonGenericTypeDefinition()
        {
            var e = Assert.Throws<ArgumentException>(() => typeof(int?).IsConstructionOfGenericTypeDefinition(typeof(int)));
            Assert.Equal("genericTypeDefinition", e.ParamName);
        }

        [Fact]
        public void IsConstructionOfGenericTypeDefinitionReturnsTrueWhenTypeIsConstructionOfGenericTypeDefinition()
        {
            Assert.True(typeof(int?).IsConstructionOfGenericTypeDefinition(typeof(Nullable<>)));
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
            Assert.True(typeof(int?).IsConstructionOfNullable());
        }

        [Fact]
        public void IsConstructionOfNullableReturnsFalseWhenTypeIsNotNullable()
        {
            Assert.False(typeof(int).IsConstructionOfNullable());
        }

        [Fact]
        public void FindParseMethodWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() => TypeExtensions.FindParseMethod(null));
            Assert.Equal("type", e.ParamName);
        }

        [Fact]
        public void FindParseMethod()
        {
            Expression<Func<int>> e = () => int.Parse(null, null);
            var mce = (MethodCallExpression) e.Body;
            Assert.Equal(mce.Method, typeof(int).FindParseMethod());
        }

        [Fact]
        public void FindParseMethodWithTypeHavingNoParseMethod()
        {
            Assert.Null(typeof(Unparseable).FindParseMethod());
        }

        class Unparseable
        {   // ReSharper disable once UnusedMember.Local
            // ReSharper disable UnusedParameter.Local
            public static object Parse(string s, IFormatProvider fp) { return null; }
            // ReSharper restore UnusedParameter.Local
        }

        [Fact]
        public void GetParser()
        {
            var parser = typeof(DateTimeOffset).GetParser();
            Assert.NotNull(parser);
            var formatProvider = new CultureInfo("en-US");
            const string input = "3/27/2015 12:34:56 AM +02:30";
            var result = parser(input, formatProvider);
            Assert.Equal(DateTimeOffset.Parse(input, formatProvider), result);
        }

        [Fact]
        public void GetDefaultValueWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() => TypeExtensions.GetDefaultValue(null));
            Assert.Equal("type", e.ParamName);
        }

        [Fact]
        public void GetDefaultValueWithGenericTypeDefinition()
        {
            var e = Assert.Throws<ArgumentException>(() => typeof(Func<>).GetDefaultValue());
            Assert.Equal("type", e.ParamName);
        }

        [Fact]
        public void GetDefaultValueWithGenericParameterType()
        {
            var e = Assert.Throws<ArgumentException>(() => typeof(Func<>).GetGenericArguments().First().GetDefaultValue());
            Assert.Equal("type", e.ParamName);
        }

        [Theory, MemberData("GetDefaultValueData")]
        public void GetDefaultValue(object expected, Type type)
        {
            Assert.Equal(expected, type.GetDefaultValue());
        }

        public static readonly object[][] GetDefaultValueData =
        {
            new object[] { default(int), typeof(int) },
            new object[] { default(int?), typeof(int?) },
            new object[] { default(DateTime), typeof(DateTime) },
            new object[] { default(DateTimeOffset), typeof(DateTimeOffset) },
            new object[] { default(Guid), typeof(Guid) },
            new object[] { StringSplitOptions.None, typeof(StringSplitOptions) },
            new object[] { default(string), typeof(string) },
            new object[] { default(object), typeof(object) },
            new object[] { default(Func<int, int>), typeof(Func<int, int>) },
        };
    }
}