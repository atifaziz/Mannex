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
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Extension methods for <see cref="Enum"/>.
    /// </summary>
    static partial class EnumExtensions
    {
        /// <summary>
        /// Determines whether any of the given bit fields are set in the
        /// current instance.
        /// </summary>

        public static bool HasEitherFlag<T>(this T value, T flag1, T flag2)
            where T : Enum
        {
            return value.HasFlag(flag1) || value.HasFlag(flag2);
        }

        /// <summary>
        /// Determines whether any of the given bit fields are set in the
        /// current instance.
        /// </summary>

        public static bool HasEitherFlag<T>(this T value, T flag1, T flag2, T flag3)
            where T : Enum
        {
            return value.HasEitherFlag(flag1, flag2) || value.HasFlag(flag3);
        }

        /// <summary>
        /// Determines whether any of the given bit fields are set in the
        /// current instance.
        /// </summary>

        public static bool HasEitherFlag<T>(this T value, T flag1, T flag2, T flag3, T flag4)
            where T : Enum
        {
            return value.HasEitherFlag(flag1, flag2, flag3) || value.HasFlag(flag4);
        }

        /// <summary>
        /// Determines whether any of the given bit fields are set in the
        /// current instance.
        /// </summary>

        public static bool HasEitherFlag<T>(this T value, T flag1, T flag2, T flag3, T flag4, T flag5, params T[] others)
            where T : Enum
        {
            return value.HasEitherFlag(flag1, flag2, flag3, flag4)
                || value.HasFlag(flag5)
                || others.Any(f => value.HasFlag(f));
        }

        /// <summary>
        /// Returns a sequence of the enumeration members that are flagged
        /// in the given enumeration value.
        /// </summary>
        /// <exception cref="NotSupportedException">
        /// <typeparam name="T"/> is not an enumeration.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The <paramref name="value"/> argument is not an instance of
        /// the enumeration <typeparamref name="T"/>.
        /// </exception>

        public static IEnumerable<T> GetFlags<T>(this T value) where T : Enum
        {
            return typeof(T).IsDefined(typeof(FlagsAttribute), false)
                 ? from T flag in Enum.GetValues(typeof(T))
                   where value.HasFlag(flag)
                   select flag
                 : Enum.IsDefined(typeof(T), value)
                 ? Enumerable.Repeat(value, 1)
                 : Enumerable.Empty<T>();
        }
    }
}
