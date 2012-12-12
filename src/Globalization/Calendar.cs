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

namespace Mannex.Globalization
{
    #region Imports

    using System;
    using System.Globalization;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="Calendar"/>.
    /// </summary>

    static partial class CalendarExtensions
    {
        /// <summary>
        /// Calculates the first date that occurs in a calendar
        /// week given the year, week-rule and which day of the
        /// week should be used as the first of the week.
        /// </summary>
        
        public static DateTime FirstDateOfWeek(this Calendar calendar, int year, int weekOfYear, CalendarWeekRule weekRule, DayOfWeek firstDayOfWeek)
        {
            // Source & credit: 
            // http://stackoverflow.com/questions/662379/calculate-date-from-week-number/914943#914943

            if (calendar == null) throw new ArgumentNullException("calendar");
            var jan1 = new DateTime(year, 1, 1);
            var daysOffset = (int) firstDayOfWeek - (int) jan1.DayOfWeek;
            var firstMonday = jan1.AddDays(daysOffset);
            var firstWeek = calendar.GetWeekOfYear(jan1, weekRule, firstDayOfWeek);
            return firstMonday.AddDays((weekOfYear - (firstWeek <= 1 ? 1 : 0)) * 7);
        }
    }
}