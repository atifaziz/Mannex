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
    using Xunit;

    public class TimeZoneInfoTests
    {
        [Theory]
        [InlineData(24.0, 2015, 1,  1, 00, 00)]
        [InlineData(24.0, 2015, 1,  1, 00, 00)]
        [InlineData(22.5, 2015, 2, 15, 00, 00)]
        [InlineData(22.5, 2015, 2, 15, 12, 34)]
        [InlineData(24.0, 2015, 2, 18, 00, 00)]
        [InlineData(25.5, 2015, 2, 20, 00, 00)]
        [InlineData(25.5, 2015, 2, 20, 12, 34)]
        [InlineData(24.0, 2015, 2, 28, 00, 00)]
        public void HoursInDay(double hours, int year, int month, int day, int hour, int minute)
        {
            const string id = "CustomTimeZone";
            var tz = TimeZoneInfo.CreateCustomTimeZone(id, TimeSpan.Zero, id, id, id + "DST", new[]
            {
                TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(
                    new DateTime(1900, 1, 1), new DateTime(2100, 1, 1), new TimeSpan(1, 30, 0),
                    TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1, 2, 0, 0), 2, 15),
                    TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1, 2, 0, 0), 2, 20)
                )
            });
            Assert.Equal(hours, tz.HoursInDay(new DateTime(year, month, day, hour, minute, 0)));
        }
    }
}
