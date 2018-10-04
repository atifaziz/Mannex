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
    using System;
    using System.Diagnostics;
    using System.Globalization;

    /// <summary>
    /// Extension methods for <see cref="IFormattable"/>.
    /// </summary>

    static partial class IFormattableExtensions
    {
        /// <summary>
        /// Formats the value of this object using the invariant culture.
        /// </summary>

        [DebuggerStepThrough]
        public static string ToInvariantString(this IFormattable formattable)
        {
            return formattable.ToInvariantString(null);
        }

        /// <summary>
        /// Formats the value of this object using a specific format and the
        /// invariant culture.
        /// </summary>

        [DebuggerStepThrough]
        public static string ToInvariantString(this IFormattable formattable, string format)
        {
            if (formattable == null) throw new ArgumentNullException(nameof(formattable));
            return formattable.ToString(format, CultureInfo.InvariantCulture);
        }
    }
}
