#region License, Terms and Author(s)
//
// Mannex - Extension methods for .NET
// Copyright (c) 2015 Philippe Raemy. All rights reserved.
//
//  Author(s):
//
//      Philippe Raemy
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
        [Fact]
        public void HoursInDay()
        {
            var tz = TimeZoneInfo.CreateCustomTimeZone("CustomTimeZone", TimeSpan.Zero, "CustomTimeZone", "CustomTimeZone", "CustomTimeZoneDST", new[]
                {
                    TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(
                        new DateTime(1900, 1, 1), new DateTime(2100, 1, 1), new TimeSpan(1, 30, 0),
                        TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1, 2, 0, 0), 2, 15),
                        TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1, 2, 0, 0), 2, 20)
                                                                                                                                                             )
                });
            Assert.Equal(24.0, tz.HoursInDay(new DateTime(2015, 1, 1)));
            Assert.Equal(22.5, tz.HoursInDay(new DateTime(2015, 2, 15)));
            Assert.Equal(24.0, tz.HoursInDay(new DateTime(2015, 2, 18)));
            Assert.Equal(25.5, tz.HoursInDay(new DateTime(2015, 2, 20)));
            Assert.Equal(24.0, tz.HoursInDay(new DateTime(2015, 2, 28)));
        }
    }
}
