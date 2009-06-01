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

namespace Mannex.Reflection
{
    #region Imports

    using System;
    using System.Reflection;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="ICustomAttributeProvider"/>.
    /// </summary>

    static partial class ICustomAttributeProviderExtensions
    {
        /// <summary>
        /// Indicates whether one or more instance of <typeparamref name="T"/> 
        /// is defined on this member.
        /// </summary>

        public static bool IsDefined<T>(this ICustomAttributeProvider provider, bool inherit) where T : class
        {
            if (provider == null) throw new ArgumentNullException("provider");
            return provider.IsDefined(typeof(T), inherit);
        }
    }
}