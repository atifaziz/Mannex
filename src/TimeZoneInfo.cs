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
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="TimeZoneInfo"/>.
    /// </summary>

    static partial class TimeZoneInfoExtensions
    {
        /// <summary>
        /// Gets the daylight start and end date and time for a given year.
        /// </summary>

        public static T GetDaylightTransitionsInYear<T>(this TimeZoneInfo timeZone, int year,
            Func<DateTime, DateTime, T> selector)
        {
            return GetDaylightTransitionsInYear(timeZone, year, selector, default(T));
        }

        /// <summary>
        /// Gets the daylight start and end date and time for a given year.
        /// An additional parameter specifies the value to return when there
        /// are no daylight transitions defined for this time zone in the given year.
        /// </summary>

        public static T GetDaylightTransitionsInYear<T>(this TimeZoneInfo timeZone, int year,
            Func<DateTime, DateTime, T> selector, T noneValue)
        {
            return GetDaylightTransitionsInYear(timeZone, year, null, selector, noneValue);
        }

        /// <summary>
        /// Gets the daylight start and end date and time for a given year.
        /// An additional parameter specifies the calendar to use for rules.
        /// </summary>

        public static T GetDaylightTransitionsInYear<T>(this TimeZoneInfo timeZone, int year, Calendar calendar,
            Func<DateTime, DateTime, T> selector)
        {
            return GetDaylightTransitionsInYear(timeZone, year, calendar, selector, default(T));
        }

        /// <summary>
        /// Gets the daylight start and end date and time for a given year.
        /// Additional parameters specifiy the calendar to use for rules and
        /// the value to return when there are no daylight transitions
        /// defined for this time zone in the given year.
        /// </summary>

        public static T GetDaylightTransitionsInYear<T>(this TimeZoneInfo timeZone, int year, Calendar calendar,
            Func<DateTime, DateTime, T> selector, T noneValue)
        {
            if (timeZone == null) throw new ArgumentNullException("timeZone");
            if (selector == null) throw new ArgumentNullException("selector");
            if (year < 0 || year > DateTime.MaxValue.Year) throw new ArgumentOutOfRangeException("year", year, null);

            // Derived from TimeZoneInfo.GetAdjustmentRules example found on:
            // https://msdn.microsoft.com/en-us/library/system.timezoneinfo.getadjustmentrules(v=vs.90).aspx

            calendar = calendar ?? CultureInfo.CurrentCulture.Calendar;

            var adjustments = timeZone.GetAdjustmentRules();
            var startYear = year;
            var endYear = startYear;

            if (adjustments.Length == 0)
                return noneValue;

            var adjustment = GetAdjustment(adjustments, year);
            if (adjustment == null)
                return noneValue;

            var dls = adjustment.DaylightTransitionStart;
            var start = dls.IsFixedDateRule
                       ? new DateTime(year, dls.Month, dls.Day, dls.TimeOfDay.Hour, dls.TimeOfDay.Minute, 0)
                       : GetTransitionForYear(dls, startYear, calendar);

            var dle = adjustment.DaylightTransitionEnd;

            // Does the transition back occur in an earlier month (i.e.,
            // the following year) than the transition to DST? If so, make
            // sure we have the right adjustment rule.

            if (dle.Month < dls.Month)
            {
                dle = GetAdjustment(adjustments, year + 1).DaylightTransitionEnd;
                endYear++;
            }

            var end = dle.IsFixedDateRule
                    ? new DateTime(year, dle.Month, dle.Day).WithTimeFrom(dle.TimeOfDay)
                    : GetTransitionForYear(dle, endYear, calendar);

            return selector(start, end);
        }

        static TimeZoneInfo.AdjustmentRule GetAdjustment(IEnumerable<TimeZoneInfo.AdjustmentRule> adjustments, int year)
        {
            return adjustments.FirstOrDefault(a => a.DateStart.Year <= year && a.DateEnd.Year >= year);
        }

        static DateTime GetTransitionForYear(TimeZoneInfo.TransitionTime transition, int year, Calendar calendar)
        {
            // Get first day of week for transition
            // For example, the 3rd week starts no earlier than the 15th of the month
            var startOfWeek = transition.Week * 7 - 6;

            // What day of the week does the month start on?
            var firstDayOfWeek = (int) calendar.GetDayOfWeek(new DateTime(year, transition.Month, 1));

            // Determine how much start date has to be adjusted
            var changeDayOfWeek = (int) transition.DayOfWeek;

            var transitionDay = firstDayOfWeek <= changeDayOfWeek
                               ? startOfWeek + (changeDayOfWeek - firstDayOfWeek)
                               : startOfWeek + (7 - firstDayOfWeek + changeDayOfWeek);

            // Adjust for months with no fifth week
            if (transitionDay > calendar.GetDaysInMonth(year, transition.Month))
                transitionDay -= 7;

            return new DateTime(year, transition.Month, transitionDay).WithTimeFrom(transition.TimeOfDay);
        }

        /// <summary>
        /// Gets the number of hours within a given day in the current time zone, taking transitions into account.
        /// </summary>
        public static double HoursInDay(this TimeZoneInfo tz, DateTime date)
        {
            if(!tz.SupportsDaylightSavingTime) return 24;
			var unzoned=new DateTime(date.Year, date.Month, date.Day,0,0,0,DateTimeKind.Unspecified);
			return (
				new DateTimeOffset(unzoned.AddDays(1), tz.GetUtcOffset(unzoned.AddDays(1)))
				- new DateTimeOffset(unzoned, tz.GetUtcOffset(unzoned))
				).TotalHours;
        }
    }
}