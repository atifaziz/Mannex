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

namespace Mannex.Globalization
{
    using System.Globalization;

    /// <summary>
    /// Extension methods for <see cref="NumberStyles"/>.
    /// </summary>

    static partial class NumberStylesExtensions
    {
        /// <summary>
        /// Additionally sets styles to allow leading and trailing white space.
        /// </summary>

        public static NumberStyles AllowWhiteSpaces(this NumberStyles styles)
        {
            return styles | NumberStyles.AllowLeadingWhite
                          | NumberStyles.AllowTrailingWhite;
        }
    }
}