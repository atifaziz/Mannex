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
    using System.Diagnostics;
    using System.Linq;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="double"/>.
    /// </summary>

    static partial class DoubleExtensions
    {
        /// <summary>
        /// Converts <see cref="double.NaN"/> value to a 
        /// <see cref="Nullable{T}"/> of <see cref="double"/> initialized to 
        /// the null state. Otherwise the <see cref="Nullable{T}"/> of 
        /// <see cref="double"/> retured holds the original input value.
        /// </summary>

        [DebuggerStepThrough]
        public static double? NullNaN(this double value)
        {
            return double.IsNaN(value) ? (double?) null : value;
        }
 
        /// <summary>
        /// Generates a sequence of given count of values between two values 
        /// (inclusive).
        /// </summary>
        /// <remarks>
        /// This method uses deferred semantics.
        /// </remarks>

        public static IEnumerable<double> To(this double first, double last, int count)
        {
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count), count, null);
            return ToImpl(first, (last - first) / (count - 1), count);
        }

        static IEnumerable<double> ToImpl(double n, double rate, int count)
        {
            for (var i = 0; i < count; n += rate, i++)
                yield return n;
        }
    }
}