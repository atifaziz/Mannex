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
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.Net;
    using System.Text;
    using System.Web;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="TimeSpan"/>.
    /// </summary>

    static partial class TimeSpanExtensions
    {
        /// <summary>
        /// Converts the object to a string representation defined by
        /// separate formats, one for zero and positive values and another
        /// for negative values.
        /// </summary>

        [DebuggerStepThrough]
        public static string ToString(this TimeSpan span,
            string format,
            string negativeFormat)
        {
            return span.ToString(format, negativeFormat, (IFormatProvider) null);
        }

        /// <summary>
        /// Converts the object to a string representation defined by
        /// separate formats, one for zero and positive values and another
        /// for negative values, and culture-specific formatting information.
        /// </summary>

        [DebuggerStepThrough]
        public static string ToString(this TimeSpan span,
            string format,
            string negativeFormat,
            IFormatProvider formatProvider)
        {
            return span.ToString(format, negativeFormat, format, formatProvider);
        }

        /// <summary>
        /// Converts the object to a string representation defined by
        /// separate formats for positive and negative values and zero.
        /// </summary>

        [DebuggerStepThrough]
        public static string ToString(this TimeSpan span,
            string positiveFormat,
            string negativeFormat,
            string zeroFormat)
        {
            return ToString(span, positiveFormat, negativeFormat, zeroFormat, null);
        }

        /// <summary>
        /// Converts the object to a string representation defined by
        /// separate formats for positive and negative values and zero. An
        /// additional parameter supplies culture-specific formatting
        /// information.
        /// </summary>

        [DebuggerStepThrough]
        public static string ToString(this TimeSpan span,
            string positiveFormat,
            string negativeFormat,
            string zeroFormat,
            IFormatProvider formatProvider)
        {
            return span.ToString(  span < TimeSpan.Zero ? negativeFormat
                                 : span > TimeSpan.Zero ? positiveFormat
                                 : zeroFormat,
                                 formatProvider);
        }
    }
}
