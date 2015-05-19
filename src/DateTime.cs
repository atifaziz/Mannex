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
        static readonly DateTime _epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

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
            return new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, time.Second, time.Kind);
        }

        /// <summary>
        /// Trims the second and millisecond components so that the 
        /// precision of the resulting time is to the minute.
        /// </summary>

        public static DateTime TrimToMinute(this DateTime time)
        {
            return new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, 0, time.Kind);
        }

        /// <summary>
        /// Trims the minute, second and millisecond components so that the 
        /// precision of the resulting time is to the hour.
        /// </summary>

        public static DateTime TrimToHour(this DateTime time)
        {
            return new DateTime(time.Year, time.Month, time.Day, time.Hour, 0, 0, time.Kind);
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
        /// Determines the quarter (from 1 to 4) to which the date belongs.
        /// </summary>

        public static int GetQuarter(this DateTime date)
        {
            return ((date.Month - 1) / 3) + 1;
        }

        /// <summary>
        /// Gets a <see cref="DateTime"/> that represents the first day of 
        /// the month of this <see cref="DateTime"/>.
        /// </summary>

        public static DateTime FirstDayOfMonth(this DateTime time)
        {
            return new DateTime(time.Year, time.Month, 1, time.Hour, time.Minute, time.Second, time.Millisecond, time.Kind);
        }

        /// <summary>
        /// Gets a <see cref="DateTime"/> that represents the first day of 
        /// the year of this <see cref="DateTime"/>.
        /// </summary>

        public static DateTime FirstDayOfYear(this DateTime time)
        {
            return new DateTime(time.Year, 1, 1, time.Hour, time.Minute, time.Second, time.Millisecond, time.Kind);
        }

        /// <summary>
        /// Gets a <see cref="DateTime"/> that represents the first day of 
        /// the quarter of this <see cref="DateTime"/>.
        /// </summary>

        public static DateTime FirstDayOfQuarter(this DateTime time)
        {
            return new DateTime(time.Year, (time.GetQuarter() - 1) * 3 + 1, 1, time.Hour, time.Minute, time.Second, time.Millisecond, time.Kind);
        }

        /// <summary>
        /// Determines if the time component is midnight exactly.
        /// </summary>

        public static bool IsMidnight(this DateTime time)
        {
            return time.TrimToDay() == time;
        }

        /// <summary>
        /// Replaces time component with that of another
        /// <see cref="DateTime"/>.
        /// </summary>

        public static DateTime WithTimeFrom(this DateTime date, DateTime time)
        {
            if (date.Kind != time.Kind) throw new ArgumentException(string.Format("Date ({0}) and time ({1}) kinds do not match.", date.Kind, time.Kind));
            return new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second, time.Millisecond);
        }

        /// <summary>
        /// Gets the number of hours within the current day in the given time zone, taking transitions into account.
        /// </summary>
        public static double HoursInDay(this DateTime date, string timeZoneId)
		{
		    var tz = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            if (tz == null)
            {
                throw new ArgumentOutOfRangeException(timeZoneId);
            }
            return tz.HoursInDay(date);
        }
    }
}
