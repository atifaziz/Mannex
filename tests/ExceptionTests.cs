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

    public class ExceptionTests
    {
        [Fact]
        public void PrepareForRethrowFailsWithNullSelf()
        {
            Assert.Throws<ArgumentNullException>(() =>
                ExceptionExtensions.PrepareForRethrow(null));
        }

        [Fact]
        public void PrepareForRethrowPreservesStackTrace()
        {
            var e = Assert.Throws<Exception>(() => Bad.Foo(ex =>
                    {
                        throw ex.PrepareForRethrow();
                    }));
            Console.WriteLine(e.StackTrace);
            Assert.Contains("Baz", e.StackTrace);
        }

        [Fact]
        public void RethrowFailsWithNullSelf()
        {
            Assert.Throws<ArgumentNullException>(() =>
                ExceptionExtensions.Rethrow(null));
        }

        [Fact]
        public void RethrowPreservesStackTrace()
        {
            var e = Assert.Throws<Exception>(() => Bad.Foo(ex => ex.Rethrow()));
            Console.WriteLine(e.StackTrace);
            Assert.Contains("Baz", e.StackTrace);
        }

        static class Bad
        {
            public static void Foo(Action<Exception> @throw)
            {
                try
                {
                    Bar();
                }
                catch (Exception e)
                {
                    @throw(e);
                }
            }

            private static void Bar()
            {
                Baz();
            }

            private static void Baz()
            {
                throw new Exception();
            }
        }
    }
}