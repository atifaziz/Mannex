﻿#region License, Terms and Author(s)
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

    using System.Diagnostics;

    #endregion

    static partial class BooleanExtensions
    {
        /// <summary>
        /// Converts a <see cref="bool"/> to <see cref="string"/> with a given
        /// value for true and false states.
        /// </summary>

        [DebuggerStepThrough]
        public static string ToString(this bool value, string trueString, string falseString)
        {
            return value ? trueString : falseString;
        }

        /// <summary>
        /// Converts a <see cref="bool"/> to <see cref="int"/> with a given
        /// value for true and false states.
        /// </summary>

        [DebuggerStepThrough]
        public static int ToInt32(this bool value, int trueValue, int falseValue)
        {
            return value ? trueValue : falseValue;
        }
    }
}