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

namespace Mannex.Tests.Text.RegularExpressions
{
    #region Imports

    using System.Text.RegularExpressions;
    using System.Web.UI.WebControls.WebParts;
    using Xunit;
    using Mannex.Text.RegularExpressions;

    #endregion

    public class MatchTests
    {

        [Fact]
        public void MatchBinds3()
        {
            var result = Regexes.Date.Match("Dec 4, 2000").Bind((d, mo, y) => new
            {
                Day   = d.Value,
                Month = mo.Value,
                Year  = y.Value
            });
            Assert.Equal("4", result.Day);
            Assert.Equal("Dec", result.Month);
            Assert.Equal("2000", result.Year);
        }

        [Fact]
        public void MatchBindNum3()
        {
            var result = Regexes.Date.Match("Dec 4, 2000").BindNum((mo, d, y) => new
            {
                Day   = d.Value,
                Month = mo.Value,
                Year  = y.Value
            });
            Assert.Equal("4", result.Day);
            Assert.Equal("Dec", result.Month);
            Assert.Equal("2000", result.Year);
        }

        [Fact]
        public void MatchBinds4()
        {
            var result = Regexes.DateWithWeekday.Match("Mon, Dec 4, 2000").Bind((d, mo, y, wd) => new
            {
                Weekday = wd.Value,
                Day     = d.Value,
                Month   = mo.Value,
                Year    = y.Value
            });
            Assert.Equal("Mon", result.Weekday);
            Assert.Equal("4", result.Day);
            Assert.Equal("Dec", result.Month);
            Assert.Equal("2000", result.Year);
        }

        [Fact]
        public void MatchBindNum4()
        {
            var result = Regexes.DateWithWeekday.Match("Mon, Dec 4, 2000").BindNum((wd, mo, d, y) => new
            {
                Weekday = wd.Value,
                Day     = d.Value,
                Month   = mo.Value,
                Year    = y.Value
            });
            Assert.Equal("Mon", result.Weekday);
            Assert.Equal("4", result.Day);
            Assert.Equal("Dec", result.Month);
            Assert.Equal("2000", result.Year);
        }

        static class Regexes
        {
            public static readonly Regex Date = Regex(
                @"    (?<mo>\w+)
                  \s+ (?<d>[0-9]{1,2})  ,
                  \s+ (?<y>19|20[0-9]{2})");

            public static readonly Regex DateWithWeekday = Regex(
                @"    (?<wd>\w+)        ,
                  \s+ (?<mo>\w+)
                  \s+ (?<d>[0-9]{1,2})  ,
                  \s+ (?<y>19|20[0-9]{2})");

            static Regex Regex(string pattern)
            {
                return new Regex(pattern, RegexOptions.IgnorePatternWhitespace
                                        | RegexOptions.CultureInvariant);
            }
        }
    }
}