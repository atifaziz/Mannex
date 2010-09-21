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

namespace Mannex
{
    #region Imports

    using System;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="DateTime"/>.
    /// </summary>

    static partial class DateTimeExtensions
    {
        private static readonly DateTime _epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Returns number of milliseconds (including fractions) in UTC between the 
        /// specified date and midnight January 1, 1970.
        /// </summary>

        public static double ToUnixTime(this DateTime localTime)
        {
            return localTime.ToUniversalTime().Subtract(_epoch).TotalMilliseconds;
        }

        /// <summary>
        /// Trims millisecond component so that the precision of the 
        /// resulting time is to the second.
        /// </summary>

        public static DateTime TrimToSecond(this DateTime time)
        {
            return new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, time.Second);
        }

        /// <summary>
        /// Trims the second and millisecond components so that the 
        /// precision of the resulting time is to the minute.
        /// </summary>

        public static DateTime TrimToMinute(this DateTime time)
        {
            return new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, 0);
        }

        /// <summary>
        /// Trims the minute, second and millisecond components so that the 
        /// precision of the resulting time is to the hour.
        /// </summary>

        public static DateTime TrimToHour(this DateTime time)
        {
            return new DateTime(time.Year, time.Month, time.Day, time.Hour, 0, 0);
        }

        /// <summary>
        /// Trims the time components so that the precision of the resulting 
        /// time is to the day.
        /// </summary>

        public static DateTime TrimToDay(this DateTime time)
        {
            return time.Date;
        }

        /// <summary>
        /// Determines if the time component is midnight exactly.
        /// </summary>

        public static bool IsMidnight(this DateTime time)
        {
            return time.TrimToDay() == time;
        }
    }
}
