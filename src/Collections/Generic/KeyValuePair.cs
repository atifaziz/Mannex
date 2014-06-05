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

namespace Mannex.Collections.Generic
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Extension methods for <see cref="KeyValuePair{TKey,TValue}"/>.
    /// </summary>

    static partial class KeyValuePairExtensions
    {
        /// <summary>
        /// Returns a projection of the key and its value.
        /// </summary>

        public static T Select<TKey, TValue, T>(this KeyValuePair<TKey, TValue> pair, Func<TKey, TValue, T> selector)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            return selector(pair.Key, pair.Value);
        }
    }
}
