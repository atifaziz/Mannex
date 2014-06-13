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
    /// Extension methods for <see cref="float"/>.
    /// </summary>

    static partial class SingleExtensions
    {
        /// <summary>
        /// Converts <see cref="float.NaN"/> value to a 
        /// <see cref="Nullable{T}"/> of <see cref="float"/> initialized to 
        /// the null state. Otherwise the <see cref="Nullable{T}"/> of 
        /// <see cref="float"/> retured holds the original input value.
        /// </summary>

        [DebuggerStepThrough]
        public static float? NullNaN(this float value)
        {
            return float.IsNaN(value) ? (float?) null : value;
        }

        /// <summary>
        /// Generates a sequence of given count of values between two values  
        /// (inclusive).
        /// </summary>
        /// <remarks>This method uses deferred semantics.</remarks>

        public static IEnumerable<float> To(this float first, float last, int count)
        {
            if (count < 0) throw new ArgumentOutOfRangeException("count", count, null);
            return ToImpl(first, (last - first) / (count - 1), count);
        }

        static IEnumerable<float> ToImpl(float n, float rate, int count)
        {
            for (var i = 0; i < count; n += rate, i++)
                yield return n;
        }
    }
}