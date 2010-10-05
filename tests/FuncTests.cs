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

    public class FuncTests
    {
        [Fact]
        public void ToConverterFailsWithNullSender()
        {
            var e = Assert.Throws<ArgumentNullException>(() => 
                        FuncExtensions.ToConverter<object, object>(null));
            Assert.Equal("converter", e.ParamName);
        }

        [Fact]
        public void ToConverter()
        {
            Func<string, int> parse = int.Parse;
            var converter = parse.ToConverter();
            Assert.NotNull(converter);
            Assert.Equal(42, converter("42"));
        }

        [Fact]
        public void ToPredicateWithNullSender()
        {
            var e = Assert.Throws<ArgumentNullException>(() =>
                        FuncExtensions.ToPredicate<object>(null));
            Assert.Equal("predicate", e.ParamName);
        }

        [Fact]
        public void ToPredicate()
        {
            Func<Type, bool> isClass = t => t.IsClass;
            var predicate = isClass.ToPredicate();
            Assert.NotNull(predicate);
            Assert.Equal(true, predicate(typeof(object)));
            Assert.Equal(false, predicate(typeof(int)));
        }
    }
}