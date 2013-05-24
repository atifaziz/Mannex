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
    using System.Linq;
    using Xunit;

    #endregion

    public class ActionTests
    {
        [Fact]
        public void ReturnWithNullThis()
        {
            Assert.Equal("action", Assert.Throws<ArgumentNullException>(() => ActionExtensions.Return<object>(null, null)).ParamName);
        }
        
        [Fact]
        public void Return()
        {
            var called = false;
            Assert.Equal(42, new Action(() => called = true).Return(42)());
            Assert.True(called);
        }


        [Fact]
        public void ReturnWithNullThis1()
        {
            Assert.Equal("action", Assert.Throws<ArgumentNullException>(() => ActionExtensions.Return<object, object>(null, null)).ParamName);
        }
        
        [Fact]
        public void Return1()
        {
            var cargs = (int[]) null;
            var args = new[] { 1 };
            var action = new Action<int>((a) => cargs = new[] { a });
            var result = action.Return(args.Sum())(args[0]);
            Assert.Equal(args, cargs);
            Assert.Equal(1, result);
        }

        [Fact]
        public void ReturnWithNullThis2()
        {
            Assert.Equal("action", Assert.Throws<ArgumentNullException>(() => ActionExtensions.Return<object, object, object>(null, null)).ParamName);
        }
        
        [Fact]
        public void Return2()
        {
            var cargs = (int[]) null;
            var args = new[] { 1, 2 };
            var action = new Action<int, int>((a, b) => cargs = new[] { a, b });
            var result = action.Return(args.Sum())(args[0], args[1]);
            Assert.Equal(args, cargs);
            Assert.Equal(3, result);
        }

        [Fact]
        public void ReturnWithNullThis3()
        {
            Assert.Equal("action", Assert.Throws<ArgumentNullException>(() => ActionExtensions.Return<object, object, object, object>(null, null)).ParamName);
        }
        
        [Fact]
        public void Return3()
        {
            var cargs = (int[]) null;
            var args = new[] { 1, 2, 3 };
            var action = new Action<int, int, int>((a, b, c) => cargs = new[] { a, b, c });
            var result = action.Return(args.Sum())(args[0], args[1], args[2]);
            Assert.Equal(args, cargs);
            Assert.Equal(6, result);
        }

        [Fact]
        public void ReturnWithNullThis4()
        {
            Assert.Equal("action", Assert.Throws<ArgumentNullException>(() => ActionExtensions.Return<object, object, object, object, object>(null, null)).ParamName);
        }
        
        [Fact]
        public void Return4()
        {
            var cargs = (int[]) null;
            var args = new[] { 1, 2, 3, 4 };
            var action = new Action<int, int, int, int>((a, b, c, d) => cargs = new[] { a, b, c, d });
            var result = action.Return(args.Sum())(args[0], args[1], args[2], args[3]);
            Assert.Equal(args, cargs);
            Assert.Equal(10, result);
        }

        [Fact]
        public void ReturnWithNullThis5()
        {
            Assert.Equal("action", Assert.Throws<ArgumentNullException>(() => ActionExtensions.Return<object, object, object, object, object, object>(null, null)).ParamName);
        }
        
        [Fact]
        public void Return5()
        {
            var cargs = (int[]) null;
            var args = new[] { 1, 2, 3, 4, 5 };
            var action = new Action<int, int, int, int, int>((a, b, c, d, e) => cargs = new[] { a, b, c, d, e });
            var result = action.Return(args.Sum())(args[0], args[1], args[2], args[3], args[4]);
            Assert.Equal(args, cargs);
            Assert.Equal(15, result);
        }

        [Fact]
        public void ReturnWithNullThis6()
        {
            Assert.Equal("action", Assert.Throws<ArgumentNullException>(() => ActionExtensions.Return<object, object, object, object, object, object, object>(null, null)).ParamName);
        }
        
        [Fact]
        public void Return6()
        {
            var cargs = (int[]) null;
            var args = new[] { 1, 2, 3, 4, 5, 6 };
            var action = new Action<int, int, int, int, int, int>((a, b, c, d, e, f) => cargs = new[] { a, b, c, d, e, f });
            var result = action.Return(args.Sum())(args[0], args[1], args[2], args[3], args[4], args[5]);
            Assert.Equal(args, cargs);
            Assert.Equal(21, result);
        }

        [Fact]
        public void ReturnWithNullThis7()
        {
            Assert.Equal("action", Assert.Throws<ArgumentNullException>(() => ActionExtensions.Return<object, object, object, object, object, object, object, object>(null, null)).ParamName);
        }
        
        [Fact]
        public void Return7()
        {
            var cargs = (int[]) null;
            var args = new[] { 1, 2, 3, 4, 5, 6, 7 };
            var action = new Action<int, int, int, int, int, int, int>((a, b, c, d, e, f, g) => cargs = new[] { a, b, c, d, e, f, g });
            var result = action.Return(args.Sum())(args[0], args[1], args[2], args[3], args[4], args[5], args[6]);
            Assert.Equal(args, cargs);
            Assert.Equal(28, result);
        }

        [Fact]
        public void ReturnWithNullThis8()
        {
            Assert.Equal("action", Assert.Throws<ArgumentNullException>(() => ActionExtensions.Return<object, object, object, object, object, object, object, object, object>(null, null)).ParamName);
        }
        
        [Fact]
        public void Return8()
        {
            var cargs = (int[]) null;
            var args = new[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            var action = new Action<int, int, int, int, int, int, int, int>((a, b, c, d, e, f, g, h) => cargs = new[] { a, b, c, d, e, f, g, h });
            var result = action.Return(args.Sum())(args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7]);
            Assert.Equal(args, cargs);
            Assert.Equal(36, result);
        }

        [Fact]
        public void ReturnWithNullThis9()
        {
            Assert.Equal("action", Assert.Throws<ArgumentNullException>(() => ActionExtensions.Return<object, object, object, object, object, object, object, object, object, object>(null, null)).ParamName);
        }
        
        [Fact]
        public void Return9()
        {
            var cargs = (int[]) null;
            var args = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var action = new Action<int, int, int, int, int, int, int, int, int>((a, b, c, d, e, f, g, h, i) => cargs = new[] { a, b, c, d, e, f, g, h, i });
            var result = action.Return(args.Sum())(args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8]);
            Assert.Equal(args, cargs);
            Assert.Equal(45, result);
        }

        [Fact]
        public void ReturnWithNullThis10()
        {
            Assert.Equal("action", Assert.Throws<ArgumentNullException>(() => ActionExtensions.Return<object, object, object, object, object, object, object, object, object, object, object>(null, null)).ParamName);
        }
        
        [Fact]
        public void Return10()
        {
            var cargs = (int[]) null;
            var args = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var action = new Action<int, int, int, int, int, int, int, int, int, int>((a, b, c, d, e, f, g, h, i, j) => cargs = new[] { a, b, c, d, e, f, g, h, i, j });
            var result = action.Return(args.Sum())(args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8], args[9]);
            Assert.Equal(args, cargs);
            Assert.Equal(55, result);
        }

        [Fact]
        public void ReturnWithNullThis11()
        {
            Assert.Equal("action", Assert.Throws<ArgumentNullException>(() => ActionExtensions.Return<object, object, object, object, object, object, object, object, object, object, object, object>(null, null)).ParamName);
        }
        
        [Fact]
        public void Return11()
        {
            var cargs = (int[]) null;
            var args = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            var action = new Action<int, int, int, int, int, int, int, int, int, int, int>((a, b, c, d, e, f, g, h, i, j, k) => cargs = new[] { a, b, c, d, e, f, g, h, i, j, k });
            var result = action.Return(args.Sum())(args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8], args[9], args[10]);
            Assert.Equal(args, cargs);
            Assert.Equal(66, result);
        }

        [Fact]
        public void ReturnWithNullThis12()
        {
            Assert.Equal("action", Assert.Throws<ArgumentNullException>(() => ActionExtensions.Return<object, object, object, object, object, object, object, object, object, object, object, object, object>(null, null)).ParamName);
        }
        
        [Fact]
        public void Return12()
        {
            var cargs = (int[]) null;
            var args = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            var action = new Action<int, int, int, int, int, int, int, int, int, int, int, int>((a, b, c, d, e, f, g, h, i, j, k, l) => cargs = new[] { a, b, c, d, e, f, g, h, i, j, k, l });
            var result = action.Return(args.Sum())(args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8], args[9], args[10], args[11]);
            Assert.Equal(args, cargs);
            Assert.Equal(78, result);
        }

        [Fact]
        public void ReturnWithNullThis13()
        {
            Assert.Equal("action", Assert.Throws<ArgumentNullException>(() => ActionExtensions.Return<object, object, object, object, object, object, object, object, object, object, object, object, object, object>(null, null)).ParamName);
        }
        
        [Fact]
        public void Return13()
        {
            var cargs = (int[]) null;
            var args = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
            var action = new Action<int, int, int, int, int, int, int, int, int, int, int, int, int>((a, b, c, d, e, f, g, h, i, j, k, l, m) => cargs = new[] { a, b, c, d, e, f, g, h, i, j, k, l, m });
            var result = action.Return(args.Sum())(args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8], args[9], args[10], args[11], args[12]);
            Assert.Equal(args, cargs);
            Assert.Equal(91, result);
        }

        [Fact]
        public void ReturnWithNullThis14()
        {
            Assert.Equal("action", Assert.Throws<ArgumentNullException>(() => ActionExtensions.Return<object, object, object, object, object, object, object, object, object, object, object, object, object, object, object>(null, null)).ParamName);
        }
        
        [Fact]
        public void Return14()
        {
            var cargs = (int[]) null;
            var args = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
            var action = new Action<int, int, int, int, int, int, int, int, int, int, int, int, int, int>((a, b, c, d, e, f, g, h, i, j, k, l, m, n) => cargs = new[] { a, b, c, d, e, f, g, h, i, j, k, l, m, n });
            var result = action.Return(args.Sum())(args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8], args[9], args[10], args[11], args[12], args[13]);
            Assert.Equal(args, cargs);
            Assert.Equal(105, result);
        }

        [Fact]
        public void ReturnWithNullThis15()
        {
            Assert.Equal("action", Assert.Throws<ArgumentNullException>(() => ActionExtensions.Return<object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object>(null, null)).ParamName);
        }
        
        [Fact]
        public void Return15()
        {
            var cargs = (int[]) null;
            var args = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            var action = new Action<int, int, int, int, int, int, int, int, int, int, int, int, int, int, int>((a, b, c, d, e, f, g, h, i, j, k, l, m, n, o) => cargs = new[] { a, b, c, d, e, f, g, h, i, j, k, l, m, n, o });
            var result = action.Return(args.Sum())(args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8], args[9], args[10], args[11], args[12], args[13], args[14]);
            Assert.Equal(args, cargs);
            Assert.Equal(120, result);
        }

        [Fact]
        public void ReturnWithNullThis16()
        {
            Assert.Equal("action", Assert.Throws<ArgumentNullException>(() => ActionExtensions.Return<object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object>(null, null)).ParamName);
        }
        
        [Fact]
        public void Return16()
        {
            var cargs = (int[]) null;
            var args = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            var action = new Action<int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int>((a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p) => cargs = new[] { a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p });
            var result = action.Return(args.Sum())(args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8], args[9], args[10], args[11], args[12], args[13], args[14], args[15]);
            Assert.Equal(args, cargs);
            Assert.Equal(136, result);
        }
    }
}