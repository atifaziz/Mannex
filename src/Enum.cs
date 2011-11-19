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
        #if NET4

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

        public static IEnumerable<T> GetFlags<T>(this Enum value)
        {
            var type = value.GetType();

            if (!typeof(T).IsEnum)
            {
                throw new NotSupportedException(string.Format(
                    @"{0} is not an enumeration and therefore an invalid type argument for {1}.",
                    typeof(T), MethodBase.GetCurrentMethod()));
            }

            if (!typeof(T).IsEquivalentTo(type))
                throw new ArgumentException(null, "value");

            return type.IsDefined(typeof(FlagsAttribute), false)
                 ? from Enum flag in Enum.GetValues(type)
                   where value.HasFlag(flag)
                   select (T) (object) flag
                 : Enum.IsDefined(type, value) 
                 ? Enumerable.Repeat((T)(object) value, 1) 
                 : Enumerable.Empty<T>();
        }

        #endif
    }
}
