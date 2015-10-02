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
    using System;
    using System.Globalization;
    using Xunit;

    public class TimeSpanTests
    {
        [Theory]
        [InlineData("-01:00", -1, 0, @"\+hh\:mm", @"\-hh\:mm")]
        [InlineData("-02:00", -2, 0, @"\+hh\:mm", @"\-hh\:mm")]
        [InlineData("+00:00", +0, 0, @"\+hh\:mm", @"\-hh\:mm")]
        [InlineData("+01:00", +1, 0, @"\+hh\:mm", @"\-hh\:mm")]
        [InlineData("+02:00", +2, 0, @"\+hh\:mm", @"\-hh\:mm")]
        public void ToStringTwoFormats(string expected, int hours, int minutes, string format, string negativeFormat)
        {
            var ts = new TimeSpan(0, hours, minutes, 0);
            Assert.Equal(expected, ts.ToString(format, negativeFormat, CultureInfo.InvariantCulture));
        }

        [Theory]
        [InlineData("-01:00", -1, 0, @"\+hh\:mm", @"\-hh\:mm", @"\ \0\0\:\0\0")]
        [InlineData("-02:00", -2, 0, @"\+hh\:mm", @"\-hh\:mm", @"\ \0\0\:\0\0")]
        [InlineData(" 00:00", +0, 0, @"\+hh\:mm", @"\-hh\:mm", @"\ \0\0\:\0\0")]
        [InlineData("+01:00", +1, 0, @"\+hh\:mm", @"\-hh\:mm", @"\ \0\0\:\0\0")]
        [InlineData("+02:00", +2, 0, @"\+hh\:mm", @"\-hh\:mm", @"\ \0\0\:\0\0")]
        public void ToStringThreeFormats(string expected, int hours, int minutes, string positiveFormat, string negativeFormat, string zeroFormat)
        {
            var ts = new TimeSpan(0, hours, minutes, 0);
            Assert.Equal(expected, ts.ToString(positiveFormat, negativeFormat, zeroFormat, CultureInfo.InvariantCulture));
        }
    }
}