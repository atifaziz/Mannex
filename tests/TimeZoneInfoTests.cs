#region License, Terms and Author(s)
//
// Mannex - Extension methods for .NET
// Copyright (c) 2009 Atif Aziz. All rights reserved.
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
    using System.Collections.Generic;
    using Xunit;

    public class TimeZoneInfoTests
    {
        const string CustomTimeZoneId = "CustomTimeZone";

        static readonly TimeZoneInfo CustomTimeZone = TimeZoneInfo.CreateCustomTimeZone(CustomTimeZoneId, TimeSpan.Zero, CustomTimeZoneId, CustomTimeZoneId, CustomTimeZoneId + "DST", new[]
        {
            TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(
                new DateTime(1900, 1, 1), new DateTime(2100, 1, 1), new TimeSpan(1, 30, 0),
                TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1, 2, 0, 0), 2, 15),
                TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1, 2, 0, 0), 2, 20)
            )
        });

        [Fact]
        public void HoursInDayWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() =>
                        TimeZoneInfoExtensions.HoursInDay(null, DateTime.MinValue));
            Assert.Equal("tz", e.ParamName);
        }

        [Theory]
        [MemberData("GetDayLengthTestData")]
        public void HoursInDay(double hours, int year, int month, int day, int hour, int minute)
        {
            Assert.Equal(hours, CustomTimeZone.HoursInDay(new DateTime(year, month, day, hour, minute, 0)));
        }

        [Fact]
        public void GetDayLengthWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() =>
                        TimeZoneInfoExtensions.GetDayLength(null, DateTime.MinValue));
            Assert.Equal("tz", e.ParamName);
        }

        [Theory]
        [MemberData("GetDayLengthTestData")]
        public void GetDayLength(double hours, int year, int month, int day, int hour, int minute)
        {
            Assert.Equal(TimeSpan.FromHours(hours), CustomTimeZone.GetDayLength(new DateTime(year, month, day, hour, minute, 0)));
        }

        public static IEnumerable<object[]> GetDayLengthTestData = new[]
        {
            new object[] { 24.0, 2015, 1,  1, 00, 00 },
            new object[] { 24.0, 2015, 1,  1, 00, 00 },
            new object[] { 22.5, 2015, 2, 15, 00, 00 },
            new object[] { 22.5, 2015, 2, 15, 12, 34 },
            new object[] { 24.0, 2015, 2, 18, 00, 00 },
            new object[] { 25.5, 2015, 2, 20, 00, 00 },
            new object[] { 25.5, 2015, 2, 20, 12, 34 },
            new object[] { 24.0, 2015, 2, 28, 00, 00 },
        };
    }
}
