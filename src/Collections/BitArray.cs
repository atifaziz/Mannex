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

namespace Mannex.Collections
{
    #region Imports

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="BitArray"/>.
    /// </summary>

    static partial class BitArrayExtensions
    {
        /// <summary>
        /// Returns a sequence of <see cref="bool"/> values where each item 
        /// of the sequence represents the state of the corresponding bit 
        /// field of the <see cref="BitArray"/>.
        /// </summary>

        [DebuggerStepThrough]
        public static IEnumerable<bool> AsEnumerable(this BitArray array)
        {
            if (array == null) throw new ArgumentNullException("array");
            return array.Cast<bool>();
        }
    }
}