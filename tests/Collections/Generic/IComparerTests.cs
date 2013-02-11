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

namespace Mannex.Tests.Collections.Generic
{
    #region Imports

    using System;
    using System.Collections.Generic;
    using Mannex.Collections.Generic;
    using Xunit;

    #endregion

    public class IComparerTests
    {
        [Fact]
        public void InvertFailsWithNullThis()
        {
            Assert.Throws<ArgumentNullException>(() => IComparerExtensions.Invert<object>(null));
        }

        [Fact]
        public void InvertInvertsComparison()
        {
            var comparer = Comparer<int>.Default.Invert();
            Assert.True(0 <  comparer.Compare(42, 43));
            Assert.True(0 == comparer.Compare(42, 42));
            Assert.True(0 >  comparer.Compare(42, 41));
        }

        [Fact]
        public void InvertOnInvertRestoresComparison()
        {
            var comparer = Comparer<int>.Default.Invert().Invert();
            Assert.True(0 >  comparer.Compare(42, 43));
            Assert.True(0 == comparer.Compare(42, 42));
            Assert.True(0 <  comparer.Compare(42, 41));
        }
    }
}