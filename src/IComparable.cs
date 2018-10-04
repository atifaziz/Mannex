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

    /// <summary>
    /// Extension methods for <see cref="IComparable{T}"/> objects.
    /// </summary>

    static partial class IComparableExtensions
    {
        /// <summary>
        /// Constrains the value to a given minimum and maximum if it
        /// exceeds either bound otherwise returns the value unmodified.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is a null reference.</exception>

        public static T MinMax<T>(this T value, T min, T max)
            where T : IComparable<T>
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            return value.CompareTo(min) < 0 ? min : value.CompareTo(max) > 0 ? max : value;
        }

        /// <summary>
        /// Constrains the value to a given minimum and maximum if it
        /// exceeds either bound otherwise returns the value unmodified.
        /// If the value is null then the result is also null.
        /// </summary>

        public static T? MinMax<T>(this T? value, T min, T max)
            where T : struct, IComparable<T>
        {
            return value is T x ? x.MinMax(min, max) : (T?) null;
        }

        /// <summary>
        /// Determines if the value lies within a range, given the lower
        /// (inclusive) and upper (inclusive) bounds.
        /// </summary>

        public static bool IsBetween<T>(this T value, T lower, T upper)
            where T : IComparable<T>
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            return value.CompareTo(lower) >= 0 && value.CompareTo(upper) <= 0;
        }

        /// <summary>
        /// Determines if the value lies within a range, given the lower
        /// (inclusive) and upper (inclusive) bounds. If the value is <c>null</c>
        /// then so is the result.
        /// </summary>

        public static bool? IsBetween<T>(this T? value, T lower, T upper)
            where T : struct, IComparable<T>
        {
            return value is T x ? x.IsBetween(lower, upper) : (bool?) null;
        }

        /// <summary>
        /// Determines if the value lies within a range, given the lower
        /// (exclusive) and upper (exclusive) bounds.
        /// </summary>

        public static bool IsInBetween<T>(this T value, T lower, T upper)
            where T : IComparable<T>
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            return value.CompareTo(lower) > 0 && value.CompareTo(upper) < 0;
        }

        /// <summary>
        /// Determines if the value lies within a range, given the lower
        /// (exclusive) and upper (exclusive) bounds. If the value is <c>null</c>
        /// then so is the result.
        /// </summary>

        public static bool? IsInBetween<T>(this T? value, T lower, T upper)
            where T : struct, IComparable<T>
        {
            return value is T x ? x.IsInBetween(lower, upper) : (bool?)null;
        }
    }
}
