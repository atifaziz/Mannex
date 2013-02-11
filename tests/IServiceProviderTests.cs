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
    using System.ComponentModel.Design;
    using Xunit;

    #endregion

    public class IServiceProviderTests
    {
        [Fact]
        public void GetServiceFailsWithNullThis()
        {
            Assert.Throws<ArgumentNullException>(() => IServiceProviderExtensions.GetService<object>(null));
        }

        [Fact]
        public void GetServiceReturnsNullWhenServiceNotFound()
        {
            var sp = new ServiceContainer();
            Assert.Null(sp.GetService<object>());
        }

        [Fact]
        public void GetServiceReturnsServiceObject()
        {
            var sp = new ServiceContainer();
            sp.AddService(typeof(IConvertible), 42);
            Assert.Equal(42, sp.GetService<IConvertible>());
        }

        [Fact]
        public void GetRequiredServiceFailsWithNullThis()
        {
            Assert.Throws<ArgumentNullException>(() => IServiceProviderExtensions.GetRequiredService<object>(null));
        }

        [Fact]
        public void GetRequiredServiceThrowsWhenServiceNotFound()
        {
            var sp = new ServiceContainer();
            var e = Assert.Throws<Exception>(() => sp.GetRequiredService<object>());
            Assert.Equal("Service of type System.Object is unavailable.", e.Message);
        }

        [Fact]
        public void GetRequiredServiceReturnsServiceObject()
        {
            var sp = new ServiceContainer();
            sp.AddService(typeof(IConvertible), 42);
            Assert.Equal(42, sp.GetRequiredService<IConvertible>());
        }
    }
}
