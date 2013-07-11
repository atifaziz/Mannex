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
    using Mannex.Reflection;
    using Xunit;

    #endregion

    public class MethodInfoTests
    {
        [Fact]
        public void CompileStaticInvokerFailsWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() => MethodInfoExtensions.CompileStaticInvoker(null));
            Assert.Equal("method", e.ParamName);
        }

        [Fact]
        public void CompileStaticInvokerFailsWithNonStaticMethod()
        {
            var e = Assert.Throws<ArgumentException>(() => 
                ((Func<string>) 1.ToString).Method.CompileStaticInvoker());
            Assert.Equal("method", e.ParamName);
        }

        static Func<object[], object> GetArrayReverseInvoker()
        {
            var method = ((Action<Array>) Array.Reverse).Method;
            var reverse = method.CompileStaticInvoker();
            Assert.NotNull(reverse);
            return reverse;
        }

        [Fact]
        public void CompileStaticInvokerOnMethodWithoutReturnValue()
        {
            var nums = new[] { 2, 0, 1, 3 };
            GetArrayReverseInvoker()(new object[] { nums });
            Assert.Equal(new[] { 3, 1, 0, 2 }, nums);
        }

        [Fact]
        public void CompileStaticInvokerInvocationWithTooFewArguments()
        {
            var e = Assert.Throws<ArgumentException>(() => GetArrayReverseInvoker()(new object[0]));
            Assert.Equal("args", e.ParamName);
        }

        [Fact]
        public void CompileStaticInvokerInvocationWithTooManyArguments()
        {
            var e = Assert.Throws<ArgumentException>(() => GetArrayReverseInvoker()(new object[2]));
            Assert.Equal("args", e.ParamName);
        }

        [Fact]
        public void CompileStaticInvokerOnMethodReturningValue()
        {
            var d = (Func<int[], Predicate<int>, int>) Array.FindIndex;
            var findIndex = d.Method.CompileStaticInvoker();
            var index = findIndex(new object[] { new[] { 1, 2, 3 }, new Predicate<int>(x => x % 2 == 0) });
            Assert.Equal(1, index);
        }

        [Fact]
        public void CompileStaticInvokerOnMethodWithOptionalParameters()
        {
            var d = (Func<int, int>) FortyTwoFactor;
            Assert.Equal(42, d.Method.CompileStaticInvoker()(new[] { Type.Missing }));
        }

        static int FortyTwoFactor(int x = 1) { return 42 * x; }
        
        [Fact]
        public void CompileStaticInvokerOnMethodWithNullForValueTypeParameter()
        {
            var d = (Func<int, int>) Echo;
            Assert.Equal(0, d.Method.CompileStaticInvoker()(new object[1]));
        }

        static int Echo(int x) { return x; }
    }
}