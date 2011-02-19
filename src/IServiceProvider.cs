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
    /// Extension methods for <see cref="IServiceProvider"/>.
    /// </summary>

    static partial class IServiceProviderExtensions
    {
        /// <summary>
        /// Gets the service object of the specified type.
        /// </summary>

        public static T GetService<T>(this IServiceProvider sp) where T : class
        {
            if (sp == null) throw new ArgumentNullException("sp");
            return (T) sp.GetService(typeof(T));
        }

        /// <summary>
        /// Gets the service object of the specified type.
        /// </summary>

        public static T GetRequiredService<T>(this IServiceProvider sp) where T : class
        {
            var service = sp.GetService<T>();
            if (service == null)
                throw new Exception(string.Format("Service of type {0} is unavailable.", typeof(T)));
            return service;
        }
    }
}