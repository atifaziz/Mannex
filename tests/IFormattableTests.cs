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

using System;

namespace Mannex.Tests
{
    #region Imports

    using System.Globalization;
    using System.Threading;
    using Xunit;

    #endregion

    public class IFormattableTests
    {
        [Fact]
        public void ToInvariantString()
        {
            IFormattable num = 1234;
            Assert.Equal("1234", WhileInFrance(() => num.ToInvariantString()));
        }
 
        [Fact]
        public void ToInvariantStringWithSpecificFormat()
        {
            IFormattable num = 1234;
            Assert.Equal("1,234.00", WhileInFrance(() => num.ToInvariantString("N2")));
        }

        static T WhileInFrance<T>(Func<T> function)
        {
            var saved = Thread.CurrentThread.CurrentCulture;
            var thisThread = Thread.CurrentThread;
            thisThread.CurrentCulture = new CultureInfo("fr-FR");
            try
            {
                return function();
            }
            finally
            {
                thisThread.CurrentCulture = saved;
            }
        }
    }
}