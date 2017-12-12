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
    using System.Diagnostics;
    using System.Globalization;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="int"/>.
    /// </summary>

    static partial class Int32Extensions
    {
        /// <summary>
        /// Converts <see cref="int"/> to its string representation in the
        /// invariant culture.
        /// </summary>

        [DebuggerStepThrough]
        public static string ToInvariantString(this int value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Calculates the quotient and remainder from dividing two numbers 
        /// and returns a user-defined result.
        /// </summary>

        [DebuggerStepThrough]
        public static T DivRem<T>(this int dividend, int divisor, Func<int, int, T> resultFunc)
        {
            if (resultFunc == null) throw new ArgumentNullException("resultFunc");
            var quotient = dividend / divisor;
            var remainder = dividend % divisor;
            return resultFunc(quotient, remainder);
        }
    }
}