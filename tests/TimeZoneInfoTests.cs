using System;

namespace Mannex.Tests
{
    using Xunit;
    public class TimeZoneInfoTests
    {
        [Fact]
        public void GetHoursInday()
        {
            var tz = TimeZoneInfo.CreateCustomTimeZone("CustomTimeZone", new TimeSpan(0, 0, 0), "CustomTimeZone", "CustomTimeZone", "CustomTimeZoneDST", new[]
                {TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(
                    new DateTime(1900,1,1), new DateTime(2100,1,1), new TimeSpan(1, 30, 0),
                    TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1, 2, 0, 0), 2, 15),
                    TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1, 2, 0, 0), 2, 20)
                    )
                });
            Assert.Equal(24.0, tz.HoursInDay(new DateTime(2015,1,1)));
            Assert.Equal(22.5, tz.HoursInDay(new DateTime(2015,2,15)));
            Assert.Equal(24.0, tz.HoursInDay(new DateTime(2015,2,18)));
            Assert.Equal(25.5, tz.HoursInDay(new DateTime(2015,2,20)));
            Assert.Equal(24.0, tz.HoursInDay(new DateTime(2015,2,28)));
        }
        
    }
}
