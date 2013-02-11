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

    public class PredicateTests
    {
        readonly Predicate<object> _true = delegate { return true; };
        readonly Predicate<object> _false = delegate { return false; };
        readonly Predicate<object> _error = delegate { throw new NotImplementedException(); };

        [Fact]
        public void AndFailsWithNullThis()
        {
            Assert.Throws<ArgumentNullException>(() => PredicateExtensions.And(null, _error));
        }

        [Fact]
        public void AndFailsWithNullThat()
        {
            Assert.Throws<ArgumentNullException>(() => _error.And(null));
        }

        [Fact]
        public void And()
        {
            Assert.False(_false.And(_false)(null));
            Assert.False(_true.And(_false)(null));
            Assert.False(_false.And(_true)(null));
            Assert.True(_true.And(_true)(null));
        }

        [Fact]
        public void OrFailsWithNullThis()
        {
            Assert.Throws<ArgumentNullException>(() => PredicateExtensions.Or(null, _error));
        }

        [Fact]
        public void OrFailsWithNullThat()
        {
            Assert.Throws<ArgumentNullException>(() => _error.Or(null));
        }

        [Fact]
        public void Or()
        {
            Assert.False(_false.Or(_false)(null));
            Assert.True(_true.Or(_false)(null));
            Assert.True(_false.Or(_true)(null));
            Assert.True(_true.Or(_true)(null));
        }
    }
}